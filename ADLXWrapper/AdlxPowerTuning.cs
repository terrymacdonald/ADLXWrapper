using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Facade for power tuning services (SmartShift, manual power/TDC via GPU tuning services).
    /// </summary>
    public sealed unsafe class AdlxPowerTuning : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXPowerTuningServices> _services;
        private bool _disposed;

        internal AdlxPowerTuning(ADLXApi owner, IADLXPowerTuningServices* services)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (services == null) throw new ArgumentNullException(nameof(services));
            _services = new ComPtr<IADLXPowerTuningServices>(services);
        }

        public PowerTuningProfile GetProfile()
        {
            ThrowIfDisposed();
            var max = ADLXPowerTuningHelpers.GetSmartShiftMax(_services.Get());
            SmartShiftEcoInfo? eco = null;
            if (ADLXHelpers.TryQueryInterface((IntPtr)_services.Get(), nameof(IADLXPowerTuningServices1), out var _))
            {
                eco = ADLXPowerTuningHelpers.GetSmartShiftEco(_services.Get());
            }
            return new PowerTuningProfile(max, eco);
        }

        public void ApplyProfile(PowerTuningProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();

            if (profile.SmartShiftMax != null && profile.SmartShiftMax.Value.IsSupported)
            {
                IADLXSmartShiftMax* pMax = null;
                var res = _services.Get()->GetSmartShiftMax(&pMax);
                if (res == ADLX_RESULT.ADLX_OK && pMax != null)
                {
                    using var max = new ComPtr<IADLXSmartShiftMax>(pMax);
                    ADLXPowerTuningHelpers.ApplySmartShiftMax(max.Get(), profile.SmartShiftMax.Value);
                }
            }

            if (profile.SmartShiftEco != null && profile.SmartShiftEco.Value.IsSupported)
            {
                if (ADLXHelpers.TryQueryInterface((IntPtr)_services.Get(), nameof(IADLXPowerTuningServices1), out var ptr) && ptr != IntPtr.Zero)
                {
                    using var svc1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)ptr);
                    IADLXSmartShiftEco* pEco = null;
                    var res = svc1.Get()->GetSmartShiftEco(&pEco);
                    if (res == ADLX_RESULT.ADLX_OK && pEco != null)
                    {
                        using var eco = new ComPtr<IADLXSmartShiftEco>(pEco);
                        ADLXPowerTuningHelpers.ApplySmartShiftEco(eco.Get(), profile.SmartShiftEco.Value);
                    }
                }
            }
        }

        public AdlxInterfaceHandle GetPowerTuningServicesHandle()
        {
            ThrowIfDisposed();
            ADLXHelpers.AddRefInterface((IntPtr)_services.Get());
            return AdlxInterfaceHandle.From(_services.Get(), addRef: false);
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

        ~AdlxPowerTuning()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(AdlxPowerTuning));
        }
    }

    /// <summary>
    /// JSON-serializable power tuning profile.
    /// </summary>
    public sealed class PowerTuningProfile
    {
        public SmartShiftMaxInfo? SmartShiftMax { get; set; }
        public SmartShiftEcoInfo? SmartShiftEco { get; set; }

        [JsonConstructor]
        public PowerTuningProfile(SmartShiftMaxInfo? smartShiftMax, SmartShiftEcoInfo? smartShiftEco)
        {
            SmartShiftMax = smartShiftMax;
            SmartShiftEco = smartShiftEco;
        }

        internal PowerTuningProfile(SmartShiftMaxInfo max, SmartShiftEcoInfo? eco)
        {
            SmartShiftMax = max;
            SmartShiftEco = eco;
        }
    }
}
