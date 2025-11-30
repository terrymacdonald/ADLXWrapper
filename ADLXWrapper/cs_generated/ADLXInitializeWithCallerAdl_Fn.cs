using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate ADLX_RESULT ADLXInitializeWithCallerAdl_Fn([NativeTypeName("adlx_uint64")] ulong version, [NativeTypeName("adlx::IADLXSystem **")] IADLXSystem** ppSystem, [NativeTypeName("adlx::IADLMapping **")] IADLMapping** ppAdlMapping, [NativeTypeName("adlx_handle")] void* adlContext, [NativeTypeName("ADLX_ADL_Main_Memory_Free")] IntPtr adlMainMemoryFree);
