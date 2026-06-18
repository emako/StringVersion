namespace System.StringVersion;

/// <summary>
/// Options controlling how version comparison is performed.
/// </summary>
public enum VersionCompareOptions
{
    /// <summary>
    /// Default SemVer comparison; pre-release segments affect ordering and equality.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Compare only core numeric identifiers; pre-release and build metadata are ignored.
    /// For example, <c>1.0.0_pre</c> equals <c>1.0.0</c>.
    /// </summary>
    IgnorePrerelease = 1,
}
