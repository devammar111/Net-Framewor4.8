using System.Collections.Generic;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI;
using System;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class Database_DetailViewModel
    {
        public Database_DetailViewModel()
        {
        }

      
        public string DatabaseName { get; set; }
        public string DataSource { get; set; }
        public string appVersion { get; set; }
        public string appLastUpdated { get; set; }
        public string TermAndConditionHeader { get; set; }
        

    }

}