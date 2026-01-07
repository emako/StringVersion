namespace System.StringVersion;

/// <summary>
/// Interface for version comparison strategies.
/// </summary>
public interface IVersionCompareStrategy
{
    /// <summary>
    /// Compares two arrays of version tokens.
    /// </summary>
    /// <param name="a">First version token array.</param>
    /// <param name="b">Second version token array.</param>
    /// <returns>Comparison result: -1 if a &lt; b, 0 if equal, 1 if a &gt; b.</returns>
    public int Compare(in VersionToken[] a, in VersionToken[] b);
}
