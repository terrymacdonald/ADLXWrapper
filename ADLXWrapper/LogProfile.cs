using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public sealed class LogProfile
    {
        [JsonProperty] public ADLX_LOG_DESTINATION Destination { get; set; }
        [JsonProperty] public ADLX_LOG_SEVERITY Severity { get; set; }
        [JsonProperty] public string? FilePath { get; set; }
        [JsonIgnore] public IAdlxLogSink? Sink { get; set; }
    }

    public interface IAdlxLogSink
    {
        ADLX_RESULT WriteLog(string message);
    }

    internal sealed unsafe class AdlxLogSinkHandle : SafeHandle
    {
        private static readonly ConcurrentDictionary<IntPtr, IAdlxLogSink> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, ushort*, ADLX_RESULT>)&OnWriteThunk;
        private readonly IntPtr _vtbl;

        private AdlxLogSinkHandle(IAdlxLogSink sink) : base(IntPtr.Zero, true)
        {
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;

            _map[inst] = sink;
        }

        public static AdlxLogSinkHandle Create(IAdlxLogSink sink)
        {
            if (sink == null) throw new ArgumentNullException(nameof(sink));
            return new AdlxLogSinkHandle(sink);
        }

        public IADLXLog* GetPointer() => (IADLXLog*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static ADLX_RESULT OnWriteThunk(IntPtr pThis, ushort* msg)
        {
            if (_map.TryGetValue(pThis, out var sink))
            {
                try
                {
                    var managedMsg = msg == null ? string.Empty : Marshal.PtrToStringUni((IntPtr)msg) ?? string.Empty;
                    return sink.WriteLog(managedMsg);
                }
                catch
                {
                    return ADLX_RESULT.ADLX_FAIL;
                }
            }

            return ADLX_RESULT.ADLX_FAIL;
        }
    }
}
