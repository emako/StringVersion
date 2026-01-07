![logo](branding/titlebar.png)

# System.StringVersion

A lightweight, high-performance .NET library for parsing and comparing string-based versions, supporting multiple comparison strategies (including SemVer). Features a Span-based tokenizer, custom strategies, and is compatible with a wide range of .NET targets.

## Features
- Parse and compare version strings (e.g., `1.2.3`, `1.2.3-beta`, `2023.01.01`)
- Semantic Versioning (SemVer) comparison out of the box
- Customizable comparison strategies via `IVersionCompareStrategy`
- Span-based tokenizer for minimal allocations
- Operator overloads for intuitive comparisons (`==`, `!=`, `>`, `<`, etc.)
- Supports .NET Standard 2.0/2.1, .NET Framework 4.6.2+, .NET 5+, .NET 6+, .NET 7+, .NET 8+, .NET 9, .NET 10

## Installation

You can add the package via NuGet (when published):

```bash
dotnet add package StringVersion
```

## Usage

```csharp
using System.StringVersion;

var v1 = new StringVersion("1.2.3-beta");
var v2 = StringVersion.Parse("1.2.3");

if (v1 < v2)
    Console.WriteLine($"{v1} is less than {v2}");

// TryParse
if (StringVersion.TryParse("2.0.0-rc1", out var v3))
    Console.WriteLine(v3);

// Custom strategy (implement IVersionCompareStrategy)
```

## Project Structure

- `src/System.StringVersion/` - Main library
- `src/System.StringVersion.Tests/` - xUnit test project
- `src/System.StringVersion.Benchmarks/` - BenchmarkDotNet performance benchmarks

## Build & Test

Run tests:

```bash
dotnet test src/System.StringVersion.Tests
```

Run benchmarks:

```bash
dotnet run -c Release --project src/System.StringVersion.Benchmarks
```

## Target Frameworks

- .NET Standard 2.0, 2.1
- .NET Framework 4.6.2, 4.7.2, 4.8, 4.8.1
- .NET 5, 6, 7, 8, 9, 10

## License

[MIT](LICENSE)

## Repository

https://github.com/emako/StringVersion
