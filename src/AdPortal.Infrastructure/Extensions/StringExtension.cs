namespace AdPortal.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static bool Empty(this string value)
            =>string.IsNullOrWhiteSpace(value);
    }
}