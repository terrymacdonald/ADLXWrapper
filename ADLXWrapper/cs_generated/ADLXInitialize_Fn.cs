using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ADLX_RESULT ADLXInitialize_Fn([NativeTypeName("adlx_uint64")] ulong version, [NativeTypeName("adlx::IADLXSystem **")] IADLXSystem** ppSystem);
}
