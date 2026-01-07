namespace System.StringVersion;

/// <summary>
/// Represents the type of a version token segment.
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

public readonly struct VersionToken
{
    public VersionTokenKind Kind { get; }
    public long Numeric { get; }
    public string? Text { get; }

    public VersionToken(long number, VersionTokenKind kind = VersionTokenKind.Numeric)
    {
        Kind = kind;
        Numeric = number;
        Text = null;
    }

    public VersionToken(string text, VersionTokenKind kind = VersionTokenKind.Text)
    {
        Kind = kind;
        Text = text;
        Numeric = 0;
    }

    public override string ToString()
    {
        return Kind == VersionTokenKind.Numeric ? Numeric.ToString() : Text ?? string.Empty;
    }
}
