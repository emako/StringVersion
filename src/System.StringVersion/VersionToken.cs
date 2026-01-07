using System.Diagnostics;

namespace System.StringVersion;

/// <summary>
/// Represents a single token in a version string (numeric, text, pre-release, or build metadata).
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public readonly struct VersionToken
{
    /// <summary>
    /// The numeric value of the token, if applicable.
    /// </summary>
    public long Numeric { get; }

    /// <summary>
    /// The text value of the token, if applicable.
    /// </summary>
    public string? Text { get; }

    /// <summary>
    /// The kind of the token (numeric, text, pre-release, build metadata).
    /// </summary>
    public VersionTokenKind Kind { get; }

    /// <summary>
    /// Constructs a numeric token.
    /// </summary>
    public VersionToken(long value, VersionTokenKind kind = VersionTokenKind.Numeric)
    {
        Numeric = value;
        Text = null;
        Kind = kind;
    }

    /// <summary>
    /// Constructs a text or special token.
    /// </summary>
    public VersionToken(string text, VersionTokenKind kind = VersionTokenKind.Text)
    {
        Numeric = 0;
        Text = text;
        Kind = kind;
    }

    public override string ToString()
    {
        return Kind == VersionTokenKind.Numeric ? Numeric.ToString() : Text ?? string.Empty;
    }
}

/// <summary>
/// Enumerates the kinds of version tokens.
/// </summary>
public enum VersionTokenKind
{
    /// <summary>
    /// Numeric segment, e.g. major/minor/patch numbers.
    /// </summary>
    Numeric,

    /// <summary>
    /// Text segment, e.g. labels like 'alpha', 'beta'.
    /// </summary>
    Text,

    /// <summary>
    /// Pre-release segment, e.g. '-rc', '-alpha', '-beta'.
    /// </summary>
    PreRelease,

    /// <summary>
    /// Build metadata segment, e.g. '+build123'.
    /// </summary>
    BuildMetadata,
}
