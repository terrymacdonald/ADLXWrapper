using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void ADLX_ADL_Main_Memory_Free(void** buffer);
}
