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
        private bool _disposed;

        /// <summary>
        /// Creates a performance monitoring helper from the native services interface.
        /// </summary>
        /// <param name="services">Native performance monitoring services pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this helper.</param>
        public ADLXPerformanceMonitoringServicesHelper(IADLXPerformanceMonitoringServices* services, bool addRef = true)
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
        }

        /// <summary>
        /// Returns the native performance monitoring services interface owned by this helper.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
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
        public AdlxInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return AdlxInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: true);
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
        public IADLXGPUMetricsSupport* GetGpuMetricsSupportNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetricsSupport* support = null;
                var result = _services.Get()->GetSupportedGPUMetrics(gpu, &support);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || support == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics support not available for this GPU");
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
        public GpuMetricsSupportInfo GetGpuMetricsSupport(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var support = new ComPtr<IADLXGPUMetricsSupport>(GetGpuMetricsSupportNative(gpu));
            return new GpuMetricsSupportInfo(support.Get());
        }

        /// <summary>
        /// Gets current GPU metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>Native GPU metrics pointer.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXGPUMetrics* GetCurrentGpuMetricsNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetrics* metrics = null;
                var result = _services.Get()->GetCurrentGPUMetrics(gpu, &metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics not supported for this GPU");
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
        public GpuMetricsSnapshotInfo GetCurrentGpuMetrics(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var metrics = new ComPtr<IADLXGPUMetrics>(GetCurrentGpuMetricsNative(gpu));
            return new GpuMetricsSnapshotInfo(metrics.Get());
        }

        /// <summary>
        /// Gets current system metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <returns>Native system metrics pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXSystemMetrics* GetCurrentSystemMetricsNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                IADLXSystemMetrics* metrics = null;
                var result = _services.Get()->GetCurrentSystemMetrics(&metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics not supported by this ADLX system");
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
        public SystemMetricsSnapshotInfo GetCurrentSystemMetrics()
        {
            ThrowIfDisposed();
            using var metrics = new ComPtr<IADLXSystemMetrics>(GetCurrentSystemMetricsNative());
            return new SystemMetricsSnapshotInfo(metrics.Get());
        }

        /// <summary>
        /// Gets current system+GPU metrics (native pointer). Caller must dispose.
        /// </summary>
        /// <returns>Native all-metrics pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXAllMetrics* GetCurrentAllMetricsNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                IADLXAllMetrics* metrics = null;
                var result = _services.Get()->GetCurrentAllMetrics(&metrics);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || metrics == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "All metrics not supported by this ADLX system");
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

            using (ADLXSync.EnterRead())
            {
                IADLXGPUMetricsList* list = null;
                var result = _services.Get()->GetGPUMetricsHistory(gpu, startMs, stopMs, &list);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics history not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get GPU metrics history");

                return list; // caller wraps/disposes
            }
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
            using (ADLXSync.EnterRead())
            {
                IADLXSystemMetricsList* list = null;
                var result = _services.Get()->GetSystemMetricsHistory(startMs, stopMs, &list);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics history not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get system metrics history");

                return list; // caller wraps/disposes
            }
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

        public PerformanceMonitoringSettingsInfo GetPerformanceMonitoringSettings()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return new PerformanceMonitoringSettingsInfo(_services.Get());
            }
        }

        public void ApplyPerformanceMonitoringSettings(PerformanceMonitoringSettingsInfo info)
        {
            ThrowIfDisposed();
            var intervalRange = GetSamplingIntervalRange();
            if (info.SamplingIntervalMs >= intervalRange.minValue && info.SamplingIntervalMs <= intervalRange.maxValue)
            {
                SetSamplingInterval(info.SamplingIntervalMs);
            }

            var maxHistory = GetMaxPerformanceMetricsHistorySize();
            var clampedHistory = Math.Min(info.MaxHistorySizeSec, maxHistory);
            SetMaxPerformanceMetricsHistorySize(clampedHistory);
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
    public readonly struct GpuMetricsSupportInfo
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
        public GpuMetricsSupportInfo(bool usageSupported, bool clockSpeedSupported, bool temperatureSupported, bool hotspotTemperatureSupported, bool powerSupported, bool fanSpeedSupported, bool vRAMSupported, bool vRAMClockSpeedSupported, bool voltageSupported, bool totalBoardPowerSupported)
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

        internal unsafe GpuMetricsSupportInfo(IADLXGPUMetricsSupport* pMetricsSupport)
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

    public readonly struct GpuMetricsSnapshotInfo
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
        public GpuMetricsSnapshotInfo(double temperature, double hotspotTemperature, double usage, int clockSpeed, int vramClockSpeed, int vramUsage, int fanSpeed, double power, double totalBoardPower, int voltage, long timestampMs)
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

        internal unsafe GpuMetricsSnapshotInfo(IADLXGPUMetrics* pMetrics)
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

    public readonly struct PowerDistributionSnapshotInfo
    {
        public int ApuShiftValue { get; init; }
        public int GpuShiftValue { get; init; }
        public int ApuShiftLimit { get; init; }
        public int GpuShiftLimit { get; init; }
        public int TotalShiftLimit { get; init; }
    }

    public readonly struct SystemMetricsSnapshotInfo
    {
        public long TimestampMs { get; init; }
        public double CpuUsage { get; init; }
        public int SystemRam { get; init; }
        public int SmartShift { get; init; }
        public PowerDistributionSnapshotInfo? PowerDistribution { get; init; }

        [JsonConstructor]
        public SystemMetricsSnapshotInfo(long timestampMs, double cpuUsage, int systemRam, int smartShift, PowerDistributionSnapshotInfo? powerDistribution)
        {
            TimestampMs = timestampMs;
            CpuUsage = cpuUsage;
            SystemRam = systemRam;
            SmartShift = smartShift;
            PowerDistribution = powerDistribution;
        }

        internal unsafe SystemMetricsSnapshotInfo(IADLXSystemMetrics* pMetrics)
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
                    PowerDistribution = new PowerDistributionSnapshotInfo
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

    public readonly struct GpuMetricsEntryInfo
    {
        public int GpuUniqueId { get; init; }
        public GpuMetricsSnapshotInfo Metrics { get; init; }

        [JsonConstructor]
        public GpuMetricsEntryInfo(int gpuUniqueId, GpuMetricsSnapshotInfo metrics)
        {
            GpuUniqueId = gpuUniqueId;
            Metrics = metrics;
        }
    }

    public readonly struct AllMetricsSnapshotInfo
    {
        public long TimestampMs { get; init; }
        public SystemMetricsSnapshotInfo? System { get; init; }
        public int? FPS { get; init; }
        public GpuMetricsEntryInfo[] GpuMetrics { get; init; }

        [JsonConstructor]
        public AllMetricsSnapshotInfo(long timestampMs, SystemMetricsSnapshotInfo? system, int? fps, GpuMetricsEntryInfo[] gpuMetrics)
        {
            TimestampMs = timestampMs;
            System = system;
            FPS = fps;
            GpuMetrics = gpuMetrics;
        }

        internal unsafe AllMetricsSnapshotInfo(IADLXAllMetrics* pMetrics)
        {
            long ts = 0; pMetrics->TimeStamp(&ts); TimestampMs = ts;

            System = null;
            IADLXSystemMetrics* pSys = null;
            if (pMetrics->GetSystemMetrics(&pSys) == ADLX_RESULT.ADLX_OK && pSys != null)
            {
                using var sysMetrics = new ComPtr<IADLXSystemMetrics>(pSys);
                System = new SystemMetricsSnapshotInfo(sysMetrics.Get());
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

            GpuMetrics = Array.Empty<GpuMetricsEntryInfo>();
        }
    }

    public readonly struct PerformanceMonitoringSettingsInfo
    {
        public int SamplingIntervalMs { get; init; }
        public int MaxHistorySizeSec { get; init; }

        [JsonConstructor]
        public PerformanceMonitoringSettingsInfo(int samplingIntervalMs, int maxHistorySizeSec)
        {
            SamplingIntervalMs = samplingIntervalMs;
            MaxHistorySizeSec = maxHistorySizeSec;
        }

        internal unsafe PerformanceMonitoringSettingsInfo(IADLXPerformanceMonitoringServices* pServices)
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

