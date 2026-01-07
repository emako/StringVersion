namespace System.StringVersion;

/// <summary>
/// Implicit conversion from string to StringVersion (already exists)
/// public static implicit operator StringVersion(string s) => Parse(s);
/// </summary>
public partial class StringVersion
{
    /// <summary>
    /// Parses a version string into a StringVersion instance, or throws if invalid.
    /// </summary>
    public static StringVersion Parse(string original)
    {
        return new StringVersion(original);
    }

    /// <summary>
    /// Implicit conversion from string to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(string s)
    {
        return Parse(s);
    }

    /// <summary>
    /// Implicitly converts a <see cref="StringVersion"/> instance to its original string representation.
    /// </summary>
    /// <param name="v">The <see cref="StringVersion"/> instance to convert.</param>
    /// <returns>The original string if <paramref name="v"/> is not null; otherwise, <see cref="string.Empty"/>.</returns>
    public static implicit operator string(StringVersion v)
        => v?.Original ?? string.Empty;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a string are equal.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string to compare.</param>
    /// <returns><c>true</c> if both are null or represent the same version; otherwise, <c>false</c>.</returns>
    public static bool operator ==(StringVersion? left, string? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Original == right || left.Equals(right);
    }

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a string are not equal.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string to compare.</param>
    /// <returns><c>true</c> if they are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(StringVersion? left, string? right)
        => !(left == right);

    /// <summary>
    /// Determines whether a string and a <see cref="StringVersion"/> instance are equal.
    /// </summary>
    /// <param name="left">The string to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if both are null or represent the same version; otherwise, <c>false</c>.</returns>
    public static bool operator ==(string? left, StringVersion? right)
        => right == left;

    /// <summary>
    /// Determines whether a string and a <see cref="StringVersion"/> instance are not equal.
    /// </summary>
    /// <param name="left">The string to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if they are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(string? left, StringVersion? right)
        => !(right == left);

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than a string version.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string version to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(StringVersion left, string right)
    {
        var rightVer = Parse(right);
        return left.CompareTo(rightVer) < 0;
    }

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than a string version.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string version to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(StringVersion left, string right)
    {
        var rightVer = Parse(right);
        return left.CompareTo(rightVer) > 0;
    }

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than or equal to a string version.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string version to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(StringVersion left, string right)
    {
        var rightVer = Parse(right);
        return left.CompareTo(rightVer) <= 0;
    }

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than or equal to a string version.
    /// </summary>
    /// <param name="left">The <see cref="StringVersion"/> instance.</param>
    /// <param name="right">The string version to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(StringVersion left, string right)
    {
        var rightVer = Parse(right);
        return left.CompareTo(rightVer) >= 0;
    }

    /// <summary>
    /// Determines whether a string version is less than a <see cref="StringVersion"/> instance.
    /// </summary>
    /// <param name="left">The string version to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(string left, StringVersion right)
    {
        var leftVer = Parse(left);
        return leftVer.CompareTo(right) < 0;
    }

    /// <summary>
    /// Determines whether a string version is greater than a <see cref="StringVersion"/> instance.
    /// </summary>
    /// <param name="left">The string version to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(string left, StringVersion right)
    {
        var leftVer = Parse(left);
        return leftVer.CompareTo(right) > 0;
    }

    /// <summary>
    /// Determines whether a string version is less than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    /// <param name="left">The string version to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(string left, StringVersion right)
    {
        var leftVer = Parse(left);
        return leftVer.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Determines whether a string version is greater than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    /// <param name="left">The string version to compare.</param>
    /// <param name="right">The <see cref="StringVersion"/> instance.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(string left, StringVersion right)
    {
        var leftVer = Parse(left);
        return leftVer.CompareTo(right) >= 0;
    }
}
