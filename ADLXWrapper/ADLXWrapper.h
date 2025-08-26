// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the ADLXWrapper_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// ADLXWrapper_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ADLXWrapper_EXPORTS
#define ADLXWrapper_API __declspec(dllexport)
#else
#define ADLXWrapper_API __declspec(dllimport)
#endif

// This class is exported from the dll
class ADLXWrapper_API CADLXWrapper {
public:
	CADLXWrapper(void);
	// TODO: add your methods here.
};

extern ADLXWrapper_API int nADLXWrapper;

ADLXWrapper_API int fnADLXWrapper(void);
