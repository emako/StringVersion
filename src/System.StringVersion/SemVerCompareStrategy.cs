using System.Collections.Generic;

namespace System.StringVersion;

/// <summary>
/// Semantic Versioning (SemVer) comparison strategy implementation.
/// Handles core, pre-release, and build metadata precedence.
/// </summary>
public sealed class SemVerCompareStrategy : IVersionCompareStrategy
{
    /// <summary>
    /// Singleton instance of the SemVer compare strategy.
    /// </summary>
    public static SemVerCompareStrategy Instance { get; } = new SemVerCompareStrategy();

    /// <summary>
    /// Compares two arrays of version tokens using SemVer rules.
    /// </summary>
    public int Compare(in VersionToken[] a, in VersionToken[] b)
    {
        VersionToken[] arrA = a ?? [];
        VersionToken[] arrB = b ?? [];
        int la = arrA.Length;
        int lb = arrB.Length;

        // Find core numeric length
        int coreA = 0;
        while (coreA < la && arrA[coreA].Kind == VersionTokenKind.Numeric) coreA++;
        int coreB = 0;
        while (coreB < lb && arrB[coreB].Kind == VersionTokenKind.Numeric) coreB++;

        int maxCore = Math.Max(coreA, coreB);
        for (int i = 0; i < maxCore; i++)
        {
            long va = i < coreA ? arrA[i].Numeric : 0;
            long vb = i < coreB ? arrB[i].Numeric : 0;
            if (va != vb) return va > vb ? 1 : -1;
        }

        // At this point core numeric identifiers are equal.
        // Now determine pre-release presence: absence of pre-release => higher precedence
        bool hasPreA = false;
        bool hasPreB = false;
        for (int i = 0; i < la; i++) if (arrA[i].Kind == VersionTokenKind.PreRelease) { hasPreA = true; break; }
        for (int i = 0; i < lb; i++) if (arrB[i].Kind == VersionTokenKind.PreRelease) { hasPreB = true; break; }

        if (hasPreA != hasPreB)
        {
            return hasPreA ? -1 : 1; // pre-release has lower precedence
        }

        if (hasPreA && hasPreB)
        {
            // Collect pre-release sequences
            List<VersionToken> seqA = [];
            List<VersionToken> seqB = [];
            for (int i = 0; i < la; i++) if (arrA[i].Kind == VersionTokenKind.PreRelease) seqA.Add(arrA[i]);
            for (int i = 0; i < lb; i++) if (arrB[i].Kind == VersionTokenKind.PreRelease) seqB.Add(arrB[i]);

            int sa = seqA.Count, sb = seqB.Count;
            int m = Math.Min(sa, sb);
            for (int i = 0; i < m; i++)
            {
                VersionToken ta = seqA[i];
                VersionToken tb = seqB[i];
                bool aIsNumeric = ta.Text is null && ta.Kind == VersionTokenKind.PreRelease;
                bool bIsNumeric = tb.Text is null && tb.Kind == VersionTokenKind.PreRelease;
                if (aIsNumeric && bIsNumeric)
                {
                    if (ta.Numeric != tb.Numeric) return ta.Numeric > tb.Numeric ? 1 : -1;
                    continue;
                }
                if (aIsNumeric && !bIsNumeric) return -1; // numeric has lower precedence than non-numeric
                if (!aIsNumeric && bIsNumeric) return 1;
                int cmp = StringComparer.OrdinalIgnoreCase.Compare(ta.Text ?? string.Empty, tb.Text ?? string.Empty);
                if (cmp != 0) return cmp;
            }

            if (sa != sb) return sa > sb ? 1 : -1;
        }

        // Ignore build metadata for precedence
        return 0;
    }
}
