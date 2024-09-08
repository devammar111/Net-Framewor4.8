using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBASE.Sylar.Helpers
{
    public class ProcessProgress
    {
        public decimal Percentage
        {
            get
            {
                if (this.Total == 0)
                    return 00.00M;
                decimal percent = 99.99M;

                if (Processed < Total)
                {
                    percent = (decimal)this.Processed / (decimal)this.Total * 100.00M;
                }

                return percent;
            }
        }
        public ProcessStatus Status { get; set; }
        public string Result { get; set; }

        public int Total { get; set; }
        public int Processed { get; set; }
    }

    public enum ProcessStatus
    {
        Running = 1,
        Pause = 2,
        Complete = 3,
        Failed = 4
    }
}