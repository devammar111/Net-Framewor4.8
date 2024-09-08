namespace PBASE.WebAPI.Helpers
{
    public static class BooleanExtentions
    {
        public static string GetShortValue(this bool? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value ? "Y" : "N";
            }
            return result;
        }

        public static string GetFormattedValue(this bool? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value ? "Yes" : "No";
            }
            return result;
        }

        public static string GetShortValue(this bool value)
        {
            return value ? "Y" : "N";
        }

        public static string GetFormattedValue(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}