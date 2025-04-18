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

public class IADLXList : IADLXInterface {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal IADLXList(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ADLXPINVOKE.IADLXList_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(IADLXList obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  internal static global::System.Runtime.InteropServices.HandleRef swigRelease(IADLXList obj) {
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
          ADLXPINVOKE.delete_IADLXList(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public new static SWIGTYPE_p_wchar_t IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXList_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public static SWIGTYPE_p_wchar_t ITEM_IID() {
    global::System.IntPtr cPtr = ADLXPINVOKE.IADLXList_ITEM_IID();
    SWIGTYPE_p_wchar_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_wchar_t(cPtr, false);
    return ret;
  }

  public virtual uint Size() {
    uint ret = ADLXPINVOKE.IADLXList_Size(swigCPtr);
    return ret;
  }

  public virtual bool Empty() {
    bool ret = ADLXPINVOKE.IADLXList_Empty(swigCPtr);
    return ret;
  }

  public virtual uint Begin() {
    uint ret = ADLXPINVOKE.IADLXList_Begin(swigCPtr);
    return ret;
  }

  public virtual uint End() {
    uint ret = ADLXPINVOKE.IADLXList_End(swigCPtr);
    return ret;
  }

  public virtual ADLX_RESULT At(uint location, SWIGTYPE_p_p_adlx__IADLXInterface ppItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXList_At(swigCPtr, location, SWIGTYPE_p_p_adlx__IADLXInterface.getCPtr(ppItem));
    return ret;
  }

  public virtual ADLX_RESULT Clear() {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXList_Clear(swigCPtr);
    return ret;
  }

  public virtual ADLX_RESULT Remove_Back() {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXList_Remove_Back(swigCPtr);
    return ret;
  }

  public virtual ADLX_RESULT Add_Back(IADLXInterface pItem) {
    ADLX_RESULT ret = (ADLX_RESULT)ADLXPINVOKE.IADLXList_Add_Back(swigCPtr, IADLXInterface.getCPtr(pItem));
    return ret;
  }

}

}
