using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Pointer-safe wrapper for ADLX Variable Graphics Memory options.
    /// </summary>
    public sealed unsafe class ADLXVariableGraphicsMemoryHelper : IDisposable
    {
        private ComPtr<IADLXVariableGraphicsMemory> _variableGraphicsMemory;
        private bool _disposed;

        internal ADLXVariableGraphicsMemoryHelper(IADLXVariableGraphicsMemory* variableGraphicsMemory)
        {
            _variableGraphicsMemory = new ComPtr<IADLXVariableGraphicsMemory>(variableGraphicsMemory);
        }

        public bool IsSupported()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            bool supported = false;
            var result = _variableGraphicsMemory.Get()->IsSupported(&supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return false;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query Variable Graphics Memory support");
            return supported;
        }

        public VariableGraphicsMemoryOptionDto GetDefaultOption() => GetOption(defaultOption: true);

        public VariableGraphicsMemoryOptionDto GetOption() => GetOption(defaultOption: false);

        public IReadOnlyList<VariableGraphicsMemoryOptionDto> GetAvailableOptions()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXVariableGraphicsMemoryOptionList* options = null;
            var result = _variableGraphicsMemory.Get()->GetAvailableOptions(&options);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || options == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Variable Graphics Memory options are not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Variable Graphics Memory options");

            using var optionsPtr = new ComPtr<IADLXVariableGraphicsMemoryOptionList>(options);
            var resultOptions = new List<VariableGraphicsMemoryOptionDto>((int)optionsPtr.Get()->Size());
            for (uint index = optionsPtr.Get()->Begin(); index < optionsPtr.Get()->End(); index++)
            {
                IADLXVariableGraphicsMemoryOption* option = null;
                var itemResult = optionsPtr.Get()->At(index, &option);
                if (itemResult != ADLX_RESULT.ADLX_OK || option == null)
                    throw new ADLXException(itemResult, "Failed to get a Variable Graphics Memory option");
                using var optionPtr = new ComPtr<IADLXVariableGraphicsMemoryOption>(option);
                resultOptions.Add(CreateOptionDto(optionPtr.Get()));
            }
            return resultOptions;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _variableGraphicsMemory.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private VariableGraphicsMemoryOptionDto GetOption(bool defaultOption)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXVariableGraphicsMemoryOption* option = null;
            var result = defaultOption
                ? _variableGraphicsMemory.Get()->GetDefaultOption(&option)
                : _variableGraphicsMemory.Get()->GetOption(&option);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || option == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Variable Graphics Memory is not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Variable Graphics Memory option");
            using var optionPtr = new ComPtr<IADLXVariableGraphicsMemoryOption>(option);
            return CreateOptionDto(optionPtr.Get());
        }

        private static VariableGraphicsMemoryOptionDto CreateOptionDto(IADLXVariableGraphicsMemoryOption* option)
        {
            sbyte* name = null;
            var nameResult = option->Name(&name);
            if (nameResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(nameResult, "Failed to get Variable Graphics Memory option name");
            ADLX_VARIABLE_GRAPHICS_MEMORY_MODE mode = default;
            var modeResult = option->Mode(&mode);
            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to get Variable Graphics Memory option mode");
            double memoryCarvedGb = 0;
            var carvedResult = option->MemoryCarved(&memoryCarvedGb);
            if (carvedResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(carvedResult, "Failed to get Variable Graphics Memory carved memory");
            double memoryRemainingGb = 0;
            var remainingResult = option->MemoryRemaining(&memoryRemainingGb);
            if (remainingResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(remainingResult, "Failed to get Variable Graphics Memory remaining memory");
            return new VariableGraphicsMemoryOptionDto(ADLXUtils.MarshalString(&name), mode, memoryCarvedGb, memoryRemainingGb);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXVariableGraphicsMemoryHelper));
        }
    }

    public readonly struct VariableGraphicsMemoryOptionDto
    {
        public string Name { get; init; }
        public ADLX_VARIABLE_GRAPHICS_MEMORY_MODE Mode { get; init; }
        public double MemoryCarvedGb { get; init; }
        public double MemoryRemainingGb { get; init; }

        public VariableGraphicsMemoryOptionDto(string name, ADLX_VARIABLE_GRAPHICS_MEMORY_MODE mode, double memoryCarvedGb, double memoryRemainingGb)
        {
            Name = name;
            Mode = mode;
            MemoryCarvedGb = memoryCarvedGb;
            MemoryRemainingGb = memoryRemainingGb;
        }
    }
}