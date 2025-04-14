// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the ADLXCSHARPBIND_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// ADLXCSHARPBIND_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ADLXCSHARPBIND_EXPORTS
#define ADLXCSHARPBIND_API __declspec(dllexport)
#else
#define ADLXCSHARPBIND_API __declspec(dllimport)
#endif

// This class is exported from the dll
class ADLXCSHARPBIND_API CADLXCSharpBind {
public:
	CADLXCSharpBind(void);
	// TODO: add your methods here.
};

extern ADLXCSHARPBIND_API int nADLXCSharpBind;

ADLXCSHARPBIND_API int fnADLXCSharpBind(void);
