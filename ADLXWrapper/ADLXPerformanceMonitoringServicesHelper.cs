using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXPerformanceMonitoringServices providing managed/native accessors.
    /// </summary>
    public sealed unsafe class ADLXPerformanceMonitoringServicesHelper : IDisposable
    {
        private ComPtr<IADLXPerformanceMonitoringServices> _services;
        private readonly IADLXSystem* _system;
        private bool _disposed;

        /// <summary>
        /// Creates a performance monitoring helper from the native services interface.
        /// </summary>
        /// <param name="services">Native performance monitoring services pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this helper.</param>
        /// <param name="system">Optional native system pointer used to upgrade to extended interfaces.</param>
        public ADLXPerformanceMonitoringServicesHelper(IADLXPerformanceMonitoringServices* services, bool addRef = true, IADLXSystem* system = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            using (ADLXSync.EnterRead())
            {
                if (addRef)
                {
                    ADLXUtils.AddRefInterface((IntPtr)services);
                }
                _services = new ComPtr<IADLXPerformanceMonitoringServices>(services);
            }
            _system = system; // IADLXSystem is not ref-counted; safe to store as raw pointer
        }

        /// <summary>
        /// Returns the native performance monitoring services interface owned by this helper.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return _services.Get();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the performance monitoring services interface.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal ADLXInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return ADLXInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: true);
            }
        }

        /// <summary>
        /// Gets GPU metrics support (native pointer). Caller must dispose.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>Native GPU metrics support pointer.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXGPUMetricsSupport* GetGpuMetricsSupportNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetricsSupport* support = null;
                var result = _services.Get()->GetSupportedGPUMetrics(gpu, &support);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || support == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get GPU metrics support");

                return support; // caller wraps/disposes
            }
        }

        /// <summary>
        /// Gets GPU metrics support DTO for a GPU.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>GPU metrics support info.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal GpuMetricsSupportDto GetGpuMetricsSupport(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            var native = GetGpuMetricsSupportNative(gpu);
            if (native == null) return default;
            using var support = new ComPtr<IADLXGPUMetricsSupport>(native);
            return new GpuMetricsSupportDto(support.Get());
        }

        /// <summary>
        /// Tries to get GPU metrics support info. Returns false if not supported for this GPU/system.
        /// </summary>
        internal bool TryGetGpuMetricsSupport(IADLXGPU* gpu, out GpuMetricsSupportDto info)
        {
            info = GetGpuMetricsSupport(gpu);
            return true;
        }

        /// <summary>
        /// Gets current GPU metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>Native GPU metrics pointer.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXGPUMetrics* GetCurrentGpuMetricsNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetrics* metrics = null;
                var result = _services.Get()->GetCurrentGPUMetrics(gpu, &metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get current GPU metrics");

                return metrics; // caller wraps/disposes
            }
        }

        /// <summary>
        /// Gets current GPU metrics as a managed snapshot DTO.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>GPU metrics snapshot.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal GpuMetricsSnapshotDto GetCurrentGpuMetrics(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            var native = GetCurrentGpuMetricsNative(gpu);
            if (native == null) return default;
            using var metrics = new ComPtr<IADLXGPUMetrics>(native);
            return new GpuMetricsSnapshotDto(metrics.Get());
        }

        internal bool TryGetCurrentGpuMetrics(IADLXGPU* gpu, out GpuMetricsSnapshotDto metrics)
        {
            metrics = GetCurrentGpuMetrics(gpu);
            return true;
        }

        /// <summary>
        /// Gets current system metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <returns>Native system metrics pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXSystemMetrics* GetCurrentSystemMetricsNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                IADLXSystemMetrics* metrics = null;
                var result = _services.Get()->GetCurrentSystemMetrics(&metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get current system metrics");

                return metrics; // caller wraps/disposes
            }
        }

        /// <summary>
        /// Gets current system metrics as a managed snapshot DTO.
        /// </summary>
        /// <returns>System metrics snapshot.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public SystemMetricsSnapshotDto GetCurrentSystemMetrics()
        {
            ThrowIfDisposed();
            var native = GetCurrentSystemMetricsNative();
            if (native == null) return default;
            using var metrics = new ComPtr<IADLXSystemMetrics>(native);
            return new SystemMetricsSnapshotDto(metrics.Get());
        }

        /// <summary>
        /// Gets current system+GPU metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <returns>Native all-metrics pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXAllMetrics* GetCurrentAllMetricsNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                IADLXAllMetrics* metrics = null;
                var result = _services.Get()->GetCurrentAllMetrics(&metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get current all metrics");

                return metrics; // caller wraps/disposes
            }
        }

        /// <summary>
        /// Gets current system+GPU metrics as a managed snapshot DTO.
        /// </summary>
        /// <returns>All-metrics snapshot.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public AllMetricsSnapshotDto GetCurrentAllMetrics()
        {
            ThrowIfDisposed();
            var native = GetCurrentAllMetricsNative();
            if (native == null) return default;
            using var metrics = new ComPtr<IADLXAllMetrics>(native);
            return new AllMetricsSnapshotDto(metrics.Get());
        }

        internal IADLXGPUMetricsList* GetGpuMetricsHistoryNative(IADLXGPU* gpu, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetricsList* list = null;
                var result = _services.Get()->GetGPUMetricsHistory(gpu, startMs, stopMs, &list);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get GPU metrics history");

                return list; // caller wraps/disposes
            }
        }

        internal IEnumerable<GpuMetricsSnapshotDto> EnumerateGpuMetricsHistory(IADLXGPU* gpu, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            var nativeList = GetGpuMetricsHistoryNative(gpu, startMs, stopMs);
            if (nativeList == null) return Array.Empty<GpuMetricsSnapshotDto>();
            using var list = new ComPtr<IADLXGPUMetricsList>(nativeList);
            var count = list.Get()->Size();
            var results = new List<GpuMetricsSnapshotDto>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXGPUMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXGPUMetrics>(metrics);
                results.Add(new GpuMetricsSnapshotDto(m.Get()));
            }

            return results;
        }

        internal bool TryEnumerateGpuMetricsHistory(IADLXGPU* gpu, int startMs, int stopMs, out IEnumerable<GpuMetricsSnapshotDto> history)
        {
            history = EnumerateGpuMetricsHistory(gpu, startMs, stopMs);
            return true;
        }

        internal IADLXSystemMetricsList* GetSystemMetricsHistoryNative(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                IADLXSystemMetricsList* list = null;
                var result = _services.Get()->GetSystemMetricsHistory(startMs, stopMs, &list);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                    return null;
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get system metrics history");

                return list; // caller wraps/disposes
            }
        }

        public IEnumerable<SystemMetricsSnapshotDto> EnumerateSystemMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            var nativeList = GetSystemMetricsHistoryNative(startMs, stopMs);
            if (nativeList == null) return Array.Empty<SystemMetricsSnapshotDto>();
            using var list = new ComPtr<IADLXSystemMetricsList>(nativeList);
            var count = list.Get()->Size();
            var results = new List<SystemMetricsSnapshotDto>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXSystemMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXSystemMetrics>(metrics);
                results.Add(new SystemMetricsSnapshotDto(m.Get()));
            }

            return results;
        }

        internal IADLXAllMetricsList* GetAllMetricsHistoryNative(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            IADLXAllMetricsList* list = null;
            var result = _services.Get()->GetAllMetricsHistory(startMs, stopMs, &list);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                return null;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get all metrics history");

            return list; // caller wraps/disposes
        }

        public IEnumerable<AllMetricsSnapshotDto> EnumerateAllMetricsHistory(int startMs, int stopMs)
        {
            ThrowIfDisposed();
            var nativeList = GetAllMetricsHistoryNative(startMs, stopMs);
            if (nativeList == null) return Array.Empty<AllMetricsSnapshotDto>();
            using var list = new ComPtr<IADLXAllMetricsList>(nativeList);
            var count = list.Get()->Size();
            var results = new List<AllMetricsSnapshotDto>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXAllMetrics* metrics = null;
                list.Get()->At(i, &metrics);
                using var m = new ComPtr<IADLXAllMetrics>(metrics);
                results.Add(new AllMetricsSnapshotDto(m.Get()));
            }

            return results;
        }

        public bool TryEnumerateAllMetricsHistory(int startMs, int stopMs, out IEnumerable<AllMetricsSnapshotDto> history)
        {
            history = EnumerateAllMetricsHistory(startMs, stopMs);
            return true;
        }

        public IntRangeDto GetSamplingIntervalRange()
        {
            ThrowIfDisposed();
            ADLX_IntRange range = default;
            var result = _services.Get()->GetSamplingIntervalRange(&range);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return default;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval range");
            return IntRangeDto.FromNative(range);
        }

        public int GetSamplingInterval()
        {
            ThrowIfDisposed();
            int interval = 0;
            var result = _services.Get()->GetSamplingInterval(&interval);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return 0;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get sampling interval");
            return interval;
        }

        public void SetSamplingInterval(int intervalMs)
        {
            ThrowIfDisposed();
            var result = _services.Get()->SetSamplingInterval(intervalMs);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set sampling interval");
        }

        public bool TrySetSamplingInterval(int intervalMs)
        {
            SetSamplingInterval(intervalMs);
            return true;
        }

        public int GetMaxPerformanceMetricsHistorySize()
        {
            ThrowIfDisposed();
            int size = 0;
            var result = _services.Get()->GetMaxPerformanceMetricsHistorySize(&size);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return 0;
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
                return 0;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get current performance metrics history size");
            return size;
        }

        public void SetMaxPerformanceMetricsHistorySize(int sizeSec)
        {
            ThrowIfDisposed();
            var result = _services.Get()->SetMaxPerformanceMetricsHistorySize(sizeSec);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set max performance metrics history size");
        }

        public bool TrySetMaxPerformanceMetricsHistorySize(int sizeSec)
        {
            SetMaxPerformanceMetricsHistorySize(sizeSec);
            return true;
        }

        public void ClearPerformanceMetricsHistory()
        {
            ThrowIfDisposed();
            var result = _services.Get()->ClearPerformanceMetricsHistory();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to clear performance metrics history");
        }

        public bool TryClearPerformanceMetricsHistory()
        {
            ClearPerformanceMetricsHistory();
            return true;
        }

        public void StartPerformanceMetricsTracking()
        {
            ThrowIfDisposed();
            var result = _services.Get()->StartPerformanceMetricsTracking();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to start performance metrics tracking");
        }

        public bool TryStartPerformanceMetricsTracking()
        {
            StartPerformanceMetricsTracking();
            return true;
        }

        public void StopPerformanceMetricsTracking()
        {
            ThrowIfDisposed();
            var result = _services.Get()->StopPerformanceMetricsTracking();
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to stop performance metrics tracking");
        }

        public bool TryStopPerformanceMetricsTracking()
        {
            StopPerformanceMetricsTracking();
            return true;
        }

        public PerformanceMonitoringSettingsDto GetPerformanceMonitoringSettings()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return new PerformanceMonitoringSettingsDto(_services.Get());
            }
        }

        public void ApplyPerformanceMonitoringSettings(PerformanceMonitoringSettingsDto info)
        {
            ThrowIfDisposed();
            var intervalRange = GetSamplingIntervalRange();
            if (intervalRange.MaxValue > 0 && info.SamplingIntervalMs >= intervalRange.MinValue && info.SamplingIntervalMs <= intervalRange.MaxValue)
            {
                SetSamplingInterval(info.SamplingIntervalMs);
            }

            var maxHistory = GetMaxPerformanceMetricsHistorySize();
            if (maxHistory > 0)
            {
                var clampedHistory = Math.Min(info.MaxHistorySizeSec, maxHistory);
                SetMaxPerformanceMetricsHistorySize(clampedHistory);
            }
        }

        // =====================================================================
        // Public per-GPU overloads (by unique id)
        // =====================================================================

        /// <summary>Gets GPU metrics support info for the GPU with the specified unique id.</summary>
        public GpuMetricsSupportDto GetGpuMetricsSupport(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetGpuMetricsSupport((IADLXGPU*)ptrGpu));
            }
        }

        /// <summary>Tries to get GPU metrics support info for the GPU with the specified unique id.</summary>
        public bool TryGetGpuMetricsSupport(int gpuUniqueId, out GpuMetricsSupportDto info)
        {
            info = GetGpuMetricsSupport(gpuUniqueId);
            return true;
        }

        /// <summary>Gets current GPU metrics snapshot for the GPU with the specified unique id.</summary>
        public GpuMetricsSnapshotDto GetCurrentGpuMetrics(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetCurrentGpuMetrics((IADLXGPU*)ptrGpu));
            }
        }

        /// <summary>Tries to get current GPU metrics snapshot for the GPU with the specified unique id.</summary>
        public bool TryGetCurrentGpuMetrics(int gpuUniqueId, out GpuMetricsSnapshotDto metrics)
        {
            metrics = GetCurrentGpuMetrics(gpuUniqueId);
            return true;
        }

        /// <summary>Enumerates GPU metrics history for the GPU with the specified unique id.</summary>
        public IEnumerable<GpuMetricsSnapshotDto> EnumerateGpuMetricsHistory(int gpuUniqueId, int startMs, int stopMs)
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return WithGpuByUniqueId(gpuUniqueId, ptrGpu => EnumerateGpuMetricsHistory((IADLXGPU*)ptrGpu, startMs, stopMs));
            }
        }

        /// <summary>Tries to enumerate GPU metrics history for the GPU with the specified unique id.</summary>
        public bool TryEnumerateGpuMetricsHistory(int gpuUniqueId, int startMs, int stopMs, out IEnumerable<GpuMetricsSnapshotDto> history)
        {
            history = EnumerateGpuMetricsHistory(gpuUniqueId, startMs, stopMs);
            return true;
        }

        private T WithGpuByUniqueId<T>(int gpuUniqueId, Func<IntPtr, T> action)
        {
            if (_system == null) throw new InvalidOperationException("System not available for GPU lookup by unique id. Ensure this helper was obtained via ADLXSystemServicesHelper.");
            IADLXGPUList* pList = null;
            var result = _system->GetGPUs(&pList);
            if (result != ADLX_RESULT.ADLX_OK || pList == null)
                throw new ADLXException(result != ADLX_RESULT.ADLX_OK ? result : ADLX_RESULT.ADLX_FAIL, "Failed to enumerate GPUs for unique id lookup");
            using var list = new ComPtr<IADLXGPUList>(pList);
            uint size = list.Get()->Size();
            for (uint i = 0; i < size; i++)
            {
                IADLXGPU* pGpu = null;
                if (list.Get()->At(i, &pGpu) != ADLX_RESULT.ADLX_OK || pGpu == null) continue;
                int uid = 0;
                pGpu->UniqueId(&uid);
                if (uid == gpuUniqueId)
                {
                    using var gpuOwner = new ComPtr<IADLXGPU>(pGpu);
                    return action((IntPtr)gpuOwner.Get());
                }
                ADLXUtils.ReleaseInterface((IntPtr)pGpu);
            }
            throw new ADLXException(ADLX_RESULT.ADLX_NOT_FOUND, $"GPU with unique id {gpuUniqueId} not found");
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

    #region Performance monitoring DTOs
    public readonly struct GpuMetricsSupportDto
    {
        public bool UsageSupported { get; init; }
        public bool ClockSpeedSupported { get; init; }
        public bool TemperatureSupported { get; init; }
        public bool HotspotTemperatureSupported { get; init; }
        public bool PowerSupported { get; init; }
        public bool FanSpeedSupported { get; init; }
        public bool VRAMSupported { get; init; }
        public bool VRAMClockSpeedSupported { get; init; }
        public bool VoltageSupported { get; init; }
        public bool TotalBoardPowerSupported { get; init; }

        [JsonConstructor]
        public GpuMetricsSupportDto(bool usageSupported, bool clockSpeedSupported, bool temperatureSupported, bool hotspotTemperatureSupported, bool powerSupported, bool fanSpeedSupported, bool vRAMSupported, bool vRAMClockSpeedSupported, bool voltageSupported, bool totalBoardPowerSupported)
        {
            UsageSupported = usageSupported;
            ClockSpeedSupported = clockSpeedSupported;
            TemperatureSupported = temperatureSupported;
            HotspotTemperatureSupported = hotspotTemperatureSupported;
            PowerSupported = powerSupported;
            FanSpeedSupported = fanSpeedSupported;
            VRAMSupported = vRAMSupported;
            VRAMClockSpeedSupported = vRAMClockSpeedSupported;
            VoltageSupported = voltageSupported;
            TotalBoardPowerSupported = totalBoardPowerSupported;
        }

        internal unsafe GpuMetricsSupportDto(IADLXGPUMetricsSupport* pMetricsSupport)
        {
            bool supported = false;
            pMetricsSupport->IsSupportedGPUUsage(&supported); UsageSupported = supported;
            pMetricsSupport->IsSupportedGPUClockSpeed(&supported); ClockSpeedSupported = supported;
            pMetricsSupport->IsSupportedGPUTemperature(&supported); TemperatureSupported = supported;
            pMetricsSupport->IsSupportedGPUHotspotTemperature(&supported); HotspotTemperatureSupported = supported;
            pMetricsSupport->IsSupportedGPUPower(&supported); PowerSupported = supported;
            pMetricsSupport->IsSupportedGPUFanSpeed(&supported); FanSpeedSupported = supported;
            pMetricsSupport->IsSupportedGPUVRAM(&supported); VRAMSupported = supported;
            pMetricsSupport->IsSupportedGPUVRAMClockSpeed(&supported); VRAMClockSpeedSupported = supported;
            pMetricsSupport->IsSupportedGPUVoltage(&supported); VoltageSupported = supported;
            pMetricsSupport->IsSupportedGPUTotalBoardPower(&supported); TotalBoardPowerSupported = supported;
        }
    }

    public readonly struct GpuMetricsSnapshotDto
    {
        public double Temperature { get; init; }
        public double HotspotTemperature { get; init; }
        public double Usage { get; init; }
        public int ClockSpeed { get; init; }
        public int VRAMClockSpeed { get; init; }
        public int VRAMUsage { get; init; }
        public int FanSpeed { get; init; }
        public double Power { get; init; }
        public double TotalBoardPower { get; init; }
        public int Voltage { get; init; }
        public long TimestampMs { get; init; }

        [JsonConstructor]
        public GpuMetricsSnapshotDto(double temperature, double hotspotTemperature, double usage, int clockSpeed, int vramClockSpeed, int vramUsage, int fanSpeed, double power, double totalBoardPower, int voltage, long timestampMs)
        {
            Temperature = temperature;
            HotspotTemperature = hotspotTemperature;
            Usage = usage;
            ClockSpeed = clockSpeed;
            VRAMClockSpeed = vramClockSpeed;
            VRAMUsage = vramUsage;
            FanSpeed = fanSpeed;
            Power = power;
            TotalBoardPower = totalBoardPower;
            Voltage = voltage;
            TimestampMs = timestampMs;
        }

        internal unsafe GpuMetricsSnapshotDto(IADLXGPUMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;
            double temp = 0; pMetrics->GPUTemperature(&temp); Temperature = temp;
            double hot = 0; pMetrics->GPUHotspotTemperature(&hot); HotspotTemperature = hot;
            double usage = 0; pMetrics->GPUUsage(&usage); Usage = usage;
            int clock = 0; pMetrics->GPUClockSpeed(&clock); ClockSpeed = clock;
            int vramClock = 0; pMetrics->GPUVRAMClockSpeed(&vramClock); VRAMClockSpeed = vramClock;
            int vram = 0; pMetrics->GPUVRAM(&vram); VRAMUsage = vram;
            int fan = 0; pMetrics->GPUFanSpeed(&fan); FanSpeed = fan;
            double power = 0; pMetrics->GPUPower(&power); Power = power;
            double totalPower = 0; pMetrics->GPUTotalBoardPower(&totalPower); TotalBoardPower = totalPower;
            int voltage = 0; pMetrics->GPUVoltage(&voltage); Voltage = voltage;
        }
    }

    public readonly struct PowerDistributionSnapshotDto
    {
        public int ApuShiftValue { get; init; }
        public int GpuShiftValue { get; init; }
        public int ApuShiftLimit { get; init; }
        public int GpuShiftLimit { get; init; }
        public int TotalShiftLimit { get; init; }
    }

    public readonly struct SystemMetricsSnapshotDto
    {
        public long TimestampMs { get; init; }
        public double CpuUsage { get; init; }
        public int SystemRam { get; init; }
        public int SmartShift { get; init; }
        public PowerDistributionSnapshotDto? PowerDistribution { get; init; }

        [JsonConstructor]
        public SystemMetricsSnapshotDto(long timestampMs, double cpuUsage, int systemRam, int smartShift, PowerDistributionSnapshotDto? powerDistribution)
        {
            TimestampMs = timestampMs;
            CpuUsage = cpuUsage;
            SystemRam = systemRam;
            SmartShift = smartShift;
            PowerDistribution = powerDistribution;
        }

        internal unsafe SystemMetricsSnapshotDto(IADLXSystemMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;
            double cpu = 0; pMetrics->CPUUsage(&cpu); CpuUsage = cpu;
            int ram = 0; pMetrics->SystemRAM(&ram); SystemRam = ram;
            int ss = 0; pMetrics->SmartShift(&ss); SmartShift = ss;

            PowerDistribution = null;
            if (ADLXUtils.TryQueryInterface((IntPtr)pMetrics, nameof(IADLXSystemMetrics1), out var pMetrics1Ptr))
            {
                using var metrics1 = new ComPtr<IADLXSystemMetrics1>((IADLXSystemMetrics1*)pMetrics1Ptr);
                int apu = 0, gpu = 0, apuLimit = 0, gpuLimit = 0, total = 0;
                if (metrics1.Get()->PowerDistribution(&apu, &gpu, &apuLimit, &gpuLimit, &total) == ADLX_RESULT.ADLX_OK)
                {
                    PowerDistribution = new PowerDistributionSnapshotDto
                    {
                        ApuShiftValue = apu,
                        GpuShiftValue = gpu,
                        ApuShiftLimit = apuLimit,
                        GpuShiftLimit = gpuLimit,
                        TotalShiftLimit = total
                    };
                }
            }
        }
    }

    public readonly struct GpuMetricsEntryDto
    {
        public int GpuUniqueId { get; init; }
        public GpuMetricsSnapshotDto Metrics { get; init; }

        [JsonConstructor]
        public GpuMetricsEntryDto(int gpuUniqueId, GpuMetricsSnapshotDto metrics)
        {
            GpuUniqueId = gpuUniqueId;
            Metrics = metrics;
        }
    }

    public readonly struct AllMetricsSnapshotDto
    {
        public long TimestampMs { get; init; }
        public SystemMetricsSnapshotDto? System { get; init; }
        public int? FPS { get; init; }
        public GpuMetricsEntryDto[] GpuMetrics { get; init; }

        [JsonConstructor]
        public AllMetricsSnapshotDto(long timestampMs, SystemMetricsSnapshotDto? system, int? fps, GpuMetricsEntryDto[] gpuMetrics)
        {
            TimestampMs = timestampMs;
            System = system;
            FPS = fps;
            GpuMetrics = gpuMetrics;
        }

        internal unsafe AllMetricsSnapshotDto(IADLXAllMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;

            System = null;
            IADLXSystemMetrics* pSys = null;
            if (pMetrics->GetSystemMetrics(&pSys) == ADLX_RESULT.ADLX_OK && pSys != null)
            {
                using var sysMetrics = new ComPtr<IADLXSystemMetrics>(pSys);
                System = new SystemMetricsSnapshotDto(sysMetrics.Get());
            }

            FPS = null;
            IADLXFPS* pFps = null;
            if (pMetrics->GetFPS(&pFps) == ADLX_RESULT.ADLX_OK && pFps != null)
            {
                using var fpsMetrics = new ComPtr<IADLXFPS>(pFps);
                int fpsValue = 0;
                if (fpsMetrics.Get()->FPS(&fpsValue) == ADLX_RESULT.ADLX_OK)
                {
                    FPS = fpsValue;
                }
            }

            GpuMetrics = Array.Empty<GpuMetricsEntryDto>();
        }
    }

    public readonly struct PerformanceMonitoringSettingsDto
    {
        public int SamplingIntervalMs { get; init; }
        public int MaxHistorySizeSec { get; init; }

        [JsonConstructor]
        public PerformanceMonitoringSettingsDto(int samplingIntervalMs, int maxHistorySizeSec)
        {
            SamplingIntervalMs = samplingIntervalMs;
            MaxHistorySizeSec = maxHistorySizeSec;
        }

        internal unsafe PerformanceMonitoringSettingsDto(IADLXPerformanceMonitoringServices* pServices)
        {
            int interval = 0;
            pServices->GetSamplingInterval(&interval);
            SamplingIntervalMs = interval;

            int size = 0;
            pServices->GetCurrentPerformanceMetricsHistorySize(&size);
            MaxHistorySizeSec = size;
        }
    }
    #endregion
}

