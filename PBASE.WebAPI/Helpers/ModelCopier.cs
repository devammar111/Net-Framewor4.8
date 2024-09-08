using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.Helpers
{
    public static class ModelCopier
    {
        public static void CopyCollection<T>(IEnumerable<T> from, ICollection<T> to)
        {
            if (from == null || to == null || to.IsReadOnly)
            {
                return;
            }

            to.Clear();
            foreach (T element in from)
            {
                to.Add(element);
            }
        }

        public static void CopyModel(object from, object to)
        {
            if (from == null || to == null)
            {
                return;
            }

            PropertyDescriptorCollection fromProperties = TypeDescriptor.GetProperties(from);
            PropertyDescriptorCollection toProperties = TypeDescriptor.GetProperties(to);

            foreach (PropertyDescriptor fromProperty in fromProperties)
            {
                PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true /* ignoreCase */);
                bool fromFieldIsReadonly = Attribute.IsDefined(fromProperty.ComponentType.GetMember(fromProperty.Name)[0], typeof(checkReadOnly));
                bool isFieldReadonly = false;
                if (fromFieldIsReadonly)
                {
                    var attribute = fromProperty.Attributes.OfType<checkReadOnly>();
                    HttpContext ctx = HttpContext.Current;
                    int userId = HttpContext.Current.User.Identity.GetUserId<int>();
                    var task = Task.Run(async () => await UserHelper.GetMenus(userId));
                    var id = attribute.Select(x=>x._menuId).FirstOrDefault();
                    var menu = task.Result.Where(x => x.Id == id).FirstOrDefault();
                    if(menu.AccessTypeId == -9968)
                    {
                        isFieldReadonly = true;
                    }
                }
                if (toProperty != null && !toProperty.IsReadOnly && !isFieldReadonly)
                {
                    // Can from.Property reference just be assigned directly to to.Property reference?
                    bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);
                    // Is from.Property just the nullable form of to.Property?
                    bool liftedValueType = (isDirectlyAssignable) ? false : (Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType);

                    if (isDirectlyAssignable || liftedValueType)
                    {
                        object fromValue = fromProperty.GetValue(from);
                        if (isDirectlyAssignable || (fromValue != null && liftedValueType))
                        {
                            toProperty.SetValue(to, fromValue);
                        }
                    }
                }
            }
        }
    }
}
