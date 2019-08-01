namespace Balakin.VSOutputEnhancer.Parsers
{
    public static class ParsedValueExtensions
    {
        public static T? ToNullable<T>(this ParsedValue<T> parsedValue)
            where T : struct
        {
            if (parsedValue.HasValue)
            {
                return parsedValue.Value;
            }

            return null;
        }
    }
}