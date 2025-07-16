# Release Notes for v3.0.0

## Summary

This release upgrades the MediatR.BackgroundService library and sample API to support .NET 9.0 and the latest Microsoft ecosystem packages, with important notes regarding MediatR licensing.

## What’s Changed

- **Upgraded Target Framework:**
  - All projects now target **.NET 9.0** for improved performance and access to the latest features.

- **Upgraded Microsoft Packages:**
  - `Microsoft.Extensions.DependencyInjection.Abstractions` to **v9.0.7**
  - `Microsoft.Extensions.Hosting` to **v9.0.7**
  - `Microsoft.Extensions.Logging` to **v9.0.7**
  - `Swashbuckle.AspNetCore` to **v9.0.3**

- **Upgraded MediatR:**
  - `MediatR` is now set to **v12.5.0**.
  - **Note:** MediatR has been restricted to v12.5.0 due to a licensing change in later versions. Please review the [MediatR license](https://github.com/jbogard/MediatR/blob/master/LICENSE) if you plan to upgrade further.

## Implementation Details

- The background queue is implemented using `System.Threading.Channels` for high-performance, thread-safe task management.
- Tasks are enqueued via an `IBackgroundTaskQueue` and processed by a hosted service, allowing MediatR requests to be handled asynchronously in the background.
- This design offloads long-running or resource-intensive operations from the main request pipeline, improving scalability and responsiveness.
- The sample API demonstrates how to enqueue and process MediatR requests using this pattern.

## Migration Notes

- Ensure your environment supports .NET 9.0 before upgrading.
- If you require a newer version of MediatR, review the new license terms before updating beyond v12.5.0.

---

Thank you for using MediatR.BackgroundService!

---

## Release History

## v2.0.0

- Upgraded to .NET 8.0
- Updated Microsoft.Extensions.* packages to v8.x
- Improved background queue performance

## v1.0.0

- Initial release of MediatR.BackgroundService
- Provided background queue implementation using MediatR
- Added sample API and documentation
