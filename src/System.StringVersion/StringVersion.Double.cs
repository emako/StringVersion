using System.Globalization;

namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a <see cref="double"/> value are equal.
    /// </summary>
    public static bool operator ==(StringVersion left, double right)
        => left.CompareTo(new StringVersion(right.ToString(CultureInfo.InvariantCulture))) == 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance and a <see cref="double"/> value are not equal.
    /// </summary>
    public static bool operator !=(StringVersion left, double right)
        => !(left == right);

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than a <see cref="double"/> value.
    /// </summary>
    public static bool operator <(StringVersion left, double right)
        => left.CompareTo(new StringVersion(right.ToString(CultureInfo.InvariantCulture))) < 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is less than or equal to a <see cref="double"/> value.
    /// </summary>
    public static bool operator <=(StringVersion left, double right)
        => left.CompareTo(new StringVersion(right.ToString(CultureInfo.InvariantCulture))) <= 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than a <see cref="double"/> value.
    /// </summary>
    public static bool operator >(StringVersion left, double right)
        => left.CompareTo(new StringVersion(right.ToString(CultureInfo.InvariantCulture))) > 0;

    /// <summary>
    /// Determines whether a <see cref="StringVersion"/> instance is greater than or equal to a <see cref="double"/> value.
    /// </summary>
    public static bool operator >=(StringVersion left, double right)
        => left.CompareTo(new StringVersion(right.ToString(CultureInfo.InvariantCulture))) >= 0;

    /// <summary>
    /// Determines whether a <see cref="double"/> value and a <see cref="StringVersion"/> instance are equal.
    /// </summary>
    public static bool operator ==(double left, StringVersion right)
        => new StringVersion(left.ToString(CultureInfo.InvariantCulture)).CompareTo(right) == 0;

    /// <summary>
    /// Determines whether a <see cref="double"/> value and a <see cref="StringVersion"/> instance are not equal.
    /// </summary>
    public static bool operator !=(double left, StringVersion right)
        => !(left == right);

    /// <summary>
    /// Determines whether a <see cref="double"/> value is less than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <(double left, StringVersion right)
        => new StringVersion(left.ToString(CultureInfo.InvariantCulture)).CompareTo(right) < 0;

    /// <summary>
    /// Determines whether a <see cref="double"/> value is less than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator <=(double left, StringVersion right)
        => new StringVersion(left.ToString(CultureInfo.InvariantCulture)).CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether a <see cref="double"/> value is greater than a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >(double left, StringVersion right)
        => new StringVersion(left.ToString(CultureInfo.InvariantCulture)).CompareTo(right) > 0;

    /// <summary>
    /// Determines whether a <see cref="double"/> value is greater than or equal to a <see cref="StringVersion"/> instance.
    /// </summary>
    public static bool operator >=(double left, StringVersion right)
        => new StringVersion(left.ToString(CultureInfo.InvariantCulture)).CompareTo(right) >= 0;
}
