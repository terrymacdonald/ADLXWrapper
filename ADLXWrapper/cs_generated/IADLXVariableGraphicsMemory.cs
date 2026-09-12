using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory"]/*' />
[NativeTypeName("struct IADLXVariableGraphicsMemory : adlx::IADLXInterface")]
public unsafe partial struct IADLXVariableGraphicsMemory
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, int>)(lpVtbl[0]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, int>)(lpVtbl[1]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory.GetDefaultOption"]/*' />
    public ADLX_RESULT GetDefaultOption(IADLXVariableGraphicsMemoryOption** ppOption)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, IADLXVariableGraphicsMemoryOption**, ADLX_RESULT>)(lpVtbl[4]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), ppOption);
    }

    /// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory.GetOption"]/*' />
    public ADLX_RESULT GetOption(IADLXVariableGraphicsMemoryOption** ppOption)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, IADLXVariableGraphicsMemoryOption**, ADLX_RESULT>)(lpVtbl[5]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), ppOption);
    }

    /// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory.GetAvailableOptions"]/*' />
    public ADLX_RESULT GetAvailableOptions(IADLXVariableGraphicsMemoryOptionList** ppOptions)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, IADLXVariableGraphicsMemoryOptionList**, ADLX_RESULT>)(lpVtbl[6]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), ppOptions);
    }

    /// <include file='IADLXVariableGraphicsMemory.xml' path='doc/member[@name="IADLXVariableGraphicsMemory.SetOption"]/*' />
    public ADLX_RESULT SetOption([NativeTypeName("adlx::IADLXVariableGraphicsMemoryOption *")] IADLXVariableGraphicsMemoryOption* pOption)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVariableGraphicsMemory*, IADLXVariableGraphicsMemoryOption*, ADLX_RESULT>)(lpVtbl[7]))((IADLXVariableGraphicsMemory*)Unsafe.AsPointer(ref this), pOption);
    }
}
