namespace System.StringVersion;

public partial class StringVersion
{
    public int CompareTo((int, int) tuple)
        => CompareTo(new Version(tuple.Item1, tuple.Item2));

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

    public static bool operator >(StringVersion a, (int, int) b)
        => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, (int, int) b)
        => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, (int, int) b)
        => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, (int, int) b)
        => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, (int, int) b)
        => a is not null && a.CompareTo(b) == 0;

    public static bool operator !=(StringVersion? a, (int, int) b)
        => !(a == b);

    public static bool operator >(StringVersion a, (int, int, int) b)
        => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, (int, int, int) b)
        => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, (int, int, int) b)
        => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, (int, int, int) b)
        => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, (int, int, int) b)
        => a is not null && a.CompareTo(b) == 0;

    public static bool operator !=(StringVersion? a, (int, int, int) b)
        => !(a == b);

    public static bool operator >(StringVersion a, (int, int, int, int) b)
        => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, (int, int, int, int) b)
        => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, (int, int, int, int) b)
        => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, (int, int, int, int) b)
        => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, (int, int, int, int) b)
        => a is not null && a.CompareTo(b) == 0;

    public static bool operator !=(StringVersion? a, (int, int, int, int) b)
        => !(a == b);

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
}
