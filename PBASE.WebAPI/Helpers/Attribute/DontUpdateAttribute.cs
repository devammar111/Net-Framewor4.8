using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBASE.WebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DontUpdateAttribute : Attribute
    {
    }
}