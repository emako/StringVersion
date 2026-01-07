namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Compares the current <see cref="StringVersion"/> instance to a specified <see cref="Version"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Version"/> object to compare with.</param>
    /// <returns>
    /// A signed integer that indicates the relative values of this instance and <paramref name="other"/>:
    /// <list type="bullet">
    /// <item><description>Less than zero: This instance is less than <paramref name="other"/>.</description></item>
    /// <item><description>Zero: This instance is equal to <paramref name="other"/>.</description></item>
    /// <item><description>Greater than zero: This instance is greater than <paramref name="other"/> or <paramref name="other"/> is null.</description></item>
    /// </list>
    /// </returns>
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

    /// <summary>
    /// Determines whether the current <see cref="StringVersion"/> instance is equal to the specified <see cref="Version"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Version"/> object to compare with.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(Version? other)
        => CompareTo(other) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator >(StringVersion left, Version right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator <(StringVersion left, Version right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator >=(StringVersion left, Version right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator <=(StringVersion left, Version right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is equal to the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator ==(StringVersion? left, Version? right)
        => left is not null && left.CompareTo(right) == 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is not equal to the right <see cref="Version"/>.
    /// </summary>
    /// <param name="left">The left <see cref="StringVersion"/> operand.</param>
    /// <param name="right">The right <see cref="Version"/> operand.</param>
    public static bool operator !=(StringVersion? left, Version? right)
        => !(left == right);

    /// <summary>
    /// Implicit conversion from System.Version to StringVersion.
    /// </summary>
    public static implicit operator StringVersion(Version v)
    {
        return new StringVersion(v.ToString());
    }
}
