//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace ADLXWrapper.Bindings {

public class IADLXManualVRAMTuning1 : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXManualVRAMTuning1(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXManualVRAMTuning1_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXManualVRAMTuning1 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXManualVRAMTuning1 obj) {
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
          ADLXPINVOKE.delete_IADLXManualVRAMTuning1(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXManualVRAMTuning1_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT IsSupportedMemoryTiming(SWIGTYPE_p_bool supported) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_IsSupportedMemoryTiming(swigCPtr, SWIGTYPE_p_bool.getCPtr(supported));
    return ret;
  }

  public virtual ADLX_RESULT GetSupportedMemoryTimingDescriptionList(SWIGTYPE_p_p_adlx__IADLXMemoryTimingDescriptionList ppDescriptionList) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_GetSupportedMemoryTimingDescriptionList(swigCPtr, SWIGTYPE_p_p_adlx__IADLXMemoryTimingDescriptionList.getCPtr(ppDescriptionList));
    return ret;
  }

  public virtual ADLX_RESULT GetMemoryTimingDescription(SWIGTYPE_p_ADLX_MEMORYTIMING_DESCRIPTION description) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_GetMemoryTimingDescription(swigCPtr, SWIGTYPE_p_ADLX_MEMORYTIMING_DESCRIPTION.getCPtr(description));
    return ret;
  }

  public virtual ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_SetMemoryTimingDescription(swigCPtr, (int)description);
    return ret;
  }

  public virtual ADLX_RESULT GetVRAMTuningRanges(ADLX_IntRange frequencyRange, ADLX_IntRange voltageRange) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_GetVRAMTuningRanges(swigCPtr, ADLX_IntRange.getCPtr(frequencyRange), ADLX_IntRange.getCPtr(voltageRange));
    return ret;
  }

  public virtual ADLX_RESULT GetVRAMTuningStates(SWIGTYPE_p_p_adlx__IADLXManualTuningStateList ppVRAMStates) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_GetVRAMTuningStates(swigCPtr, SWIGTYPE_p_p_adlx__IADLXManualTuningStateList.getCPtr(ppVRAMStates));
    return ret;
  }

  public virtual ADLX_RESULT GetEmptyVRAMTuningStates(SWIGTYPE_p_p_adlx__IADLXManualTuningStateList ppVRAMStates) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_GetEmptyVRAMTuningStates(swigCPtr, SWIGTYPE_p_p_adlx__IADLXManualTuningStateList.getCPtr(ppVRAMStates));
    return ret;
  }

  public virtual ADLX_RESULT IsValidVRAMTuningStates(IADLXManualTuningStateList pVRAMStates, SWIGTYPE_p_int errorIndex) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_IsValidVRAMTuningStates(swigCPtr, IADLXManualTuningStateList.getCPtr(pVRAMStates), SWIGTYPE_p_int.getCPtr(errorIndex));
    return ret;
  }

  public virtual ADLX_RESULT SetVRAMTuningStates(IADLXManualTuningStateList pVRAMStates) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXManualVRAMTuning1_SetVRAMTuningStates(swigCPtr, IADLXManualTuningStateList.getCPtr(pVRAMStates));
    return ret;
  }

}

}
