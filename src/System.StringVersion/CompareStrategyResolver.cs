namespace System.StringVersion;

internal static class CompareStrategyResolver
{
    public static IVersionCompareStrategy Resolve(VersionCompareOptions options)
    {
        return options switch
        {
            VersionCompareOptions.IgnorePrerelease => new IgnorePrereleaseSemVerCompareStrategy(),
            _ => new SemVerCompareStrategy(),
        };
    }
}
