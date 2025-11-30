using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public static unsafe partial class ADLX
{
    [NativeTypeName("#define ADLX_DLL_NAMEW L\"amdadlx64.dll\"")]
    public const string ADLX_DLL_NAMEW = "amdadlx64.dll";

    [NativeTypeName("#define ADLX_DLL_NAMEA \"amdadlx64.dll\"")]
    public static ReadOnlySpan<byte> ADLX_DLL_NAMEA => new byte[] { 0x61, 0x6D, 0x64, 0x61, 0x64, 0x6C, 0x78, 0x36, 0x34, 0x2E, 0x64, 0x6C, 0x6C, 0x00 };

    [NativeTypeName("#define ADLX_DLL_NAME ADLX_DLL_NAMEA")]
    public static ReadOnlySpan<byte> ADLX_DLL_NAME => new byte[] { 0x61, 0x6D, 0x64, 0x61, 0x64, 0x6C, 0x78, 0x36, 0x34, 0x2E, 0x64, 0x6C, 0x6C, 0x00 };

    [NativeTypeName("#define ADLX_QUERY_FULL_VERSION_FUNCTION_NAME \"ADLXQueryFullVersion\"")]
    public static ReadOnlySpan<byte> ADLX_QUERY_FULL_VERSION_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x51, 0x75, 0x65, 0x72, 0x79, 0x46, 0x75, 0x6C, 0x6C, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x00 };

    [NativeTypeName("#define ADLX_QUERY_VERSION_FUNCTION_NAME \"ADLXQueryVersion\"")]
    public static ReadOnlySpan<byte> ADLX_QUERY_VERSION_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x51, 0x75, 0x65, 0x72, 0x79, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x00 };

    [NativeTypeName("#define ADLX_INIT_FUNCTION_NAME \"ADLXInitialize\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x49, 0x6E, 0x69, 0x74, 0x69, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x00 };

    [NativeTypeName("#define ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME \"ADLXInitializeWithIncompatibleDriver\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_WITH_INCOMPATIBLE_DRIVER_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x49, 0x6E, 0x69, 0x74, 0x69, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x57, 0x69, 0x74, 0x68, 0x49, 0x6E, 0x63, 0x6F, 0x6D, 0x70, 0x61, 0x74, 0x69, 0x62, 0x6C, 0x65, 0x44, 0x72, 0x69, 0x76, 0x65, 0x72, 0x00 };

    [NativeTypeName("#define ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME \"ADLXInitializeWithCallerAdl\"")]
    public static ReadOnlySpan<byte> ADLX_INIT_WITH_CALLER_ADL_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x49, 0x6E, 0x69, 0x74, 0x69, 0x61, 0x6C, 0x69, 0x7A, 0x65, 0x57, 0x69, 0x74, 0x68, 0x43, 0x61, 0x6C, 0x6C, 0x65, 0x72, 0x41, 0x64, 0x6C, 0x00 };

    [NativeTypeName("#define ADLX_TERMINATE_FUNCTION_NAME \"ADLXTerminate\"")]
    public static ReadOnlySpan<byte> ADLX_TERMINATE_FUNCTION_NAME => new byte[] { 0x41, 0x44, 0x4C, 0x58, 0x54, 0x65, 0x72, 0x6D, 0x69, 0x6E, 0x61, 0x74, 0x65, 0x00 };

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_atomic_inc@@YAJPEAJ@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_long")]
    public static extern int adlx_atomic_inc([NativeTypeName("adlx_long *")] int* x);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_atomic_dec@@YAJPEAJ@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_long")]
    public static extern int adlx_atomic_dec([NativeTypeName("adlx_long *")] int* x);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_load_library@@YAPEAXPEBD@Z", ExactSpelling = true)]
    [return: NativeTypeName("adlx_handle")]
    public static extern void* adlx_load_library([NativeTypeName("const TCHAR *")] sbyte* filename);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_free_library@@YAHPEAX@Z", ExactSpelling = true)]
    public static extern int adlx_free_library([NativeTypeName("adlx_handle")] void* module);

    [DllImport("amdadlx64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?adlx_get_proc_address@@YAPEAXPEAXPEBD@Z", ExactSpelling = true)]
    public static extern void* adlx_get_proc_address([NativeTypeName("adlx_handle")] void* module, [NativeTypeName("const char *")] sbyte* procName);

    [NativeTypeName("#define ADLX_FULL_VERSION ADLX_MAKE_FULL_VER(ADLX_VER_MAJOR, ADLX_VER_MINOR, ADLX_VER_RELEASE, ADLX_VER_BUILD_NUM)")]
    public const ulong ADLX_FULL_VERSION = (((ulong)(1) << 48) | ((ulong)(4) << 32) | ((ulong)(0) << 16) | (ulong)(110));

    [NativeTypeName("#define ADLX_VERSION_STR ADLX_VER_MAJOR")]
    public const int ADLX_VERSION_STR = 1;

    [NativeTypeName("#define MAX_USER_3DLUT_NUM_POINTS 17")]
    public const int MAX_USER_3DLUT_NUM_POINTS = 17;

    [NativeTypeName("#define ADLX_VER_MAJOR 1")]
    public const int ADLX_VER_MAJOR = 1;

    [NativeTypeName("#define ADLX_VER_MINOR 4")]
    public const int ADLX_VER_MINOR = 4;

    [NativeTypeName("#define ADLX_VER_RELEASE 0")]
    public const int ADLX_VER_RELEASE = 0;

    [NativeTypeName("#define ADLX_VER_BUILD_NUM 110")]
    public const int ADLX_VER_BUILD_NUM = 110;
}
