# Windows Phone Silverlight Support

## Current status

This repository contains the in-tree Windows Silverlight launch path used by WPR:

- `Src/Platforms/WPR.Platform.Windows/SilverlightLauncher.cs`
- `Src/Platforms/WPR.Platform.Windows/XnaLauncher.cs`

`Docs/ARCHITECTURE.md` also documents the intended routing:

- Silverlight XAPs route through `SilverlightLauncher.LaunchAsync(...)`
- XNA titles continue through `XnaLauncher`

This is a minimal, reviewable compatibility baseline. It is not a claim that all Silverlight apps are fully supported.

## Verified behavior

The repository currently verifies the following architecture facts:

- The Windows platform project contains the Silverlight launcher implementation.
- The architecture doc names the launch flow explicitly.
- The launcher sets host context, bootstraps the app, and displays a window onto the Silverlight `RootVisual`.
- XNA support remains in a separate launcher path.

## Untested / not yet guaranteed

The following are not claimed as full verification here without running a real XAP on a supported host:

- every Silverlight control type
- every layout container
- every event binding pattern
- every WP7/WP8 lifecycle edge case
- every API compatibility shim

## Minimal manual validation

Use a minimal Silverlight app that renders a page with:

- a `TextBlock` with a title
- a `Button`
- a managed `Click` handler that updates the text on the page

### Steps

1. Build the Windows project.
2. Install or launch a minimal Silverlight XAP that contains the above UI.
3. Open the app through WPR on the Windows head.
4. Confirm the page displays text.
5. Click the button.
6. Confirm the click handler runs and the visible page text changes.
7. Close the app and confirm the host cleans up without leaving a locked DLL or stale window state.

## Supported minimal controls

The smallest realistic working path is:

- `TextBlock`
- `Button`
- simple page-level layout containers such as `StackPanel` or `Grid`
- page-level click event handlers attached to managed controls

## Unsupported controls and APIs

These should be treated as unsupported unless separately implemented and validated:

- complex custom controls
- custom templates and data templates beyond the simplest page layout
- unsupported transforms and advanced animations
- unsupported 3D/XAML composition scenarios
- advanced WP7 lifecycle hooks not fully mapped to the desktop environment
- advanced media, navigation, and app-bar APIs without explicit shims

Unsupported functionality must emit useful diagnostics rather than silently appearing to work.

## Diagnostic expectation

If a Silverlight API or control is not implemented, the host should fail with an early, explicit message identifying:

- the app assembly
- the type or control that is unsupported
- the file or method boundary where the compatibility gap is detected

This is preferable to a silent black screen or false success.

## Regression note

The XNA path is intentionally kept separate. The Silverlight work should not silently redirect or break XNA title launch and hosting.
