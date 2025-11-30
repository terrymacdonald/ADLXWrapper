using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXEyefinityDesktop : adlx::IADLXInterface")]
public unsafe partial struct IADLXEyefinityDesktop
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXEyefinityDesktop* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXEyefinityDesktop* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXEyefinityDesktop* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GridSize(IADLXEyefinityDesktop* pThis, [NativeTypeName("adlx_uint *")] uint* rows, [NativeTypeName("adlx_uint *")] uint* cols);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplay(IADLXEyefinityDesktop* pThis, [NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, IADLXDisplay** ppDisplay);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DisplayOrientation(IADLXEyefinityDesktop* pThis, [NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_ORIENTATION* displayOrientation);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DisplaySize(IADLXEyefinityDesktop* pThis, [NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, [NativeTypeName("adlx_int *")] int* displayWidth, [NativeTypeName("adlx_int *")] int* displayHeight);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DisplayTopLeft(IADLXEyefinityDesktop* pThis, [NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_Point* displayLocationTopLeft);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GridSize([NativeTypeName("adlx_uint *")] uint* rows, [NativeTypeName("adlx_uint *")] uint* cols)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GridSize>((IntPtr)(lpVtbl[3]))(pThis, rows, cols);
        }
    }

    public ADLX_RESULT GetDisplay([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, IADLXDisplay** ppDisplay)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplay>((IntPtr)(lpVtbl[4]))(pThis, row, col, ppDisplay);
        }
    }

    public ADLX_RESULT DisplayOrientation([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_ORIENTATION* displayOrientation)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DisplayOrientation>((IntPtr)(lpVtbl[5]))(pThis, row, col, displayOrientation);
        }
    }

    public ADLX_RESULT DisplaySize([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, [NativeTypeName("adlx_int *")] int* displayWidth, [NativeTypeName("adlx_int *")] int* displayHeight)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DisplaySize>((IntPtr)(lpVtbl[6]))(pThis, row, col, displayWidth, displayHeight);
        }
    }

    public ADLX_RESULT DisplayTopLeft([NativeTypeName("adlx_uint")] uint row, [NativeTypeName("adlx_uint")] uint col, ADLX_Point* displayLocationTopLeft)
    {
        fixed (IADLXEyefinityDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DisplayTopLeft>((IntPtr)(lpVtbl[7]))(pThis, row, col, displayLocationTopLeft);
        }
    }
}
