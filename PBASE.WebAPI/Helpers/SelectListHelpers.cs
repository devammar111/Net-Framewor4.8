using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PBASE.Helpers
{
    public static class SelectListHelpers
    {
        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified items for data value field.
        /// </summary>
        /// <param name="enumerable">The items.</param>
        /// <param name="value">The data value field.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value)
        {
            return enumerable.ToSelectList(value, value, null);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified items for data value field and a selected value.
        /// </summary>
        /// <param name="enumerable">The items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value,
                                                                  object selectedValue)
        {
            return enumerable.ToSelectList(value, value, selectedValue);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified items for data value field and the data text field.
        /// </summary>
        /// <param name="enumerable">The items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value,
                                                                  Func<T, string> text)
        {
            return enumerable.ToSelectList(value, text, null);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified items for data value field, the data text field, and a selected value.
        /// </summary>
        /// <param name="enumerable">The items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        /// <param name="selectedValue">The selected value.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value,
                                                                  Func<T, string> text, object selectedValue)
        {
            if (enumerable == null)
            {
                return Enumerable.Empty<SelectListItem>();
            }
            return enumerable.Select(f => new SelectListItem()
            {
                Value = value(f),
                Text = text(f),
                Selected = value(f).Equals(selectedValue)
            });
        }


        public static SelectList ToSelectList<T>(this T enumeration)
        {
            var source = Enum.GetValues(typeof(T));
            var items = new Dictionary<object, string>();
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in source)
            {
                FieldInfo field = value.GetType().GetField(value.ToString());
                var attrs = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();
                items.Add((int)value, attrs != null ? attrs.GetName() : value.ToString());
            }

            return new SelectList(items, "Key", "Value", enumeration);
        }

    

    }
}