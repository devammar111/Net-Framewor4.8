using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using PBASE.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace PBASE.Repository
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("name=AppContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppContext>());
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 1200;
        }

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<AspNetUsersInvalid> AspNetUsersInvalids { get; set; }
        public DbSet<vw_AgreementGrid> vw_AgreementGrids { get; set; }
        public DbSet<vw_InvalidEmailLogGrid> vw_InvalidEmailLogGrid { get; set; }
        public DbSet<vw_AgreementVersionSubGrid> vw_AgreementVersionSubGrids { get; set; }
        public DbSet<vw_AgreementPreviousVersionNumber> vw_AgreementPreviousVersionNumbers { get; set; }
        public DbSet<vw_SystemAlertIsClosed> vw_SystemAlertIsCloseds { get; set; }
        public DbSet<SystemAlert> SystemAlerts { get; set; }
        public DbSet<vw_LookupAlertType> vw_LookupAlertTypes { get; set; }
        public DbSet<vw_LookupAccessType> vw_LookupAccessTypes { get; set; }
        public DbSet<vw_LookupObject> vw_LookupObjects { get; set; }
        public DbSet<vw_LookupObjectType> vw_LookupObjectTypes { get; set; }
        public DbSet<vw_SystemAlertGrid> vw_SystemAlertGrids { get; set; }
        public DbSet<vw_AgreementUserSubGrid> vw_AgreementUserSubGrids { get; set; }
        public DbSet<vw_SystemAlertMessages> vw_SystemAlertMessagess { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<AgreementUserType> AgreementUserTypes { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<ApplicationInformation> ApplicationInformations { get; set; }
        public DbSet<EmailAttachment> EmailAttachments { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailTemplateTag> EmailTemplateTags { get; set; }
        public DbSet<vw_LookupUserRoles> vw_LookupUserRoless { get; set; }
        public DbSet<vw_ExportLogGrid> vw_ExportLogGrid { get; set; }
        public DbSet<UserExportLog> UserExportLog { get; set; }
        public DbSet<vw_LookupUsersSignature> vw_LookupUsersSignatures { get; set; }
        public DbSet<vw_UserMenuOption> vw_UserMenuOptions { get; set; }
        public DbSet<vw_UserDashboardOption> vw_UserDashboardOptions { get; set; }
        public DbSet<vw_LookupMenuOptionGroup> vw_LookupMenuOptionGroup { get; set; }
        public DbSet<vw_UserGroupGrid> vw_UserGroupGrids { get; set; }
        public DbSet<vw_UserGroupObjectSubGrid> vw_UserGroupObjectSubGrids { get; set; }
        public DbSet<AspNetUserLogs> AspNetUserLogss { get; set; }
        public DbSet<vw_AspNetUserLogsGrid> vw_AspNetUserLogsGrids { get; set; }
        public DbSet<vw_EmailGrid> vw_EmailGrids { get; set; }
        public DbSet<vw_AspNetUserAccountLogs> vw_AspNetUserAccountLogss { get; set; }
        public DbSet<vw_UserMenuOptionRoleGrid> vw_UserMenuOptionRoleGrids { get; set; }
        public DbSet<vw_UserDashboardOptionRoleGrid> vw_UserDashboardOptionRoleGrids { get; set; }
        public DbSet<UserDashboardOptionRole> UserDashboardOptionRoles { get; set; }
        public DbSet<vw_RoleGrid> vw_RoleGrids { get; set; }
        public DbSet<vw_LookupUserGroup> vw_LookupUserGroup { get; set; }
        public DbSet<UserGroupDashboardOption> UserGroupDashboardOptions { get; set; }
        public DbSet<UserGroupMenuOption> UserGroupMenuOptions { get; set; }
        public DbSet<UserMenuOptionRole> UserMenuOptionRoles { get; set; }
        public DbSet<vw_LookupUserAssignedRoles> vw_LookupUserAssignedRoless { get; set; }
        public DbSet<vw_LookupGridAlertType> vw_LookupGridAlertTypes { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<InternalFilterHeader> InternalFilterHeader { get; set; }
        public DbSet<InternalGridSetting> InternalGridSetting { get; set; }
        public DbSet<InternalGridSettingDefault> InternalGridSettingDefault { get; set; }
        public DbSet<InternalReport> InternalReports { get; set; }
        public DbSet<InternalReportField> InternalReportField { get; set; }
        public DbSet<InterviewAppointment> InterviewAppointment { get; set; }
        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<LookupType> LookupType { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<SafeIPs> SafeIPs { get; set; }
        public DbSet<TemplateTag> TemplateTag { get; set; }
        public DbSet<vw_LookupGrid> vw_LookupGrid { get; set; }
        public DbSet<vw_LookupFromEmailAddress> vw_LookupFromEmailAddresss { get; set; }
        public DbSet<vw_LookupGridEmailTemplateType> vw_LookupGridEmailTemplateType { get; set; }
        public DbSet<vw_LookupGridEmailType> vw_LookupGridEmailType { get; set; }
        public DbSet<vw_LookupGridTemplateAllowedType> vw_LookupGridTemplateAllowedType { get; set; }
        public DbSet<vw_EmailTemplateGrid> vw_EmailTemplateGrid { get; set; }
        public DbSet<vw_LookupDashboardOptionGroup> vw_LookupDashboardOptionGroup { get; set; }
        public DbSet<vw_LookupEmailType> vw_LookupEmailType { get; set; }
        public DbSet<vw_LookupGridUsers> vw_LookupGridUsers { get; set; }
        public DbSet<vw_LookupGridRole> vw_LookupGridRole { get; set; }
        public DbSet<vw_LookupGridUserGroup> vw_LookupGridUserGroup { get; set; }
        public DbSet<vw_LookupGridUserAccessType> vw_LookupGridUserAccessType { get; set; }
        public DbSet<vw_LookupGridUserType> vw_LookupGridUserType { get; set; }
        public DbSet<vw_LookupEmailTemplateType> vw_LookupEmailTemplateType { get; set; }        
        public DbSet<vw_LookupRole> vw_LookupRoles { get; set; }
        public DbSet<vw_LookupTemplateType> vw_LookupTemplateType { get; set; }
        public DbSet<vw_LookupUsers> vw_LookupUsers { get; set; }
        public DbSet<vw_LookupUserType> vw_LookupUserType { get; set; }
        //public DbSet<vw_MessageGrid> vw_MessageGrid { get; set; }
        public DbSet<vw_TemplateGrid> vw_TemplateGrid { get; set; }
        public DbSet<vw_UserGrid> vw_UserGrid { get; set; }
        //public DbSet<CachedUser> CachedUser { get; set; }
        //public DbSet<User> User { get; set; }
        public DbSet<vw_LookupTypeGrid> vw_LookupTypeGrid { get; set; }
        public DbSet<vw_LookupUserAccessType> vw_LookupUserAccessType { get; set; }
        public DbSet<vw_LookupMenuOption> vw_LookupMenuOption { get; set; }
        public DbSet<vw_LookupList> vw_LookupList { get; set; }
        public DbSet<vw_LookupDashboardOption> vw_LookupDashboardOption { get; set; }
        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<UserClaims> UserClaims { get; set; }
        public DbSet<vw_LookupTemplateAllowedType> vw_LookupTemplateAllowedTypes { get; set; }
        public DbSet<vw_UserAgreementSubGrid> vw_UserAgreementSubGrid { get; set; }
        public DbSet<vw_UserAgreementForm> vw_UserAgreementForms { get; set; }
        public DbSet<UserAgreement> UserAgreements { get; set; }
        public DbSet<vw_LookupTestType> vw_LookupTestTypes { get; set; }
        public DbSet<vw_TestGrid> vw_TestGrids { get; set; }
        public DbSet<vw_LookupGridTestType> vw_LookupGridTestTypes { get; set; }
        public DbSet<vw_TestSubGrid> vw_TestSubGrids { get; set; }
        public DbSet<vw_TestNoteGrid> vw_TestNoteGrids { get; set; }
        public DbSet<vw_LookupDashboardObjectType> vw_LookupDashboardObjectTypes { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestNote> TestNotes { get; set; }
        public DbSet<TestNoteAttachment> TestNoteAttachments { get; set; }
        public DbSet<TestSub> TestSubs { get; set; }
        public DbSet<vw_UserGroupDashboardObjectSubGrid> vw_UserGroupDashboardObjectSubGrids { get; set; }
        public DbSet<vw_LookupDashboardObject> vw_LookupDashboardObjects { get; set; }
        public DbSet<vw_LookupSetting> vw_LookupSettings { get; set; }



        public virtual void Commit(int currentUserId)
        {
            SaveChanges(currentUserId);
        }
        public async virtual Task<int> CommitAsync(int currentUserId)
        {
            return await SaveChangesAsync(currentUserId);
        }

        public int SaveChanges(int currentUserId)
        {
            try
            {
                DateTime currentDateTime = DateTime.Now;
                // Added entity
                var added = this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Added);
                foreach (var item in added)
                {
                    if (item.Entity is ITrackingEntity)
                    {
                        ((TrackingEntity)item.Entity).CreatedDate = currentDateTime;
                        ((TrackingEntity)item.Entity).CreatedUserId = currentUserId;
                    }
                }

                // Updated entity
                var modified = this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Modified);
                foreach (var item in modified)
                {
                    if (item.Entity is ITrackingEntity)
                    {
                        ((TrackingEntity)item.Entity).UpdatedDate = currentDateTime;
                        ((TrackingEntity)item.Entity).UpdatedUserId = currentUserId;
                    }
                }

                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("The record you attempted to save was modified by another user after you load the original record. " +
                    " The save operation was canceled. Please reload form.");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        throw new Exception(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (DbUpdateException dbu)
            {
                var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

                try
                {
                    foreach (var result in dbu.Entries)
                    {
                        builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                    }
                }
                catch (Exception e)
                {
                    builder.Append("Error parsing DbUpdateException: " + e.ToString());
                }

                string message = builder.ToString();
                throw new Exception(message, dbu);
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }

        public async Task<int> SaveChangesAsync(int currentUserId)
        {
            try
            {
                DateTime currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time");
                // Added entity
                var added = this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Added);
                foreach (var item in added)
                {
                    if (item.Entity is ITrackingEntity)
                    {
                        ((TrackingEntity)item.Entity).CreatedDate = currentDateTime;
                        ((TrackingEntity)item.Entity).CreatedUserId = currentUserId;
                    }
                }

                // Updated entity
                var modified = this.ChangeTracker.Entries().Where(e => e.State == System.Data.Entity.EntityState.Modified);
                foreach (var item in modified)
                {
                    if (item.Entity is ITrackingEntity)
                    {
                        ((TrackingEntity)item.Entity).UpdatedDate = currentDateTime;
                        ((TrackingEntity)item.Entity).UpdatedUserId = currentUserId;
                    }
                }

                return await base.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("The record you attempted to save was modified by another user after you load the original record. " +
                    " The save operation was canceled. Please reload form.");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        throw new Exception(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class AppContextInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            //
        }
    }
}
