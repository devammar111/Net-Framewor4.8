using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBASE.Entity
{
    public class LookupEntity : BaseEntity
    {
        public int? LookupId { get; set; }
        public string LookupValue { get; set; }
        public string LookupValueShow { get; set; }
        public bool? disabled { get; set; }
        public string LookupExtraText { get; set; }
        public int? LookupExtraInt { get; set; }
        public int? AspNetRoleId { get; set; }
        public string GroupBy { get; set; }
        public bool? IsGlobal { get; set; }


    }
}
