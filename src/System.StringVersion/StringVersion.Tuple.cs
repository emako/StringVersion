namespace System.StringVersion;

public partial class StringVersion
{
    public int CompareTo(Tuple<int, int> tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2));

    public int CompareTo(Tuple<int, int, int> tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2, tuple.Item3));

    public int CompareTo(Tuple<int, int, int, int> tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4));

    public int CompareTo((int, int, int) tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2, tuple.Item3));

    public int CompareTo((int, int, int, int) tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4));

    public bool Equals((int, int) tuple)
        => CompareTo(tuple) == 0;

    public bool Equals((int, int, int) tuple)
        => CompareTo(tuple) == 0;

    public bool Equals((int, int, int, int) tuple)
        => CompareTo(tuple) == 0;

    public bool Equals(Tuple<int, int> tuple)
        => CompareTo(tuple) == 0;

    public bool Equals(Tuple<int, int, int> tuple)
        => CompareTo(tuple) == 0;

    public bool Equals(Tuple<int, int, int, int> tuple)
        => CompareTo(tuple) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, (int, int) right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, (int, int) right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, (int, int) right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, (int, int) right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, (int, int) right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, (int, int) right)
        => !(left == right);

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, (int, int, int) right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, (int, int, int) right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, (int, int, int) right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, (int, int, int) right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, (int, int, int) right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, (int, int, int) right)
        => !(left == right);

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, (int, int, int, int) right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, (int, int, int, int) right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, (int, int, int, int) right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, (int, int, int, int) right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, (int, int, int, int) right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, (int, int, int, int) right)
        => !(left == right);

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, Tuple<int, int> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, Tuple<int, int> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, Tuple<int, int> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, Tuple<int, int> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, Tuple<int, int> right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, Tuple<int, int> right)
        => !(left == right);

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, Tuple<int, int, int> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, Tuple<int, int, int> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, Tuple<int, int, int> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, Tuple<int, int, int> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, Tuple<int, int, int> right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, Tuple<int, int, int> right)
        => !(left == right);

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right tuple.
    /// </summary>
    public static bool operator >(StringVersion left, Tuple<int, int, int, int> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right tuple.
    /// </summary>
    public static bool operator <(StringVersion left, Tuple<int, int, int, int> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right tuple.
    /// </summary>
    public static bool operator >=(StringVersion left, Tuple<int, int, int, int> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right tuple.
    /// </summary>
    public static bool operator <=(StringVersion left, Tuple<int, int, int, int> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right tuple.
    /// </summary>
    public static bool operator ==(StringVersion? left, Tuple<int, int, int, int> right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right tuple.
    /// </summary>
    public static bool operator !=(StringVersion? left, Tuple<int, int, int, int> right)
        => !(left == right);

    /// <summary>
    /// Implicit conversion from (int, int) tuple to StringVersion.
    /// </summary>
    public static implicit operator StringVersion((int, int) tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2).ToString());
    }

    /// <summary>
    /// Implicit conversion from (int, int, int) tuple to StringVersion.
    /// </summary>
    public static implicit operator StringVersion((int, int, int) tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2, tuple.Item3).ToString());
    }

    /// <summary>
    /// Implicit conversion from (int, int, int, int) tuple to StringVersion.
    /// </summary>
    public static implicit operator StringVersion((int, int, int, int) tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4).ToString());
    }

    /// <summary>
    /// Implicit conversion from Tuple<int, int> to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(Tuple<int, int> tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2).ToString());
    }

    /// <summary>
    /// Implicit conversion from Tuple<int, int, int> to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(Tuple<int, int, int> tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2, tuple.Item3).ToString());
    }

    /// <summary>
    /// Implicit conversion from Tuple<int, int, int, int> to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(Tuple<int, int, int, int> tuple)
    {
        return new StringVersion(new Version(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4).ToString());
    }
}
