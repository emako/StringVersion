namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Compares this version to another StringVersion.
    /// </summary>
    public int CompareTo(StringVersion? other)
    {
        if (other is null) return 1;
        return Strategy.Compare(_tokens, other._tokens);
    }

    /// <summary>
    /// Checks equality with another StringVersion.
    /// </summary>
    public bool Equals(StringVersion? other)
    {
        if (other is null) return false;
        return CompareTo(other) == 0;
    }

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than the right <see cref="StringVersion"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator >(StringVersion left, StringVersion right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than the right <see cref="StringVersion"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator <(StringVersion left, StringVersion right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is greater than or equal to the right <see cref="StringVersion"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator >=(StringVersion left, StringVersion right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Determines whether the left <see cref="StringVersion"/> is less than or equal to the right <see cref="StringVersion"/>.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator <=(StringVersion left, StringVersion right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Determines whether two <see cref="StringVersion"/> instances are equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator ==(StringVersion? left, StringVersion? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.CompareTo(right) == 0;
    }

    /// <summary>
    /// Determines whether two <see cref="StringVersion"/> instances are not equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    public static bool operator !=(StringVersion? left, StringVersion? right)
        => !(left == right);
}
