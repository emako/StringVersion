namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a <see cref="long"/> value are equal.
    /// </summary>
    public static bool operator ==(StringVersion left, long right)
        => left.CompareTo(new StringVersion(right.ToString())) == 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a <see cref="long"/> value are not equal.
    /// </summary>
    public static bool operator !=(StringVersion left, long right)
        => !(left == right);

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than a <see cref="long"/> value.
    /// </summary>
    public static bool operator <(StringVersion left, long right)
        => left.CompareTo(new StringVersion(right.ToString())) < 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than or equal to a <see cref="long"/> value.
    /// </summary>
    public static bool operator <=(StringVersion left, long right)
        => left.CompareTo(new StringVersion(right.ToString())) <= 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than a <see cref="long"/> value.
    /// </summary>
    public static bool operator >(StringVersion left, long right)
        => left.CompareTo(new StringVersion(right.ToString())) > 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than or equal to a <see cref="long"/> value.
    /// </summary>
    public static bool operator >=(StringVersion left, long right)
        => left.CompareTo(new StringVersion(right.ToString())) >= 0;

    /// <summary>
    /// Determines whether a <see cref="long"/> value and a <see cref="StringVersion"/> instance are equal.
    /// </summary>
    public static bool operator ==(long left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) == 0;

    /// <summary>
    /// Determines whether a <see cref="long"/> value and a <see cref="StringVersion"/> instance are not equal.
    /// </summary>
    public static bool operator !=(long left, StringVersion right)
        => !(left == right);

    /// <summary>
    /// Determines whether a <see cref="long"/> value is less than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <(long left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) < 0;

    /// <summary>
    /// Determines whether a <see cref="long"/> value is less than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <=(long left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether a <see cref="long"/> value is greater than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >(long left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) > 0;

    /// <summary>
    /// Determines whether a <see cref="long"/> value is greater than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >=(long left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) >= 0;
}
