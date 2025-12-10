﻿## ADLXWrapper expansion plan

### Phase 1: Core Feature Implementation (Complete)

1.  [x] **Display Settings (get/set)**: Implemented color depth/format, scaling, FreeSync, etc.
2.  [x] **Desktop/Eyefinity**: Implemented grid/layout reading, change handling, and create/destroy wrappers.
3.  [x] **3D Settings**: Implemented full get/apply support for Anti-Lag, Boost, RIS, etc.
4.  [x] **Event Handling**: Added listeners for Display, Desktop, and GPU Tuning changes.
5.  [x] **API Polish**: Refactored into a clean, helper-based architecture.

### Phase 2: Next Steps

- [ ] Create comprehensive sample applications demonstrating save/load of full system state.
- [ ] Expand test coverage for `Apply` methods.
- [ ] Document advanced use cases like event handling.
