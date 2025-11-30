using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplay
{
}

[NativeTypeName("struct IADLXDisplay : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplay
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplay* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplay* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplay* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ManufacturerID(IADLXDisplay* pThis, [NativeTypeName("adlx_uint *")] uint* manufacturerID);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DisplayType(IADLXDisplay* pThis, ADLX_DISPLAY_TYPE* displayType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ConnectorType(IADLXDisplay* pThis, ADLX_DISPLAY_CONNECTOR_TYPE* connectType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Name(IADLXDisplay* pThis, [NativeTypeName("const char **")] sbyte** displayName);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _EDID(IADLXDisplay* pThis, [NativeTypeName("const char **")] sbyte** edid);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _NativeResolution(IADLXDisplay* pThis, [NativeTypeName("adlx_int *")] int* maxHResolution, [NativeTypeName("adlx_int *")] int* maxVResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RefreshRate(IADLXDisplay* pThis, [NativeTypeName("adlx_double *")] double* refreshRate);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PixelClock(IADLXDisplay* pThis, [NativeTypeName("adlx_uint *")] uint* pixelClock);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ScanType(IADLXDisplay* pThis, ADLX_DISPLAY_SCAN_TYPE* scanType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLXDisplay* pThis, IADLXGPU** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _UniqueId(IADLXDisplay* pThis, [NativeTypeName("adlx_size *")] UIntPtr* uniqueId);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public readonly ADLX_RESULT ManufacturerID([NativeTypeName("adlx_uint *")] uint* manufacturerID)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ManufacturerID>((IntPtr)(lpVtbl[3]))(pThis, manufacturerID);
        }
    }

    public readonly ADLX_RESULT DisplayType(ADLX_DISPLAY_TYPE* displayType)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DisplayType>((IntPtr)(lpVtbl[4]))(pThis, displayType);
        }
    }

    public readonly ADLX_RESULT ConnectorType(ADLX_DISPLAY_CONNECTOR_TYPE* connectType)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ConnectorType>((IntPtr)(lpVtbl[5]))(pThis, connectType);
        }
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** displayName)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Name>((IntPtr)(lpVtbl[6]))(pThis, displayName);
        }
    }

    public readonly ADLX_RESULT EDID([NativeTypeName("const char **")] sbyte** edid)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_EDID>((IntPtr)(lpVtbl[7]))(pThis, edid);
        }
    }

    public readonly ADLX_RESULT NativeResolution([NativeTypeName("adlx_int *")] int* maxHResolution, [NativeTypeName("adlx_int *")] int* maxVResolution)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_NativeResolution>((IntPtr)(lpVtbl[8]))(pThis, maxHResolution, maxVResolution);
        }
    }

    public readonly ADLX_RESULT RefreshRate([NativeTypeName("adlx_double *")] double* refreshRate)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RefreshRate>((IntPtr)(lpVtbl[9]))(pThis, refreshRate);
        }
    }

    public readonly ADLX_RESULT PixelClock([NativeTypeName("adlx_uint *")] uint* pixelClock)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PixelClock>((IntPtr)(lpVtbl[10]))(pThis, pixelClock);
        }
    }

    public readonly ADLX_RESULT ScanType(ADLX_DISPLAY_SCAN_TYPE* scanType)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ScanType>((IntPtr)(lpVtbl[11]))(pThis, scanType);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[12]))(pThis, ppGPU);
        }
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_size *")] UIntPtr* uniqueId)
    {
        fixed (IADLXDisplay* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_UniqueId>((IntPtr)(lpVtbl[13]))(pThis, uniqueId);
        }
    }
}

public partial struct IADLXDisplay
{
}
