using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Probase.GridHelper;
using System.Threading.Tasks;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Repository;
using System.Data.Entity;

namespace PBASE.Service
{
    public partial class UserLogService : BaseService, IUserLogService
    {
        #region Initialization

        public UserLogService(

        )
        {

        }
        #endregion Initialization

        #region AspNetUserLogs

        public bool SaveAspNetUserLogsForm(AspNetUserLogs aspNetUserLogs)
        {
            try
            {
                using (var db = new PBASE.Repository.AppContext())
                {
                    var log = db.Set<AspNetUserLogs>();
                    log.Add(new AspNetUserLogs
                    {
                        AspNetUserKey = aspNetUserLogs.AspNetUserKey,
                        IsStatus = aspNetUserLogs.IsStatus,
                        RequestType = aspNetUserLogs.RequestType,
                        IPAddress = aspNetUserLogs.IPAddress,
                        Location = aspNetUserLogs.Location,
                        CreatedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time"),
                        CreatedUserId = 1,

                    });

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }
        public vw_UserGrid SelectSingle_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where)
        {
            using (var db = new PBASE.Repository.AppContext())
            {
                return db.vw_UserGrid
                      .Where(where)
                      .FirstOrDefault<vw_UserGrid>();

            }
        }



        #endregion AspNetUserLogs

    }

    public partial interface IUserLogService : IBaseService
    {
        #region aspNetUserLogs
        bool SaveAspNetUserLogsForm(AspNetUserLogs aspNetUserLogs);
        vw_UserGrid SelectSingle_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where);


        #endregion
    }
}

