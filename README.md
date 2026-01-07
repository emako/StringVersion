# System.StringVersion

Lightweight library to parse and compare string-based versions with multiple strategies (SemVer-aware, default numeric/text comparison). Includes Span-based tokenizer and sample benchmarks and tests.

Projects added:
- [src/StringVersion](src/StringVersion/StringVersion.csproj)
- [tests/StringVersion.Tests](tests/StringVersion.Tests/StringVersion.Tests.csproj)
- [benchmarks/StringVersion.Benchmarks](benchmarks/StringVersion.Benchmarks/StringVersion.Benchmarks.csproj)

Run tests:

```bash
dotnet test tests/StringVersion.Tests
```

Run benchmarks:

```bash
dotnet run -c Release --project benchmarks/StringVersion.Benchmarks
```
