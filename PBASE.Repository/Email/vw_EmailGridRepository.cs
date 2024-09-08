using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
using System.Data.Entity;
namespace PBASE.Repository
{
    public partial class vw_EmailGridRepository : RepositoryBase<vw_EmailGrid>, Ivw_EmailGridRepository
    {
        public vw_EmailGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        protected override IQueryable<vw_EmailGrid> ProcessFields(Probase.GridHelper.Rule rule, IQueryable<vw_EmailGrid> query)
        {
            if (rule.field.Equals("RequestedDate"))
            {
                //
                // We need to skip time part from DateTime value.
                DateTime result;
                if (DateTime.TryParse(rule.data, out result))
                {
                    switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                    {
                        case WhereOperation.Equal:
                        case WhereOperation.Contains:
                            query = query.Where(x => DbFunctions.DiffDays(x.RequestedDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.RequestedDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.RequestedDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.RequestedDate, result) <= 0);
                            break;
                        default:
                            break;
                    }
                }
                else if (rule.data == null)
                {
                    query = ProcessCommonFields(rule, query);
                }
            }
            else if (rule.field.Equals("SentDate"))
            {
                //
                // We need to skip time part from DateTime value.
                DateTime result;
                if (DateTime.TryParse(rule.data, out result))
                {
                    switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                    {
                        case WhereOperation.Equal:
                        case WhereOperation.Contains:
                            query = query.Where(x => DbFunctions.DiffDays(x.SentDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.SentDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.SentDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.SentDate, result) <= 0);
                            break;
                        default:
                            break;
                    }
                }
                else if (rule.data == null)
                {
                    query = ProcessCommonFields(rule, query);
                }
            }
            else
            {
                query = ProcessCommonFields(rule, query);
            }
            return query;
        }
    }
    
    public partial interface Ivw_EmailGridRepository : IRepository<vw_EmailGrid>
    {
    }
}

