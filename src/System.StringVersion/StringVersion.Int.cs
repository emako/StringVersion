namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and an <see cref="int"/> value are equal.
    /// </summary>
    public static bool operator ==(StringVersion left, int right)
        => left.CompareTo(new StringVersion(right.ToString())) == 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and an <see cref="int"/> value are not equal.
    /// </summary>
    public static bool operator !=(StringVersion left, int right)
        => !(left == right);

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than an <see cref="int"/> value.
    /// </summary>
    public static bool operator <(StringVersion left, int right)
        => left.CompareTo(new StringVersion(right.ToString())) < 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than or equal to an <see cref="int"/> value.
    /// </summary>
    public static bool operator <=(StringVersion left, int right)
        => left.CompareTo(new StringVersion(right.ToString())) <= 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than an <see cref="int"/> value.
    /// </summary>
    public static bool operator >(StringVersion left, int right)
        => left.CompareTo(new StringVersion(right.ToString())) > 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than or equal to an <see cref="int"/> value.
    /// </summary>
    public static bool operator >=(StringVersion left, int right)
        => left.CompareTo(new StringVersion(right.ToString())) >= 0;

    /// <summary>
    /// Determines whether an <see cref="int"/> value and a <see cref="StringVersion"/> instance are equal.
    /// </summary>
    public static bool operator ==(int left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) == 0;

    /// <summary>
    /// Determines whether an <see cref="int"/> value and a <see cref="StringVersion"/> instance are not equal.
    /// </summary>
    public static bool operator !=(int left, StringVersion right)
        => !(left == right);

    /// <summary>
    /// Determines whether an <see cref="int"/> value is less than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <(int left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) < 0;

    /// <summary>
    /// Determines whether an <see cref="int"/> value is less than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <=(int left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether an <see cref="int"/> value is greater than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >(int left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) > 0;

    /// <summary>
    /// Determines whether an <see cref="int"/> value is greater than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >=(int left, StringVersion right)
        => new StringVersion(left.ToString()).CompareTo(right) >= 0;
}
