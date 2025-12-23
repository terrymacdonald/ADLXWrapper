## System + GPU façade alignment plan

Goal: Keep everything routed through `ADLXSystemServicesHelper` while providing façade-first accessors for GPUs, displays, and desktops. GPUs follow the same pattern as displays/desktops: pointer-free for typical callers, with handle/DTO paths retained for advanced use.

Guiding principles
- System-centric: `ADLXSystemServicesHelper` is the entry point for all top-level objects (GPUs, displays, desktops).
- Facade-first: add façade-returning methods for GPUs and desktops that mirror the display pattern: `EnumerateGPUs() -> IReadOnlyList<ADLXGPU>`.
- Keep advanced paths: retain handle/DTO enumerations for native/advanced scenarios (handles and `GpuInfo`/`DesktopInfo`/`DisplayInfo`).
- No new helper type: do not introduce a separate GPU services helper; extend the system helper instead.

Execution steps
1) Add façade enumeration on the system helper
   - Implement `EnumerateGPUs()` returning `IReadOnlyList<ADLXGPU>` (construct facades with available display/desktop services where needed).
   - Keep existing `EnumerateGPUsHandle()` and `EnumerateGPUs()` (DTO) for advanced/native users.
2) Desktop/display consistency
   - Keep `ADLXDesktopServicesHelper.EnumerateAdlxDesktops()` returning `IReadOnlyList<ADLXDesktop>`.
   - Keep `ADLXDisplayServicesHelper.EnumerateAdlxDisplays()` / `EnumerateDisplays()` returning `IReadOnlyList<ADLXDisplay>`.
3) GPU façade shape
   - Ensure `ADLXGPU` (facade) exposes identity props and existing helper/event wiring; no pointer handling required by callers.
   - Optional: add convenience methods on the GPU façade to enumerate its displays/desktops via the system/display/desktop services (internally acquire needed services from the system helper).
4) Samples/README/tests
   - Update usage snippets to show `sys.EnumerateGPUs()` returning `ADLXGPU` facades and basic identity access.
   - Add/adjust a test that enumerates GPU facades via the system helper (skipping on hardware absence as today).
   - Keep handle-based tests intact for advanced coverage.
