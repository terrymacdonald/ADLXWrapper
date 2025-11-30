using System.Runtime.InteropServices;

namespace ADLXWrapper;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate ADLX_RESULT ADLXTerminate_Fn();
