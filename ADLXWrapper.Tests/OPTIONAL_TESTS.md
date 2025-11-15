# Optional Test Categories in ADLXWrapper.Tests

## Overview

Some tests in the ADLXWrapper test suite can **modify your system configuration**. These tests are marked with special category traits and are **excluded from normal test runs** by default.

## CreateEyefinity Category

### What It Does

Tests in the `CreateEyefinity` category will:
- Create an AMD Eyefinity desktop (combining multiple displays into one large surface)
- Verify the creation was successful
- Destroy the Eyefinity desktop
- Verify restoration to the original state

### ?? Warnings

- Your displays will **temporarily reconfigure** (may go black for 2-5 seconds)
- Open windows **may be repositioned**
- Requires **2 or more compatible displays** connected to AMD GPU
- Not recommended while running fullscreen applications

### Requirements

? **Required:**
- AMD Radeon GPU (RX 5000 series or newer recommended)
- 2 or more displays connected
- AMD Adrenalin drivers 21.10.1 or newer

? **Recommended:**
- Displays with matching resolution and refresh rate
- No fullscreen applications running
- Save your work before running these tests

### How to Run

#### Command Line

```bash
# Navigate to test project
cd ADLXWrapper.Tests

# Run ONLY CreateEyefinity tests
dotnet test --filter "Category=CreateEyefinity"

# Run with detailed output (recommended)
dotnet test --filter "Category=CreateEyefinity" --verbosity detailed

# Run all tests EXCEPT CreateEyefinity (default)
dotnet test --filter "Category!=CreateEyefinity"
```

#### Visual Studio

1. Open **Test Explorer** (`Test` ? `Test Explorer`)
2. Find test: `DesktopTests` ? `Optional_Test_Eyefinity_Create_And_Restore`
3. **Right-click** on the test
4. Select **Run** or **Debug**
5. Watch the **Output** window for detailed status messages

#### Visual Studio Code

```bash
# In terminal
dotnet test --filter "Category=CreateEyefinity" --logger "console;verbosity=detailed"
```

### Test Output Example

```
================================================================================
??  WARNING: EYEFINITY CREATE/RESTORE TEST
================================================================================
This test will MODIFY your desktop configuration!
- Your displays will reconfigure (may go black briefly)
- Open windows may be repositioned
- The test will restore your original configuration when complete
================================================================================

Step 1: Checking Eyefinity support...
? Eyefinity is supported

Step 2: Checking current Eyefinity state...
Original Eyefinity state: Disabled

Step 3: Creating Eyefinity desktop...
??  Your displays will reconfigure now...
? Eyefinity desktop created successfully
? Verified: Eyefinity is now enabled

Step 4: Restoring original desktop configuration...
??  Your displays will reconfigure again...
? Eyefinity desktop destroyed successfully
? Verified: Eyefinity is now disabled
? Original desktop configuration restored

================================================================================
? TEST COMPLETED SUCCESSFULLY
  Your desktop configuration should be restored to its original state
================================================================================
```

### Safety Features

The test includes several safety features:

1. **Pre-flight Checks**:
   - Verifies Eyefinity is supported
   - Checks minimum display count
   - Saves current Eyefinity state

2. **State Restoration**:
   - Automatically restores original state after test
   - Includes emergency restoration on failure
   - Provides clear instructions if restoration fails

3. **Clear Warnings**:
   - Displays warnings before modifying configuration
   - Shows status at each step
   - Logs detailed error information

4. **Skip Conditions**:
   - Skips if Eyefinity not supported
   - Skips if less than 2 displays
   - Skips if already enabled (preserves user config)

### What If Something Goes Wrong?

If the test fails and doesn't restore your configuration:

1. **Check test output** for error messages
2. **Manually disable Eyefinity**:
   - Open AMD Software (right-click on desktop)
   - Go to `Display` ? `Eyefinity`
   - Click `Disable Eyefinity`
3. **Restart your system** if displays don't restore

The test includes emergency restoration code, but manual intervention may be needed in rare cases.

### Adding More Optional Tests

To add more tests that modify system configuration:

1. Choose an appropriate category name (e.g., `CreateVirtualDisplay`, `ModifyOverclock`)
2. Add the `[Trait("Category", "YourCategory")]` attribute
3. Add `[Trait("Category", "Integration")]` for visibility
4. Include clear warnings in test output
5. Document the category in this file
6. Update test README.md

Example:
```csharp
[Trait("Category", "ModifyOverclock")]
[Trait("Category", "Integration")]
[SkippableFact]
public void Optional_Test_Apply_Overclock_Settings()
{
    _output.WriteLine("??  WARNING: This test will modify GPU settings!");
    // Test implementation...
}
```

### CI/CD Integration

In CI/CD pipelines:

```yaml
# .github/workflows/test.yml
- name: Run Tests (Exclude Optional)
  run: dotnet test --filter "Category!=CreateEyefinity"

# Optional: Run CreateEyefinity tests on dedicated hardware
- name: Run CreateEyefinity Tests
  if: matrix.os == 'windows-amd-multi-display'
  run: dotnet test --filter "Category=CreateEyefinity"
```

## Summary

| Category | Purpose | Risk Level | Default Run |
|----------|---------|------------|-------------|
| None | Standard read-only tests | None | ? Yes |
| `CreateEyefinity` | Create/destroy Eyefinity desktops | Medium | ? No |

---

**Always save your work and close fullscreen applications before running optional tests that modify system configuration.**
