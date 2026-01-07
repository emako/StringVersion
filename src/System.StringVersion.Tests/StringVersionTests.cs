using Xunit;

namespace System.StringVersion.Tests;

public sealed class StringVersionTests
{
    [Theory]
    [InlineData("1.0", "1.0-beta", 1)]
    [InlineData("1.0-beta", "1.0-rc", -1)]
    [InlineData("1.0-rc.1", "1.0", -1)]
    [InlineData("1.0_rc.1", "1.0", -1)]
    [InlineData("1.0_a1", "1.0", -1)]
    [InlineData("1.0.0", "1.0", 0)]
    [InlineData("10.0", "2.0", 1)]
    [InlineData("2024.01.15", "2023.12.31", 1)]
    public void CompareSamples(string a, string b, int expected)
    {
        Assert.True(StringVersion.TryParse(a, out var va));
        Assert.True(StringVersion.TryParse(b, out var vb));
        Assert.NotNull(va);
        Assert.NotNull(vb);

        int cmp = va!.CompareTo(vb!);
        int sign = cmp == 0 ? 0 : (cmp > 0 ? 1 : -1);
        Assert.Equal(expected, sign);
    }

    [Fact]
    public void SemVerEquality()
    {
        Assert.True(StringVersion.TryParse("1.0.0+build.123", out var a));
        Assert.True(StringVersion.TryParse("1.0.0", out var b));
        Assert.NotNull(a);
        Assert.NotNull(b);
        Assert.True(a!.CompareTo(b!) == 0);
    }

    [Fact]
    public void PreReleaseOrdering()
    {
        Assert.True(StringVersion.TryParse("1.0-beta", out var v1));
        Assert.True(StringVersion.TryParse("1.0-rc", out var v2));
        Assert.True(StringVersion.TryParse("1.0", out var v3));
        Assert.NotNull(v1);
        Assert.NotNull(v2);
        Assert.NotNull(v3);
        Assert.True(v1!.CompareTo(v2!) < 0);
        Assert.True(v2!.CompareTo(v3!) < 0);
    }

    [Fact]
    public void PrefixedVersionsAreParsed()
    {
        Assert.True(StringVersion.TryParse("V1.2.3", out var vA));
        Assert.True(StringVersion.TryParse("1.2.3", out var vB));
        Assert.NotNull(vA);
        Assert.NotNull(vB);
        Assert.True(vA!.CompareTo(vB!) == 0);

        Assert.True(StringVersion.TryParse("MyProduct V2.36.6", out var vC));
        Assert.True(StringVersion.TryParse("2.36.6", out var vD));
        Assert.NotNull(vC);
        Assert.NotNull(vD);
        Assert.True(vC!.CompareTo(vD!) == 0);

        Assert.True(StringVersion.TryParse("MyProduct V1.2.3 for agent", out var vE));
        Assert.True(StringVersion.TryParse("1.2.3", out var vF));
        Assert.NotNull(vE);
        Assert.NotNull(vF);
        Assert.True(vE!.CompareTo(vF!) == 0);
    }

    [Fact]
    public void DiffTypeOfVersions()
    {
        //Assert.True(new StringVersion("V1.2.3") >= 1);
        //Assert.True(new StringVersion("V1.2.3") >= 1L);
        //Assert.True(new StringVersion("V1.2.3") >= 1d);
        Assert.True(new StringVersion("V1.2.3") == "1.2.3");
        Assert.True(new StringVersion("V1.2.3") == (1, 2, 3));
        Assert.True(new StringVersion("V1.2.3") == new StringVersion("1.2.3"));
        Assert.True(new StringVersion("V1.2.3") == new Version(1, 2, 3));
    }
}
