using System;

namespace System.StringVersion;

/// <summary>
/// Default version comparison strategy, compares numeric and text tokens in order.
/// </summary>
public sealed class DefaultCompareStrategy : IVersionCompareStrategy
{
    /// <summary>
    /// Singleton instance of the default compare strategy.
    /// </summary>
    public static DefaultCompareStrategy Instance { get; } = new DefaultCompareStrategy();

    /// <summary>
    /// Compares two arrays of version tokens using default rules.
    /// </summary>
    public int Compare(in VersionToken[] a, in VersionToken[] b)
    {
        VersionToken[] arrA = a ?? [];
        VersionToken[] arrB = b ?? [];
        int la = arrA.Length;
        int lb = arrB.Length;
        int max = Math.Max(la, lb);
        for (int i = 0; i < max; i++)
        {
            if (i >= la) return -1;
            if (i >= lb) return 1;
            VersionToken ta = arrA[i];
            VersionToken tb = arrB[i];
            if (ta.Kind != tb.Kind)
            {
                // Numeric tokens have higher precedence than text tokens
                return ta.Kind == VersionTokenKind.Numeric ? 1 : -1;
            }
            if (ta.Kind == VersionTokenKind.Numeric)
            {
                if (ta.Numeric != tb.Numeric) return ta.Numeric > tb.Numeric ? 1 : -1;
            }
            else
            {
                int cmp = StringComparer.OrdinalIgnoreCase.Compare(ta.Text ?? string.Empty, tb.Text ?? string.Empty);
                if (cmp != 0) return cmp;
            }
        }
        return 0;
    }
}
