```markdown
# ADLXWrapper

## Overview

This repository provides a wrapper for ADLX (AMD Display Library Extensions), enabling developers to interact with AMD GPU features programmatically. It simplifies the integration of ADLX functionalities into your applications.

## Features

- Access and control AMD GPU settings.
- Simplified API for ADLX integration.
- Customizable and extensible for various use cases.

## Getting Started

### Prerequisites

- AMD GPU with ADLX support.
- Visual Studio 2022 or later installed on your system (we use msbuild).

### Build Instructions

1. Open a terminal and navigate to the root directory of the repository.
2. Run the `prepare_adlx.ps1` script to download teh ADLX SDK and install SWIG:
    ```powershell
    powershell.exe -ExecutionPolicy Bypass -File prepare_adlx.ps1
    ```
    This script will:
    - Install SWIG if not already installed.
    - Download the ADLX SDK.

3. After the script completes, run the `rebuild_adlx.bat` file to compile the DLLs and generate the binding files:
    ```cmd
    rebuild_adlx.bat
    ```

4. Once the build process is complete, the generated files will be available in the output directory.
    - The 64-bit DLL (ADLXWrapper.dll) will be available in ADLXWrapper/x64/Debug
    - Tbe C# Bindings files will be available in ADLXWrapper/cs_bindings/*.cs

### How to Use

1. Copy the ADLXWrapper.dll file into the root folder of your C# project.
2. Right click on the ADLXWrapper.dll file and set it to 'Copy if Newer' so that the file is included when your C# project is built.
3. Copy the ADLXWrapper/cs_bindings folder into your project.
4. Add `ūse ADLXWrapper;` into any source file that you want to use the ADLXWrapper within.

### Test Instructions
IMPORTANT: The Unit Tests will only work if run on a computer with AMD GPU hardware in it.

1. Run the `test_adlx.bat` file to compile ADLXWrapper.Tests project to test ADLXWrapper.dll can be loaded and will work:
    ```cmd
    test_adlx.bat
    ```

```