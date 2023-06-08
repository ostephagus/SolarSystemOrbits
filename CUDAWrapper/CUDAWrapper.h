// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the CUDAWRAPPER_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// CUDAWRAPPER_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CUDAWRAPPER_EXPORTS
#define CUDAWRAPPER_API __declspec(dllexport)
#else
#define CUDAWRAPPER_API __declspec(dllimport)
#endif

// This class is exported from the dll
class CUDAWRAPPER_API CCUDAWrapper {
public:
	CCUDAWrapper(void);
	// TODO: add your methods here.
};

extern CUDAWRAPPER_API int nCUDAWrapper;

CUDAWRAPPER_API int fnCUDAWrapper(void);
