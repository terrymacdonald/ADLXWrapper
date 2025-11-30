using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLX3DSettingsChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _On3DSettingsChanged(IADLX3DSettingsChangedListener* pThis, [NativeTypeName("adlx::IADLX3DSettingsChangedEvent *")] IADLX3DSettingsChangedEvent* p3DSettingsChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool On3DSettingsChanged([NativeTypeName("adlx::IADLX3DSettingsChangedEvent *")] IADLX3DSettingsChangedEvent* p3DSettingsChangedEvent)
    {
        fixed (IADLX3DSettingsChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_On3DSettingsChanged>((IntPtr)(lpVtbl[0]))(pThis, p3DSettingsChangedEvent) != 0;
        }
    }
}
