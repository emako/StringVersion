using System;

namespace System.StringVersion;

public interface IVersionCompareStrategy
{
    int Compare(in VersionToken[] a, in VersionToken[] b);
}
