namespace System.StringVersion;

/// <summary>
/// SemVer-based comparison that ignores pre-release and build metadata.
/// Only core numeric identifiers are compared.
/// </summary>
public class IgnorePrereleaseSemVerCompareStrategy : SemVerCompareStrategy
{
    /// <summary>
    /// Compares two arrays of version tokens using only core numeric identifiers.
    /// </summary>
    public override int Compare(in VersionToken[] a, in VersionToken[] b)
    {
        VersionToken[] arrA = a ?? [];
        VersionToken[] arrB = b ?? [];
        int la = arrA.Length;
        int lb = arrB.Length;

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

        return 0;
    }
}
