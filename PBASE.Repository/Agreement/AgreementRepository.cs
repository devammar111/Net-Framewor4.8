using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
using System.Data.Common;
using System.Data.SqlClient;

namespace PBASE.Repository
{
    public partial class AgreementRepository : RepositoryBase<Agreement>, IAgreementRepository
    {
        public AgreementRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        public int? sp_AgreementVersionNew(int userId, int agreemtntId, ref int? outputMessage)
        {
            try
            {

                this.DataContext.Database.Connection.Open();
                DbCommand cmd = DataContext.Database.Connection.CreateCommand();
                cmd.CommandTimeout = 1200;
                cmd.CommandText = "sp_AgreementVersionNew";
                cmd.CommandType = CommandType.StoredProcedure;
                var outputMessageParam = new SqlParameter("NewAgreementId", SqlDbType.Int, 100) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputMessageParam);
                cmd.Parameters.Add(new SqlParameter("AgreementId", agreemtntId));
                cmd.Parameters.Add(new SqlParameter("userid", userId));
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        outputMessage = Convert.ToInt32(outputMessageParam.Value);
                    }
                }
            }

            catch (Exception)
            {

            }
            finally
            {
                this.DataContext.Database.Connection.Close();
            }
            return outputMessage;
        }
    }
    
    public partial interface IAgreementRepository : IRepository<Agreement>
    {
        int? sp_AgreementVersionNew(int userId, int agreemtntId, ref int? outputMessage);
    }
}

