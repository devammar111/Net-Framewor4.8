using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace PBASE.WebAPI.Helpers
{
    public static class StringExtentions
    {
        public static int GetIntValue(this string value)
        {
            int result = 0;

            if (value != null)
            {
                int.TryParse(value, out result);
            }

            return result;
        }
        public static string GetSubStringByIndexLength(this string value, int index, int length)
        {
            string subString = String.Empty;

            if (value != null && value != "")
            {
                subString = value.Substring(index, length);
                return subString.TrimEnd();
            }
            value = " ";
            return value;

        }
        public static int GetIntValueByIndexLength(this string value, int index, int length)
        {
            string subString = String.Empty;
            int result = 0;

            if (value != null)
            {
                subString = value.Substring(index, length);

                int.TryParse(subString, out result);
            }

            return result;
        }
    }
}