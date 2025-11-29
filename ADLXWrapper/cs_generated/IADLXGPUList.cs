using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [NativeTypeName("struct IADLXGPUList : adlx::IADLXList")]
    public unsafe partial struct IADLXGPUList
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Acquire(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Release(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _QueryInterface(IADLXGPUList* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_uint")]
        public delegate uint _Size(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_bool")]
        public delegate byte _Empty(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_uint")]
        public delegate uint _Begin(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_uint")]
        public delegate uint _End(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _At(IADLXGPUList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Clear(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Remove_Back(IADLXGPUList* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Add_Back(IADLXGPUList* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _At1(IADLXGPUList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXGPU** ppItem);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Add_Back1(IADLXGPUList* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pItem);

        [return: NativeTypeName("const wchar_t *")]
        public static ushort* IID()
        {
            return "IADLXInterface";
        }

        [return: NativeTypeName("const wchar_t *")]
        public static ushort* ITEM_IID()
        {
            return "IADLXInterface";
        }

        [return: NativeTypeName("adlx_long")]
        public int Acquire()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_long")]
        public int Release()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
            }
        }

        public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
            }
        }

        [return: NativeTypeName("adlx_uint")]
        public uint Size()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_bool")]
        public bool Empty()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
            }
        }

        [return: NativeTypeName("adlx_uint")]
        public uint Begin()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_uint")]
        public uint End()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
            }
        }

        public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
            }
        }

        public ADLX_RESULT Clear()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
            }
        }

        public ADLX_RESULT Remove_Back()
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
            }
        }

        public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
            }
        }

        public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPU** ppItem)
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
            }
        }

        public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pItem)
        {
            fixed (IADLXGPUList* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
            }
        }
    }
}
