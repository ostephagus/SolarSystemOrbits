// CUDAWrapper.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "CUDAWrapper.h"


// This is an example of an exported variable
CUDAWRAPPER_API int nCUDAWrapper=0;

// This is an example of an exported function.
CUDAWRAPPER_API int fnCUDAWrapper(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CCUDAWrapper::CCUDAWrapper()
{
    return;
}
