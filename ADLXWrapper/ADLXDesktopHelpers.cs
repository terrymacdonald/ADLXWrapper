using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for desktop and Eyefinity services
    /// </summary>
    public static unsafe class ADLXDesktopHelpers
    {
        /// <summary>
        /// Gets the IADLXDesktopServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXDesktopServices* GetDesktopServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLXDesktopServices* pDesktopServices;
            var result = pSystem->GetDesktopsServices(&pDesktopServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop services");
            }

            return pDesktopServices;
        }

        /// <summary>
        /// Enumerates all available desktops.
        /// </summary>
        public static IEnumerable<DesktopInfo> EnumerateAllDesktops(IADLXSystem* pSystem)
        {
            if (pSystem == null) yield break;

            using var desktopServices = new ComPtr<IADLXDesktopServices>(GetDesktopServices(pSystem));
            if (desktopServices.Get() == null) yield break;

            desktopServices.Get()->GetDesktops(out var pDesktopList);
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);

            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                desktopList.Get()->At(i, out var pDesktop);
                using var desktop = new ComPtr<IADLXDesktop>(pDesktop);
                yield return new DesktopInfo(desktop.Get());
            }
        }

        private static IADLXDisplay* GetEyefinityDisplayInternal(IADLXEyefinityDesktop* pEyefinityDesktop, uint row, uint col)
        {
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            IADLXDisplay* pDisplay;
            var result = pEyefinityDesktop->GetDisplay(row, col, &pDisplay);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, $"Failed to get Eyefinity display at {row},{col}");
            return pDisplay;
        }

        /// <summary>
        /// Gets the Simple Eyefinity information.
        /// </summary>
        public static SimpleEyefinityInfo GetSimpleEyefinity(IADLXDesktopServices* pDesktopServices)
        {
            if (pDesktopServices == null) throw new ArgumentNullException(nameof(pDesktopServices));

            IADLXSimpleEyefinity* pSimple;
            var result = pDesktopServices->GetSimpleEyefinity(&pSimple);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");
            }

            using var simpleEyefinity = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return new SimpleEyefinityInfo(simpleEyefinity.Get());
        }

        /// <summary>
        /// Creates an Eyefinity desktop.
        /// </summary>
        public static EyefinityDesktopInfo CreateEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            IADLXEyefinityDesktop* pDesktop;
            var result = pSimpleEyefinity->Create(&pDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to create Eyefinity desktop");
            }

            using var eyefinityDesktop = new ComPtr<IADLXEyefinityDesktop>(pDesktop);
            return new EyefinityDesktopInfo(eyefinityDesktop.Get());
        }

        public static void DestroyEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity, IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var result = pSimpleEyefinity->Destroy(pEyefinityDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy Eyefinity desktop");
            }
        }

        public static (uint rows, uint cols) GetEyefinityGridSize(IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            uint rows = 0, cols = 0;
            var result = pEyefinityDesktop->GridSize(&rows, &cols);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query Eyefinity grid size");
            return (rows, cols);
        }

        /// <summary>
        /// Enumerates displays within an Eyefinity desktop.
        /// </summary>
        public static IEnumerable<DisplayInfo> EnumerateEyefinityDisplays(IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pEyefinityDesktop == null) yield break;

            var (rows, cols) = GetEyefinityGridSize(pEyefinityDesktop);
            for (uint r = 0; r < rows; r++)
            {
                for (uint c = 0; c < cols; c++)
                {
                    var pDisplay = GetEyefinityDisplayInternal(pEyefinityDesktop, r, c);
                    using var display = new ComPtr<IADLXDisplay>(pDisplay);
                    yield return new DisplayInfo(display.Get());
                }
            }
        }

        public static void DestroyAllEyefinityDesktops(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var result = pSimpleEyefinity->DestroyAll();
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy all Eyefinity desktops");
            }
        }
    }

    /// <summary>
    /// Represents the collected information for a desktop.
    /// </summary>
    public readonly struct DesktopInfo
    {
        public string Name { get; init; }
        public ADLX_DESKTOP_TYPE Type { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int TopLeftX { get; init; }
        public int TopLeftY { get; init; }
        public ADLX_ORIENTATION Orientation { get; init; }

        [JsonConstructor]
        public DesktopInfo(string name, ADLX_DESKTOP_TYPE type, int width, int height, int topLeftX, int topLeftY, ADLX_ORIENTATION orientation)
        {
            Name = name;
            Type = type;
            Width = width;
            Height = height;
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Orientation = orientation;
        }

        internal unsafe DesktopInfo(IADLXDesktop* pDesktop)
        {
            pDesktop->Name(out var namePtr);
            Name = ADLXHelpers.MarshalString(namePtr);

            ADLX_DESKTOP_TYPE type = default;
            pDesktop->Type(&type);
            Type = type;

            int w = 0, h = 0;
            pDesktop->Size(&w, &h);
            Width = w;
            Height = h;

            int x = 0, y = 0;
            pDesktop->TopLeft(&x, &y);
            TopLeftX = x;
            TopLeftY = y;

            ADLX_ORIENTATION orientation = default;
            pDesktop->Orientation(&orientation);
            Orientation = orientation;
        }
    }
}