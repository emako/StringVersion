namespace System.StringVersion;

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
