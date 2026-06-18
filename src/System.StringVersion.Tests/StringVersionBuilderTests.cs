using Xunit;

namespace System.StringVersion.Tests;

public sealed class StringVersionBuilderTests
{
    [Fact]
    public void IgnorePrerelease_MakesPreReleaseEqualToRelease()
    {
        StringVersion pre = StringVersionBuilder.From("1.0.0_pre").IgnorePrerelease().Build();
        StringVersion release = StringVersionBuilder.From("1.0.0").IgnorePrerelease().Build();

        Assert.True(pre == release);
        Assert.Equal(0, pre.CompareTo(release));
    }

    [Fact]
    public void DefaultCompareOptions_StillTreatsPreReleaseAsDifferent()
    {
        StringVersion pre = StringVersionBuilder.From("1.0.0_pre").ComparePrerelease().Build();
        StringVersion release = StringVersionBuilder.From("1.0.0").ComparePrerelease().Build();

        Assert.False(pre == release);
        Assert.True(pre < release);
    }

    [Fact]
    public void WithCompareOptions_IgnorePrerelease_WorksViaEnum()
    {
        StringVersion left = StringVersionBuilder
            .From("2.1.0-beta")
            .WithCompareOptions(VersionCompareOptions.IgnorePrerelease)
            .Build();
        StringVersion right = StringVersionBuilder
            .From("2.1.0")
            .WithCompareOptions(VersionCompareOptions.IgnorePrerelease)
            .Build();

        Assert.True(left == right);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1L)]
    [InlineData(1.2)]
    [InlineData(1.2f)]
    public void FromNumericTypes_BuildExpectedVersion(object value)
    {
        StringVersion version = value switch
        {
            int i => StringVersionBuilder.From(i).Build(),
            long l => StringVersionBuilder.From(l).Build(),
            double d => StringVersionBuilder.From(d).Build(),
            float f => StringVersionBuilder.From(f).Build(),
            _ => throw new InvalidOperationException(),
        };

        Assert.Equal(value.ToString(), version.Original);
    }

    [Fact]
    public void FromStringVersion_PreservesOriginalAndAllowsStrategyOverride()
    {
        StringVersion source = new("1.2.3-alpha");
        StringVersion built = StringVersionBuilder
            .From(source)
            .IgnorePrerelease()
            .Build();

        Assert.Equal("1.2.3-alpha", built.Original);
        Assert.True(built == StringVersionBuilder.From("1.2.3").IgnorePrerelease().Build());
    }

    [Fact]
    public void FromVersionAndTuples_BuildComparableVersions()
    {
        StringVersion fromVersion = StringVersionBuilder.From(new Version(1, 2, 3)).Build();
        StringVersion fromTuple = StringVersionBuilder.From((1, 2, 3)).Build();

        Assert.True(fromVersion == fromTuple);
    }

    [Fact]
    public void WithCompareStrategy_OverridesCompareOptions()
    {
        StringVersion version = StringVersionBuilder
            .From("1.0.0_pre")
            .WithCompareOptions(VersionCompareOptions.Default)
            .WithCompareStrategy(new IgnorePrereleaseSemVerCompareStrategy())
            .Build();

        Assert.True(version == StringVersionBuilder.From("1.0.0").Build());
    }

    [Fact]
    public void TryBuild_ReturnsFalseForInvalidStrictInput()
    {
        bool ok = StringVersionBuilder.From("a.b.c.d").TryBuild(out var result);

        Assert.False(ok);
        Assert.Null(result);
    }

    [Fact]
    public void TryBuild_ReturnsTrueForValidInput()
    {
        bool ok = StringVersionBuilder.From("1.0.0").TryBuild(out var result);

        Assert.True(ok);
        Assert.NotNull(result);
        Assert.Equal("1.0.0", result!.Original);
    }
}
