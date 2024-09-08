using PBASE.Repository.Infrastructure;
using PBASE.Entity;
using System;
using Probase.GridHelper;
using System.Linq;
using System.Data.Entity;

namespace PBASE.Repository
{
    public partial class vw_InvalidEmailLogGridRepository : RepositoryBase<vw_InvalidEmailLogGrid>, Ivw_InvalidEmailLogGridRepository
    {
        public vw_InvalidEmailLogGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }

        protected override IQueryable<vw_InvalidEmailLogGrid> ProcessFields(Rule rule, IQueryable<vw_InvalidEmailLogGrid> query)
        {
            DateTime result;
            if (rule.field.Equals("LastAccessDate"))
            {
                if (DateTime.TryParse(rule.data, out result))
                {
                    switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                    {
                        case WhereOperation.Equal:
                        case WhereOperation.Contains:
                            query = query.Where(x => DbFunctions.DiffDays(x.LastAccessDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.LastAccessDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.LastAccessDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.LastAccessDate, result) <= 0);
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (rule.field.Equals("CreatedDate"))
            {
                if (DateTime.TryParse(rule.data, out result))
                {
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
            }
            else if (rule.field.Equals("UpdatedDate"))
            {
                if (DateTime.TryParse(rule.data, out result))
                {
                    switch (((WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op)))
                    {
                        case WhereOperation.Equal:
                        case WhereOperation.Contains:
                            query = query.Where(x => DbFunctions.DiffDays(x.UpdatedDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.UpdatedDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.UpdatedDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.UpdatedDate, result) <= 0);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                query = ProcessCommonFields(rule, query);
            }

            return query;
        }

    }
    
    public partial interface Ivw_InvalidEmailLogGridRepository : IRepository<vw_InvalidEmailLogGrid>
    {
    }
}

