using System.Buffers;

namespace System.StringVersion;

/// <summary>
/// Tokenizer for parsing version strings into VersionToken arrays.
/// Handles numeric, text, pre-release, and build metadata tokens.
/// </summary>
internal static class Tokenizer
{
    /// <summary>
    /// Tokenizes a version string span into an array of VersionToken.
    /// Supports separators: '.', '-', '_', '+', and space for suffix filtering.
    /// </summary>
    /// <param name="s">The input version string span.</param>
    /// <returns>Array of VersionToken representing the parsed version.</returns>
    public static VersionToken[] Tokenize(ReadOnlySpan<char> s)
    {
        if (s.Length == 0) return [];

        // Find first digit — this is the start of the version substring.
        int len = s.Length;
        int start = -1;
        for (int i = 0; i < len; i++)
        {
            if (char.IsDigit(s[i])) { start = i; break; }
        }

        if (start < 0) return [];

        // Slice to candidate version substring
        s = s.Slice(start);

        // Trim surrounding whitespace
        while (s.Length > 0 && char.IsWhiteSpace(s[0])) s = s.Slice(1);
        while (s.Length > 0 && char.IsWhiteSpace(s[s.Length - 1])) s = s.Slice(0, s.Length - 1);

        PooledList<VersionToken> list = new();
        int iIdx = 0;
        while (iIdx < s.Length)
        {
            int j = iIdx;
            // Read until separator
            while (j < s.Length && s[j] != '.' && s[j] != '-' && s[j] != '+' && s[j] != '_' && s[j] != ' ') j++;

            ReadOnlySpan<char> seg = s.Slice(iIdx, j - iIdx);
            if (seg.Length > 0)
            {
                // Check if segment is all digits (numeric token)
                bool allDigits = true;
                long value = 0;
                for (int k = 0; k < seg.Length; k++)
                {
                    char c = seg[k];
                    if (c < '0' || c > '9') { allDigits = false; break; }
                    value = value * 10 + (c - '0');
                }
                if (allDigits)
                {
                    list.Add(new VersionToken(value));
                }
                else
                {
                    list.Add(new VersionToken(seg.ToString(), VersionTokenKind.Text));
                }
            }

            if (j >= s.Length) break;

            char sep = s[j];
            // Handle pre-release or build metadata
            if (sep == '-' || sep == '_')
            {
                int k = j + 1;
                int startPr = k;
                while (k < s.Length && s[k] != '+') k++;
                ReadOnlySpan<char> pr = s.Slice(startPr, k - startPr);
                int p = 0;
                while (p < pr.Length)
                {
                    int q = p;
                    while (q < pr.Length && pr[q] != '.') q++;
                    ReadOnlySpan<char> sub = pr.Slice(p, q - p);
                    bool digits = true; long val = 0;
                    for (int x = 0; x < sub.Length; x++)
                    {
                        char c = sub[x];
                        if (c < '0' || c > '9') { digits = false; break; }
                        val = val * 10 + (c - '0');
                    }
                    if (digits) list.Add(new VersionToken(val, VersionTokenKind.PreRelease)); else list.Add(new VersionToken(sub.ToString(), VersionTokenKind.PreRelease));
                    p = q + 1;
                }
                iIdx = j + 1 + pr.Length;
                if (iIdx < s.Length && s[iIdx] == '+')
                {
                    int bstart = iIdx + 1;
                    ReadOnlySpan<char> build = s.Slice(bstart);
                    int bp = 0;
                    while (bp < build.Length)
                    {
                        int bq = bp;
                        while (bq < build.Length && build[bq] != '.') bq++;
                        ReadOnlySpan<char> bsub = build.Slice(bp, bq - bp);
                        list.Add(new VersionToken(bsub.ToString(), VersionTokenKind.BuildMetadata));
                        bp = bq + 1;
                    }
                    break;
                }
                continue;
            }

            if (sep == '+')
            {
                int startB = j + 1;
                ReadOnlySpan<char> build = s.Slice(startB);
                int bp = 0;
                while (bp < build.Length)
                {
                    int bq = bp;
                    while (bq < build.Length && build[bq] != '.') bq++;
                    ReadOnlySpan<char> bsub = build.Slice(bp, bq - bp);
                    list.Add(new VersionToken(bsub.ToString(), VersionTokenKind.BuildMetadata));
                    bp = bq + 1;
                }
                break;
            }

            // '.' separator -> continue
            iIdx = j + 1;
        }

        return list.ToArray();
    }

    /// <summary>
    /// Lightweight pooled list to reduce allocations during tokenization.
    /// </summary>
    private struct PooledList<T>
    {
        private T[]? _array;
        private int _count;

        /// <summary>
        /// Adds an item to the pooled list.
        /// </summary>
        public void Add(T item)
        {
            if (_array == null)
            {
                _array = ArrayPool<T>.Shared.Rent(8);
                _count = 0;
            }
            if (_count >= _array.Length)
            {
                // Grow the array
                T[] newArr = ArrayPool<T>.Shared.Rent(_array.Length * 2);
                Array.Copy(_array, 0, newArr, 0, _array.Length);
                ArrayPool<T>.Shared.Return(_array, clearArray: true);
                _array = newArr;
            }
            _array[_count++] = item;
        }

        /// <summary>
        /// Converts the pooled list to an array and returns the pooled array.
        /// </summary>
        public T[] ToArray()
        {
            if (_array == null || _count == 0) return [];
            T[] result = new T[_count];
            Array.Copy(_array, 0, result, 0, _count);
            ArrayPool<T>.Shared.Return(_array, clearArray: true);
            _array = null;
            _count = 0;
            return result;
        }
    }
}
