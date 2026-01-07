namespace System.StringVersion;

public interface IVersionCompareStrategy
{
    public int Compare(in VersionToken[] a, in VersionToken[] b);
}
