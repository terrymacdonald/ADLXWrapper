using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXApplication.xml' path='doc/member[@name="IADLXApplication"]/*' />
[NativeTypeName("struct IADLXApplication : adlx::IADLXInterface")]
public unsafe partial struct IADLXApplication
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, int>)(lpVtbl[0]))((IADLXApplication*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, int>)(lpVtbl[1]))((IADLXApplication*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXApplication*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXApplication.xml' path='doc/member[@name="IADLXApplication.ProcessID"]/*' />
    public ADLX_RESULT ProcessID([NativeTypeName("adlx_ulong *")] uint* pid)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXApplication*)Unsafe.AsPointer(ref this), pid);
    }

    /// <include file='IADLXApplication.xml' path='doc/member[@name="IADLXApplication.Name"]/*' />
    public ADLX_RESULT Name([NativeTypeName("const wchar_t **")] ushort** ppAppName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, ushort**, ADLX_RESULT>)(lpVtbl[4]))((IADLXApplication*)Unsafe.AsPointer(ref this), ppAppName);
    }

    /// <include file='IADLXApplication.xml' path='doc/member[@name="IADLXApplication.FullPath"]/*' />
    public ADLX_RESULT FullPath([NativeTypeName("const wchar_t **")] ushort** ppAppPath)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, ushort**, ADLX_RESULT>)(lpVtbl[5]))((IADLXApplication*)Unsafe.AsPointer(ref this), ppAppPath);
    }

    /// <include file='IADLXApplication.xml' path='doc/member[@name="IADLXApplication.GPUDependencyType"]/*' />
    public ADLX_RESULT GPUDependencyType(ADLX_APP_GPU_DEPENDENCY* gpuDependency)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXApplication*, ADLX_APP_GPU_DEPENDENCY*, ADLX_RESULT>)(lpVtbl[6]))((IADLXApplication*)Unsafe.AsPointer(ref this), gpuDependency);
    }
}
