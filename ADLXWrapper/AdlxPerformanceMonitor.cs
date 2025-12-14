using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Facade for ADLX performance monitoring services.
    /// </summary>
    public sealed unsafe class AdlxPerformanceMonitor : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXPerformanceMonitoringServices> _services;
        private bool _disposed;

        internal AdlxPerformanceMonitor(ADLXApi owner, IADLXPerformanceMonitoringServices* services)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (services == null) throw new ArgumentNullException(nameof(services));
            _services = new ComPtr<IADLXPerformanceMonitoringServices>(services);
        }

        public PerformanceMonitoringProfile GetProfile()
        {
            ThrowIfDisposed();
            return new PerformanceMonitoringProfile(_services.Get());
        }

        public void ApplyProfile(PerformanceMonitoringProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();

            var range = GetSamplingIntervalRange();
            if (profile.SamplingIntervalMs >= range.minValue && profile.SamplingIntervalMs <= range.maxValue)
            {
                var res = _services.Get()->SetSamplingInterval(profile.SamplingIntervalMs);
                if (res != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(res, "Failed to set sampling interval");
            }

            var resSize = _services.Get()->SetMaxPerformanceMetricsHistorySize(profile.MaxHistorySizeSec);
            if (resSize != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(resSize, "Failed to set max performance metrics history size");
        }

        public ADLX_IntRange GetSamplingIntervalRange()
        {
            ThrowIfDisposed();
            ADLX_IntRange range = default;
            var res = _services.Get()->GetSamplingIntervalRange(&range);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get sampling interval range");
            return range;
        }

        public GpuMetricsSupportInfo GetGpuMetricsSupport(AdlxInterfaceHandle gpu)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid)
                throw new ArgumentException("GPU handle is invalid", nameof(gpu));

            IADLXGPUMetricsSupport* pSupport = null;
            var res = _services.Get()->GetSupportedGPUMetrics(gpu.As<IADLXGPU>(), &pSupport);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get supported GPU metrics");

            using var support = new ComPtr<IADLXGPUMetricsSupport>(pSupport);
            return new GpuMetricsSupportInfo(support.Get());
        }

        public GpuMetricsSnapshotInfo GetCurrentGpuMetrics(AdlxInterfaceHandle gpu)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid)
                throw new ArgumentException("GPU handle is invalid", nameof(gpu));

            IADLXGPUMetrics* pMetrics = null;
            var res = _services.Get()->GetCurrentGPUMetrics(gpu.As<IADLXGPU>(), &pMetrics);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get current GPU metrics");

            using var metrics = new ComPtr<IADLXGPUMetrics>(pMetrics);
            return new GpuMetricsSnapshotInfo(metrics.Get());
        }

        public IReadOnlyList<GpuMetricsSnapshotInfo> EnumerateGpuMetricsHistory(AdlxInterfaceHandle gpu, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid)
                throw new ArgumentException("GPU handle is invalid", nameof(gpu));

            IADLXGPUMetricsList* pList = null;
            var res = _services.Get()->GetGPUMetricsHistory(gpu.As<IADLXGPU>(), startMs, stopMs, &pList);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get GPU metrics history");

            var snapshots = new List<GpuMetricsSnapshotInfo>();
            using var list = new ComPtr<IADLXGPUMetricsList>(pList);
            for (uint i = 0; i < list.Get()->Size(); i++)
            {
                IADLXGPUMetrics* pItem = null;
                list.Get()->At(i, &pItem);
                using var item = new ComPtr<IADLXGPUMetrics>(pItem);
                snapshots.Add(new GpuMetricsSnapshotInfo(item.Get()));
            }
            return snapshots;
        }

        public SystemMetricsSnapshotInfo GetCurrentSystemMetrics()
        {
            ThrowIfDisposed();
            IADLXSystemMetrics* pMetrics = null;
            var res = _services.Get()->GetCurrentSystemMetrics(&pMetrics);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get current system metrics");
            using var metrics = new ComPtr<IADLXSystemMetrics>(pMetrics);
            return new SystemMetricsSnapshotInfo(metrics.Get());
        }

        public AllMetricsSnapshotInfo GetCurrentAllMetrics()
        {
            ThrowIfDisposed();
            IADLXAllMetrics* pAll = null;
            var res = _services.Get()->GetCurrentAllMetrics(&pAll);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get current all metrics");
            using var all = new ComPtr<IADLXAllMetrics>(pAll);
            return new AllMetricsSnapshotInfo(all.Get());
        }

        public IReadOnlyList<SystemMetricsSnapshotInfo> EnumerateSystemMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            IADLXSystemMetricsList* pList = null;
            var res = _services.Get()->GetSystemMetricsHistory(startMs, stopMs, &pList);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get system metrics history");

            var snapshots = new List<SystemMetricsSnapshotInfo>();
            using var list = new ComPtr<IADLXSystemMetricsList>(pList);
            for (uint i = 0; i < list.Get()->Size(); i++)
            {
                IADLXSystemMetrics* pItem = null;
                list.Get()->At(i, &pItem);
                using var item = new ComPtr<IADLXSystemMetrics>(pItem);
                snapshots.Add(new SystemMetricsSnapshotInfo(item.Get()));
            }
            return snapshots;
        }

        public IReadOnlyList<AllMetricsSnapshotInfo> EnumerateAllMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            IADLXAllMetricsList* pList = null;
            var res = _services.Get()->GetAllMetricsHistory(startMs, stopMs, &pList);
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to get all metrics history");

            var snapshots = new List<AllMetricsSnapshotInfo>();
            using var list = new ComPtr<IADLXAllMetricsList>(pList);
            for (uint i = 0; i < list.Get()->Size(); i++)
            {
                IADLXAllMetrics* pItem = null;
                list.Get()->At(i, &pItem);
                using var item = new ComPtr<IADLXAllMetrics>(pItem);
                snapshots.Add(new AllMetricsSnapshotInfo(item.Get()));
            }
            return snapshots;
        }

        public void StartTracking()
        {
            ThrowIfDisposed();
            var res = _services.Get()->StartPerformanceMetricsTracking();
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to start metrics tracking");
        }

        public void StopTracking()
        {
            ThrowIfDisposed();
            var res = _services.Get()->StopPerformanceMetricsTracking();
            if (res != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(res, "Failed to stop metrics tracking");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            _services.Dispose();
            _services = default;
            _disposed = true;
        }

        ~AdlxPerformanceMonitor()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed || _owner == null)
                throw new ObjectDisposedException(nameof(AdlxPerformanceMonitor));
        }
    }

    /// <summary>
    /// Serializable profile for performance monitoring configuration.
    /// </summary>
    public sealed class PerformanceMonitoringProfile
    {
        public int SamplingIntervalMs { get; set; }
        public int MaxHistorySizeSec { get; set; }

        public PerformanceMonitoringProfile(int samplingIntervalMs, int maxHistorySizeSec)
        {
            SamplingIntervalMs = samplingIntervalMs;
            MaxHistorySizeSec = maxHistorySizeSec;
        }

        internal unsafe PerformanceMonitoringProfile(IADLXPerformanceMonitoringServices* services)
        {
            int interval = 0;
            services->GetSamplingInterval(&interval);
            SamplingIntervalMs = interval;

            int size = 0;
            services->GetCurrentPerformanceMetricsHistorySize(&size);
            MaxHistorySizeSec = size;
        }
    }
}