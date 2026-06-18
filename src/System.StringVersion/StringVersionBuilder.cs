using System.Globalization;

namespace System.StringVersion;

/// <summary>
/// Fluent builder for constructing <see cref="StringVersion"/> instances with configurable comparison strategies.
/// </summary>
public sealed class StringVersionBuilder
{
    private string _original = string.Empty;
    private VersionToken[]? _tokens;
    private IVersionCompareStrategy? _strategy;
    private VersionCompareOptions _compareOptions = VersionCompareOptions.Default;

    private StringVersionBuilder()
    {
    }

    /// <summary>
    /// Creates a builder from a version string.
    /// </summary>
    public static StringVersionBuilder From(string? value)
    {
        return new StringVersionBuilder()
        {
            _original = value ?? string.Empty,
        };
    }

    /// <summary>
    /// Creates a builder from an existing <see cref="StringVersion"/> instance.
    /// </summary>
    public static StringVersionBuilder From(StringVersion value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return new StringVersionBuilder
        {
            _original = value.Original,
            _tokens = value.Tokens,
            _strategy = value.Strategy,
        };
    }

    /// <summary>
    /// Creates a builder from an <see cref="int"/> value.
    /// </summary>
    public static StringVersionBuilder From(int value)
        => From(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a builder from a <see cref="long"/> value.
    /// </summary>
    public static StringVersionBuilder From(long value)
        => From(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a builder from a <see cref="double"/> value.
    /// </summary>
    public static StringVersionBuilder From(double value)
        => From(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a builder from a <see cref="float"/> value.
    /// </summary>
    public static StringVersionBuilder From(float value)
        => From(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Creates a builder from a <see cref="Version"/> instance.
    /// </summary>
    public static StringVersionBuilder From(Version value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return From(value.ToString());
    }

    /// <summary>
    /// Creates a builder from a major/minor tuple.
    /// </summary>
    public static StringVersionBuilder From((int, int) value)
        => From(new Version(value.Item1, value.Item2));

    /// <summary>
    /// Creates a builder from a major/minor/build tuple.
    /// </summary>
    public static StringVersionBuilder From((int, int, int) value)
        => From(new Version(value.Item1, value.Item2, value.Item3));

    /// <summary>
    /// Creates a builder from a major/minor/build/revision tuple.
    /// </summary>
    public static StringVersionBuilder From((int, int, int, int) value)
        => From(new Version(value.Item1, value.Item2, value.Item3, value.Item4));

    /// <summary>
    /// Creates a builder from a <see cref="Tuple{int, int}"/>.
    /// </summary>
    public static StringVersionBuilder From(Tuple<int, int> value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return From(new Version(value.Item1, value.Item2));
    }

    /// <summary>
    /// Creates a builder from a <see cref="Tuple{int, int, int}"/>.
    /// </summary>
    public static StringVersionBuilder From(Tuple<int, int, int> value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return From(new Version(value.Item1, value.Item2, value.Item3));
    }

    /// <summary>
    /// Creates a builder from a <see cref="Tuple{int, int, int, int}"/>.
    /// </summary>
    public static StringVersionBuilder From(Tuple<int, int, int, int> value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return From(new Version(value.Item1, value.Item2, value.Item3, value.Item4));
    }

    /// <summary>
    /// Sets the comparison options used when building the <see cref="StringVersion"/>.
    /// </summary>
    public StringVersionBuilder WithCompareOptions(VersionCompareOptions options)
    {
        _compareOptions = options;
        _strategy = null;
        return this;
    }

    /// <summary>
    /// Sets a custom comparison strategy, overriding <see cref="VersionCompareOptions"/>.
    /// </summary>
    public StringVersionBuilder WithCompareStrategy(IVersionCompareStrategy strategy)
    {
        _strategy = strategy ?? new SemVerCompareStrategy();
        return this;
    }

    /// <summary>
    /// Configures comparison to ignore pre-release and build metadata.
    /// </summary>
    public StringVersionBuilder IgnorePrerelease()
        => WithCompareOptions(VersionCompareOptions.IgnorePrerelease);

    /// <summary>
    /// Configures default SemVer comparison where pre-release segments are considered.
    /// </summary>
    public StringVersionBuilder ComparePrerelease()
        => WithCompareOptions(VersionCompareOptions.Default);

    /// <summary>
    /// Builds a <see cref="StringVersion"/> instance using the configured source and comparison strategy.
    /// </summary>
    public StringVersion Build()
    {
        IVersionCompareStrategy strategy = _strategy ?? CompareStrategyResolver.Resolve(_compareOptions);

        if (_tokens is not null)
        {
            return new StringVersion(_tokens, _original, strategy);
        }

        return BuildFromOriginal(strategy);
    }

    /// <summary>
    /// Attempts to build a <see cref="StringVersion"/> using strict parsing rules.
    /// </summary>
    public bool TryBuild(out StringVersion? result)
    {
        IVersionCompareStrategy strategy = _strategy ?? CompareStrategyResolver.Resolve(_compareOptions);

        if (_tokens is not null)
        {
            result = new StringVersion(_tokens, _original, strategy);
            return true;
        }

        if (StringVersion.TryParse(_original, out result, strategy))
        {
            return true;
        }

        result = null;
        return false;
    }

    private StringVersion BuildFromOriginal(IVersionCompareStrategy strategy)
    {
        VersionToken[] tokens;
        try
        {
            tokens = string.IsNullOrWhiteSpace(_original) ? [] : Tokenizer.Tokenize(_original.AsSpan());
        }
        catch
        {
            tokens = [];
        }

        return new StringVersion(tokens, _original, strategy);
    }
}
