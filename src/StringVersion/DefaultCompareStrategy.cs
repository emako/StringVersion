namespace System.StringVersion;

public sealed class DefaultCompareStrategy : IVersionCompareStrategy
{
    public static DefaultCompareStrategy Instance { get; } = new DefaultCompareStrategy();

    public int Compare(in VersionToken[] a, in VersionToken[] b)
    {
        var arrA = a ?? Array.Empty<VersionToken>();
        var arrB = b ?? Array.Empty<VersionToken>();
        int na = arrA.Length;
        int nb = arrB.Length;
        int n = Math.Max(na, nb);
        for (int i = 0; i < n; i++)
        {
            VersionToken ta = i < na ? arrA[i] : new VersionToken(0);
            VersionToken tb = i < nb ? arrB[i] : new VersionToken(0);

            if (ta.Kind == VersionTokenKind.Numeric && tb.Kind == VersionTokenKind.Numeric)
            {
                if (ta.Numeric != tb.Numeric)
                    return ta.Numeric > tb.Numeric ? 1 : -1;
                continue;
            }

            // Numeric beats text
            if (ta.Kind == VersionTokenKind.Numeric && tb.Kind != VersionTokenKind.Numeric) return 1;
            if (tb.Kind == VersionTokenKind.Numeric && ta.Kind != VersionTokenKind.Numeric) return -1;

            // Textual comparison
            string sa = ta.Text ?? string.Empty;
            string sb = tb.Text ?? string.Empty;
            int cmp = StringComparer.OrdinalIgnoreCase.Compare(sa, sb);
            if (cmp != 0) return cmp;
        }
        return 0;
    }
}
