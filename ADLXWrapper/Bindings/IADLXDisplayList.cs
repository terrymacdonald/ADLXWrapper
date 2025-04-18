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

public class IADLXDisplayList : IADLXList {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXDisplayList(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXDisplayList_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXDisplayList obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXDisplayList obj) {
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
          ADLXPINVOKE.delete_IADLXDisplayList(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayList_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public new static SWIGTYPE_p_wchar_t ITEM_IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXDisplayList_ITEM_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual ADLX_RESULT At(uint location, SWIGTYPE_p_p_adlx__IADLXDisplay ppItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayList_At(swigCPtr, location, SWIGTYPE_p_p_adlx__IADLXDisplay.getCPtr(ppItem));
    return ret;
  }

  public virtual ADLX_RESULT Add_Back(IADLXDisplay pItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXDisplayList_Add_Back(swigCPtr, IADLXDisplay.getCPtr(pItem));
    return ret;
  }

}

}
