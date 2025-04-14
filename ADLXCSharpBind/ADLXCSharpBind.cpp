// ADLXCSharpBind.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "ADLXCSharpBind.h"


// This is an example of an exported variable
ADLXCSHARPBIND_API int nADLXCSharpBind=0;

// This is an example of an exported function.
ADLXCSHARPBIND_API int fnADLXCSharpBind(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CADLXCSharpBind::CADLXCSharpBind()
{
    return;
}
