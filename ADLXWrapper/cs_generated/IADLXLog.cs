using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXLog
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _WriteLog(IADLXLog* pThis, [NativeTypeName("const wchar_t *")] ushort* msg);

    public ADLX_RESULT WriteLog([NativeTypeName("const wchar_t *")] ushort* msg)
    {
        fixed (IADLXLog* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_WriteLog>((IntPtr)(lpVtbl[0]))(pThis, msg);
        }
    }
}

public partial struct IADLXLog
{
}
