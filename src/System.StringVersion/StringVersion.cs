namespace System.StringVersion;

public sealed class StringVersion(VersionToken[] tokens, string original, IVersionCompareStrategy? strategy = null) : IComparable<StringVersion>, IEquatable<StringVersion>
{
    private readonly VersionToken[] _tokens = tokens ?? [];

    public string Original { get; } = original ?? string.Empty;

    public IVersionCompareStrategy Strategy { get; } = strategy ?? DefaultCompareStrategy.Instance;

    public static bool TryParse(string? s, out StringVersion? result, IVersionCompareStrategy? strategy = null)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(s)) return false;

        try
        {
            var span = s.AsSpan();
            var tokens = Tokenizer.Tokenize(span);

            // simple recognition: if contains pre-release or build -> semver strategy
            bool hasPre = false, hasBuild = false;
            foreach (var t in tokens)
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

    public static StringVersion Parse(string s)
    {
        if (TryParse(s, out var v) && v != null) return v;
        throw new FormatException("Invalid version string");
    }

    public int CompareTo(StringVersion? other)
    {
        if (other == null) return 1;
        return Strategy.Compare(_tokens, other._tokens);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StringVersion);
    }

    public bool Equals(StringVersion? other)
    {
        if (other == null) return false;
        return CompareTo(other) == 0;
    }

    public override int GetHashCode()
    {
        // simple hash combining first few tokens
        int h = 17;
        for (int i = 0; i < Math.Min(4, _tokens.Length); i++)
        {
            var t = _tokens[i];
            h = h * 31 + (t.Kind.GetHashCode() * 397);
            if (t.Kind == VersionTokenKind.Numeric) h = h * 31 + t.Numeric.GetHashCode(); else h = h * 31 + (t.Text?.GetHashCode() ?? 0);
        }
        return h;
    }

    public override string ToString() => Original;

    public static implicit operator StringVersion(string s)
    {
        return Parse(s);
    }

    public static bool operator >(StringVersion a, StringVersion b) => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, StringVersion b) => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, StringVersion b) => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, StringVersion b) => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, StringVersion? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.CompareTo(b) == 0;
    }

    public static bool operator !=(StringVersion? a, StringVersion? b) => !(a == b);
}
