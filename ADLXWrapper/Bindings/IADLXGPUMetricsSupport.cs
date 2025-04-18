//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.3.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace ADLXWrapper.Bindings {

public class IADLXGPUMetricsSupport : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXGPUMetricsSupport(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXGPUMetricsSupport_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXGPUMetricsSupport obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXGPUMetricsSupport obj) {
    if (obj != null) {
      if (!obj.swigCMemOwn)
        throw new global::System.ApplicationException("Cannot release ownership as memory is not owned");
      global::System.Runtime.InteropServices.HandleRef ptr = obj.swigCPtr;
      obj.swigCMemOwn = false;
      obj.Dispose();
      return ptr;
    } else {
      return new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
    }
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ADLXPINVOKE.delete_IADLXGPUMetricsSupport(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXGPUMetricsSupport_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUUsage(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUUsage(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUClockSpeed(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUClockSpeed(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUVRAMClockSpeed(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUVRAMClockSpeed(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUTemperature(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUTemperature(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUHotspotTemperature(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUHotspotTemperature(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUPower(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUPower(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUTotalBoardPower(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUTotalBoardPower(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUFanSpeed(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUFanSpeed(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUVRAM(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUVRAM(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUVoltage(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUVoltage(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUUsageRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUUsageRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUClockSpeedRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUClockSpeedRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUVRAMClockSpeedRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUVRAMClockSpeedRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUTemperatureRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUTemperatureRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUHotspotTemperatureRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUHotspotTemperatureRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUPowerRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUPowerRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUFanSpeedRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUFanSpeedRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUVRAMRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUVRAMRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUVoltageRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUVoltageRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUTotalBoardPowerRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUTotalBoardPowerRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT GetGPUIntakeTemperatureRange(SWIGTYPE_p_int minValue, SWIGTYPE_p_int maxValue) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_GetGPUIntakeTemperatureRange(swigCPtr, SWIGTYPE_p_int.getCPtr(minValue), SWIGTYPE_p_int.getCPtr(maxValue));
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedGPUIntakeTemperature(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetricsSupport_IsSupportedGPUIntakeTemperature(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

}

}
