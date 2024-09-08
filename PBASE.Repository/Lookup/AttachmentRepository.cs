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
    public partial class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        public string GetFileHandle(int? attachmentId)
        {
            string fileHandle = "";
            try
            {
                DbCommand cmd = DataContext.Database.Connection.CreateCommand();
                cmd.CommandText = "SELECT AttachmentFileHandle FROM Attachment WHERE AttachmentId = " + attachmentId.Value;
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
    
    public partial interface IAttachmentRepository : IRepository<Attachment>
    {
        string GetFileHandle(int? attachmentId);
    }
}

