using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct ADLXHelper
{
    public void** lpVtbl;

    [NativeTypeName("adlx_handle")]
    public void* m_hDLLHandle;

    [NativeTypeName("adlx_uint64")]
    public ulong m_ADLXFullVersion;

    [NativeTypeName("const char *")]
    public sbyte* m_ADLXVersion;

    [NativeTypeName("adlx::IADLXSystem *")]
    public IADLXSystem* m_pSystemServices;

    [NativeTypeName("adlx::IADLMapping *")]
    public IADLMapping* m_pAdlMapping;

    [NativeTypeName("ADLXQueryFullVersion_Fn")]
    public delegate* unmanaged[Cdecl]<ulong*, ADLX_RESULT> m_fullVersionFn;

    [NativeTypeName("ADLXQueryVersion_Fn")]
    public delegate* unmanaged[Cdecl]<sbyte**, ADLX_RESULT> m_versionFn;

    [NativeTypeName("ADLXInitializeWithCallerAdl_Fn")]
    public delegate* unmanaged[Cdecl]<ulong, IADLXSystem**, IADLMapping**, void*, delegate* unmanaged[Stdcall]<void**, void>, ADLX_RESULT> m_initWithADLFn;

    [NativeTypeName("ADLXInitialize_Fn")]
    public delegate* unmanaged[Cdecl]<ulong, IADLXSystem**, ADLX_RESULT> m_initFnEx;

    [NativeTypeName("ADLXInitialize_Fn")]
    public delegate* unmanaged[Cdecl]<ulong, IADLXSystem**, ADLX_RESULT> m_initFn;

    [NativeTypeName("ADLXTerminate_Fn")]
    public delegate* unmanaged[Cdecl]<ADLX_RESULT> m_terminateFn;

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "??0ADLXHelper@@QEAA@XZ", ExactSpelling = true)]
    public static extern ADLXHelper(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?InitializeWithCallerAdl@ADLXHelper@@QEAA?AW4ADLX_RESULT@@PEAXP6AXPEAPEAX@Z@Z", ExactSpelling = true)]
    public static extern ADLX_RESULT InitializeWithCallerAdl(ADLXHelper* pThis, [NativeTypeName("adlx_handle")] void* adlContext, [NativeTypeName("ADLX_ADL_Main_Memory_Free")] delegate* unmanaged[Stdcall]<void**, void> adlMainMemoryFree);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?Initialize@ADLXHelper@@QEAA?AW4ADLX_RESULT@@XZ", ExactSpelling = true)]
    public static extern ADLX_RESULT Initialize(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?InitializeWithIncompatibleDriver@ADLXHelper@@QEAA?AW4ADLX_RESULT@@XZ", ExactSpelling = true)]
    public static extern ADLX_RESULT InitializeWithIncompatibleDriver(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?Terminate@ADLXHelper@@QEAA?AW4ADLX_RESULT@@XZ", ExactSpelling = true)]
    public static extern ADLX_RESULT Terminate(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?QueryFullVersion@ADLXHelper@@QEAA_KXZ", ExactSpelling = true)]
    [return: NativeTypeName("adlx_uint64")]
    public static extern ulong QueryFullVersion(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?QueryVersion@ADLXHelper@@QEAAPEBDXZ", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* QueryVersion(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?GetSystemServices@ADLXHelper@@QEAAPEAVIADLXSystem@adlx@@XZ", ExactSpelling = true)]
    [return: NativeTypeName("adlx::IADLXSystem *")]
    public static extern IADLXSystem* GetSystemServices(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?GetAdlMapping@ADLXHelper@@QEAAPEAVIADLMapping@adlx@@XZ", ExactSpelling = true)]
    [return: NativeTypeName("adlx::IADLMapping *")]
    public static extern IADLMapping* GetAdlMapping(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?LoadADLXDll@ADLXHelper@@IEAA?AW4ADLX_RESULT@@XZ", ExactSpelling = true)]
    public static extern ADLX_RESULT LoadADLXDll(ADLXHelper* pThis);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.ThisCall, EntryPoint = "?InitializePrivate@ADLXHelper@@IEAA?AW4ADLX_RESULT@@PEAXP6AXPEAPEAX@Z_N@Z", ExactSpelling = true)]
    public static extern ADLX_RESULT InitializePrivate(ADLXHelper* pThis, [NativeTypeName("adlx_handle")] void* adlContext, [NativeTypeName("ADLX_ADL_Main_Memory_Free")] delegate* unmanaged[Stdcall]<void**, void> adlMainMemoryFree, [NativeTypeName("adlx_bool")] byte useIncompatibleDriver = false);

    public void Dispose()
    {
        ((delegate* unmanaged[Thiscall]<ADLXHelper*, void>)(lpVtbl[0]))((ADLXHelper*)Unsafe.AsPointer(ref this));
    }
}
