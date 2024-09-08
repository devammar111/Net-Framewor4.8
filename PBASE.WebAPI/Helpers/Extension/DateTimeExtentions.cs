using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace PBASE.WebAPI.Helpers
{
    public static class DateTimeExtentions
    {
        public static string GetFormattedValue(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("d");
            }

            return result;
        }

        public static string GetSpecialFormattedValue(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("dd MMM");
            }

            return result;
        }

        public static string GetSpecialFormattedValue(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }
        public static string GetSpecialFormattedValueForFile(this DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        public static string GetFormattedValueForFile(this DateTime? date)
        {
            return date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        public static string GetSpecialFormattedValueWithFullMonth(this DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        }

        // Format a date as in "August 20th, 2020."
        public static string ToOrdinalDate(this DateTime value)
        {
            return value.Day + value.Day.ToOrdinal() + " " + value.ToString("MMMM") + " " + value.Year;
        }

        public static string GetFormattedValue(this DateTime date)
        {
            return date.ToString("d");
        }

        public static string GetSpecialFormattedDateValue(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("dd-MMM-yyyy");
            }

            return result;
        }

        public static string GetSpecialFormattedDateTimeValue(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("dd-MMM-yyyy hh:mm tt");
            }

            return result;
        }

        public static string GetFormattedEmailValue(this DateTime date)
        {
            return date.ToString("d-MMMM-yy HH:mm");
        }

        public static string GetFormattedTime(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("hh:mm tt");
            }

            return result;
        }
        public static string GetFormattedTime24hr(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("HH:mm:ss");
            }

            return result;
        }

        // Return the int's ordinal extension.
        public static string ToOrdinal(this int value)
        {
            // Start with the most common extension.
            string extension = "th";

            // Examine the last 2 digits.
            int last_digits = value % 100;

            // If the last digits are 11, 12, or 13, use th. Otherwise:
            if (last_digits < 11 || last_digits > 13)
            {
                // Check the last digit.
                switch (last_digits % 10)
                {
                    case 1:
                        extension = "st";
                        break;
                    case 2:
                        extension = "nd";
                        break;
                    case 3:
                        extension = "rd";
                        break;
                }
            }

            return extension;
        }

        public static string GetFormattedTime24hrs(this DateTime? date)
        {
            string result = string.Empty;
            if (date.HasValue)
            {
                result = date.Value.ToString("HH:mm");
            }

            return result;
        }
        public static string GetFormattedTime24hrs(this TimeSpan? time)
        {
            string result = string.Empty;
            if (time.HasValue)
            {
                result = time.Value.ToString(@"hh\:mm");
            }

            return result;
        }
    }
}