using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ADLX_RESULT ADLXQueryVersion_Fn([NativeTypeName("const char **")] sbyte** version);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate ADLX_RESULT ADLXQueryVersion_Fn([NativeTypeName("const char **")] sbyte** version);
}
