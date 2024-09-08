using System;
using System.Linq;
using System.Data;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace PBASE.Repository
{
    public partial class vw_AspNetUserAccountLogsRepository : RepositoryBase<vw_AspNetUserAccountLogs>, Ivw_AspNetUserAccountLogsRepository
    {
        public vw_AspNetUserAccountLogsRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }

        protected override IQueryable<vw_AspNetUserAccountLogs> ProcessFields(Probase.GridHelper.Rule rule, IQueryable<vw_AspNetUserAccountLogs> query)
        {
            if (rule.field.Equals("CreatedDate"))
            {
                DateTime? result;
                if(rule.data == null)
                {
                    result = null;
                }
                else
                {
                    result = DateTime.Parse(rule.data);
                }
                switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                {
                    case WhereOperation.Equal:
                    case WhereOperation.Contains:
                        query = query.Where(x => DbFunctions.DiffDays(x.CreatedDate, result) == 0);
                        break;
                    case WhereOperation.NotEqual:
                        query = query.Where(x => DbFunctions.DiffDays(x.CreatedDate, result) != 0);
                        break;
                    case WhereOperation.LessOrEqual:
                        query = query.Where(x => DbFunctions.DiffDays(x.CreatedDate, result) >= 0);
                        break;
                    case WhereOperation.GreaterOrEqual:
                        query = query.Where(x => DbFunctions.DiffDays(x.CreatedDate, result) <= 0);
                        break;
                    default:
                        break;
                }
            }
            else if (rule.field.Equals("Time"))
            {
                TimeSpan? result;
                if(rule.data == null)
                {
                    result = null;
                }
                else
                {
                    result = TimeSpan.Parse(rule.data);
                }
                switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                {
                    case WhereOperation.Equal:
                    case WhereOperation.Contains:
                        query = query.Where(x => DbFunctions.DiffMinutes(DbFunctions.CreateTime(SqlFunctions.DatePart("hh", x.CreatedDate), SqlFunctions.DatePart("mi", x.CreatedDate), 0), result) == 0);
                        break;
                    case WhereOperation.NotEqual:
                        query = query.Where(x => DbFunctions.DiffMinutes(DbFunctions.CreateTime(SqlFunctions.DatePart("hh", x.CreatedDate), SqlFunctions.DatePart("mi", x.CreatedDate), 0), result) != 0);
                        break;
                    case WhereOperation.LessOrEqual:
                        query = query.Where(x => DbFunctions.DiffMinutes(DbFunctions.CreateTime(SqlFunctions.DatePart("hh", x.CreatedDate), SqlFunctions.DatePart("mi", x.CreatedDate), 0), result) >= 0);
                        break;
                    case WhereOperation.GreaterOrEqual:
                        query = query.Where(x => DbFunctions.DiffMinutes(DbFunctions.CreateTime(SqlFunctions.DatePart("hh", x.CreatedDate), SqlFunctions.DatePart("mi", x.CreatedDate), 0), result) <= 0);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = base.ProcessCommonFields(rule, query);
            }
            return query;
        }

    }

    public partial interface Ivw_AspNetUserAccountLogsRepository : IRepository<vw_AspNetUserAccountLogs>
    {
    }
}

