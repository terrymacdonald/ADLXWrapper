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

public class IADLXGPUMetrics1 : IADLXGPUMetrics {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXGPUMetrics1(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXGPUMetrics1_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXGPUMetrics1 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXGPUMetrics1 obj) {
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
          ADLXPINVOKE.delete_IADLXGPUMetrics1(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXGPUMetrics1_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT GPUMemoryTemperature(SWIGTYPE_p_double data) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetrics1_GPUMemoryTemperature(swigCPtr, SWIGTYPE_p_double.getCPtr(data));
    return ret;
  }

  public virtual ADLX_RESULT NPUFrequency(SWIGTYPE_p_int data) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetrics1_NPUFrequency(swigCPtr, SWIGTYPE_p_int.getCPtr(data));
    return ret;
  }

  public virtual ADLX_RESULT NPUActivityLevel(SWIGTYPE_p_int data) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXGPUMetrics1_NPUActivityLevel(swigCPtr, SWIGTYPE_p_int.getCPtr(data));
    return ret;
  }

}

}
