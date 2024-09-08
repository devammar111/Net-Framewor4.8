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
namespace PBASE.Repository
{
    public partial class UserAccountsRepository : RepositoryBase<UserAccounts>, IUserAccountsRepository
    {
        public UserAccountsRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        public string GetFileHandle(int? attachmentId)
        {
            string fileHandle = "";
            try
            {
                DbCommand cmd = DataContext.Database.Connection.CreateCommand();
                cmd.CommandText = "SELECT UserAccountsFileHandle FROM UserAccounts WHERE UserAccountsId = " + attachmentId.Value;
                this.DataContext.Database.Connection.Open();
                fileHandle = (string)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.DataContext.Database.Connection.Close();
            }
            return fileHandle;
        }

    }
    
    public partial interface IUserAccountsRepository : IRepository<UserAccounts>
    {
        string GetFileHandle(int? attachmentId);
    }
}

