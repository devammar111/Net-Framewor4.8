using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PBASE.Helpers
{
    public class SMTPConnect
    {
        public bool IsSendRunning { get; set; }
        private static SMTPConnect instance;
        private static object syncRoot = new Object();

        private SMTPConnect()
        {
            //
        }

        public static SMTPConnect Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SMTPConnect();
                        }
                    }
                }
                return instance;
            }
        }
    }
}