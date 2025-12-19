using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXPerformanceMonitoringServices providing managed/native accessors.
    /// </summary>
    public sealed unsafe class ADLXPerformanceMonitoringServicesHelper : IDisposable
    {
        private ComPtr<IADLXPerformanceMonitoringServices> _services;
        private bool _disposed;

        public ADLXPerformanceMonitoringServicesHelper(IADLXPerformanceMonitoringServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXPerformanceMonitoringServices>(services);
        }

        public IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
        {
            ThrowIfDisposed();
            return _services.Get();
        }

        public AdlxInterfaceHandle GetPerformanceMonitoringServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: true);
        }

        public IADLXGPUMetricsSupport* GetGpuMetricsSupportNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXGPUMetricsSupport* support = null;
            var result = _services.Get()->GetSupportedGPUMetrics(gpu, &support);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || support == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics support not available for this GPU");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU metrics support");

            return support; // caller wraps/disposes
        }

        public GpuMetricsSupportInfo GetGpuMetricsSupport(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var support = new ComPtr<IADLXGPUMetricsSupport>(GetGpuMetricsSupportNative(gpu));
            return new GpuMetricsSupportInfo(support.Get());
        }

        public IADLXGPUMetrics* GetCurrentGpuMetricsNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXGPUMetrics* metrics = null;
            var result = _services.Get()->GetCurrentGPUMetrics(gpu, &metrics);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics not supported for this GPU");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current GPU metrics");

            return metrics; // caller wraps/disposes
        }

        public GpuMetricsSnapshotInfo GetCurrentGpuMetrics(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var metrics = new ComPtr<IADLXGPUMetrics>(GetCurrentGpuMetricsNative(gpu));
            return new GpuMetricsSnapshotInfo(metrics.Get());
        }

        public IADLXSystemMetrics* GetCurrentSystemMetricsNative()
        {
            ThrowIfDisposed();
            IADLXSystemMetrics* metrics = null;
            var result = _services.Get()->GetCurrentSystemMetrics(&metrics);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current system metrics");

            return metrics; // caller wraps/disposes
        }

        public SystemMetricsSnapshotInfo GetCurrentSystemMetrics()
        {
            ThrowIfDisposed();
            using var metrics = new ComPtr<IADLXSystemMetrics>(GetCurrentSystemMetricsNative());
            return new SystemMetricsSnapshotInfo(metrics.Get());
        }

        public IADLXAllMetrics* GetCurrentAllMetricsNative()
        {
            ThrowIfDisposed();
            IADLXAllMetrics* metrics = null;
            var result = _services.Get()->GetCurrentAllMetrics(&metrics);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "All metrics not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current all metrics");

            return metrics; // caller wraps/disposes
        }

        public AllMetricsSnapshotInfo GetCurrentAllMetrics()
        {
            ThrowIfDisposed();
            using var metrics = new ComPtr<IADLXAllMetrics>(GetCurrentAllMetricsNative());
            return new AllMetricsSnapshotInfo(metrics.Get());
        }

        public IADLXGPUMetricsList* GetGpuMetricsHistoryNative(IADLXGPU* gpu, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXGPUMetricsList* list = null;
            var result = _services.Get()->GetGPUMetricsHistory(gpu, startMs, stopMs, &list);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU metrics history");

            return list; // caller wraps/disposes
        }

        public IEnumerable<GpuMetricsSnapshotInfo> EnumerateGpuMetricsHistory(IADLXGPU* gpu, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var list = new ComPtr<IADLXGPUMetricsList>(GetGpuMetricsHistoryNative(gpu, startMs, stopMs));
            var count = list.Get()->Size();
            var results = new List<GpuMetricsSnapshotInfo>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXGPUMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXGPUMetrics>(metrics);
                results.Add(new GpuMetricsSnapshotInfo(m.Get()));
            }

            return results;
        }

        public IADLXSystemMetricsList* GetSystemMetricsHistoryNative(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            IADLXSystemMetricsList* list = null;
            var result = _services.Get()->GetSystemMetricsHistory(startMs, stopMs, &list);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get system metrics history");

            return list; // caller wraps/disposes
        }

        public IEnumerable<SystemMetricsSnapshotInfo> EnumerateSystemMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            using var list = new ComPtr<IADLXSystemMetricsList>(GetSystemMetricsHistoryNative(startMs, stopMs));
            var count = list.Get()->Size();
            var results = new List<SystemMetricsSnapshotInfo>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXSystemMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXSystemMetrics>(metrics);
                results.Add(new SystemMetricsSnapshotInfo(m.Get()));
            }

            return results;
        }

        public IADLXAllMetricsList* GetAllMetricsHistoryNative(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            IADLXAllMetricsList* list = null;
            var result = _services.Get()->GetAllMetricsHistory(startMs, stopMs, &list);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "All metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get all metrics history");

            return list; // caller wraps/disposes
        }

        public IEnumerable<AllMetricsSnapshotInfo> EnumerateAllMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            using var list = new ComPtr<IADLXAllMetricsList>(GetAllMetricsHistoryNative(startMs, stopMs));
            var count = list.Get()->Size();
            var results = new List<AllMetricsSnapshotInfo>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXAllMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXAllMetrics>(metrics);
                results.Add(new AllMetricsSnapshotInfo(m.Get()));
            }

            return results;
        }

        public ADLX_IntRange GetSamplingIntervalRange()
        {
            ThrowIfDisposed();
            ADLX_IntRange range = default;
            var result = _services.Get()->GetSamplingIntervalRange(&range);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Sampling interval not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval range");
            return range;
        }

        public int GetSamplingInterval()
        {
            ThrowIfDisposed();
            int interval = 0;
            var result = _services.Get()->GetSamplingInterval(&interval);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Sampling interval not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval");
            return interval;
        }

        public void SetSamplingInterval(int intervalMs)
        {
            ThrowIfDisposed();
            var result = _services.Get()->SetSamplingInterval(intervalMs);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Sampling interval not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set sampling interval");
        }

        public int GetMaxPerformanceMetricsHistorySize()
        {
            ThrowIfDisposed();
            int size = 0;
            var result = _services.Get()->GetMaxPerformanceMetricsHistorySize(&size);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get max performance metrics history size");
            return size;
        }

        public int GetCurrentPerformanceMetricsHistorySize()
        {
            ThrowIfDisposed();
            int size = 0;
            var result = _services.Get()->GetCurrentPerformanceMetricsHistorySize(&size);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current performance metrics history size");
            return size;
        }

        public void SetMaxPerformanceMetricsHistorySize(int sizeSec)
        {
            ThrowIfDisposed();
            var result = _services.Get()->SetMaxPerformanceMetricsHistorySize(sizeSec);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set max performance metrics history size");
        }

        public void ClearPerformanceMetricsHistory()
        {
            ThrowIfDisposed();
            var result = _services.Get()->ClearPerformanceMetricsHistory();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics history not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to clear performance metrics history");
        }

        public void StartPerformanceMetricsTracking()
        {
            ThrowIfDisposed();
            var result = _services.Get()->StartPerformanceMetricsTracking();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics tracking not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to start performance metrics tracking");
        }

        public void StopPerformanceMetricsTracking()
        {
            ThrowIfDisposed();
            var result = _services.Get()->StopPerformanceMetricsTracking();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance metrics tracking not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to stop performance metrics tracking");
        }

        public void Dispose()
        {
            if (_disposed) return;
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXPerformanceMonitoringServicesHelper));
        }

        ~ADLXPerformanceMonitoringServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}

