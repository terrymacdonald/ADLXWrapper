using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXVariableGraphicsMemoryOption.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOption"]/*' />
[NativeTypeName("struct IADLXVariableGraphicsMemoryOption : adlx::IADLXInterface")]
public unsafe partial struct IADLXVariableGraphicsMemoryOption
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, int>)(lpVtbl[0]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, int>)(lpVtbl[1]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXVariableGraphicsMemoryOption.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOption.Name"]/*' />
    public ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** optionName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, sbyte**, ADLX_RESULT>)(lpVtbl[3]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this), optionName);
    }

    /// <include file='IADLXVariableGraphicsMemoryOption.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOption.Mode"]/*' />
    public ADLX_RESULT Mode([NativeTypeName("adlx::ADLX_VARIABLE_GRAPHICS_MEMORY_MODE *")] ADLX_VARIABLE_GRAPHICS_MEMORY_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, ADLX_VARIABLE_GRAPHICS_MEMORY_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLXVariableGraphicsMemoryOption.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOption.MemoryCarved"]/*' />
    public ADLX_RESULT MemoryCarved([NativeTypeName("adlx_double *")] double* memoryCarvedGb)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, double*, ADLX_RESULT>)(lpVtbl[5]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this), memoryCarvedGb);
    }

    /// <include file='IADLXVariableGraphicsMemoryOption.xml' path='doc/member[@name="IADLXVariableGraphicsMemoryOption.MemoryRemaining"]/*' />
    public ADLX_RESULT MemoryRemaining([NativeTypeName("adlx_double *")] double* memoryRemainingGb)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemoryOption*, double*, ADLX_RESULT>)(lpVtbl[6]))((IADLXVariableGraphicsMemoryOption*)Unsafe.AsPointer(ref this), memoryRemainingGb);
    }
}
