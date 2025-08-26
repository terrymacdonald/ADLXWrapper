// ADLXWrapper.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "ADLXWrapper.h"


// This is an example of an exported variable
ADLXWrapper_API int nADLXWrapper=0;

// This is an example of an exported function.
ADLXWrapper_API int fnADLXWrapper(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CADLXWrapper::CADLXWrapper()
{
    return;
}
