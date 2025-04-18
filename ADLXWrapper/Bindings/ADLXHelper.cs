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

public class ADLXHelper : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ADLXHelper(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ADLXHelper obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(ADLXHelper obj) {
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

  ~ADLXHelper() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ADLXPINVOKE.delete_ADLXHelper(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ADLXHelper() : this(ADLXPINVOKE.new_ADLXHelper(), true) {
  }

  public ADLX_RESULT InitializeWithCallerAdl(SWIGTYPE_p_void adlContext, SWIGTYPE_p_ADLX_ADL_Main_Memory_Free adlMainMemoryFree) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.ADLXHelper_InitializeWithCallerAdl(swigCPtr, SWIGTYPE_p_void.getCPtr(adlContext), SWIGTYPE_p_ADLX_ADL_Main_Memory_Free.getCPtr(adlMainMemoryFree));
    if (ADLXPINVOKE.SWIGPendingException.Pending) throw ADLXPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ADLX_RESULT Initialize() {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.ADLXHelper_Initialize(swigCPtr);
    return ret;
  }

  public ADLX_RESULT InitializeWithIncompatibleDriver() {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.ADLXHelper_InitializeWithIncompatibleDriver(swigCPtr);
    return ret;
  }

  public ADLX_RESULT Terminate() {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.ADLXHelper_Terminate(swigCPtr);
    return ret;
  }

  public ulong QueryFullVersion() {
    ulong ret = ADLXPINVOKE.ADLXHelper_QueryFullVersion(swigCPtr);
    return ret;
  }

  public string QueryVersion() {
    string ret = ADLXPINVOKE.ADLXHelper_QueryVersion(swigCPtr);
    return ret;
  }

  public IADLXSystem GetSystemServices() {
    global::System.IntPtr cPtr = ADLXPINVOKE.ADLXHelper_GetSystemServices(swigCPtr);
    IADLXSystem ret = (cPtr == global::System.IntPtr.Zero) ? null : new IADLXSystem(cPtr, false);
    return ret;
  }

  public IADLMapping GetAdlMapping() {
    global::System.IntPtr cPtr = ADLXPINVOKE.ADLXHelper_GetAdlMapping(swigCPtr);
    IADLMapping ret = (cPtr == global::System.IntPtr.Zero) ? null : new IADLMapping(cPtr, false);
    return ret;
  }

}

}
