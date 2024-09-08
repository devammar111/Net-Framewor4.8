using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace PBASE.WebAPI.Helpers
{
    public static class DecimalExtentions
    {
        public static string GetFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("N", new CultureInfo("en-GB"));
            }

            return result;
        }
        public static string GetZeroDecimalFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("0,0");
            }
            return result;
        }

        public static string GetOneDecimalFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("#.#");
            }

            return result;
        }

        public static string GetMoneyFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("N2", new CultureInfo("en-GB"));
            }

            return result;
        }

        public static string GetFormattedValue(this decimal value)
        {
            return value.ToString("N", new CultureInfo("en-GB"));
        }

        public static string GetOneDecimalFormattedValue(this decimal value)
        {
            return value.ToString("#.#");
        }

        public static string GetMoneyFormatted(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("N6", new CultureInfo("en-GB"));
            }
            return result;
        }

        public static string GetSixDecimalFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("0,0.000000");
            }

            return result;
        }
        public static string GetSixDecimalFormattedValue(this decimal value)
        {
            return value.ToString("0,0.000000");
        }
        public static string GetFourDecimalFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("0,0.0000");
            }

            return result;
        }

        public static string GetTowDecimalFormattedValue(this decimal? value)
        {
            string result = string.Empty;
            if (value.HasValue)
            {
                result = value.Value.ToString("0,0.00");
            }

            return result;
        }
    }
}