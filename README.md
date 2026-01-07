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

### Basic Construction & Parsing

```csharp
using System.StringVersion;

// Construct directly
var v1 = new StringVersion("1.2.3-beta");

// Parse (throws on invalid input)
var v2 = StringVersion.Parse("1.2.3");

// TryParse (safe, returns bool)
if (StringVersion.TryParse("2.0.0-rc1", out var v3))
    Console.WriteLine($"Parsed: {v3}");

// ToString
Console.WriteLine(v1.ToString()); // Output: 1.2.3-beta
```

### Comparison

```csharp
// Operator overloads
if (v1 < v2)
    Console.WriteLine($"{v1} is less than {v2}");
if (v1 == v2)
    Console.WriteLine("Equal");

// CompareTo
int cmp = v1.CompareTo(v2); // -1, 0, 1

// Equals
bool eq = v1.Equals(v2);
```

#### Supported Operators
- `==`, `!=`
- `<`, `>`, `<=`, `>=`

```csharp
if (v1 >= v2) { /* ... */ }
```

### Semantic Versioning (SemVer) & Other Formats

```csharp
var semver1 = new StringVersion("1.2.3-alpha.1");
var semver2 = new StringVersion("1.2.3-alpha.2");
Console.WriteLine(semver1 < semver2); // True

// Date-based or custom versions
var dateVer = new StringVersion("2023.01.01");
```

### Custom Comparison Strategy

Implement `IVersionCompareStrategy` to define your own comparison logic:

```csharp
public class MyStrategy : IVersionCompareStrategy
{
    public int Compare(ReadOnlySpan<StringToken> x, ReadOnlySpan<StringToken> y)
    {
        // Custom comparison logic
        return x.Length.CompareTo(y.Length); // Example
    }
}

// Use custom strategy
var custom = new StringVersion("1.2.3", new MyStrategy());
```

### Advanced Usage

- Supports `IComparable`, `IEquatable`
- Can be used as dictionary keys or in sets
- Efficient, allocation-minimizing parsing (Span-based)
- Handles versions with prefixes, suffixes, and various delimiters

```csharp
// Sorting
var versions = new[] {
    new StringVersion("1.0.0"),
    new StringVersion("1.0.0-beta"),
    new StringVersion("2.0.0")
};
Array.Sort(versions);

// Dictionary usage
var dict = new Dictionary<StringVersion, string>();
dict[new StringVersion("1.0.0")] = "Release 1";
```

For more details and extension scenarios, see the source code and interface definitions.

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
