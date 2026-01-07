namespace System.StringVersion;

public partial class StringVersion
{
    public int CompareTo(Version? other)
    {
        if (other == null) return 1;
        int[] otherParts = [other.Major, other.Minor, other.Build, other.Revision];
        long[] thisParts = new long[4];
        int i = 0;
        foreach (var t in _tokens)
        {
            if (t.Kind == VersionTokenKind.Numeric && i < 4)
            {
                thisParts[i++] = t.Numeric;
            }
        }
        for (; i < 4; i++) thisParts[i] = 0;
        for (i = 0; i < 4; i++)
        {
            int cmp = thisParts[i].CompareTo(otherParts[i]);
            if (cmp != 0) return cmp;
        }
        return 0;
    }

    public bool Equals(Version? other)
        => CompareTo(other) == 0;

    public static bool operator >(StringVersion a, Version b)
        => a.CompareTo(b) > 0;

    public static bool operator <(StringVersion a, Version b)
        => a.CompareTo(b) < 0;

    public static bool operator >=(StringVersion a, Version b)
        => a.CompareTo(b) >= 0;

    public static bool operator <=(StringVersion a, Version b)
        => a.CompareTo(b) <= 0;

    public static bool operator ==(StringVersion? a, Version? b)
        => a is not null && a.CompareTo(b) == 0;

    public static bool operator !=(StringVersion? a, Version? b)
        => !(a == b);
}
