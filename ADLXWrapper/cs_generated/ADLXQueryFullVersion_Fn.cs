using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ADLX_RESULT ADLXQueryFullVersion_Fn([NativeTypeName("adlx_uint64 *")] ulong* fullVersion);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ADLX_RESULT ADLXQueryFullVersion_Fn([NativeTypeName("adlx_uint64 *")] ulong* fullVersion);
}
