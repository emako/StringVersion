namespace System.StringVersion;

/// <summary>
/// Represents a parsed version string with comparison and equality logic.
/// Supports custom comparison strategies and implicit conversion from string.
/// </summary>
public partial class StringVersion : IComparable<StringVersion>, IEquatable<StringVersion>
{
    private readonly VersionToken[] _tokens;

    /// <summary>
    /// The original version string.
    /// </summary>
    public string Original { get; }

    /// <summary>
    /// The comparison strategy used for this version.
    /// </summary>
    public IVersionCompareStrategy Strategy { get; }

    /// <summary>
    /// Constructs a StringVersion by parsing a version string.
    /// Throws FormatException if the string is null, empty, or invalid.
    /// </summary>
    /// <param name="original">The version string to parse.</param>
    public StringVersion(string? original)
    {
        if (string.IsNullOrWhiteSpace(original))
            throw new FormatException("Invalid version string");
        VersionToken[] tokens = Tokenizer.Tokenize(original.AsSpan());
        _tokens = tokens;
        Original = original!;
        bool hasPre = false, hasBuild = false;
        foreach (VersionToken t in tokens)
        {
            if (t.Kind == VersionTokenKind.PreRelease) hasPre = true;
            if (t.Kind == VersionTokenKind.BuildMetadata) hasBuild = true;
        }
        Strategy = hasPre || hasBuild ? SemVerCompareStrategy.Instance : DefaultCompareStrategy.Instance;
    }

    /// <summary>
    /// Constructs a StringVersion from tokens, original string, and optional strategy.
    /// </summary>
    /// <param name="tokens">Parsed version tokens.</param>
    /// <param name="original">The original version string.</param>
    /// <param name="strategy">Comparison strategy (optional).</param>
    public StringVersion(VersionToken[] tokens, string original, IVersionCompareStrategy? strategy = null)
    {
        _tokens = tokens ?? [];
        Original = original ?? string.Empty;
        Strategy = strategy ?? DefaultCompareStrategy.Instance;
    }

    /// <summary>
    /// Attempts to parse a version string into a StringVersion instance.
    /// </summary>
    public static bool TryParse(string? original, out StringVersion? result, IVersionCompareStrategy? strategy = null)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(original)) return false;
        try
        {
            result = new StringVersion(original);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Parses a version string into a StringVersion instance, or throws if invalid.
    /// </summary>
    public static StringVersion Parse(string original)
    {
        return new StringVersion(original);
    }

    /// <summary>
    /// Compares this version to another StringVersion.
    /// </summary>
    public int CompareTo(StringVersion? other)
    {
        if (other is null) return 1;
        return Strategy.Compare(_tokens, other._tokens);
    }

    /// <summary>
    /// Checks equality with another object (StringVersion, Version, or tuple).
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            StringVersion v => Equals(v),
            Version v => Equals(v),
            Tuple<int, int> v => Equals(v),
            _ => base.Equals(obj),
        };
    }

    /// <summary>
    /// Checks equality with another StringVersion.
    /// </summary>
    public bool Equals(StringVersion? other)
    {
        if (other is null) return false;
        return CompareTo(other) == 0;
    }

    /// <summary>
    /// Gets a hash code for this version.
    /// </summary>
    public override int GetHashCode()
    {
        // Simple hash combining first few tokens
        int h = 17;
        for (int i = 0; i < Math.Min(4, _tokens.Length); i++)
        {
            VersionToken t = _tokens[i];
            h = h * 31 + (t.Kind.GetHashCode() * 397);
            if (t.Kind == VersionTokenKind.Numeric) h = h * 31 + t.Numeric.GetHashCode(); else h = h * 31 + (t.Text?.GetHashCode() ?? 0);
        }
        return h;
    }

    /// <summary>
    /// Returns the original version string.
    /// </summary>
    public override string ToString() => Original;

    /// <summary>
    /// Implicit conversion from string to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(string s)
    {
        return Parse(s);
    }

    public static bool operator >(StringVersion a, StringVersion b)
        => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, StringVersion b)
        => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, StringVersion b)
        => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, StringVersion b)
        => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, StringVersion? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.CompareTo(b) == 0;
    }

    public static bool operator !=(StringVersion? a, StringVersion? b)
        => !(a == b);
}
