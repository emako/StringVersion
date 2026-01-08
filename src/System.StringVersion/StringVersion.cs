namespace System.StringVersion;

/// <summary>
/// Represents a parsed version string with comparison and equality logic.
/// Supports custom comparison strategies and implicit conversion from string.
/// </summary>
public partial class StringVersion : IComparable<StringVersion>, IEquatable<StringVersion>
{
    /// <summary>
    /// The parsed tokens representing each part of the version string (numeric, text, etc.).
    /// </summary>
    public VersionToken[] Tokens { get; }

    /// <summary>
    /// The original version string.
    /// </summary>
    public string Original { get; }

    /// <summary>
    /// The comparison strategy used for this version.
    /// </summary>
    public IVersionCompareStrategy Strategy { get; }

    /// <summary>
    /// Indicates whether this version is a prerelease version (contains any pre-release tokens).
    /// </summary>
    public bool IsPrerelease
    {
        get
        {
            if (Tokens == null) return false;
            foreach (var t in Tokens)
            {
                if (t.Kind == VersionTokenKind.PreRelease)
                    return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Constructs a StringVersion by parsing a version string.
    /// This constructor is guaranteed not to throw any exceptions, even if the input is invalid or tokenization fails.
    /// </summary>
    /// <param name="original">The version string to parse.</param>
    public StringVersion(string? original)
    {
        VersionToken[] tokens;
        try
        {
            tokens = string.IsNullOrWhiteSpace(original) ? [] : Tokenizer.Tokenize(original.AsSpan());
        }
        catch
        {
            // Swallow all exceptions to guarantee no exception is thrown from this constructor
            tokens = [];
        }
        Tokens = tokens;
        Original = original ?? string.Empty;
        Strategy = new SemVerCompareStrategy();
    }

    /// <summary>
    /// Constructs a StringVersion from tokens, original string, and optional strategy.
    /// This constructor is guaranteed not to throw any exceptions.
    /// </summary>
    /// <param name="tokens">Parsed version tokens.</param>
    /// <param name="original">The original version string.</param>
    /// <param name="strategy">Comparison strategy (optional).</param>
    public StringVersion(VersionToken[] tokens, string? original, IVersionCompareStrategy? strategy = null)
    {
        // Null checks to guarantee no exception is thrown
        Tokens = tokens ?? [];
        Original = original ?? string.Empty;
        Strategy = strategy ?? new SemVerCompareStrategy();
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
            VersionToken[] tokens = Tokenizer.Tokenize(original.AsSpan());

            // No empty version is allowed in strict mode
            if (tokens is null || tokens.Length is 0)
            {
                result = null;
                return false;
            }
            result = new StringVersion(tokens, original, strategy);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    /// Checks equality with another object (StringVersion, Version, or tuple).
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            string v => Equals(v),
            StringVersion v => Equals(v),
            Version v => Equals(v),
            Tuple<int, int> v => Equals(v),
            Tuple<int, int, int> v => Equals(v),
            Tuple<int, int, int, int> v => Equals(v),
            _ => base.Equals(obj),
        };
    }

    /// <summary>
    /// Gets a hash code for this version.
    /// </summary>
    public override int GetHashCode()
    {
        // Simple hash combining first few tokens
        int h = 17;
        for (int i = 0; i < Math.Min(4, Tokens.Length); i++)
        {
            VersionToken t = Tokens[i];
            h = h * 31 + (t.Kind.GetHashCode() * 397);
            if (t.Kind == VersionTokenKind.Numeric) h = h * 31 + t.Numeric.GetHashCode(); else h = h * 31 + (t.Text?.GetHashCode() ?? 0);
        }
        return h;
    }

    /// <summary>
    /// Returns the original version string.
    /// </summary>
    public override string ToString() => Original;
}
