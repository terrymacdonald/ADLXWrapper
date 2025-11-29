// BuildADLX.h
#define WIN32
#define _WIN64

// Since we pass the include path in the config, we can just list filenames.
// ClangSharp will merge all these into one "Context", preventing duplicates.

#include "../ADLX/SDK/Include/ADLX.h"
#include "../ADLX/SDK/Include/ISystem.h"
#include "../ADLX/SDK/Include/IDisplays.h"
#include "../ADLX/SDK/Include/IDisplaySettings.h"
#include "../ADLX/SDK/Include/IGPUTuning.h"
#include "../ADLX/SDK/Include/IPerformanceMonitoring.h"
#include "../ADLX/SDK/Include/I3DSettings.h"
#include "../ADLX/SDK/Include/IDesktops.h"