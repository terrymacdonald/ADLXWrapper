using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXI2C : adlx::IADLXInterface")]
public unsafe partial struct IADLXI2C
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXI2C* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXI2C* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXI2C* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Version(IADLXI2C* pThis, [NativeTypeName("adlx_int *")] int* major, [NativeTypeName("adlx_int *")] int* minor);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXI2C* pThis, ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_bool *")] bool* isSupported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Read(IADLXI2C* pThis, ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RepeatedStartRead(IADLXI2C* pThis, ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Write(IADLXI2C* pThis, ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT Version([NativeTypeName("adlx_int *")] int* major, [NativeTypeName("adlx_int *")] int* minor)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Version>((IntPtr)(lpVtbl[3]))(pThis, major, minor);
        }
    }

    public ADLX_RESULT IsSupported(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_bool *")] bool* isSupported)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[4]))(pThis, line, address, isSupported);
        }
    }

    public ADLX_RESULT Read(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Read>((IntPtr)(lpVtbl[5]))(pThis, line, speed, address, offset, dataSize, data);
        }
    }

    public ADLX_RESULT RepeatedStartRead(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RepeatedStartRead>((IntPtr)(lpVtbl[6]))(pThis, line, speed, address, offset, dataSize, data);
        }
    }

    public ADLX_RESULT Write(ADLX_I2C_LINE line, [NativeTypeName("adlx_int")] int speed, [NativeTypeName("adlx_int")] int address, [NativeTypeName("adlx_int")] int offset, [NativeTypeName("adlx_int")] int dataSize, [NativeTypeName("adlx_byte *")] byte* data)
    {
        fixed (IADLXI2C* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Write>((IntPtr)(lpVtbl[7]))(pThis, line, speed, address, offset, dataSize, data);
        }
    }
}

public partial struct IADLXI2C
{
}
