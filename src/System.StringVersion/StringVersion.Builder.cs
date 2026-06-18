namespace System.StringVersion;

public partial class StringVersion
{
    /// <summary>
    /// Creates a fluent builder for constructing <see cref="StringVersion"/> instances.
    /// </summary>
    public static StringVersionBuilder CreateBuilder() => StringVersionBuilder.From(string.Empty);
}
