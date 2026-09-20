# Contributing

Thanks for improving Desktop Search. Open an issue before large changes. Keep the CLI dependency-light, cross-platform, local-first, and backward compatible where practical.

## Development
1. Install .NET 8 SDK.
2. `dotnet build DesktopSearch.sln -c Release`
3. `dotnet run --project tests/DesktopSearch.Tests/DesktopSearch.Tests.csproj -c Release`
4. Update both English and Arabic README sections when user-facing behavior changes.

Use focused commits. Add a regression test for bug fixes. Never commit private paths, credentials, generated build output, or user data.

By contributing, you agree that your contribution is licensed under the repository MIT License.