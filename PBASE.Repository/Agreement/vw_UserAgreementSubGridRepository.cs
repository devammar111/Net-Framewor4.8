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
    public partial class vw_UserAgreementSubGridRepository : RepositoryBase<vw_UserAgreementSubGrid>, Ivw_UserAgreementSubGridRepository
    {
        public vw_UserAgreementSubGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        protected override IQueryable<vw_UserAgreementSubGrid> ProcessFields(Probase.GridHelper.Rule rule, IQueryable<vw_UserAgreementSubGrid> query)
        {
            if (rule.field.Equals("AgreementDate"))
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
                            query = query.Where(x => DbFunctions.DiffDays(x.AgreementDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AgreementDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AgreementDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AgreementDate, result) <= 0);
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
            else if (rule.field.Equals("AcceptDeclineDate"))
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
                            query = query.Where(x => DbFunctions.DiffDays(x.AcceptDeclineDate, result) == 0);
                            break;
                        case WhereOperation.NotEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AcceptDeclineDate, result) != 0);
                            break;
                        case WhereOperation.LessOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AcceptDeclineDate, result) >= 0);
                            break;
                        case WhereOperation.GreaterOrEqual:
                            query = query.Where(x => DbFunctions.DiffDays(x.AcceptDeclineDate, result) <= 0);
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
    
    public partial interface Ivw_UserAgreementSubGridRepository : IRepository<vw_UserAgreementSubGrid>
    {
    }
}

