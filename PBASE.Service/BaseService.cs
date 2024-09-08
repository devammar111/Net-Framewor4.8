using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Repository;

using Probase.GridHelper;

namespace PBASE.Service
{
    public abstract class BaseService : IBaseService
    {
        public string LastErrorMessage { get; set; }


        protected void ProcessServiceException(Exception ex)
        {
            if (!ex.Message.Contains("See the inner exception for details."))
            {
                LastErrorMessage += ex.Message;
            }

            if (ex.InnerException != null)
            {
                ProcessServiceException(ex.InnerException);
            }
        }
    }

    public interface IBaseService
    {
        string LastErrorMessage { get; set; }
    }
}
