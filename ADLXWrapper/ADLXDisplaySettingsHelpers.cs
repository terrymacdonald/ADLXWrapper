using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Display settings helpers (FreeSync, GPU scaling, scaling mode, color depth, pixel format, VSR, integer scaling, HDCP, VariBright, display blanking, custom color).
    /// </summary>
    public static unsafe class ADLXDisplaySettingsHelpers
    {
        /// <summary>
        /// Gets the Gamma settings for a specific display.
        /// </summary>
        public static GammaInfo GetGamma(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplayGamma*, ADLX_RESULT>)pDisplayServices->Vtbl->GetGamma;
            IntPtr pGamma;
            var result = getFn(pDisplayServices, pDisplay, &pGamma);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamma interface");
            using var gamma = new ComPtr<IADLXDisplayGamma>((IADLXDisplayGamma*)pGamma);
            return new GammaInfo(gamma.Get());
        }

        /// <summary>
        /// Reapplies the current Gamma settings.
        /// </summary>
        public static void ReapplyGamma(IADLXDisplayGamma* pGamma)
        {
            if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));

            byte srgb = 0, bt709 = 0, pq = 0, pq2084 = 0, g36 = 0, coeff = 0, reRamp = 0, deRamp = 0;
            pGamma->IsCurrentReGammaSRGB(&srgb);
            pGamma->IsCurrentReGammaBT709(&bt709);
            pGamma->IsCurrentReGammaPQ(&pq);
            pGamma->IsCurrentReGammaPQ2084Interim(&pq2084);
            pGamma->IsCurrentReGamma36(&g36);
            pGamma->IsCurrentRegammaCoefficient(&coeff);
            pGamma->IsCurrentReGammaRamp(&reRamp);
            pGamma->IsCurrentDeGammaRamp(&deRamp);

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (srgb != 0) { r = pGamma->SetReGammaSRGB(); }
            else if (bt709 != 0) { r = pGamma->SetReGammaBT709(); }
            else if (pq != 0) { r = pGamma->SetReGammaPQ(); }
            else if (pq2084 != 0) { r = pGamma->SetReGammaPQ2084Interim(); }
            else if (g36 != 0) { r = pGamma->SetReGamma36(); }

            if (r == ADLX_RESULT.ADLX_OK) { return; }
            if (r != ADLX_RESULT.ADLX_FAIL) throw new ADLXException(r, "Failed to reapply preset gamma");

            if (coeff != 0)
            {
                ADLX_RegammaCoeff current = default;
                pGamma->GetGammaCoefficient(&current);
                r = pGamma->SetReGammaCoefficient(current);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply gamma coefficient");
                return;
            }

            ADLX_GammaRamp ramp = default;
            if (reRamp != 0)
            {
                pGamma->GetGammaRamp(&ramp);
                r = pGamma->SetReGammaRamp(ramp);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply re-gamma ramp");
                return;
            }

            if (deRamp != 0)
            {
                pGamma->GetGammaRamp(&ramp);
                r = pGamma->SetDeGammaRamp(ramp);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply de-gamma ramp");
                return;
            }

            r = pGamma->ResetGammaRamp();
            if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reset gamma ramp");
        }

        /// <summary>
        /// Gets the Gamut settings for a specific display.
        /// </summary>
        public static GamutInfo GetGamut(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplayGamut*, ADLX_RESULT>)pDisplayServices->Vtbl->GetGamut;
            IntPtr pGamut;
            var result = getFn(pDisplayServices, pDisplay, &pGamut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamut interface");
            using var gamut = new ComPtr<IADLXDisplayGamut>((IADLXDisplayGamut*)pGamut);
            return new GamutInfo(gamut.Get());
        }

        /// <summary>
        /// Reapplies the current Gamut settings.
        /// </summary>
        public static void ReapplyGamut(IADLXDisplayGamut* pGamut)
        {
            if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));

            byte cur5000 = 0, cur6500 = 0, cur7500 = 0, cur9300 = 0, curCustomWhite = 0;
            pGamut->IsCurrent5000kWhitePoint(&cur5000);
            pGamut->IsCurrent6500kWhitePoint(&cur6500);
            pGamut->IsCurrent7500kWhitePoint(&cur7500);
            pGamut->IsCurrent9300kWhitePoint(&cur9300);
            pGamut->IsCurrentCustomWhitePoint(&curCustomWhite);

            byte cur709 = 0, cur601 = 0, curAdobe = 0, curCIERgb = 0, cur2020 = 0, curCustomSpace = 0;
            pGamut->IsCurrentCCIR709ColorSpace(&cur709);
            pGamut->IsCurrentCCIR601ColorSpace(&cur601);
            pGamut->IsCurrentAdobeRgbColorSpace(&curAdobe);
            pGamut->IsCurrentCIERgbColorSpace(&curCIERgb);
            pGamut->IsCurrentCCIR2020ColorSpace(&cur2020);
            pGamut->IsCurrentCustomColorSpace(&curCustomSpace);

            ADLX_GamutColorSpace currentSpace = default;
            pGamut->GetGamutColorSpace(&currentSpace);

            ADLX_Point whitePoint = default;
            bool hasCustomWhitePoint = pGamut->GetWhitePoint(&whitePoint) == ADLX_RESULT.ADLX_OK;

            ADLX_WHITE_POINT? wp = cur5000 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_5000K :
                                   cur6500 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_6500K :
                                   cur7500 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_7500K :
                                   cur9300 != 0 ? ADLX_WHITE_POINT.WHITE_POINT_9300K :
                                   (ADLX_WHITE_POINT?)null;

            ADLX_GAMUT_SPACE? gamutSpace = cur709 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_709 :
                                         cur601 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_601 :
                                         curAdobe != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_ADOBE_RGB :
                                         curCIERgb != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CIE_RGB :
                                         cur2020 != 0 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_2020 :
                                         (ADLX_GAMUT_SPACE?)null;

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (gamutSpace.HasValue)
            {
                if (wp.HasValue)
                {
                    r = pGamut->SetGamut_PredefinedWhite_PredefinedGamut(wp.Value, gamutSpace.Value);
                }
                else if (hasCustomWhitePoint || curCustomWhite != 0)
                {
                    ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                    r = pGamut->SetGamut_CustomWhite_PredefinedGamut(white, gamutSpace.Value);
                }
            }
            else if (wp.HasValue)
            {
                r = pGamut->SetGamut_PredefinedWhite_CustomGamut(wp.Value, currentSpace);
            }
            else if (hasCustomWhitePoint || curCustomWhite != 0 || curCustomSpace != 0)
            {
                ADLX_RGB white = new ADLX_RGB { gamutR = whitePoint.x, gamutG = whitePoint.y, gamutB = 0 };
                r = pGamut->SetGamut_CustomWhite_CustomGamut(white, currentSpace);
            }

            if (r != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r, "Failed to reapply gamut configuration");
        }

        /// <summary>
        /// Gets the 3DLUT settings for a specific display.
        /// </summary>
        public static ThreeDLUTInfo Get3DLUT(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplay3DLUT*, ADLX_RESULT>)pDisplayServices->Vtbl->Get3DLUT;
            IntPtr pLut;
            var result = getFn(pDisplayServices, pDisplay, &pLut);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3DLUT interface");
            using var lut = new ComPtr<IADLXDisplay3DLUT>((IADLXDisplay3DLUT*)pLut);
            return new ThreeDLUTInfo(lut.Get());
        }

        /// <summary>
        /// Reapplies the current 3DLUT settings.
        /// </summary>
        public static void Reapply3DLUT(IADLXDisplay3DLUT* p3dLut)
        {
            if (p3dLut == null) throw new ArgumentNullException(nameof(p3dLut));

            byte supSce = 0, supVivid = 0, curDis = 0, curVivid = 0;
            p3dLut->IsSupportedSCE(&supSce);
            p3dLut->IsSupportedSCEVividGaming(&supVivid);
            p3dLut->IsCurrentSCEDisabled(&curDis);
            p3dLut->IsCurrentSCEVividGaming(&curVivid);

            ADLX_RESULT r = ADLX_RESULT.ADLX_FAIL;

            if (supSce != 0 || supVivid != 0)
            {
                if (curVivid != 0 && supVivid != 0) { r = p3dLut->SetSCEVividGaming(); }
                else { r = p3dLut->SetSCEDisabled(); }

                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply SCE state");
            }

            byte supDynamic = 0;
            p3dLut->IsSupportedSCEDynamicContrast(&supDynamic);
            if (supDynamic != 0)
            {
                ADLX_IntRange range = default;
                p3dLut->GetSCEDynamicContrastRange(&range);

                int currentContrast = 0;
                p3dLut->GetSCEDynamicContrast(&currentContrast);

                int clamped = Math.Clamp(currentContrast, range.minValue, range.maxValue);
                r = p3dLut->SetSCEDynamicContrast(clamped);
                if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to reapply SCE dynamic contrast");
            }
        }

        /// <summary>
        /// Gets the Display Connectivity Experience settings for a specific display.
        /// </summary>
        public static ConnectivityExperienceInfo GetDisplayConnectivityExperience(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplayConnectivityExperience*, ADLX_RESULT>)pDisplayServices->Vtbl->GetDisplayConnectivityExperience;

            IntPtr pConn;
            var result = getFn(pDisplayServices, pDisplay, &pConn);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Display Connectivity Experience interface");

            using var connectivity = new ComPtr<IADLXDisplayConnectivityExperience>((IADLXDisplayConnectivityExperience*)pConn);
            return new ConnectivityExperienceInfo(connectivity.Get());
        }

        /// <summary>
        /// Sets the enabled state of HDMI Quality Detection.
        /// </summary>
        public static void SetDisplayConnectivityHDMIQualityDetectionEnabled(IADLXDisplayConnectivityExperience* pConnectivity, bool enable)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, byte, ADLX_RESULT>)pConnectivity->Vtbl->SetEnabledHDMIQualityDetection;
            var result = setFn(pConnectivity, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDMI quality detection");
        }

        /// <summary>
        /// Sets the relative pre-emphasis for display connectivity.
        /// </summary>
        public static void SetDisplayConnectivityRelativePreEmphasis(IADLXDisplayConnectivityExperience* pConnectivity, int value)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
            var setFn = (delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)pConnectivity->Vtbl->SetRelativePreEmphasis;
            var result = setFn(pConnectivity, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative pre-emphasis");
        }

        /// <summary>
        /// Sets the relative voltage swing for display connectivity.
        /// </summary>
        public static void SetDisplayConnectivityRelativeVoltageSwing(IADLXDisplayConnectivityExperience* pConnectivity, int value)
        {
            if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
            var setFn = (delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)pConnectivity->Vtbl->SetRelativeVoltageSwing;
            var result = setFn(pConnectivity, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set relative voltage swing");
        }

        /// <summary>
        /// Gets the Custom Resolution settings for a specific display.
        /// </summary>
        public static CustomResolutionInfo GetCustomResolution(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplayCustomResolution*, ADLX_RESULT>)pDisplayServices->Vtbl->GetCustomResolution;

            IntPtr pCustomRes;
            var result = getFn(pDisplayServices, pDisplay, &pCustomRes);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Resolution interface");
            }

            using var customRes = new ComPtr<IADLXDisplayCustomResolution>((IADLXDisplayCustomResolution*)pCustomRes);
            return new CustomResolutionInfo(customRes.Get());
        }

        /// <summary>
        /// Sets the enabled state of Vari-Bright Backlight Adaptive.
        /// </summary>
        public static void SetVariBrightBacklightAdaptiveEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
        {
            if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
            var setFn = (delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)pVariBright->Vtbl->SetBacklightAdaptiveEnabled;
            var result = setFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set backlight adaptive mode");
        }

        /// <summary>
        /// Sets the enabled state of Vari-Bright Battery Life.
        /// </summary>
        public static void SetVariBrightBatteryLifeEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
        {
            if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
            var setFn = (delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)pVariBright->Vtbl->SetBatteryLifeEnabled;
            var result = setFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set battery life mode");
        }

        /// <summary>
        /// Gets the Custom Color settings for a specific display.
        /// </summary>
        public static CustomColorInfo GetCustomColor(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, out IADLXDisplayCustomColor*, ADLX_RESULT>)pDisplayServices->Vtbl->GetCustomColor;

            IntPtr pCustomColor;
            var result = getFn(pDisplayServices, pDisplay, &pCustomColor);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Custom Color interface");
            }

            using var customColor = new ComPtr<IADLXDisplayCustomColor>((IADLXDisplayCustomColor*)pCustomColor);
            return new CustomColorInfo(customColor.Get());
        }

        private static void SetCustomColorIntProperty(IntPtr pCustomColor, IntPtr setterPtr, int value)
        {
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetIntValueFn>(setterPtr);
            var result = setFn(pCustomColor, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Custom Color value");
        }

        public static unsafe IntPtr GetDisplayBlankingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDisplayBlankingFn>(vtbl->GetDisplayBlanking);

            IntPtr pBlanking;
            var result = getFn(pDisplayServices, pDisplay, &pBlanking);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Display Blanking interface");
            }

            return pBlanking;
        }

        public static unsafe (bool supported, bool blanked) GetDisplayBlankingState(IntPtr pBlanking)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var vtbl = *(ADLXVTables.IADLXDisplayBlankingVtbl**)pBlanking;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var blankedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentBlanked);

            byte supported = 0;
            byte blanked = 0;
            var r1 = supFn(pBlanking, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Display Blanking support");

            var r2 = blankedFn(pBlanking, &blanked);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query current blanking state");

            return (supported != 0, blanked != 0);
        }

        public static unsafe void SetDisplayBlanked(IntPtr pBlanking, bool blank)
        {
            if (pBlanking == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pBlanking));

            var vtbl = *(ADLXVTables.IADLXDisplayBlankingVtbl**)pBlanking;
            if (blank)
            {
                var fn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetBlanked);
                var result = fn(pBlanking);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set display blanked");
            }
            else
            {
                var fn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetUnblanked);
                var result = fn(pBlanking);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set display unblanked");
            }
        }

        public static unsafe IntPtr GetVirtualSuperResolutionHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVirtualSuperResolutionFn>(vtbl->GetVirtualSuperResolution);

            IntPtr pVsr;
            var result = getFn(pDisplayServices, pDisplay, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Virtual Super Resolution interface");
            }

            return pVsr;
        }

        public static unsafe (bool supported, bool enabled) GetVirtualSuperResolutionState(IntPtr pVsr)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXDisplayVSRVtbl**)pVsr;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pVsr, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VSR support");

            var r2 = enFn(pVsr, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VSR enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetVirtualSuperResolutionEnabled(IntPtr pVsr, bool enable)
        {
            if (pVsr == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVsr));

            var vtbl = *(ADLXVTables.IADLXDisplayVSRVtbl**)pVsr;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pVsr, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VSR state");
        }

        public static unsafe IntPtr GetIntegerScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetIntegerScalingFn>(vtbl->GetIntegerScaling);

            IntPtr pIntegerScaling;
            var result = getFn(pDisplayServices, pDisplay, &pIntegerScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Integer Scaling interface");
            }

            return pIntegerScaling;
        }

        public static unsafe (bool supported, bool enabled) GetIntegerScalingState(IntPtr pIntegerScaling)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayIntegerScalingVtbl**)pIntegerScaling;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pIntegerScaling, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Integer Scaling support");

            var r2 = enFn(pIntegerScaling, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Integer Scaling enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetIntegerScalingEnabled(IntPtr pIntegerScaling, bool enable)
        {
            if (pIntegerScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pIntegerScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayIntegerScalingVtbl**)pIntegerScaling;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pIntegerScaling, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Integer Scaling state");
        }

        public static unsafe IntPtr GetHDCPHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetHDCPFn>(vtbl->GetHDCP);

            IntPtr pHdcp;
            var result = getFn(pDisplayServices, pDisplay, &pHdcp);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get HDCP interface");
            }

            return pHdcp;
        }

        public static unsafe (bool supported, bool enabled) GetHDCPState(IntPtr pHdcp)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var vtbl = *(ADLXVTables.IADLXDisplayHDCPVtbl**)pHdcp;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;

            var r1 = supFn(pHdcp, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query HDCP support");

            var r2 = enFn(pHdcp, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query HDCP enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetHDCPEnabled(IntPtr pHdcp, bool enable)
        {
            if (pHdcp == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pHdcp));

            var vtbl = *(ADLXVTables.IADLXDisplayHDCPVtbl**)pHdcp;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pHdcp, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set HDCP state");
        }

        public enum VariBrightMode
        {
            Unknown = 0,
            MaximizeBrightness,
            OptimizeBrightness,
            Balanced,
            OptimizeBattery,
            MaximizeBattery
        }

        public static unsafe IntPtr GetVariBrightHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetVariBrightFn>(vtbl->GetVariBright);

            IntPtr pVariBright;
            var result = getFn(pDisplayServices, pDisplay, &pVariBright);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get VariBright interface");
            }

            return pVariBright;
        }

        public static unsafe (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState(IntPtr pVariBright)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var vtbl = *(ADLXVTables.IADLXDisplayVariBrightVtbl**)pVariBright;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported = 0;
            byte enabled = 0;
            var r1 = supFn(pVariBright, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query VariBright support");

            var r2 = enFn(pVariBright, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query VariBright enabled");

            VariBrightMode mode = VariBrightMode.Unknown;
            if (supported != 0)
            {
                mode = DetectVariBrightMode(pVariBright, vtbl);
            }

            return (supported != 0, enabled != 0, mode);
        }

        private static unsafe VariBrightMode DetectVariBrightMode(IntPtr pVariBright, ADLXVTables.IADLXDisplayVariBrightVtbl* vtbl)
        {
            byte flag = 0;

            var maxBrightFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentMaximizeBrightness);
            if (maxBrightFn(pVariBright, &flag) == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.MaximizeBrightness;

            flag = 0;
            var optBrightFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentOptimizeBrightness);
            if (optBrightFn(pVariBright, &flag) == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.OptimizeBrightness;

            flag = 0;
            var balancedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentBalanced);
            if (balancedFn(pVariBright, &flag) == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.Balanced;

            flag = 0;
            var optBatteryFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentOptimizeBattery);
            if (optBatteryFn(pVariBright, &flag) == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.OptimizeBattery;

            flag = 0;
            var maxBatteryFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsCurrentMaximizeBattery);
            if (maxBatteryFn(pVariBright, &flag) == ADLX_RESULT.ADLX_OK && flag != 0) return VariBrightMode.MaximizeBattery;

            return VariBrightMode.Unknown;
        }

        public static unsafe void SetVariBright(IntPtr pVariBright, bool enable, VariBrightMode mode)
        {
            if (pVariBright == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pVariBright));

            var vtbl = *(ADLXVTables.IADLXDisplayVariBrightVtbl**)pVariBright;
            var setEnabledFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setEnabledFn(pVariBright, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set VariBright enabled state");

            if (!enable)
                return;

            ADLX_RESULT modeResult;
            switch (mode)
            {
                case VariBrightMode.MaximizeBrightness:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetMaximizeBrightness)(pVariBright);
                    break;
                case VariBrightMode.OptimizeBrightness:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetOptimizeBrightness)(pVariBright);
                    break;
                case VariBrightMode.Balanced:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetBalanced)(pVariBright);
                    break;
                case VariBrightMode.OptimizeBattery:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetOptimizeBattery)(pVariBright);
                    break;
                case VariBrightMode.MaximizeBattery:
                    modeResult = Marshal.GetDelegateForFunctionPointer<ADLXVTables.InvokeFn>(vtbl->SetMaximizeBattery)(pVariBright);
                    break;
                default:
                    modeResult = ADLX_RESULT.ADLX_OK;
                    break;
            }

            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set VariBright mode");
        }

        public static unsafe IntPtr GetColorDepthHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetColorDepthFn>(vtbl->GetColorDepth);

            IntPtr pColorDepth;
            var result = getFn(pDisplayServices, pDisplay, &pColorDepth);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Color Depth interface");
            }

            return pColorDepth;
        }

        public static unsafe (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState(IntPtr pColorDepth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var vtbl = *(ADLXVTables.IADLXDisplayColorDepthVtbl**)pColorDepth;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetColorDepthValueFn>(vtbl->GetValue);

            byte supported = 0;
            ADLX_COLOR_DEPTH depth = default;

            var r1 = supFn(pColorDepth, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Color Depth support");

            var r2 = getFn(pColorDepth, &depth);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Color Depth value");

            return (supported != 0, depth);
        }

        public static unsafe void SetColorDepth(IntPtr pColorDepth, ADLX_COLOR_DEPTH depth)
        {
            if (pColorDepth == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pColorDepth));

            var vtbl = *(ADLXVTables.IADLXDisplayColorDepthVtbl**)pColorDepth;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetColorDepthValueFn>(vtbl->SetValue);
            var result = setFn(pColorDepth, depth);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Color Depth");
        }

        public static unsafe IntPtr GetPixelFormatHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetPixelFormatFn>(vtbl->GetPixelFormat);

            IntPtr pPixelFormat;
            var result = getFn(pDisplayServices, pDisplay, &pPixelFormat);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Pixel Format interface");
            }

            return pPixelFormat;
        }

        public static unsafe (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState(IntPtr pPixelFormat)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var vtbl = *(ADLXVTables.IADLXDisplayPixelFormatVtbl**)pPixelFormat;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetPixelFormatValueFn>(vtbl->GetValue);

            byte supported = 0;
            ADLX_PIXEL_FORMAT format = default;

            var r1 = supFn(pPixelFormat, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query Pixel Format support");

            var r2 = getFn(pPixelFormat, &format);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query Pixel Format value");

            return (supported != 0, format);
        }

        public static unsafe void SetPixelFormat(IntPtr pPixelFormat, ADLX_PIXEL_FORMAT format)
        {
            if (pPixelFormat == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPixelFormat));

            var vtbl = *(ADLXVTables.IADLXDisplayPixelFormatVtbl**)pPixelFormat;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetPixelFormatValueFn>(vtbl->SetValue);
            var result = setFn(pPixelFormat, format);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set Pixel Format");
        }

        public static unsafe IntPtr GetFreeSyncHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetFreeSyncFn>(vtbl->GetFreeSync);

            IntPtr pFS;
            var result = getFn(pDisplayServices, pDisplay, &pFS);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync interface");
            }

            return pFS;
        }

        public static unsafe IntPtr GetGPUScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetGPUScalingFn>(vtbl->GetGPUScaling);

            IntPtr pScaling;
            var result = getFn(pDisplayServices, pDisplay, &pScaling);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU scaling interface");
            }

            return pScaling;
        }

        public static unsafe IntPtr GetScalingModeHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetScalingModeFn>(vtbl->GetScalingMode);

            IntPtr pMode;
            var result = getFn(pDisplayServices, pDisplay, &pMode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get scaling mode interface");
            }

            return pMode;
        }

        public static unsafe (bool supported, bool enabled) GetFreeSyncState(IntPtr pFreeSync)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncVtbl**)pFreeSync;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pFreeSync, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync support");

            var r2 = enFn(pFreeSync, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetFreeSyncEnabled(IntPtr pFreeSync, bool enable)
        {
            if (pFreeSync == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFreeSync));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncVtbl**)pFreeSync;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pFreeSync, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync enabled state");
            }
        }

        public static unsafe (bool supported, bool enabled) GetGPUScalingState(IntPtr pGPUScaling)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayGPUScalingVtbl**)pGPUScaling;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pGPUScaling, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query GPU scaling support");

            var r2 = enFn(pGPUScaling, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query GPU scaling enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetGPUScalingEnabled(IntPtr pGPUScaling, bool enable)
        {
            if (pGPUScaling == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUScaling));

            var vtbl = *(ADLXVTables.IADLXDisplayGPUScalingVtbl**)pGPUScaling;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pGPUScaling, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set GPU scaling enabled state");
            }
        }

        public static unsafe (bool supported, ADLX_SCALE_MODE mode) GetScalingMode(IntPtr pScalingMode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var vtbl = *(ADLXVTables.IADLXDisplayScalingModeVtbl**)pScalingMode;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetScaleModeFn>(vtbl->GetMode);

            byte supported;
            var r1 = supFn(pScalingMode, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query scaling mode support");

            ADLX_SCALE_MODE mode;
            var r2 = getFn(pScalingMode, &mode);
            if (r2 != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(r2, "Failed to get scaling mode");
            }

            return (supported != 0, mode);
        }

        public static unsafe void SetScalingMode(IntPtr pScalingMode, ADLX_SCALE_MODE mode)
        {
            if (pScalingMode == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pScalingMode));

            var vtbl = *(ADLXVTables.IADLXDisplayScalingModeVtbl**)pScalingMode;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.SetScaleModeFn>(vtbl->SetMode);
            var result = setFn(pScalingMode, mode);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set scaling mode");
            }
        }

        public static unsafe IntPtr GetFreeSyncColorAccuracyHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetFreeSyncColorAccuracyFn>(vtbl->GetFreeSyncColorAccuracy);

            IntPtr pFSCA;
            var result = getFn(pDisplayServices, pDisplay, &pFSCA);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get FreeSync Color Accuracy interface");
            }

            return pFSCA;
        }

        public static unsafe (bool supported, bool enabled) GetFreeSyncColorAccuracyState(IntPtr pFSCA)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncColorAccuracyVtbl**)pFSCA;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pFSCA, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query FreeSync Color Accuracy support");

            var r2 = enFn(pFSCA, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query FreeSync Color Accuracy enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetFreeSyncColorAccuracyEnabled(IntPtr pFSCA, bool enable)
        {
            if (pFSCA == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pFSCA));

            var vtbl = *(ADLXVTables.IADLXDisplayFreeSyncColorAccuracyVtbl**)pFSCA;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetEnabledFn>(vtbl->SetEnabled);
            var result = setFn(pFSCA, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set FreeSync Color Accuracy state");
            }
        }

        public static unsafe IntPtr GetDynamicRefreshRateControlHandle(IntPtr pDisplayServices, IntPtr pDisplay)
        {
            if (pDisplayServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDynamicRefreshRateControlFn>(vtbl->GetDynamicRefreshRateControl);

            IntPtr pDRR;
            var result = getFn(pDisplayServices, pDisplay, &pDRR);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get Dynamic Refresh Rate Control interface");
            }

            return pDRR;
        }

        public static unsafe (bool supported, bool enabled) GetDynamicRefreshRateControlState(IntPtr pDRR)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var vtbl = *(ADLXVTables.IADLXDisplayDynamicRefreshRateControlVtbl**)pDRR;
            var supFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSupportedFn>(vtbl->IsSupported);
            var enFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolEnabledFn>(vtbl->IsEnabled);

            byte supported;
            byte enabled = 0;
            var r1 = supFn(pDRR, &supported);
            if (r1 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r1, "Failed to query DRR support");

            var r2 = enFn(pDRR, &enabled);
            if (r2 != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(r2, "Failed to query DRR enabled");

            return (supported != 0, enabled != 0);
        }

        public static unsafe void SetDynamicRefreshRateControlEnabled(IntPtr pDRR, bool enable)
        {
            if (pDRR == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDRR));

            var vtbl = *(ADLXVTables.IADLXDisplayDynamicRefreshRateControlVtbl**)pDRR;
            var setFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.BoolSetFn>(vtbl->SetEnabled);
            var result = setFn(pDRR, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to set DRR state");
            }
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDisplaySettingsChangedListener backed by a managed delegate. 
    /// </summary>
    public sealed unsafe class DisplaySettingsListenerHandle : SafeHandle
    {
        public delegate bool DisplaySettingsChangedCallback(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, DisplaySettingsChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplaySettingsChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplaySettingsListenerHandle(DisplaySettingsChangedCallback cb)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplaySettingsListenerHandle Create(DisplaySettingsChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplaySettingsListenerHandle(cb);
        }

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplaySettingsChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}