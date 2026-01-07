namespace System.StringVersion;

/// <summary>
/// Represents a parsed version string with comparison and equality logic.
/// Supports custom comparison strategies and implicit conversion from string.
/// </summary>
public partial class StringVersion(VersionToken[] tokens, string original, IVersionCompareStrategy? strategy = null) : IComparable<StringVersion>, IEquatable<StringVersion>
{
    private readonly VersionToken[] _tokens = tokens ?? [];

    /// <summary>
    /// The original version string.
    /// </summary>
    public string Original { get; } = original ?? string.Empty;

    /// <summary>
    /// The comparison strategy used for this version.
    /// </summary>
    public IVersionCompareStrategy Strategy { get; } = strategy ?? DefaultCompareStrategy.Instance;

    /// <summary>
    /// Attempts to parse a version string into a StringVersion instance.
    /// </summary>
    public static bool TryParse(string? s, out StringVersion? result, IVersionCompareStrategy? strategy = null)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s)) return false;

        try
        {
            ReadOnlySpan<char> span = s.AsSpan();
            VersionToken[] tokens = Tokenizer.Tokenize(span);

            // Simple recognition: if contains pre-release or build -> semver strategy
            bool hasPre = false, hasBuild = false;
            foreach (VersionToken t in tokens)
            {
                if (t.Kind == VersionTokenKind.PreRelease) hasPre = true;
                if (t.Kind == VersionTokenKind.BuildMetadata) hasBuild = true;
            }

            IVersionCompareStrategy used = strategy ?? (hasPre || hasBuild ? SemVerCompareStrategy.Instance : DefaultCompareStrategy.Instance);
            result = new StringVersion(tokens, s ?? string.Empty, used);
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
    public static StringVersion Parse(string s)
    {
        if (TryParse(s, out StringVersion? v) && v is not null) return v;
        throw new FormatException("Invalid version string");
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
