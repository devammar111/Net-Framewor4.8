using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_AspNetUserAccountLogs : BaseEntity
    {
        [Key]
        public int? AspNetUserLogsKey { get; set; }
        public int? AspNetUserKey { get; set; }
        public bool? IsStatus { get; set; }
        public string RequestType { get; set; }
        public int? CreatedUserId { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public TimeSpan? Time
        {
            get
            {
                TimeSpan result = new TimeSpan();
                if (CreatedDate.HasValue)
                {
                    var stingResult = CreatedDate.Value.ToString("HH:mm");
                    result = TimeSpan.Parse(stingResult);
                }

                return result;
            }
            set
            {
                TimeSpan result = new TimeSpan();
                if (CreatedDate.HasValue)
                {
                    var stingResult = CreatedDate.Value.ToString("HH:mm");
                    result = TimeSpan.Parse(stingResult);
                }

                Time = result;
            }
        }

    }
}

