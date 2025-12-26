using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXLog.xml' path='doc/member[@name="IADLXLog"]/*' />
public unsafe partial struct IADLXLog
{
    public void** lpVtbl;

    /// <include file='IADLXLog.xml' path='doc/member[@name="IADLXLog.WriteLog"]/*' />
    public ADLX_RESULT WriteLog([NativeTypeName("const wchar_t *")] ushort* msg)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXLog*, ushort*, ADLX_RESULT>)(lpVtbl[0]))((IADLXLog*)Unsafe.AsPointer(ref this), msg);
    }
}
