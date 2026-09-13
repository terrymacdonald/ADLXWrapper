using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ADLXWrapper
{
    /// <summary>
    /// Represents a running ADLX GPU stress test and its one-shot completion callback.
    /// </summary>
    public sealed unsafe class ADLXGpuStressTest : IDisposable
    {
        private readonly ADLXInterfaceHandle _gpu3;
        private readonly GpuStressTestFinishedListenerHandle _listener;
        private readonly TaskCompletionSource<GpuStressTestResultDto> _completionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private int _completed;
        private bool _disposed;

        internal ADLXGpuStressTest(IADLXGPU3* gpu3, int gpuUniqueId, uint durationSeconds)
        {
            _gpu3 = ADLXInterfaceHandle.From(gpu3, addRef: true);
            GpuUniqueId = gpuUniqueId;
            DurationSeconds = durationSeconds;
            _listener = GpuStressTestFinishedListenerHandle.Create(Complete);
        }

        /// <summary>The GPU unique identifier supplied when the stress test started.</summary>
        public int GpuUniqueId { get; }

        /// <summary>The requested stress-test duration in seconds.</summary>
        public uint DurationSeconds { get; }

        /// <summary>Gets whether ADLX has reported stress-test completion.</summary>
        public bool IsCompleted => Volatile.Read(ref _completed) != 0;

        /// <summary>Completes when ADLX reports the final stress-test result.</summary>
        public Task<GpuStressTestResultDto> Completion => _completionSource.Task;

        internal IADLXGPUStressTestFinishedListener* GetListener() => _listener.GetListener();

        internal void ReleaseAfterStartFailure()
        {
            _listener.Dispose();
            _gpu3.Dispose();
            _disposed = true;
        }

        /// <summary>
        /// Releases operation resources after completion.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the stress test is still running.</exception>
        public void Dispose()
        {
            if (_disposed) return;
            if (!IsCompleted)
                throw new InvalidOperationException("A GPU stress test cannot be disposed before it has completed.");

            _listener.Dispose();
            _gpu3.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private bool Complete(IntPtr gpu3, bool succeeded)
        {
            if (Interlocked.Exchange(ref _completed, 1) != 0)
                return true;

            var gpuUniqueId = GpuUniqueId;
            try
            {
                if (gpu3 != IntPtr.Zero)
                {
                    var callbackGpu = (IADLXGPU3*)gpu3;
                    var result = callbackGpu->UniqueId(&gpuUniqueId);
                    if (result != ADLX_RESULT.ADLX_OK)
                        throw new ADLXException(result, "Failed to query the GPU that completed the stress test");
                }
                _completionSource.TrySetResult(new GpuStressTestResultDto(gpuUniqueId, succeeded, DurationSeconds, DateTimeOffset.UtcNow));
            }
            catch (Exception ex)
            {
                _completionSource.TrySetException(ex);
            }

            return true;
        }
    }

    /// <summary>Pointer-free result of a completed ADLX GPU stress test.</summary>
    public readonly struct GpuStressTestResultDto
    {
        public int GpuUniqueId { get; init; }
        public bool Succeeded { get; init; }
        public uint RequestedDurationSeconds { get; init; }
        public DateTimeOffset CompletedAt { get; init; }

        public GpuStressTestResultDto(int gpuUniqueId, bool succeeded, uint requestedDurationSeconds, DateTimeOffset completedAt)
        {
            GpuUniqueId = gpuUniqueId;
            Succeeded = succeeded;
            RequestedDurationSeconds = requestedDurationSeconds;
            CompletedAt = completedAt;
        }
    }

    internal sealed unsafe class GpuStressTestFinishedListenerHandle : SafeHandle
    {
        internal delegate bool GpuStressTestCompletedCallback(IntPtr gpu3, bool succeeded);

        private static readonly ConcurrentDictionary<IntPtr, GpuStressTestCompletedCallback> Callbacks = new();
        private static readonly IntPtr Thunk = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte, byte>)&OnGPUStressTestFinished;
        private readonly GCHandle _callbackHandle;
        private readonly IntPtr _vtable;

        private GpuStressTestFinishedListenerHandle(GpuStressTestCompletedCallback callback) : base(IntPtr.Zero, true)
        {
            _callbackHandle = GCHandle.Alloc(callback);
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtable, Thunk);
            handle = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(handle, _vtable);
            Callbacks[handle] = callback;
        }

        internal static GpuStressTestFinishedListenerHandle Create(GpuStressTestCompletedCallback callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            return new GpuStressTestFinishedListenerHandle(callback);
        }

        internal IADLXGPUStressTestFinishedListener* GetListener() => (IADLXGPUStressTestFinishedListener*)handle;

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            Callbacks.TryRemove(handle, out _);
            if (_callbackHandle.IsAllocated) _callbackHandle.Free();
            if (_vtable != IntPtr.Zero) Marshal.FreeHGlobal(_vtable);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnGPUStressTestFinished(IntPtr self, IntPtr gpu3, byte result)
        {
            try
            {
                return Callbacks.TryGetValue(self, out var callback) && callback(gpu3, result != 0) ? (byte)1 : (byte)0;
            }
            catch
            {
                return 0;
            }
        }
    }
}