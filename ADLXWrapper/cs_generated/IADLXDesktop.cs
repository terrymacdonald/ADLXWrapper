using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktop : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktop
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDesktop* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDesktop* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDesktop* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Orientation(IADLXDesktop* pThis, ADLX_ORIENTATION* orientation);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Size(IADLXDesktop* pThis, [NativeTypeName("adlx_int *")] int* width, [NativeTypeName("adlx_int *")] int* height);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TopLeft(IADLXDesktop* pThis, ADLX_Point* locationTopLeft);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Type(IADLXDesktop* pThis, ADLX_DESKTOP_TYPE* desktopType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNumberOfDisplays(IADLXDesktop* pThis, [NativeTypeName("adlx_uint *")] uint* numDisplays);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplays(IADLXDesktop* pThis, IADLXDisplayList** ppDisplays);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT Orientation(ADLX_ORIENTATION* orientation)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Orientation>((IntPtr)(lpVtbl[3]))(pThis, orientation);
        }
    }

    public ADLX_RESULT Size([NativeTypeName("adlx_int *")] int* width, [NativeTypeName("adlx_int *")] int* height)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[4]))(pThis, width, height);
        }
    }

    public ADLX_RESULT TopLeft(ADLX_Point* locationTopLeft)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TopLeft>((IntPtr)(lpVtbl[5]))(pThis, locationTopLeft);
        }
    }

    public ADLX_RESULT Type(ADLX_DESKTOP_TYPE* desktopType)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Type>((IntPtr)(lpVtbl[6]))(pThis, desktopType);
        }
    }

    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNumberOfDisplays>((IntPtr)(lpVtbl[7]))(pThis, numDisplays);
        }
    }

    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplays)
    {
        fixed (IADLXDesktop* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplays>((IntPtr)(lpVtbl[8]))(pThis, ppDisplays);
        }
    }
}

public partial struct IADLXDesktop
{
}
