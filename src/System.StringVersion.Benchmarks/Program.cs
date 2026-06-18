using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace System.StringVersion.Benchmarks;

public class Benchmarks
{
    private readonly string[] samples =
    [
        "1.0.0+build.123",
        "1.0.0-rc.1",
        "10.0.22621",
        "2024.01.15",
    ];

    [Benchmark]
    public void ParseAll()
    {
        foreach (var s in samples) _ = StringVersion.Parse(s);
    }
}

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<Benchmarks>();
        _ = summary; // prevent unused variable warning
    }
}
