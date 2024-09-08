using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Repository;
using System.Web;
using PBASE.Entity.Enum;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace PBASE.Service
{
    public partial class LookupService : BaseService, ILookupService
    {
        #region Initialization
        private readonly IAttachmentRepository AttachmentRepository;
        private readonly ISafeIPsRepository SafeIPsRepository;
        private readonly IInternalFilterHeaderRepository InternalFilterHeaderRepository;
        private readonly Ivw_LookupFromEmailAddressRepository vw_LookupFromEmailAddressRepository;
        private readonly IInternalGridSettingRepository InternalGridSettingRepository;
        private readonly IInternalGridSettingDefaultRepository InternalGridSettingDefaultRepository;
        private readonly IInternalReportRepository InternalReportRepository;
        private readonly IInternalReportFieldRepository InternalReportFieldRepository;
        private readonly IInterviewAppointmentRepository InterviewAppointmentRepository;
        private readonly ILookupRepository LookupRepository;
        private readonly ILookupTypeRepository LookupTypeRepository;
        private readonly IMessageRepository MessageRepository;
        private readonly ITemplateRepository TemplateRepository;
        private readonly Ivw_LookupGridRepository vw_LookupGridRepository;
        private readonly Ivw_LookupDashboardOptionGroupRepository vw_LookupDashboardOptionGroupRepository;        
        private readonly Ivw_LookupGridUsersRepository vw_LookupGridUsersRepository;
        private readonly Ivw_LookupGridRoleRepository vw_LookupGridRoleRepository;
        private readonly Ivw_LookupGridUserGroupRepository vw_LookupGridUserGroupRepository;
        private readonly Ivw_LookupGridUserAccessTypeRepository vw_LookupGridUserAccessTypeRepository;
        private readonly Ivw_LookupGridUserTypeRepository vw_LookupGridUserTypeRepository;
        private readonly Ivw_LookupMenuOptionGroupRepository vw_LookupMenuOptionGroupRepository;
        private readonly Ivw_LookupRoleRepository vw_LookupRoleRepository;
        private readonly Ivw_LookupTemplateTypeRepository vw_LookupTemplateTypeRepository;
        private readonly Ivw_LookupUsersRepository vw_LookupUsersRepository;
        private readonly Ivw_LookupUserAccessTypeRepository vw_LookupUserAccessTypeRepository;
        private readonly Ivw_LookupUserTypeRepository vw_LookupUserTypeRepository;
        private readonly Ivw_TemplateGridRepository vw_TemplateGridRepository;
        private readonly Ivw_UserGridRepository vw_UserGridRepository;
        private readonly Ivw_LookupTypeGridRepository vw_LookupTypeGridRepository;
        private readonly Ivw_LookupMenuOptionRepository vw_LookupMenuOptionRepository;
        private readonly Ivw_LookupListRepository vw_LookupListRepository;
        private readonly Ivw_LookupUsersSignatureRepository vw_LookupUsersSignatureRepository;
        private readonly Ivw_LookupDashboardOptionRepository vw_LookupDashboardOptionRepository;        
        private readonly IUserExportLogRepository UserExportLogRepository;
        private readonly Ivw_LookupUserGroupRepository vw_LookupUserGroupRepository;
        private readonly Ivw_ExportLogGridRepository vw_ExportLogGridRepository;
        private readonly Ivw_LookupTemplateAllowedTypeRepository vw_LookupTemplateAllowedTypeRepository;
        private readonly Ivw_LookupGridEmailTemplateTypeRepository vw_LookupGridEmailTemplateTypeRepository;
        private readonly Ivw_LookupGridEmailTypeRepository vw_LookupGridEmailTypeRepository;
        private readonly Ivw_LookupGridTemplateAllowedTypeRepository vw_LookupGridTemplateAllowedTypeRepository;
        private readonly Ivw_LookupEmailTemplateTypeRepository vw_LookupEmailTemplateTypeRepository;
        private readonly Ivw_LookupGridAlertTypeRepository vw_LookupGridAlertTypeRepository;
        private readonly Ivw_LookupEmailTypeRepository vw_LookupEmailTypeRepository;
        private readonly Ivw_LookupAlertTypeRepository vw_LookupAlertTypeRepository;
        private readonly Ivw_LookupAccessTypeRepository vw_LookupAccessTypeRepository;
        private readonly Ivw_LookupObjectRepository vw_LookupObjectRepository;
        private readonly Ivw_LookupObjectTypeRepository vw_LookupObjectTypeRepository;
        private readonly ITemplateTagRepository TemplateTagRepository;
        private readonly Ivw_LookupDashboardObjectTypeRepository vw_LookupDashboardObjectTypeRepository;
        private readonly Ivw_LookupDashboardObjectRepository vw_LookupDashboardObjectRepository;
        private readonly Ivw_LookupSettingRepository vw_LookupSettingRepository;



        private readonly IUnitOfWork unitOfWork;

        public LookupService(

            IAttachmentRepository AttachmentRepository,
            ISafeIPsRepository SafeIPsRepository,
            IInternalFilterHeaderRepository InternalFilterHeaderRepository,
            IInternalGridSettingRepository InternalGridSettingRepository,
            IInternalGridSettingDefaultRepository InternalGridSettingDefaultRepository,
            IInternalReportRepository InternalReportRepository,
            IInternalReportFieldRepository InternalReportFieldRepository,
            IInterviewAppointmentRepository InterviewAppointmentRepository,
            ILookupRepository LookupRepository,
            ILookupTypeRepository LookupTypeRepository,
            IMessageRepository MessageRepository,
            ITemplateRepository TemplateRepository,
            Ivw_LookupGridRepository vw_LookupGridRepository,
            Ivw_LookupDashboardOptionGroupRepository vw_LookupDashboardOptionGroupRepository,
            Ivw_LookupFromEmailAddressRepository vw_LookupFromEmailAddressRepository,            
            Ivw_LookupUserAccessTypeRepository vw_LookupUserAccessTypeRepository,
            Ivw_LookupMenuOptionGroupRepository vw_LookupMenuOptionGroupRepository,
            Ivw_LookupUsersSignatureRepository vw_LookupUsersSignatureRepository,
            Ivw_LookupGridUsersRepository vw_LookupGridUsersRepository,
            Ivw_LookupGridRoleRepository vw_LookupGridRoleRepository,
            Ivw_LookupGridUserGroupRepository vw_LookupGridUserGroupRepository,
            Ivw_LookupGridUserAccessTypeRepository vw_LookupGridUserAccessTypeRepository,
            Ivw_LookupGridUserTypeRepository vw_LookupGridUserTypeRepository,
            Ivw_LookupRoleRepository vw_LookupRoleRepository,
            Ivw_LookupTemplateTypeRepository vw_LookupTemplateTypeRepository,
            Ivw_LookupUsersRepository vw_LookupUsersRepository,
            Ivw_LookupUserTypeRepository vw_LookupUserTypeRepository,
            Ivw_TemplateGridRepository vw_TemplateGridRepository,
            Ivw_ExportLogGridRepository vw_ExportLogGridRepository,
            Ivw_UserGridRepository vw_UserGridRepository,
            Ivw_LookupTypeGridRepository vw_LookupTypeGridRepository,
            Ivw_LookupMenuOptionRepository vw_LookupMenuOptionRepository,
            Ivw_LookupListRepository vw_LookupListRepository,
            Ivw_LookupDashboardOptionRepository vw_LookupDashboardOptionRepository,
            Ivw_LookupUserGroupRepository vw_LookupUserGroupRepository,
            IUserExportLogRepository UserExportLogRepository,
            Ivw_LookupTemplateAllowedTypeRepository vw_LookupTemplateAllowedTypeRepository,
            Ivw_LookupGridEmailTemplateTypeRepository vw_LookupGridEmailTemplateTypeRepository,
            Ivw_LookupGridEmailTypeRepository vw_LookupGridEmailTypeRepository,
            Ivw_LookupGridTemplateAllowedTypeRepository vw_LookupGridTemplateAllowedTypeRepository,
            Ivw_LookupEmailTemplateTypeRepository vw_LookupEmailTemplateTypeRepository,
            Ivw_LookupGridAlertTypeRepository vw_LookupGridAlertTypeRepository,
            Ivw_LookupEmailTypeRepository vw_LookupEmailTypeRepository,
            Ivw_LookupAlertTypeRepository vw_LookupAlertTypeRepository,
            Ivw_LookupAccessTypeRepository vw_LookupAccessTypeRepository,
            Ivw_LookupObjectRepository vw_LookupObjectRepository,
            Ivw_LookupObjectTypeRepository vw_LookupObjectTypeRepository,
            ITemplateTagRepository TemplateTagRepository,
            Ivw_LookupDashboardObjectTypeRepository vw_LookupDashboardObjectTypeRepository,
            Ivw_LookupDashboardObjectRepository vw_LookupDashboardObjectRepository,
            Ivw_LookupSettingRepository vw_LookupSettingRepository,


            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.AttachmentRepository = AttachmentRepository;
            this.SafeIPsRepository = SafeIPsRepository;
            this.InternalFilterHeaderRepository = InternalFilterHeaderRepository;
            this.InternalGridSettingRepository = InternalGridSettingRepository;
            this.InternalGridSettingDefaultRepository = InternalGridSettingDefaultRepository;
            this.InternalReportRepository = InternalReportRepository;
            this.InternalReportFieldRepository = InternalReportFieldRepository;
            this.InterviewAppointmentRepository = InterviewAppointmentRepository;
            this.LookupRepository = LookupRepository;
            this.LookupTypeRepository = LookupTypeRepository;
            this.MessageRepository = MessageRepository;
            this.TemplateRepository = TemplateRepository;
            this.vw_LookupGridRepository = vw_LookupGridRepository;
            this.vw_LookupFromEmailAddressRepository = vw_LookupFromEmailAddressRepository;
            this.vw_LookupGridUsersRepository = vw_LookupGridUsersRepository;
            this.vw_LookupGridRoleRepository = vw_LookupGridRoleRepository;
            this.vw_LookupGridUserGroupRepository = vw_LookupGridUserGroupRepository;
            this.vw_LookupGridUserAccessTypeRepository = vw_LookupGridUserAccessTypeRepository;
            this.vw_LookupGridUserTypeRepository = vw_LookupGridUserTypeRepository;
            this.vw_LookupRoleRepository = vw_LookupRoleRepository;
            this.vw_LookupTemplateTypeRepository = vw_LookupTemplateTypeRepository;
            this.vw_LookupUsersSignatureRepository = vw_LookupUsersSignatureRepository;
            this.vw_LookupUsersRepository = vw_LookupUsersRepository;
            this.vw_LookupMenuOptionGroupRepository = vw_LookupMenuOptionGroupRepository;
            this.vw_LookupUserTypeRepository = vw_LookupUserTypeRepository;
            this.vw_TemplateGridRepository = vw_TemplateGridRepository;
            this.vw_ExportLogGridRepository = vw_ExportLogGridRepository;
            this.vw_LookupDashboardOptionGroupRepository = vw_LookupDashboardOptionGroupRepository;     
            this.vw_UserGridRepository = vw_UserGridRepository;
            this.vw_LookupUserAccessTypeRepository = vw_LookupUserAccessTypeRepository;
            this.vw_LookupTypeGridRepository = vw_LookupTypeGridRepository;
            this.vw_LookupMenuOptionRepository = vw_LookupMenuOptionRepository;
            this.vw_LookupListRepository = vw_LookupListRepository;
            this.vw_LookupDashboardOptionRepository = vw_LookupDashboardOptionRepository;
            this.UserExportLogRepository = UserExportLogRepository;
            this.vw_LookupUserGroupRepository = vw_LookupUserGroupRepository;
            this.vw_LookupTemplateAllowedTypeRepository = vw_LookupTemplateAllowedTypeRepository;
            this.vw_LookupGridEmailTemplateTypeRepository = vw_LookupGridEmailTemplateTypeRepository;
            this.vw_LookupGridEmailTypeRepository = vw_LookupGridEmailTypeRepository;
            this.vw_LookupGridTemplateAllowedTypeRepository = vw_LookupGridTemplateAllowedTypeRepository;
            this.vw_LookupEmailTemplateTypeRepository = vw_LookupEmailTemplateTypeRepository;
            this.vw_LookupGridAlertTypeRepository = vw_LookupGridAlertTypeRepository;
            this.vw_LookupEmailTypeRepository = vw_LookupEmailTypeRepository;
            this.vw_LookupAlertTypeRepository = vw_LookupAlertTypeRepository;
            this.vw_LookupAccessTypeRepository = vw_LookupAccessTypeRepository;
            this.vw_LookupObjectRepository = vw_LookupObjectRepository;
            this.vw_LookupObjectTypeRepository = vw_LookupObjectTypeRepository;
            this.TemplateTagRepository = TemplateTagRepository;
            this.vw_LookupDashboardObjectTypeRepository = vw_LookupDashboardObjectTypeRepository;
            this.vw_LookupDashboardObjectRepository = vw_LookupDashboardObjectRepository;
            this.vw_LookupSettingRepository = vw_LookupSettingRepository;
        }

        public void Save()
        {
            //unitOfWork.Commit(1, 1);
            unitOfWork.Commit(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        #endregion Initialization

        #region Attachment

        #region Sync Methods

        public Attachment SelectByAttachmentId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return AttachmentRepository.SelectById(lookupTypeId);
            }
            else
            {
                Attachment lookupType = CacheService.Get<Attachment>("SelectByAttachmentId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = AttachmentRepository.SelectById(lookupTypeId);
                    CacheService.Add<Attachment>("SelectByAttachmentId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByAttachmentId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<Attachment> SelectMany_Attachment(Expression<Func<Attachment, bool>> where)
        {
            return AttachmentRepository.SelectMany(where);
        }

        public Attachment SelectSingle_Attachment(Expression<Func<Attachment, bool>> where)
        {
            return AttachmentRepository.Select(where);
        }

        public IEnumerable<Attachment> SelectAllAttachments()
        {
            return AttachmentRepository.SelectAll();
        }

        public bool SaveAttachmentForm(Attachment lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    AttachmentRepository.Add(lookupType);
                }
                else
                {
                    AttachmentRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteAttachmentForm(int lookupTypeId)
        {
            try
            {
                Attachment lookupType = AttachmentRepository.SelectById(lookupTypeId);
                AttachmentRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<Attachment> SelectByAttachmentIdAsync(int lookupTypeId)
        {
            return AttachmentRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<Attachment>> SelectMany_AttachmentAsync(Expression<Func<Attachment, bool>> where)
        {
            return AttachmentRepository.SelectManyAsync(where);
        }

        public Task<Attachment> SelectSingle_AttachmentAsync(Expression<Func<Attachment, bool>> where)
        {
            return AttachmentRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Attachment>> SelectAllAttachmentsAsync()
        {
            return AttachmentRepository.SelectAllAsync();
        }

        public Task<int> SaveAttachmentFormAsync(Attachment lookupType)
        {
            try
            {
                if (lookupType.AttachmentId == 0)
                {
                    AttachmentRepository.Add(lookupType);
                }
                else
                {
                    AttachmentRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteAttachmentFormAsync(int lookupTypeId)
        {
            try
            {
                Attachment lookupType = AttachmentRepository.SelectById(lookupTypeId);
                AttachmentRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Attachment

        #region SafeIPs

        #region Sync Methods

        public SafeIPs SelectBySafeIPsId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return SafeIPsRepository.SelectById(lookupTypeId);
            }
            else
            {
                SafeIPs lookupType = CacheService.Get<SafeIPs>("SelectBySafeIPsId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = SafeIPsRepository.SelectById(lookupTypeId);
                    CacheService.Add<SafeIPs>("SelectBySafeIPsId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectBySafeIPsId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<SafeIPs> SelectMany_SafeIPs(Expression<Func<SafeIPs, bool>> where)
        {
            return SafeIPsRepository.SelectMany(where);
        }

        public SafeIPs SelectSingle_SafeIPs(Expression<Func<SafeIPs, bool>> where)
        {
            return SafeIPsRepository.Select(where);
        }

        public IEnumerable<SafeIPs> SelectAllSafeIPss()
        {
            return SafeIPsRepository.SelectAll();
        }

        public bool SaveSafeIPsForm(SafeIPs lookupType)
        {
            try
            {
                if (lookupType.SafeIPsId == 0)
                {
                    SafeIPsRepository.Add(lookupType);
                }
                else
                {
                    SafeIPsRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteSafeIPsForm(int lookupTypeId)
        {
            try
            {
                SafeIPs lookupType = SafeIPsRepository.SelectById(lookupTypeId);
                SafeIPsRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<SafeIPs> SelectBySafeIPsIdAsync(int lookupTypeId)
        {
            return SafeIPsRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<SafeIPs>> SelectMany_SafeIPsAsync(Expression<Func<SafeIPs, bool>> where)
        {
            return SafeIPsRepository.SelectManyAsync(where);
        }

        public Task<SafeIPs> SelectSingle_SafeIPsAsync(Expression<Func<SafeIPs, bool>> where)
        {
            return SafeIPsRepository.SelectAsync(where);
        }

        public Task<IEnumerable<SafeIPs>> SelectAllSafeIPssAsync()
        {
            return SafeIPsRepository.SelectAllAsync();
        }

        public Task<int> SaveSafeIPsFormAsync(SafeIPs lookupType)
        {
            try
            {
                if (lookupType.SafeIPsId == 0)
                {
                    SafeIPsRepository.Add(lookupType);
                }
                else
                {
                    SafeIPsRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteSafeIPsFormAsync(int lookupTypeId)
        {
            try
            {
                SafeIPs lookupType = SafeIPsRepository.SelectById(lookupTypeId);
                SafeIPsRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion SafeIPs

        #region InternalFilterHeader

        #region Sync Methods

        public InternalFilterHeader SelectByInternalFilterHeaderId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return InternalFilterHeaderRepository.SelectById(lookupTypeId);
            }
            else
            {
                InternalFilterHeader lookupType = CacheService.Get<InternalFilterHeader>("SelectByInternalFilterHeaderId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = InternalFilterHeaderRepository.SelectById(lookupTypeId);
                    CacheService.Add<InternalFilterHeader>("SelectByInternalFilterHeaderId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByInternalFilterHeaderId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<InternalFilterHeader> SelectMany_InternalFilterHeader(Expression<Func<InternalFilterHeader, bool>> where)
        {
            return InternalFilterHeaderRepository.SelectMany(where);
        }

        public InternalFilterHeader SelectSingle_InternalFilterHeader(Expression<Func<InternalFilterHeader, bool>> where)
        {
            return InternalFilterHeaderRepository.Select(where);
        }

        public IEnumerable<InternalFilterHeader> SelectAllInternalFilterHeaders()
        {
            return InternalFilterHeaderRepository.SelectAll();
        }

        public bool SaveInternalFilterHeaderForm(InternalFilterHeader lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    InternalFilterHeaderRepository.Add(lookupType);
                }
                else
                {
                    InternalFilterHeaderRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteInternalFilterHeaderForm(int lookupTypeId)
        {
            try
            {
                InternalFilterHeader lookupType = InternalFilterHeaderRepository.SelectById(lookupTypeId);
                InternalFilterHeaderRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<InternalFilterHeader> SelectByInternalFilterHeaderIdAsync(int lookupTypeId)
        {
            return InternalFilterHeaderRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<InternalFilterHeader>> SelectMany_InternalFilterHeaderAsync(Expression<Func<InternalFilterHeader, bool>> where)
        {
            return InternalFilterHeaderRepository.SelectManyAsync(where);
        }

        public Task<InternalFilterHeader> SelectSingle_InternalFilterHeaderAsync(Expression<Func<InternalFilterHeader, bool>> where)
        {
            return InternalFilterHeaderRepository.SelectAsync(where);
        }

        public Task<IEnumerable<InternalFilterHeader>> SelectAllInternalFilterHeadersAsync()
        {
            return InternalFilterHeaderRepository.SelectAllAsync();
        }

        public Task<int> SaveInternalFilterHeaderFormAsync(InternalFilterHeader lookupType)
        {
            try
            {
                if (lookupType.InternalFilterHeaderId == 0)
                {
                    InternalFilterHeaderRepository.Add(lookupType);
                }
                else
                {
                    InternalFilterHeaderRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteInternalFilterHeaderFormAsync(int lookupTypeId)
        {
            try
            {
                InternalFilterHeader lookupType = InternalFilterHeaderRepository.SelectById(lookupTypeId);
                InternalFilterHeaderRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion InternalFilterHeader

        #region InternalGridSetting

        #region Sync Methods

        public InternalGridSetting SelectByInternalGridSettingId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return InternalGridSettingRepository.SelectById(lookupTypeId);
            }
            else
            {
                InternalGridSetting lookupType = CacheService.Get<InternalGridSetting>("SelectByInternalGridSettingId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = InternalGridSettingRepository.SelectById(lookupTypeId);
                    CacheService.Add<InternalGridSetting>("SelectByInternalGridSettingId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByInternalGridSettingId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<InternalGridSetting> SelectMany_InternalGridSetting(Expression<Func<InternalGridSetting, bool>> where)
        {
            return InternalGridSettingRepository.SelectMany(where);
        }

        public InternalGridSetting SelectSingle_InternalGridSetting(Expression<Func<InternalGridSetting, bool>> where)
        {
            return InternalGridSettingRepository.Select(where);
        }

        public IEnumerable<InternalGridSetting> SelectAllInternalGridSettings()
        {
            return InternalGridSettingRepository.SelectAll();
        }

        public bool SaveInternalGridSettingForm(InternalGridSetting lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    InternalGridSettingRepository.Add(lookupType);
                }
                else
                {
                    InternalGridSettingRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteInternalGridSettingForm(int lookupTypeId)
        {
            try
            {
                InternalGridSetting lookupType = InternalGridSettingRepository.SelectById(lookupTypeId);
                InternalGridSettingRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<InternalGridSetting> SelectByInternalGridSettingIdAsync(int lookupTypeId)
        {
            return InternalGridSettingRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<InternalGridSetting>> SelectMany_InternalGridSettingAsync(Expression<Func<InternalGridSetting, bool>> where)
        {
            return InternalGridSettingRepository.SelectManyAsync(where);
        }

        public Task<InternalGridSetting> SelectSingle_InternalGridSettingAsync(Expression<Func<InternalGridSetting, bool>> where)
        {
            return InternalGridSettingRepository.SelectAsync(where);
        }

        public Task<IEnumerable<InternalGridSetting>> SelectAllInternalGridSettingsAsync()
        {
            return InternalGridSettingRepository.SelectAllAsync();
        }

        public Task<int> SaveInternalGridSettingFormAsync(InternalGridSetting lookupType)
        {
            try
            {
                if (lookupType.InternalGridSettingId == 0)
                {
                    InternalGridSettingRepository.Add(lookupType);
                }
                else
                {
                    InternalGridSettingRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteInternalGridSettingFormAsync(int lookupTypeId)
        {
            try
            {
                InternalGridSetting lookupType = InternalGridSettingRepository.SelectById(lookupTypeId);
                InternalGridSettingRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion InternalGridSetting

        #region InternalGridSettingDefault

        #region Sync Methods

        public InternalGridSettingDefault SelectByInternalGridSettingDefaultId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return InternalGridSettingDefaultRepository.SelectById(lookupTypeId);
            }
            else
            {
                InternalGridSettingDefault lookupType = CacheService.Get<InternalGridSettingDefault>("SelectByInternalGridSettingDefaultId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = InternalGridSettingDefaultRepository.SelectById(lookupTypeId);
                    CacheService.Add<InternalGridSettingDefault>("SelectByInternalGridSettingDefaultId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByInternalGridSettingDefaultId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<InternalGridSettingDefault> SelectMany_InternalGridSettingDefault(Expression<Func<InternalGridSettingDefault, bool>> where)
        {
            return InternalGridSettingDefaultRepository.SelectMany(where);
        }

        public InternalGridSettingDefault SelectSingle_InternalGridSettingDefault(Expression<Func<InternalGridSettingDefault, bool>> where)
        {
            return InternalGridSettingDefaultRepository.Select(where);
        }

        public IEnumerable<InternalGridSettingDefault> SelectAllInternalGridSettingDefaults()
        {
            return InternalGridSettingDefaultRepository.SelectAll();
        }

        public bool SaveInternalGridSettingDefaultForm(InternalGridSettingDefault lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    InternalGridSettingDefaultRepository.Add(lookupType);
                }
                else
                {
                    InternalGridSettingDefaultRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteInternalGridSettingDefaultForm(int lookupTypeId)
        {
            try
            {
                InternalGridSettingDefault lookupType = InternalGridSettingDefaultRepository.SelectById(lookupTypeId);
                InternalGridSettingDefaultRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<InternalGridSettingDefault> SelectByInternalGridSettingDefaultIdAsync(int lookupTypeId)
        {
            return InternalGridSettingDefaultRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<InternalGridSettingDefault>> SelectMany_InternalGridSettingDefaultAsync(Expression<Func<InternalGridSettingDefault, bool>> where)
        {
            return InternalGridSettingDefaultRepository.SelectManyAsync(where);
        }

        public Task<InternalGridSettingDefault> SelectSingle_InternalGridSettingDefaultAsync(Expression<Func<InternalGridSettingDefault, bool>> where)
        {
            return InternalGridSettingDefaultRepository.SelectAsync(where);
        }

        public Task<IEnumerable<InternalGridSettingDefault>> SelectAllInternalGridSettingDefaultsAsync()
        {
            return InternalGridSettingDefaultRepository.SelectAllAsync();
        }

        public Task<int> SaveInternalGridSettingDefaultFormAsync(InternalGridSettingDefault lookupType)
        {
            try
            {
                if (lookupType.InternalGridSettingDefaultId == 0)
                {
                    InternalGridSettingDefaultRepository.Add(lookupType);
                }
                else
                {
                    InternalGridSettingDefaultRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteInternalGridSettingDefaultFormAsync(int lookupTypeId)
        {
            try
            {
                InternalGridSettingDefault lookupType = InternalGridSettingDefaultRepository.SelectById(lookupTypeId);
                InternalGridSettingDefaultRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion InternalGridSettingDefault

        #region InternalReport

        #region Sync Methods

        public InternalReport SelectByInternalReportId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return InternalReportRepository.SelectById(lookupTypeId);
            }
            else
            {
                InternalReport lookupType = CacheService.Get<InternalReport>("SelectByInternalReportId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = InternalReportRepository.SelectById(lookupTypeId);
                    CacheService.Add<InternalReport>("SelectByInternalReportId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByInternalReportId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<InternalReport> SelectMany_InternalReport(Expression<Func<InternalReport, bool>> where)
        {
            return InternalReportRepository.SelectMany(where);
        }

        public InternalReport SelectSingle_InternalReport(Expression<Func<InternalReport, bool>> where)
        {
            return InternalReportRepository.Select(where);
        }

        public IEnumerable<InternalReport> SelectAllInternalReports()
        {
            return InternalReportRepository.SelectAll();
        }

        public bool SaveInternalReportForm(InternalReport lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    InternalReportRepository.Add(lookupType);
                }
                else
                {
                    InternalReportRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteInternalReportForm(int lookupTypeId)
        {
            try
            {
                InternalReport lookupType = InternalReportRepository.SelectById(lookupTypeId);
                InternalReportRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<InternalReport> SelectByInternalReportIdAsync(int lookupTypeId)
        {
            return InternalReportRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<InternalReport>> SelectMany_InternalReportAsync(Expression<Func<InternalReport, bool>> where)
        {
            return InternalReportRepository.SelectManyAsync(where);
        }

        public Task<InternalReport> SelectSingle_InternalReportAsync(Expression<Func<InternalReport, bool>> where)
        {
            return InternalReportRepository.SelectAsync(where);
        }

        public Task<IEnumerable<InternalReport>> SelectAllInternalReportsAsync()
        {
            return InternalReportRepository.SelectAllAsync();
        }

        public Task<int> SaveInternalReportFormAsync(InternalReport lookupType)
        {
            try
            {
                if (lookupType.InternalReportId == 0)
                {
                    InternalReportRepository.Add(lookupType);
                }
                else
                {
                    InternalReportRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteInternalReportFormAsync(int lookupTypeId)
        {
            try
            {
                InternalReport lookupType = InternalReportRepository.SelectById(lookupTypeId);
                InternalReportRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion InternalReport

        #region InternalReportField

        public List<InternalReportField> SelectInternalReportFieldsByGridSetting(GridSetting gridSetting)
        {
            return InternalReportFieldRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<InternalReportField>> SelectInternalReportFieldsByGridSettingAsync(GridSetting gridSetting)
        {
            return InternalReportFieldRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<InternalReportField> SelectAllInternalReportFields()
        {
            return InternalReportFieldRepository.SelectAll();
        }
        public Task<IEnumerable<InternalReportField>> SelectAllInternalReportFieldsAsync()
        {
            return InternalReportFieldRepository.SelectAllAsync();
        }
        public IEnumerable<InternalReportField> SelectMany_InternalReportField(Expression<Func<InternalReportField, bool>> where)
        {
            return InternalReportFieldRepository.SelectMany(where);
        }
        public Task<IEnumerable<InternalReportField>> SelectMany_InternalReportFieldAsync(Expression<Func<InternalReportField, bool>> where)
        {
            return InternalReportFieldRepository.SelectManyAsync(where);
        }
        public InternalReportField SelectSingle_InternalReportField(Expression<Func<InternalReportField, bool>> where)
        {
            return InternalReportFieldRepository.Select(where);
        }
        public Task<InternalReportField> SelectSingle_InternalReportFieldAsync(Expression<Func<InternalReportField, bool>> where)
        {
            return InternalReportFieldRepository.SelectAsync(where);
        }

        #endregion InternalReportField

        #region InterviewAppointment

        #region Sync Methods

        public InterviewAppointment SelectByInterviewAppointmentId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return InterviewAppointmentRepository.SelectById(lookupTypeId);
            }
            else
            {
                InterviewAppointment lookupType = CacheService.Get<InterviewAppointment>("SelectByInterviewAppointmentId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = InterviewAppointmentRepository.SelectById(lookupTypeId);
                    CacheService.Add<InterviewAppointment>("SelectByInterviewAppointmentId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByInterviewAppointmentId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<InterviewAppointment> SelectMany_InterviewAppointment(Expression<Func<InterviewAppointment, bool>> where)
        {
            return InterviewAppointmentRepository.SelectMany(where);
        }

        public InterviewAppointment SelectSingle_InterviewAppointment(Expression<Func<InterviewAppointment, bool>> where)
        {
            return InterviewAppointmentRepository.Select(where);
        }

        public IEnumerable<InterviewAppointment> SelectAllInterviewAppointments()
        {
            return InterviewAppointmentRepository.SelectAll();
        }

        public bool SaveInterviewAppointmentForm(InterviewAppointment lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    InterviewAppointmentRepository.Add(lookupType);
                }
                else
                {
                    InterviewAppointmentRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteInterviewAppointmentForm(int lookupTypeId)
        {
            try
            {
                InterviewAppointment lookupType = InterviewAppointmentRepository.SelectById(lookupTypeId);
                InterviewAppointmentRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<InterviewAppointment> SelectByInterviewAppointmentIdAsync(int lookupTypeId)
        {
            return InterviewAppointmentRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<InterviewAppointment>> SelectMany_InterviewAppointmentAsync(Expression<Func<InterviewAppointment, bool>> where)
        {
            return InterviewAppointmentRepository.SelectManyAsync(where);
        }

        public Task<InterviewAppointment> SelectSingle_InterviewAppointmentAsync(Expression<Func<InterviewAppointment, bool>> where)
        {
            return InterviewAppointmentRepository.SelectAsync(where);
        }

        public Task<IEnumerable<InterviewAppointment>> SelectAllInterviewAppointmentsAsync()
        {
            return InterviewAppointmentRepository.SelectAllAsync();
        }

        public Task<int> SaveInterviewAppointmentFormAsync(InterviewAppointment lookupType)
        {
            try
            {
                if (lookupType.InterviewAppointmentId == 0)
                {
                    InterviewAppointmentRepository.Add(lookupType);
                }
                else
                {
                    InterviewAppointmentRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteInterviewAppointmentFormAsync(int lookupTypeId)
        {
            try
            {
                InterviewAppointment lookupType = InterviewAppointmentRepository.SelectById(lookupTypeId);
                InterviewAppointmentRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion InterviewAppointment

        #region Lookup

        #region Sync Methods

        public Lookup SelectByLookupId(int lookupId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return LookupRepository.SelectById(lookupId);
            }
            else
            {
                Lookup lookup = CacheService.Get<Lookup>("SelectByLookupId" + lookupId);
                if (lookup == null)
                {
                    lookup = LookupRepository.SelectById(lookupId);
                    CacheService.Add<Lookup>("SelectByLookupId" + lookupId, lookup);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByLookupId" + lookupId);
                }
                return lookup;
            }
        }

        public IEnumerable<Lookup> SelectMany_Lookup(Expression<Func<Lookup, bool>> where)
        {
            return LookupRepository.SelectMany(where);
        }

        public Lookup SelectSingle_Lookup(Expression<Func<Lookup, bool>> where)
        {
            return LookupRepository.Select(where);
        }

        public IEnumerable<Lookup> SelectAllLookups()
        {
            return LookupRepository.SelectAll();
        }

        public bool SaveLookupForm(Lookup lookup)
        {
            try
            {
                if (lookup.FormMode == FormMode.Create)
                {
                    LookupRepository.Add(lookup);
                }
                else
                {
                    LookupRepository.Update(lookup);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteLookupForm(int lookupId)
        {
            try
            {
                Lookup lookup = LookupRepository.SelectById(lookupId);
                LookupRepository.Delete(lookup);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<Lookup> SelectByLookupIdAsync(int lookupId)
        {
            return LookupRepository.SelectByIdAsync(lookupId);
        }

        public Task<IEnumerable<Lookup>> SelectMany_LookupAsync(Expression<Func<Lookup, bool>> where)
        {
            return LookupRepository.SelectManyAsync(where);
        }

        public Task<Lookup> SelectSingle_LookupAsync(Expression<Func<Lookup, bool>> where)
        {
            return LookupRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Lookup>> SelectAllLookupsAsync()
        {
            return LookupRepository.SelectAllAsync();
        }

        public Task<int> SaveLookupFormAsync(Lookup lookup)
        {
            try
            {
                if (lookup.LookupId == 0)
                {
                    LookupRepository.Add(lookup);
                }
                else
                {
                    LookupRepository.Update(lookup);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteLookupFormAsync(int lookupId)
        {
            try
            {
                Lookup lookup = LookupRepository.SelectById(lookupId);
                LookupRepository.Delete(lookup);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Lookup       

        #region LookupType

        #region Sync Methods

        public LookupType SelectByLookupTypeId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return LookupTypeRepository.SelectById(lookupTypeId);
            }
            else
            {
                LookupType lookupType = CacheService.Get<LookupType>("SelectByLookupTypeId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = LookupTypeRepository.SelectById(lookupTypeId);
                    CacheService.Add<LookupType>("SelectByLookupTypeId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByLookupTypeId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<LookupType> SelectMany_LookupType(Expression<Func<LookupType, bool>> where)
        {
            return LookupTypeRepository.SelectMany(where);
        }

        public LookupType SelectSingle_LookupType(Expression<Func<LookupType, bool>> where)
        {
            return LookupTypeRepository.Select(where);
        }

        public IEnumerable<LookupType> SelectAllLookupTypes()
        {
            return LookupTypeRepository.SelectAll();
        }

        public bool SaveLookupTypeForm(LookupType lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    LookupTypeRepository.Add(lookupType);
                }
                else
                {
                    LookupTypeRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteLookupTypeForm(int lookupTypeId)
        {
            try
            {
                LookupType lookupType = LookupTypeRepository.SelectById(lookupTypeId);
                LookupTypeRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<LookupType> SelectByLookupTypeIdAsync(int lookupTypeId)
        {
            return LookupTypeRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<LookupType>> SelectMany_LookupTypeAsync(Expression<Func<LookupType, bool>> where)
        {
            return LookupTypeRepository.SelectManyAsync(where);
        }

        public Task<LookupType> SelectSingle_LookupTypeAsync(Expression<Func<LookupType, bool>> where)
        {
            return LookupTypeRepository.SelectAsync(where);
        }

        public Task<IEnumerable<LookupType>> SelectAllLookupTypesAsync()
        {
            return LookupTypeRepository.SelectAllAsync();
        }

        public Task<int> SaveLookupTypeFormAsync(LookupType lookupType)
        {
            try
            {
                if (lookupType.LookupTypeId == 0)
                {
                    LookupTypeRepository.Add(lookupType);
                }
                else
                {
                    LookupTypeRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteLookupTypeFormAsync(int lookupTypeId)
        {
            try
            {
                LookupType lookupType = LookupTypeRepository.SelectById(lookupTypeId);
                LookupTypeRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion LookupType

        #region Message

        #region Sync Methods

        public Message SelectByMessageId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return MessageRepository.SelectById(lookupTypeId);
            }
            else
            {
                Message lookupType = CacheService.Get<Message>("SelectByMessageId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = MessageRepository.SelectById(lookupTypeId);
                    CacheService.Add<Message>("SelectByMessageId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByMessageId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<Message> SelectMany_Message(Expression<Func<Message, bool>> where)
        {
            return MessageRepository.SelectMany(where);
        }

        public Message SelectSingle_Message(Expression<Func<Message, bool>> where)
        {
            return MessageRepository.Select(where);
        }

        public IEnumerable<Message> SelectAllMessages()
        {
            return MessageRepository.SelectAll();
        }

        public bool SaveMessageForm(Message lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    MessageRepository.Add(lookupType);
                }
                else
                {
                    MessageRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteMessageForm(int lookupTypeId)
        {
            try
            {
                Message lookupType = MessageRepository.SelectById(lookupTypeId);
                MessageRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<Message> SelectByMessageIdAsync(int lookupTypeId)
        {
            return MessageRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<Message>> SelectMany_MessageAsync(Expression<Func<Message, bool>> where)
        {
            return MessageRepository.SelectManyAsync(where);
        }

        public Task<Message> SelectSingle_MessageAsync(Expression<Func<Message, bool>> where)
        {
            return MessageRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Message>> SelectAllMessagesAsync()
        {
            return MessageRepository.SelectAllAsync();
        }

        public Task<int> SaveMessageFormAsync(Message lookupType)
        {
            try
            {
                if (lookupType.MessageId == 0)
                {
                    MessageRepository.Add(lookupType);
                }
                else
                {
                    MessageRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteMessageFormAsync(int lookupTypeId)
        {
            try
            {
                Message lookupType = MessageRepository.SelectById(lookupTypeId);
                MessageRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Message

        #region Template

        #region Sync Methods

        public Template SelectByTemplateId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TemplateRepository.SelectById(lookupTypeId);
            }
            else
            {
                Template lookupType = CacheService.Get<Template>("SelectByTemplateId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TemplateRepository.SelectById(lookupTypeId);
                    CacheService.Add<Template>("SelectByTemplateId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTemplateId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<Template> SelectMany_Template(Expression<Func<Template, bool>> where)
        {
            return TemplateRepository.SelectMany(where);
        }

        public Template SelectSingle_Template(Expression<Func<Template, bool>> where)
        {
            return TemplateRepository.Select(where);
        }

        public IEnumerable<Template> SelectAllTemplates()
        {
            return TemplateRepository.SelectAll();
        }

        public bool SaveTemplateForm(Template lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TemplateRepository.Add(lookupType);
                }
                else
                {
                    TemplateRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTemplateForm(int lookupTypeId)
        {
            try
            {
                Template lookupType = TemplateRepository.SelectById(lookupTypeId);
                TemplateRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<Template> SelectByTemplateIdAsync(int lookupTypeId)
        {
            return TemplateRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<Template>> SelectMany_TemplateAsync(Expression<Func<Template, bool>> where)
        {
            return TemplateRepository.SelectManyAsync(where);
        }

        public Task<Template> SelectSingle_TemplateAsync(Expression<Func<Template, bool>> where)
        {
            return TemplateRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Template>> SelectAllTemplatesAsync()
        {
            return TemplateRepository.SelectAllAsync();
        }

        public Task<int> SaveTemplateFormAsync(Template lookupType)
        {
            try
            {
                if (lookupType.TemplateId == 0)
                {
                    TemplateRepository.Add(lookupType);
                }
                else
                {
                    TemplateRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTemplateFormAsync(int lookupTypeId)
        {
            try
            {
                Template lookupType = TemplateRepository.SelectById(lookupTypeId);
                TemplateRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Template

        #region vw_LookupGrid

        public List<vw_LookupGrid> Selectvw_LookupGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGrid>> Selectvw_LookupGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGrid> SelectAllvw_LookupGrids()
        {
            return vw_LookupGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGrid>> SelectAllvw_LookupGridsAsync()
        {
            return vw_LookupGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGrid> SelectMany_vw_LookupGrid(Expression<Func<vw_LookupGrid, bool>> where)
        {
            return vw_LookupGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGrid>> SelectMany_vw_LookupGridAsync(Expression<Func<vw_LookupGrid, bool>> where)
        {
            return vw_LookupGridRepository.SelectManyAsync(where);
        }
        public vw_LookupGrid SelectSingle_vw_LookupGrid(Expression<Func<vw_LookupGrid, bool>> where)
        {
            return vw_LookupGridRepository.Select(where);
        }
        public Task<vw_LookupGrid> SelectSingle_vw_LookupGridAsync(Expression<Func<vw_LookupGrid, bool>> where)
        {
            return vw_LookupGridRepository.SelectAsync(where);
        }

        #endregion vw_LookupGrid

        #region vw_LookupUserGroup

        public List<vw_LookupUserGroup> Selectvw_LookupUserGroupsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUserGroupRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUserGroup>> Selectvw_LookupUserGroupsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUserGroupRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUserGroup> SelectAllvw_LookupUserGroups()
        {
            return vw_LookupUserGroupRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupUserGroupsAsync()
        {
            var lookup = await vw_LookupUserGroupRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.UserGroupId,
                LookupValue = x.UserGroupName,
                AspNetRoleId = x.AspNetRoleId,
                disabled = false,
                GroupBy = "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupUserGroup> SelectMany_vw_LookupUserGroup(Expression<Func<vw_LookupUserGroup, bool>> where)
        {
            return vw_LookupUserGroupRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUserGroup>> SelectMany_vw_LookupUserGroupAsync(Expression<Func<vw_LookupUserGroup, bool>> where)
        {
            return vw_LookupUserGroupRepository.SelectManyAsync(where);
        }
        public vw_LookupUserGroup SelectSingle_vw_LookupUserGroup(Expression<Func<vw_LookupUserGroup, bool>> where)
        {
            return vw_LookupUserGroupRepository.Select(where);
        }
        public Task<vw_LookupUserGroup> SelectSingle_vw_LookupUserGroupAsync(Expression<Func<vw_LookupUserGroup, bool>> where)
        {
            return vw_LookupUserGroupRepository.SelectAsync(where);
        }

        #endregion vw_LookupUserGroup

        #region vw_LookupDashboardOptionGroup

        public List<vw_LookupDashboardOptionGroup> Selectvw_LookupDashboardOptionGroupsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupDashboardOptionGroupRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupDashboardOptionGroup>> Selectvw_LookupDashboardOptionGroupsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupDashboardOptionGroupRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupDashboardOptionGroup> SelectAllvw_LookupDashboardOptionGroups()
        {
            return vw_LookupDashboardOptionGroupRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupDashboardOptionGroup>> SelectAllvw_LookupDashboardOptionGroupsAsync()
        {
            return vw_LookupDashboardOptionGroupRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupDashboardOptionGroup> SelectMany_vw_LookupDashboardOptionGroup(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where)
        {
            return vw_LookupDashboardOptionGroupRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupDashboardOptionGroup>> SelectMany_vw_LookupDashboardOptionGroupAsync(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where)
        {
            return vw_LookupDashboardOptionGroupRepository.SelectManyAsync(where);
        }
        public vw_LookupDashboardOptionGroup SelectSingle_vw_LookupDashboardOptionGroup(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where)
        {
            return vw_LookupDashboardOptionGroupRepository.Select(where);
        }
        public Task<vw_LookupDashboardOptionGroup> SelectSingle_vw_LookupDashboardOptionGroupAsync(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where)
        {
            return vw_LookupDashboardOptionGroupRepository.SelectAsync(where);
        }

        #endregion vw_LookupDashboardOptionGroup        

        #region vw_LookupUserAccessType

        public List<vw_LookupUserAccessType> Selectvw_LookupUserAccessTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUserAccessTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUserAccessType>> Selectvw_LookupUserAccessTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUserAccessTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUserAccessType> SelectAllvw_LookupUserAccessTypes()
        {
            return vw_LookupUserAccessTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUserAccessType>> SelectAllvw_LookupUserAccessTypesAsync()
        {
            return vw_LookupUserAccessTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUserAccessType> SelectMany_vw_LookupUserAccessType(Expression<Func<vw_LookupUserAccessType, bool>> where)
        {
            return vw_LookupUserAccessTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUserAccessType>> SelectMany_vw_LookupUserAccessTypeAsync(Expression<Func<vw_LookupUserAccessType, bool>> where)
        {
            return vw_LookupUserAccessTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupUserAccessType SelectSingle_vw_LookupUserAccessType(Expression<Func<vw_LookupUserAccessType, bool>> where)
        {
            return vw_LookupUserAccessTypeRepository.Select(where);
        }
        public Task<vw_LookupUserAccessType> SelectSingle_vw_LookupUserAccessTypeAsync(Expression<Func<vw_LookupUserAccessType, bool>> where)
        {
            return vw_LookupUserAccessTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupUserAccessType

        #region vw_LookupGridUsers

        public List<vw_LookupGridUsers> Selectvw_LookupGridUserssByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridUsersRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridUsers>> Selectvw_LookupGridUserssByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridUsersRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridUsers> SelectAllvw_LookupGridUserss()
        {
            return vw_LookupGridUsersRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridUsers>> SelectAllvw_LookupGridUserssAsync()
        {
            return vw_LookupGridUsersRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridUsers> SelectMany_vw_LookupGridUsers(Expression<Func<vw_LookupGridUsers, bool>> where)
        {
            return vw_LookupGridUsersRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridUsers>> SelectMany_vw_LookupGridUsersAsync(Expression<Func<vw_LookupGridUsers, bool>> where)
        {
            return vw_LookupGridUsersRepository.SelectManyAsync(where);
        }
        public vw_LookupGridUsers SelectSingle_vw_LookupGridUsers(Expression<Func<vw_LookupGridUsers, bool>> where)
        {
            return vw_LookupGridUsersRepository.Select(where);
        }
        public Task<vw_LookupGridUsers> SelectSingle_vw_LookupGridUsersAsync(Expression<Func<vw_LookupGridUsers, bool>> where)
        {
            return vw_LookupGridUsersRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridUsers

        #region vw_LookupGridRole

        public List<vw_LookupGridRole> Selectvw_LookupGridRolesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridRoleRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridRole>> Selectvw_LookupGridRolesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridRoleRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridRole> SelectAllvw_LookupGridRoles()
        {
            return vw_LookupGridRoleRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridRole>> SelectAllvw_LookupGridRolesAsync()
        {
            return vw_LookupGridRoleRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridRole> SelectMany_vw_LookupGridRole(Expression<Func<vw_LookupGridRole, bool>> where)
        {
            return vw_LookupGridRoleRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridRole>> SelectMany_vw_LookupGridRoleAsync(Expression<Func<vw_LookupGridRole, bool>> where)
        {
            return vw_LookupGridRoleRepository.SelectManyAsync(where);
        }
        public vw_LookupGridRole SelectSingle_vw_LookupGridRole(Expression<Func<vw_LookupGridRole, bool>> where)
        {
            return vw_LookupGridRoleRepository.Select(where);
        }
        public Task<vw_LookupGridRole> SelectSingle_vw_LookupGridRoleAsync(Expression<Func<vw_LookupGridRole, bool>> where)
        {
            return vw_LookupGridRoleRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridRole

        #region vw_LookupGridUserGroup

        public List<vw_LookupGridUserGroup> Selectvw_LookupGridUserGroupsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridUserGroupRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridUserGroup>> Selectvw_LookupGridUserGroupsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridUserGroupRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridUserGroup> SelectAllvw_LookupGridUserGroups()
        {
            return vw_LookupGridUserGroupRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridUserGroup>> SelectAllvw_LookupGridUserGroupsAsync()
        {
            return vw_LookupGridUserGroupRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridUserGroup> SelectMany_vw_LookupGridUserGroup(Expression<Func<vw_LookupGridUserGroup, bool>> where)
        {
            return vw_LookupGridUserGroupRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridUserGroup>> SelectMany_vw_LookupGridUserGroupAsync(Expression<Func<vw_LookupGridUserGroup, bool>> where)
        {
            return vw_LookupGridUserGroupRepository.SelectManyAsync(where);
        }
        public vw_LookupGridUserGroup SelectSingle_vw_LookupGridUserGroup(Expression<Func<vw_LookupGridUserGroup, bool>> where)
        {
            return vw_LookupGridUserGroupRepository.Select(where);
        }
        public Task<vw_LookupGridUserGroup> SelectSingle_vw_LookupGridUserGroupAsync(Expression<Func<vw_LookupGridUserGroup, bool>> where)
        {
            return vw_LookupGridUserGroupRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridUserGroup

        #region vw_LookupGridUserAccessType

        public List<vw_LookupGridUserAccessType> Selectvw_LookupGridUserAccessTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridUserAccessTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridUserAccessType>> Selectvw_LookupGridUserAccessTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridUserAccessTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridUserAccessType> SelectAllvw_LookupGridUserAccessTypes()
        {
            return vw_LookupGridUserAccessTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridUserAccessType>> SelectAllvw_LookupGridUserAccessTypesAsync()
        {
            return vw_LookupGridUserAccessTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridUserAccessType> SelectMany_vw_LookupGridUserAccessType(Expression<Func<vw_LookupGridUserAccessType, bool>> where)
        {
            return vw_LookupGridUserAccessTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridUserAccessType>> SelectMany_vw_LookupGridUserAccessTypeAsync(Expression<Func<vw_LookupGridUserAccessType, bool>> where)
        {
            return vw_LookupGridUserAccessTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridUserAccessType SelectSingle_vw_LookupGridUserAccessType(Expression<Func<vw_LookupGridUserAccessType, bool>> where)
        {
            return vw_LookupGridUserAccessTypeRepository.Select(where);
        }
        public Task<vw_LookupGridUserAccessType> SelectSingle_vw_LookupGridUserAccessTypeAsync(Expression<Func<vw_LookupGridUserAccessType, bool>> where)
        {
            return vw_LookupGridUserAccessTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridUserAccessType

        #region vw_LookupGridUserType

        public List<vw_LookupGridUserType> Selectvw_LookupGridUserTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridUserTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridUserType>> Selectvw_LookupGridUserTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridUserTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridUserType> SelectAllvw_LookupGridUserTypes()
        {
            return vw_LookupGridUserTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridUserType>> SelectAllvw_LookupGridUserTypesAsync()
        {
            return vw_LookupGridUserTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridUserType> SelectMany_vw_LookupGridUserType(Expression<Func<vw_LookupGridUserType, bool>> where)
        {
            return vw_LookupGridUserTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridUserType>> SelectMany_vw_LookupGridUserTypeAsync(Expression<Func<vw_LookupGridUserType, bool>> where)
        {
            return vw_LookupGridUserTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridUserType SelectSingle_vw_LookupGridUserType(Expression<Func<vw_LookupGridUserType, bool>> where)
        {
            return vw_LookupGridUserTypeRepository.Select(where);
        }
        public Task<vw_LookupGridUserType> SelectSingle_vw_LookupGridUserTypeAsync(Expression<Func<vw_LookupGridUserType, bool>> where)
        {
            return vw_LookupGridUserTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridUserType

        #region vw_LookupRole

        public List<vw_LookupRole> Selectvw_LookupRolesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupRoleRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupRole>> Selectvw_LookupRolesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupRoleRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupRole> SelectAllvw_LookupRoles()
        {
            return vw_LookupRoleRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupRolesAsync()
        {
            var lookup = await vw_LookupRoleRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.Id,
                LookupValue = x.Name,
                disabled = false, GroupBy = "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupRole> SelectMany_vw_LookupRole(Expression<Func<vw_LookupRole, bool>> where)
        {
            return vw_LookupRoleRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupRole>> SelectMany_vw_LookupRoleAsync(Expression<Func<vw_LookupRole, bool>> where)
        {
            return vw_LookupRoleRepository.SelectManyAsync(where);
        }
        public vw_LookupRole SelectSingle_vw_LookupRole(Expression<Func<vw_LookupRole, bool>> where)
        {
            return vw_LookupRoleRepository.Select(where);
        }
        public Task<vw_LookupRole> SelectSingle_vw_LookupRoleAsync(Expression<Func<vw_LookupRole, bool>> where)
        {
            return vw_LookupRoleRepository.SelectAsync(where);
        }

        #endregion vw_LookupRole

        #region vw_LookupMenuOptionGroup

        public List<vw_LookupMenuOptionGroup> Selectvw_LookupMenuOptionGroupsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupMenuOptionGroupRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupMenuOptionGroup>> Selectvw_LookupMenuOptionGroupsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupMenuOptionGroupRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupMenuOptionGroup> SelectAllvw_LookupMenuOptionGroups()
        {
            return vw_LookupMenuOptionGroupRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupMenuOptionGroup>> SelectAllvw_LookupMenuOptionGroupsAsync()
        {
            return vw_LookupMenuOptionGroupRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupMenuOptionGroup> SelectMany_vw_LookupMenuOptionGroup(Expression<Func<vw_LookupMenuOptionGroup, bool>> where)
        {
            return vw_LookupMenuOptionGroupRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupMenuOptionGroup>> SelectMany_vw_LookupMenuOptionGroupAsync(Expression<Func<vw_LookupMenuOptionGroup, bool>> where)
        {
            return vw_LookupMenuOptionGroupRepository.SelectManyAsync(where);
        }
        public vw_LookupMenuOptionGroup SelectSingle_vw_LookupMenuOptionGroup(Expression<Func<vw_LookupMenuOptionGroup, bool>> where)
        {
            return vw_LookupMenuOptionGroupRepository.Select(where);
        }
        public Task<vw_LookupMenuOptionGroup> SelectSingle_vw_LookupMenuOptionGroupAsync(Expression<Func<vw_LookupMenuOptionGroup, bool>> where)
        {
            return vw_LookupMenuOptionGroupRepository.SelectAsync(where);
        }

        #endregion vw_LookupMenuOptionGroup


        #region vw_LookupTemplateType

        public List<vw_LookupTemplateType> Selectvw_LookupTemplateTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupTemplateTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupTemplateType>> Selectvw_LookupTemplateTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupTemplateTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupTemplateType> SelectAllvw_LookupTemplateTypes()
        {
            return vw_LookupTemplateTypeRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupTemplateTypesAsync()
        {
            var lookup = await vw_LookupTemplateTypeRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.TemplateTypeId,
                LookupValue = x.TemplateType,
                LookupExtraInt = x.LookupExtraInt,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupTemplateType> SelectMany_vw_LookupTemplateType(Expression<Func<vw_LookupTemplateType, bool>> where)
        {
            return vw_LookupTemplateTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupTemplateType>> SelectMany_vw_LookupTemplateTypeAsync(Expression<Func<vw_LookupTemplateType, bool>> where)
        {
            return vw_LookupTemplateTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupTemplateType SelectSingle_vw_LookupTemplateType(Expression<Func<vw_LookupTemplateType, bool>> where)
        {
            return vw_LookupTemplateTypeRepository.Select(where);
        }
        public Task<vw_LookupTemplateType> SelectSingle_vw_LookupTemplateTypeAsync(Expression<Func<vw_LookupTemplateType, bool>> where)
        {
            return vw_LookupTemplateTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupTemplateType

        #region vw_LookupUsers

        public List<vw_LookupUsers> Selectvw_LookupUserssByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUsersRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUsers>> Selectvw_LookupUserssByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUsersRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUsers> SelectAllvw_LookupUserss()
        {
            return vw_LookupUsersRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUsers>> SelectAllvw_LookupUserssAsync()
        {
            return vw_LookupUsersRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUsers> SelectMany_vw_LookupUsers(Expression<Func<vw_LookupUsers, bool>> where)
        {
            return vw_LookupUsersRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUsers>> SelectMany_vw_LookupUsersAsync(Expression<Func<vw_LookupUsers, bool>> where)
        {
            return vw_LookupUsersRepository.SelectManyAsync(where);
        }
        public vw_LookupUsers SelectSingle_vw_LookupUsers(Expression<Func<vw_LookupUsers, bool>> where)
        {
            return vw_LookupUsersRepository.Select(where);
        }
        public Task<vw_LookupUsers> SelectSingle_vw_LookupUsersAsync(Expression<Func<vw_LookupUsers, bool>> where)
        {
            return vw_LookupUsersRepository.SelectAsync(where);
        }

        #endregion vw_LookupUsers

        #region vw_LookupUserType

        public List<vw_LookupUserType> Selectvw_LookupUserTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUserTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUserType>> Selectvw_LookupUserTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUserTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUserType> SelectAllvw_LookupUserTypes()
        {
            return vw_LookupUserTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUserType>> SelectAllvw_LookupUserTypesAsync()
        {
            return vw_LookupUserTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUserType> SelectMany_vw_LookupUserType(Expression<Func<vw_LookupUserType, bool>> where)
        {
            return vw_LookupUserTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUserType>> SelectMany_vw_LookupUserTypeAsync(Expression<Func<vw_LookupUserType, bool>> where)
        {
            return vw_LookupUserTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupUserType SelectSingle_vw_LookupUserType(Expression<Func<vw_LookupUserType, bool>> where)
        {
            return vw_LookupUserTypeRepository.Select(where);
        }
        public Task<vw_LookupUserType> SelectSingle_vw_LookupUserTypeAsync(Expression<Func<vw_LookupUserType, bool>> where)
        {
            return vw_LookupUserTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupUserType

        #region vw_TemplateGrid

        public List<vw_TemplateGrid> Selectvw_TemplateGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_TemplateGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_TemplateGrid>> Selectvw_TemplateGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_TemplateGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_TemplateGrid> SelectAllvw_TemplateGrids()
        {
            return vw_TemplateGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_TemplateGrid>> SelectAllvw_TemplateGridsAsync()
        {
            return vw_TemplateGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_TemplateGrid> SelectMany_vw_TemplateGrid(Expression<Func<vw_TemplateGrid, bool>> where)
        {
            return vw_TemplateGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_TemplateGrid>> SelectMany_vw_TemplateGridAsync(Expression<Func<vw_TemplateGrid, bool>> where)
        {
            return vw_TemplateGridRepository.SelectManyAsync(where);
        }
        public vw_TemplateGrid SelectSingle_vw_TemplateGrid(Expression<Func<vw_TemplateGrid, bool>> where)
        {
            return vw_TemplateGridRepository.Select(where);
        }
        public Task<vw_TemplateGrid> SelectSingle_vw_TemplateGridAsync(Expression<Func<vw_TemplateGrid, bool>> where)
        {
            return vw_TemplateGridRepository.SelectAsync(where);
        }

        #endregion vw_TemplateGrid

        #region vw_ExportLogGrid

        public List<vw_ExportLogGrid> Selectvw_ExportLogGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_ExportLogGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_ExportLogGrid>> Selectvw_ExportLogGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_ExportLogGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_ExportLogGrid> SelectAllvw_ExportLogGrids()
        {
            return vw_ExportLogGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_ExportLogGrid>> SelectAllvw_ExportLogGridsAsync()
        {
            return vw_ExportLogGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_ExportLogGrid> SelectMany_vw_ExportLogGrid(Expression<Func<vw_ExportLogGrid, bool>> where)
        {
            return vw_ExportLogGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_ExportLogGrid>> SelectMany_vw_ExportLogGridAsync(Expression<Func<vw_ExportLogGrid, bool>> where)
        {
            return vw_ExportLogGridRepository.SelectManyAsync(where);
        }
        public vw_ExportLogGrid SelectSingle_vw_ExportLogGrid(Expression<Func<vw_ExportLogGrid, bool>> where)
        {
            return vw_ExportLogGridRepository.Select(where);
        }
        public Task<vw_ExportLogGrid> SelectSingle_vw_ExportLogGridAsync(Expression<Func<vw_ExportLogGrid, bool>> where)
        {
            return vw_ExportLogGridRepository.SelectAsync(where);
        }

        #endregion vw_ExportLogGrid

        #region vw_LookupUsersSignature

        public List<vw_LookupUsersSignature> Selectvw_LookupUsersSignaturesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUsersSignatureRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUsersSignature>> Selectvw_LookupUsersSignaturesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUsersSignatureRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUsersSignature> SelectAllvw_LookupUsersSignatures()
        {
            return vw_LookupUsersSignatureRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUsersSignature>> SelectAllvw_LookupUsersSignaturesAsync()
        {
            return vw_LookupUsersSignatureRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUsersSignature> SelectMany_vw_LookupUsersSignature(Expression<Func<vw_LookupUsersSignature, bool>> where)
        {
            return vw_LookupUsersSignatureRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUsersSignature>> SelectMany_vw_LookupUsersSignatureAsync(Expression<Func<vw_LookupUsersSignature, bool>> where)
        {
            return vw_LookupUsersSignatureRepository.SelectManyAsync(where);
        }
        public vw_LookupUsersSignature SelectSingle_vw_LookupUsersSignature(Expression<Func<vw_LookupUsersSignature, bool>> where)
        {
            return vw_LookupUsersSignatureRepository.Select(where);
        }
        public Task<vw_LookupUsersSignature> SelectSingle_vw_LookupUsersSignatureAsync(Expression<Func<vw_LookupUsersSignature, bool>> where)
        {
            return vw_LookupUsersSignatureRepository.SelectAsync(where);
        }

        #endregion vw_LookupUsersSignature

        #region vw_UserGrid

        public List<vw_UserGrid> Selectvw_UserGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserGrid>> Selectvw_UserGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserGrid> SelectAllvw_UserGrids()
        {
            return vw_UserGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserGrid>> SelectAllvw_UserGridsAsync()
        {
            return vw_UserGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserGrid> SelectMany_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where)
        {
            return vw_UserGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserGrid>> SelectMany_vw_UserGridAsync(Expression<Func<vw_UserGrid, bool>> where)
        {
            return vw_UserGridRepository.SelectManyAsync(where);
        }
        public vw_UserGrid SelectSingle_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where)
        {
            return vw_UserGridRepository.Select(where);
        }
        public Task<vw_UserGrid> SelectSingle_vw_UserGridAsync(Expression<Func<vw_UserGrid, bool>> where)
        {
            return vw_UserGridRepository.SelectAsync(where);
        }

        #endregion vw_UserGrid

        #region vw_LookupTypeGrid

        public List<vw_LookupTypeGrid> Selectvw_LookupTypeGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupTypeGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupTypeGrid>> Selectvw_LookupTypeGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupTypeGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupTypeGrid> SelectAllvw_LookupTypeGrids()
        {
            return vw_LookupTypeGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupTypeGrid>> SelectAllvw_LookupTypeGridsAsync()
        {
            return vw_LookupTypeGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupTypeGrid> SelectMany_vw_LookupTypeGrid(Expression<Func<vw_LookupTypeGrid, bool>> where)
        {
            return vw_LookupTypeGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupTypeGrid>> SelectMany_vw_LookupTypeGridAsync(Expression<Func<vw_LookupTypeGrid, bool>> where)
        {
            return vw_LookupTypeGridRepository.SelectManyAsync(where);
        }
        public vw_LookupTypeGrid SelectSingle_vw_LookupTypeGrid(Expression<Func<vw_LookupTypeGrid, bool>> where)
        {
            return vw_LookupTypeGridRepository.Select(where);
        }
        public Task<vw_LookupTypeGrid> SelectSingle_vw_LookupTypeGridAsync(Expression<Func<vw_LookupTypeGrid, bool>> where)
        {
            return vw_LookupTypeGridRepository.SelectAsync(where);
        }

        #endregion vw_LookupTypeGrid

        #region vw_LookupMenuOption

        public List<vw_LookupMenuOption> Selectvw_LookupMenuOptionsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupMenuOptionRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupMenuOption>> Selectvw_LookupMenuOptionsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupMenuOptionRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupMenuOption> SelectAllvw_LookupMenuOptions()
        {
            return vw_LookupMenuOptionRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupMenuOption>> SelectAllvw_LookupMenuOptionsAsync()
        {
            return vw_LookupMenuOptionRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupMenuOption> SelectMany_vw_LookupMenuOption(Expression<Func<vw_LookupMenuOption, bool>> where)
        {
            return vw_LookupMenuOptionRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupMenuOption>> SelectMany_vw_LookupMenuOptionAsync(Expression<Func<vw_LookupMenuOption, bool>> where)
        {
            return vw_LookupMenuOptionRepository.SelectManyAsync(where);
        }
        public vw_LookupMenuOption SelectSingle_vw_LookupMenuOption(Expression<Func<vw_LookupMenuOption, bool>> where)
        {
            return vw_LookupMenuOptionRepository.Select(where);
        }
        public Task<vw_LookupMenuOption> SelectSingle_vw_LookupMenuOptionAsync(Expression<Func<vw_LookupMenuOption, bool>> where)
        {
            return vw_LookupMenuOptionRepository.SelectAsync(where);
        }

        #endregion vw_LookupMenuOption

        #region vw_LookupList

        public List<vw_LookupList> Selectvw_LookupListsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupListRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupList>> Selectvw_LookupListsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupListRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupList> SelectAllvw_LookupLists()
        {
            return vw_LookupListRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupList>> SelectAllvw_LookupListsAsync()
        {
            return vw_LookupListRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupList> SelectMany_vw_LookupList(Expression<Func<vw_LookupList, bool>> where)
        {
            return vw_LookupListRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupList>> SelectMany_vw_LookupListAsync(Expression<Func<vw_LookupList, bool>> where)
        {
            return vw_LookupListRepository.SelectManyAsync(where);
        }
        public vw_LookupList SelectSingle_vw_LookupList(Expression<Func<vw_LookupList, bool>> where)
        {
            return vw_LookupListRepository.Select(where);
        }
        public Task<vw_LookupList> SelectSingle_vw_LookupListAsync(Expression<Func<vw_LookupList, bool>> where)
        {
            return vw_LookupListRepository.SelectAsync(where);
        }

        #endregion vw_LookupList

        #region vw_LookupDashboardOption

        public List<vw_LookupDashboardOption> Selectvw_LookupDashboardOptionsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupDashboardOptionRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupDashboardOption>> Selectvw_LookupDashboardOptionsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupDashboardOptionRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupDashboardOption> SelectAllvw_LookupDashboardOptions()
        {
            return vw_LookupDashboardOptionRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupDashboardOption>> SelectAllvw_LookupDashboardOptionsAsync()
        {
            return vw_LookupDashboardOptionRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupDashboardOption> SelectMany_vw_LookupDashboardOption(Expression<Func<vw_LookupDashboardOption, bool>> where)
        {
            return vw_LookupDashboardOptionRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupDashboardOption>> SelectMany_vw_LookupDashboardOptionAsync(Expression<Func<vw_LookupDashboardOption, bool>> where)
        {
            return vw_LookupDashboardOptionRepository.SelectManyAsync(where);
        }
        public vw_LookupDashboardOption SelectSingle_vw_LookupDashboardOption(Expression<Func<vw_LookupDashboardOption, bool>> where)
        {
            return vw_LookupDashboardOptionRepository.Select(where);
        }
        public Task<vw_LookupDashboardOption> SelectSingle_vw_LookupDashboardOptionAsync(Expression<Func<vw_LookupDashboardOption, bool>> where)
        {
            return vw_LookupDashboardOptionRepository.SelectAsync(where);
        }

        #endregion vw_LookupDashboardOption                    

        #region vw_LookupFromEmailAddress

        public List<vw_LookupFromEmailAddress> Selectvw_LookupFromEmailAddresssByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupFromEmailAddressRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupFromEmailAddress>> Selectvw_LookupFromEmailAddresssByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupFromEmailAddressRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupFromEmailAddress> SelectAllvw_LookupFromEmailAddresss()
        {
            return vw_LookupFromEmailAddressRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupFromEmailAddresssAsync()
        {
            var lookup = await vw_LookupFromEmailAddressRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.FromEmailAddressId,
                LookupValue = x.FromEmailAddress,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupFromEmailAddress> SelectMany_vw_LookupFromEmailAddress(Expression<Func<vw_LookupFromEmailAddress, bool>> where)
        {
            return vw_LookupFromEmailAddressRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupFromEmailAddress>> SelectMany_vw_LookupFromEmailAddressAsync(Expression<Func<vw_LookupFromEmailAddress, bool>> where)
        {
            return vw_LookupFromEmailAddressRepository.SelectManyAsync(where);
        }
        public vw_LookupFromEmailAddress SelectSingle_vw_LookupFromEmailAddress(Expression<Func<vw_LookupFromEmailAddress, bool>> where)
        {
            return vw_LookupFromEmailAddressRepository.Select(where);
        }
        public Task<vw_LookupFromEmailAddress> SelectSingle_vw_LookupFromEmailAddressAsync(Expression<Func<vw_LookupFromEmailAddress, bool>> where)
        {
            return vw_LookupFromEmailAddressRepository.SelectAsync(where);
        }

        #endregion vw_LookupFromEmailAddress                    

        #region UserExportLog

        #region Sync Methods

        public UserExportLog SelectByUserExportLogId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return UserExportLogRepository.SelectById(lookupTypeId);
            }
            else
            {
                UserExportLog lookupType = CacheService.Get<UserExportLog>("SelectByUserExportLogId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = UserExportLogRepository.SelectById(lookupTypeId);
                    CacheService.Add<UserExportLog>("SelectByUserExportLogId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserExportLogId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<UserExportLog> SelectMany_UserExportLog(Expression<Func<UserExportLog, bool>> where)
        {
            return UserExportLogRepository.SelectMany(where);
        }

        public UserExportLog SelectSingle_UserExportLog(Expression<Func<UserExportLog, bool>> where)
        {
            return UserExportLogRepository.Select(where);
        }

        public IEnumerable<UserExportLog> SelectAllUserExportLogs()
        {
            return UserExportLogRepository.SelectAll();
        }

        public bool SaveUserExportLogForm(UserExportLog lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    UserExportLogRepository.Add(lookupType);
                }
                else
                {
                    UserExportLogRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteUserExportLogForm(int lookupTypeId)
        {
            try
            {
                UserExportLog lookupType = UserExportLogRepository.SelectById(lookupTypeId);
                UserExportLogRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<UserExportLog> SelectByUserExportLogIdAsync(int lookupTypeId)
        {
            return UserExportLogRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<UserExportLog>> SelectMany_UserExportLogAsync(Expression<Func<UserExportLog, bool>> where)
        {
            return UserExportLogRepository.SelectManyAsync(where);
        }

        public Task<UserExportLog> SelectSingle_UserExportLogAsync(Expression<Func<UserExportLog, bool>> where)
        {
            return UserExportLogRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserExportLog>> SelectAllUserExportLogsAsync()
        {
            return UserExportLogRepository.SelectAllAsync();
        }

        public Task<int> SaveUserExportLogFormAsync(UserExportLog lookupType)
        {
            try
            {
                if (lookupType.UserExportLogId == 0)
                {
                    UserExportLogRepository.Add(lookupType);
                }
                else
                {
                    UserExportLogRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserExportLogFormAsync(int lookupTypeId)
        {
            try
            {
                UserExportLog lookupType = UserExportLogRepository.SelectById(lookupTypeId);
                UserExportLogRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion UserExportLog

        #region vw_LookupTemplateAllowedType

        public List<vw_LookupTemplateAllowedType> Selectvw_LookupTemplateAllowedTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupTemplateAllowedType>> Selectvw_LookupTemplateAllowedTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupTemplateAllowedType> SelectAllvw_LookupTemplateAllowedTypes()
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupTemplateAllowedType>> SelectAllvw_LookupTemplateAllowedTypesAsync()
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupTemplateAllowedType> SelectMany_vw_LookupTemplateAllowedType(Expression<Func<vw_LookupTemplateAllowedType, bool>> where)
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupTemplateAllowedType>> SelectMany_vw_LookupTemplateAllowedTypeAsync(Expression<Func<vw_LookupTemplateAllowedType, bool>> where)
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupTemplateAllowedType SelectSingle_vw_LookupTemplateAllowedType(Expression<Func<vw_LookupTemplateAllowedType, bool>> where)
        {
            return vw_LookupTemplateAllowedTypeRepository.Select(where);
        }
        public Task<vw_LookupTemplateAllowedType> SelectSingle_vw_LookupTemplateAllowedTypeAsync(Expression<Func<vw_LookupTemplateAllowedType, bool>> where)
        {
            return vw_LookupTemplateAllowedTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupTemplateAllowedType

        #region vw_LookupGridEmailTemplateType

        public List<vw_LookupGridEmailTemplateType> Selectvw_LookupGridEmailTemplateTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridEmailTemplateType>> Selectvw_LookupGridEmailTemplateTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridEmailTemplateType> SelectAllvw_LookupGridEmailTemplateTypes()
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridEmailTemplateType>> SelectAllvw_LookupGridEmailTemplateTypesAsync()
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridEmailTemplateType> SelectMany_vw_LookupGridEmailTemplateType(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where)
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridEmailTemplateType>> SelectMany_vw_LookupGridEmailTemplateTypeAsync(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where)
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridEmailTemplateType SelectSingle_vw_LookupGridEmailTemplateType(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where)
        {
            return vw_LookupGridEmailTemplateTypeRepository.Select(where);
        }
        public Task<vw_LookupGridEmailTemplateType> SelectSingle_vw_LookupGridEmailTemplateTypeAsync(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where)
        {
            return vw_LookupGridEmailTemplateTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridEmailTemplateType

        #region vw_LookupGridEmailType

        public List<vw_LookupGridEmailType> Selectvw_LookupGridEmailTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridEmailTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridEmailType>> Selectvw_LookupGridEmailTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridEmailTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridEmailType> SelectAllvw_LookupGridEmailTypes()
        {
            return vw_LookupGridEmailTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridEmailType>> SelectAllvw_LookupGridEmailTypesAsync()
        {
            return vw_LookupGridEmailTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridEmailType> SelectMany_vw_LookupGridEmailType(Expression<Func<vw_LookupGridEmailType, bool>> where)
        {
            return vw_LookupGridEmailTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridEmailType>> SelectMany_vw_LookupGridEmailTypeAsync(Expression<Func<vw_LookupGridEmailType, bool>> where)
        {
            return vw_LookupGridEmailTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridEmailType SelectSingle_vw_LookupGridEmailType(Expression<Func<vw_LookupGridEmailType, bool>> where)
        {
            return vw_LookupGridEmailTypeRepository.Select(where);
        }
        public Task<vw_LookupGridEmailType> SelectSingle_vw_LookupGridEmailTypeAsync(Expression<Func<vw_LookupGridEmailType, bool>> where)
        {
            return vw_LookupGridEmailTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridEmailType
        
        #region vw_LookupGridTemplateAllowedType

        public List<vw_LookupGridTemplateAllowedType> Selectvw_LookupGridTemplateAllowedTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridTemplateAllowedType>> Selectvw_LookupGridTemplateAllowedTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridTemplateAllowedType> SelectAllvw_LookupGridTemplateAllowedTypes()
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridTemplateAllowedType>> SelectAllvw_LookupGridTemplateAllowedTypesAsync()
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridTemplateAllowedType> SelectMany_vw_LookupGridTemplateAllowedType(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridTemplateAllowedType>> SelectMany_vw_LookupGridTemplateAllowedTypeAsync(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridTemplateAllowedType SelectSingle_vw_LookupGridTemplateAllowedType(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.Select(where);
        }
        public Task<vw_LookupGridTemplateAllowedType> SelectSingle_vw_LookupGridTemplateAllowedTypeAsync(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where)
        {
            return vw_LookupGridTemplateAllowedTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridTemplateAllowedType

        #region vw_LookupEmailTemplateType

        public List<vw_LookupEmailTemplateType> Selectvw_LookupEmailTemplateTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupEmailTemplateTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupEmailTemplateType>> Selectvw_LookupEmailTemplateTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupEmailTemplateTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupEmailTemplateType> SelectAllvw_LookupEmailTemplateTypes()
        {
            return vw_LookupEmailTemplateTypeRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupEmailTemplateTypesAsync()
        {
            var lookup = await vw_LookupEmailTemplateTypeRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.EmailTemplateTypeId,
                LookupValue = x.EmailTemplateType,
                LookupExtraInt = x.LookupExtraInt,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupEmailTemplateType> SelectMany_vw_LookupEmailTemplateType(Expression<Func<vw_LookupEmailTemplateType, bool>> where)
        {
            return vw_LookupEmailTemplateTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupEmailTemplateType>> SelectMany_vw_LookupEmailTemplateTypeAsync(Expression<Func<vw_LookupEmailTemplateType, bool>> where)
        {
            return vw_LookupEmailTemplateTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupEmailTemplateType SelectSingle_vw_LookupEmailTemplateType(Expression<Func<vw_LookupEmailTemplateType, bool>> where)
        {
            return vw_LookupEmailTemplateTypeRepository.Select(where);
        }
        public Task<vw_LookupEmailTemplateType> SelectSingle_vw_LookupEmailTemplateTypeAsync(Expression<Func<vw_LookupEmailTemplateType, bool>> where)
        {
            return vw_LookupEmailTemplateTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupEmailTemplateType

        #region vw_LookupGridAlertType

        public List<vw_LookupGridAlertType> Selectvw_LookupGridAlertTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupGridAlertTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupGridAlertType>> Selectvw_LookupGridAlertTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupGridAlertTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupGridAlertType> SelectAllvw_LookupGridAlertTypes()
        {
            return vw_LookupGridAlertTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupGridAlertType>> SelectAllvw_LookupGridAlertTypesAsync()
        {
            return vw_LookupGridAlertTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupGridAlertType> SelectMany_vw_LookupGridAlertType(Expression<Func<vw_LookupGridAlertType, bool>> where)
        {
            return vw_LookupGridAlertTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupGridAlertType>> SelectMany_vw_LookupGridAlertTypeAsync(Expression<Func<vw_LookupGridAlertType, bool>> where)
        {
            return vw_LookupGridAlertTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupGridAlertType SelectSingle_vw_LookupGridAlertType(Expression<Func<vw_LookupGridAlertType, bool>> where)
        {
            return vw_LookupGridAlertTypeRepository.Select(where);
        }
        public Task<vw_LookupGridAlertType> SelectSingle_vw_LookupGridAlertTypeAsync(Expression<Func<vw_LookupGridAlertType, bool>> where)
        {
            return vw_LookupGridAlertTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupGridAlertType

        #region vw_LookupEmailType

        public List<vw_LookupEmailType> Selectvw_LookupEmailTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupEmailTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupEmailType>> Selectvw_LookupEmailTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupEmailTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupEmailType> SelectAllvw_LookupEmailTypes()
        {
            return vw_LookupEmailTypeRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupEmailTypesAsync()
        {
            var lookup = await vw_LookupEmailTypeRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.EmailTypeId,
                LookupValue = x.EmailType,
                LookupExtraInt = x.LookupExtraInt,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupEmailType> SelectMany_vw_LookupEmailType(Expression<Func<vw_LookupEmailType, bool>> where)
        {
            return vw_LookupEmailTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupEmailType>> SelectMany_vw_LookupEmailTypeAsync(Expression<Func<vw_LookupEmailType, bool>> where)
        {
            return vw_LookupEmailTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupEmailType SelectSingle_vw_LookupEmailType(Expression<Func<vw_LookupEmailType, bool>> where)
        {
            return vw_LookupEmailTypeRepository.Select(where);
        }
        public Task<vw_LookupEmailType> SelectSingle_vw_LookupEmailTypeAsync(Expression<Func<vw_LookupEmailType, bool>> where)
        {
            return vw_LookupEmailTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupEmailType

        #region vw_LookupAlertType

        public List<vw_LookupAlertType> Selectvw_LookupAlertTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupAlertTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupAlertType>> Selectvw_LookupAlertTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupAlertTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupAlertType> SelectAllvw_LookupAlertTypes()
        {
            return vw_LookupAlertTypeRepository.SelectAll();
        }
        public async Task<IEnumerable<LookupEntity>> SelectAllvw_LookupAlertTypesAsync()
        {
            var lookup = await vw_LookupAlertTypeRepository.SelectAllAsync();
            return lookup.Select(x => new LookupEntity() {
                LookupId = x.AlertTypeId,
                LookupValue = x.AlertType,
                disabled = x.IsArchived,
                GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE"
            }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        public IEnumerable<vw_LookupAlertType> SelectMany_vw_LookupAlertType(Expression<Func<vw_LookupAlertType, bool>> where)
        {
            return vw_LookupAlertTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupAlertType>> SelectMany_vw_LookupAlertTypeAsync(Expression<Func<vw_LookupAlertType, bool>> where)
        {
            return vw_LookupAlertTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupAlertType SelectSingle_vw_LookupAlertType(Expression<Func<vw_LookupAlertType, bool>> where)
        {
            return vw_LookupAlertTypeRepository.Select(where);
        }
        public Task<vw_LookupAlertType> SelectSingle_vw_LookupAlertTypeAsync(Expression<Func<vw_LookupAlertType, bool>> where)
        {
            return vw_LookupAlertTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupAlertType

        #region vw_LookupAccessType

        public List<vw_LookupAccessType> Selectvw_LookupAccessTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupAccessTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupAccessType>> Selectvw_LookupAccessTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupAccessTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupAccessType> SelectAllvw_LookupAccessTypes()
        {
            return vw_LookupAccessTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupAccessType>> SelectAllvw_LookupAccessTypesAsync()
        {
            return vw_LookupAccessTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupAccessType> SelectMany_vw_LookupAccessType(Expression<Func<vw_LookupAccessType, bool>> where)
        {
            return vw_LookupAccessTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupAccessType>> SelectMany_vw_LookupAccessTypeAsync(Expression<Func<vw_LookupAccessType, bool>> where)
        {
            return vw_LookupAccessTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupAccessType SelectSingle_vw_LookupAccessType(Expression<Func<vw_LookupAccessType, bool>> where)
        {
            return vw_LookupAccessTypeRepository.Select(where);
        }
        public Task<vw_LookupAccessType> SelectSingle_vw_LookupAccessTypeAsync(Expression<Func<vw_LookupAccessType, bool>> where)
        {
            return vw_LookupAccessTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupAccessType

        #region vw_LookupObject

        public List<vw_LookupObject> Selectvw_LookupObjectsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupObjectRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupObject>> Selectvw_LookupObjectsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupObjectRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupObject> SelectAllvw_LookupObjects()
        {
            return vw_LookupObjectRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupObject>> SelectAllvw_LookupObjectsAsync()
        {
            return vw_LookupObjectRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupObject> SelectMany_vw_LookupObject(Expression<Func<vw_LookupObject, bool>> where)
        {
            return vw_LookupObjectRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupObject>> SelectMany_vw_LookupObjectAsync(Expression<Func<vw_LookupObject, bool>> where)
        {
            return vw_LookupObjectRepository.SelectManyAsync(where);
        }
        public vw_LookupObject SelectSingle_vw_LookupObject(Expression<Func<vw_LookupObject, bool>> where)
        {
            return vw_LookupObjectRepository.Select(where);
        }
        public Task<vw_LookupObject> SelectSingle_vw_LookupObjectAsync(Expression<Func<vw_LookupObject, bool>> where)
        {
            return vw_LookupObjectRepository.SelectAsync(where);
        }

        #endregion vw_LookupObject

        #region vw_LookupObjectType

        public List<vw_LookupObjectType> Selectvw_LookupObjectTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupObjectTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupObjectType>> Selectvw_LookupObjectTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupObjectTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupObjectType> SelectAllvw_LookupObjectTypes()
        {
            return vw_LookupObjectTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupObjectType>> SelectAllvw_LookupObjectTypesAsync()
        {
            return vw_LookupObjectTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupObjectType> SelectMany_vw_LookupObjectType(Expression<Func<vw_LookupObjectType, bool>> where)
        {
            return vw_LookupObjectTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupObjectType>> SelectMany_vw_LookupObjectTypeAsync(Expression<Func<vw_LookupObjectType, bool>> where)
        {
            return vw_LookupObjectTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupObjectType SelectSingle_vw_LookupObjectType(Expression<Func<vw_LookupObjectType, bool>> where)
        {
            return vw_LookupObjectTypeRepository.Select(where);
        }
        public Task<vw_LookupObjectType> SelectSingle_vw_LookupObjectTypeAsync(Expression<Func<vw_LookupObjectType, bool>> where)
        {
            return vw_LookupObjectTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupObjectType

        #region TemplateTag

        #region Sync Methods

        public TemplateTag SelectByTemplateTagId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return TemplateTagRepository.SelectById(lookupTypeId);
            }
            else
            {
                TemplateTag lookupType = CacheService.Get<TemplateTag>("SelectByTemplateTagId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = TemplateTagRepository.SelectById(lookupTypeId);
                    CacheService.Add<TemplateTag>("SelectByTemplateTagId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByTemplateTagId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<TemplateTag> SelectMany_TemplateTag(Expression<Func<TemplateTag, bool>> where)
        {
            return TemplateTagRepository.SelectMany(where);
        }

        public TemplateTag SelectSingle_TemplateTag(Expression<Func<TemplateTag, bool>> where)
        {
            return TemplateTagRepository.Select(where);
        }

        public IEnumerable<TemplateTag> SelectAllTemplateTags()
        {
            return TemplateTagRepository.SelectAll();
        }

        public bool SaveTemplateTagForm(TemplateTag lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    TemplateTagRepository.Add(lookupType);
                }
                else
                {
                    TemplateTagRepository.Update(lookupType);
                }

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        public bool DeleteTemplateTagForm(int lookupTypeId)
        {
            try
            {
                TemplateTag lookupType = TemplateTagRepository.SelectById(lookupTypeId);
                TemplateTagRepository.Delete(lookupType);

                Save();

                return true;
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
            }

            return false;
        }

        #endregion Sync Methods

        #region Async Methods

        public Task<TemplateTag> SelectByTemplateTagIdAsync(int lookupTypeId)
        {
            return TemplateTagRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<TemplateTag>> SelectMany_TemplateTagAsync(Expression<Func<TemplateTag, bool>> where)
        {
            return TemplateTagRepository.SelectManyAsync(where);
        }

        public Task<TemplateTag> SelectSingle_TemplateTagAsync(Expression<Func<TemplateTag, bool>> where)
        {
            return TemplateTagRepository.SelectAsync(where);
        }

        public Task<IEnumerable<TemplateTag>> SelectAllTemplateTagsAsync()
        {
            return TemplateTagRepository.SelectAllAsync();
        }

        public Task<int> SaveTemplateTagFormAsync(TemplateTag lookupType)
        {
            try
            {
                if (lookupType.TemplateTagId == 0)
                {
                    TemplateTagRepository.Add(lookupType);
                }
                else
                {
                    TemplateTagRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteTemplateTagFormAsync(int lookupTypeId)
        {
            try
            {
                TemplateTag lookupType = TemplateTagRepository.SelectById(lookupTypeId);
                TemplateTagRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion TemplateTag

        #region vw_LookupDashboardObjectType

        public List<vw_LookupDashboardObjectType> Selectvw_LookupDashboardObjectTypesByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupDashboardObjectTypeRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupDashboardObjectType>> Selectvw_LookupDashboardObjectTypesByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupDashboardObjectTypeRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupDashboardObjectType> SelectAllvw_LookupDashboardObjectTypes()
        {
            return vw_LookupDashboardObjectTypeRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupDashboardObjectType>> SelectAllvw_LookupDashboardObjectTypesAsync()
        {
            return vw_LookupDashboardObjectTypeRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupDashboardObjectType> SelectMany_vw_LookupDashboardObjectType(Expression<Func<vw_LookupDashboardObjectType, bool>> where)
        {
            return vw_LookupDashboardObjectTypeRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupDashboardObjectType>> SelectMany_vw_LookupDashboardObjectTypeAsync(Expression<Func<vw_LookupDashboardObjectType, bool>> where)
        {
            return vw_LookupDashboardObjectTypeRepository.SelectManyAsync(where);
        }
        public vw_LookupDashboardObjectType SelectSingle_vw_LookupDashboardObjectType(Expression<Func<vw_LookupDashboardObjectType, bool>> where)
        {
            return vw_LookupDashboardObjectTypeRepository.Select(where);
        }
        public Task<vw_LookupDashboardObjectType> SelectSingle_vw_LookupDashboardObjectTypeAsync(Expression<Func<vw_LookupDashboardObjectType, bool>> where)
        {
            return vw_LookupDashboardObjectTypeRepository.SelectAsync(where);
        }

        #endregion vw_LookupDashboardObjectType

        #region vw_LookupDashboardObject

        public List<vw_LookupDashboardObject> Selectvw_LookupDashboardObjectsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupDashboardObjectRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupDashboardObject>> Selectvw_LookupDashboardObjectsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupDashboardObjectRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupDashboardObject> SelectAllvw_LookupDashboardObjects()
        {
            return vw_LookupDashboardObjectRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupDashboardObject>> SelectAllvw_LookupDashboardObjectsAsync()
        {
            return vw_LookupDashboardObjectRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupDashboardObject> SelectMany_vw_LookupDashboardObject(Expression<Func<vw_LookupDashboardObject, bool>> where)
        {
            return vw_LookupDashboardObjectRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupDashboardObject>> SelectMany_vw_LookupDashboardObjectAsync(Expression<Func<vw_LookupDashboardObject, bool>> where)
        {
            return vw_LookupDashboardObjectRepository.SelectManyAsync(where);
        }
        public vw_LookupDashboardObject SelectSingle_vw_LookupDashboardObject(Expression<Func<vw_LookupDashboardObject, bool>> where)
        {
            return vw_LookupDashboardObjectRepository.Select(where);
        }
        public Task<vw_LookupDashboardObject> SelectSingle_vw_LookupDashboardObjectAsync(Expression<Func<vw_LookupDashboardObject, bool>> where)
        {
            return vw_LookupDashboardObjectRepository.SelectAsync(where);
        }

        #endregion vw_LookupDashboardObject

        #region vw_LookupSetting

        public List<vw_LookupSetting> Selectvw_LookupSettingsByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupSettingRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupSetting>> Selectvw_LookupSettingsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupSettingRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupSetting> SelectAllvw_LookupSettings()
        {
            return vw_LookupSettingRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupSetting>> SelectAllvw_LookupSettingsAsync()
        {
            return vw_LookupSettingRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupSetting> SelectMany_vw_LookupSetting(Expression<Func<vw_LookupSetting, bool>> where)
        {
            return vw_LookupSettingRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupSetting>> SelectMany_vw_LookupSettingAsync(Expression<Func<vw_LookupSetting, bool>> where)
        {
            return vw_LookupSettingRepository.SelectManyAsync(where);
        }
        public vw_LookupSetting SelectSingle_vw_LookupSetting(Expression<Func<vw_LookupSetting, bool>> where)
        {
            return vw_LookupSettingRepository.Select(where);
        }
        public Task<vw_LookupSetting> SelectSingle_vw_LookupSettingAsync(Expression<Func<vw_LookupSetting, bool>> where)
        {
            return vw_LookupSettingRepository.SelectAsync(where);
        }

        #endregion vw_LookupSetting
    }

    public partial interface ILookupService : IBaseService
    {
        // Interface Methods
        #region Attachment

        Attachment SelectByAttachmentId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<Attachment> SelectMany_Attachment(Expression<Func<Attachment, bool>> where);

        Attachment SelectSingle_Attachment(Expression<Func<Attachment, bool>> where);

        IEnumerable<Attachment> SelectAllAttachments();

        bool SaveAttachmentForm(Attachment lookupTypeRepository);

        bool DeleteAttachmentForm(int lookupTypeId);

        Task<Attachment> SelectByAttachmentIdAsync(int lookupTypeId);

        Task<IEnumerable<Attachment>> SelectMany_AttachmentAsync(Expression<Func<Attachment, bool>> where);

        Task<Attachment> SelectSingle_AttachmentAsync(Expression<Func<Attachment, bool>> where);

        Task<IEnumerable<Attachment>> SelectAllAttachmentsAsync();

        Task<int> SaveAttachmentFormAsync(Attachment lookupTypeRepository);

        Task<int> DeleteAttachmentFormAsync(int lookupTypeId);
        #endregion

        #region SafeIPs

        SafeIPs SelectBySafeIPsId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<SafeIPs> SelectMany_SafeIPs(Expression<Func<SafeIPs, bool>> where);

        SafeIPs SelectSingle_SafeIPs(Expression<Func<SafeIPs, bool>> where);

        IEnumerable<SafeIPs> SelectAllSafeIPss();

        bool SaveSafeIPsForm(SafeIPs lookupTypeRepository);

        bool DeleteSafeIPsForm(int lookupTypeId);

        Task<SafeIPs> SelectBySafeIPsIdAsync(int lookupTypeId);

        Task<IEnumerable<SafeIPs>> SelectMany_SafeIPsAsync(Expression<Func<SafeIPs, bool>> where);

        Task<SafeIPs> SelectSingle_SafeIPsAsync(Expression<Func<SafeIPs, bool>> where);

        Task<IEnumerable<SafeIPs>> SelectAllSafeIPssAsync();

        Task<int> SaveSafeIPsFormAsync(SafeIPs lookupTypeRepository);

        Task<int> DeleteSafeIPsFormAsync(int lookupTypeId);
        #endregion

        #region InternalFilterHeader

        InternalFilterHeader SelectByInternalFilterHeaderId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<InternalFilterHeader> SelectMany_InternalFilterHeader(Expression<Func<InternalFilterHeader, bool>> where);

        InternalFilterHeader SelectSingle_InternalFilterHeader(Expression<Func<InternalFilterHeader, bool>> where);

        IEnumerable<InternalFilterHeader> SelectAllInternalFilterHeaders();

        bool SaveInternalFilterHeaderForm(InternalFilterHeader lookupTypeRepository);

        bool DeleteInternalFilterHeaderForm(int lookupTypeId);

        Task<InternalFilterHeader> SelectByInternalFilterHeaderIdAsync(int lookupTypeId);

        Task<IEnumerable<InternalFilterHeader>> SelectMany_InternalFilterHeaderAsync(Expression<Func<InternalFilterHeader, bool>> where);

        Task<InternalFilterHeader> SelectSingle_InternalFilterHeaderAsync(Expression<Func<InternalFilterHeader, bool>> where);

        Task<IEnumerable<InternalFilterHeader>> SelectAllInternalFilterHeadersAsync();

        Task<int> SaveInternalFilterHeaderFormAsync(InternalFilterHeader lookupTypeRepository);

        Task<int> DeleteInternalFilterHeaderFormAsync(int lookupTypeId);
        #endregion

        #region InternalGridSetting

        InternalGridSetting SelectByInternalGridSettingId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<InternalGridSetting> SelectMany_InternalGridSetting(Expression<Func<InternalGridSetting, bool>> where);

        InternalGridSetting SelectSingle_InternalGridSetting(Expression<Func<InternalGridSetting, bool>> where);

        IEnumerable<InternalGridSetting> SelectAllInternalGridSettings();

        bool SaveInternalGridSettingForm(InternalGridSetting lookupTypeRepository);

        bool DeleteInternalGridSettingForm(int lookupTypeId);

        Task<InternalGridSetting> SelectByInternalGridSettingIdAsync(int lookupTypeId);

        Task<IEnumerable<InternalGridSetting>> SelectMany_InternalGridSettingAsync(Expression<Func<InternalGridSetting, bool>> where);

        Task<InternalGridSetting> SelectSingle_InternalGridSettingAsync(Expression<Func<InternalGridSetting, bool>> where);

        Task<IEnumerable<InternalGridSetting>> SelectAllInternalGridSettingsAsync();

        Task<int> SaveInternalGridSettingFormAsync(InternalGridSetting lookupTypeRepository);

        Task<int> DeleteInternalGridSettingFormAsync(int lookupTypeId);
        #endregion

        #region InternalGridSettingDefault

        InternalGridSettingDefault SelectByInternalGridSettingDefaultId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<InternalGridSettingDefault> SelectMany_InternalGridSettingDefault(Expression<Func<InternalGridSettingDefault, bool>> where);

        InternalGridSettingDefault SelectSingle_InternalGridSettingDefault(Expression<Func<InternalGridSettingDefault, bool>> where);

        IEnumerable<InternalGridSettingDefault> SelectAllInternalGridSettingDefaults();

        bool SaveInternalGridSettingDefaultForm(InternalGridSettingDefault lookupTypeRepository);

        bool DeleteInternalGridSettingDefaultForm(int lookupTypeId);

        Task<InternalGridSettingDefault> SelectByInternalGridSettingDefaultIdAsync(int lookupTypeId);

        Task<IEnumerable<InternalGridSettingDefault>> SelectMany_InternalGridSettingDefaultAsync(Expression<Func<InternalGridSettingDefault, bool>> where);

        Task<InternalGridSettingDefault> SelectSingle_InternalGridSettingDefaultAsync(Expression<Func<InternalGridSettingDefault, bool>> where);

        Task<IEnumerable<InternalGridSettingDefault>> SelectAllInternalGridSettingDefaultsAsync();

        Task<int> SaveInternalGridSettingDefaultFormAsync(InternalGridSettingDefault lookupTypeRepository);

        Task<int> DeleteInternalGridSettingDefaultFormAsync(int lookupTypeId);
        #endregion

        #region InternalReport

        InternalReport SelectByInternalReportId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<InternalReport> SelectMany_InternalReport(Expression<Func<InternalReport, bool>> where);

        InternalReport SelectSingle_InternalReport(Expression<Func<InternalReport, bool>> where);

        IEnumerable<InternalReport> SelectAllInternalReports();

        bool SaveInternalReportForm(InternalReport lookupTypeRepository);

        bool DeleteInternalReportForm(int lookupTypeId);

        Task<InternalReport> SelectByInternalReportIdAsync(int lookupTypeId);

        Task<IEnumerable<InternalReport>> SelectMany_InternalReportAsync(Expression<Func<InternalReport, bool>> where);

        Task<InternalReport> SelectSingle_InternalReportAsync(Expression<Func<InternalReport, bool>> where);

        Task<IEnumerable<InternalReport>> SelectAllInternalReportsAsync();

        Task<int> SaveInternalReportFormAsync(InternalReport lookupTypeRepository);

        Task<int> DeleteInternalReportFormAsync(int lookupTypeId);
        #endregion

        #region InternalReportField
        List<InternalReportField> SelectInternalReportFieldsByGridSetting(GridSetting gridSetting);
        IEnumerable<InternalReportField> SelectAllInternalReportFields();
        Task<IEnumerable<InternalReportField>> SelectAllInternalReportFieldsAsync();
        Task<List<InternalReportField>> SelectInternalReportFieldsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<InternalReportField> SelectMany_InternalReportField(Expression<Func<InternalReportField, bool>> where);
        Task<IEnumerable<InternalReportField>> SelectMany_InternalReportFieldAsync(Expression<Func<InternalReportField, bool>> where);
        InternalReportField SelectSingle_InternalReportField(Expression<Func<InternalReportField, bool>> where);
        Task<InternalReportField> SelectSingle_InternalReportFieldAsync(Expression<Func<InternalReportField, bool>> where);

        #endregion

        #region InterviewAppointment

        InterviewAppointment SelectByInterviewAppointmentId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<InterviewAppointment> SelectMany_InterviewAppointment(Expression<Func<InterviewAppointment, bool>> where);

        InterviewAppointment SelectSingle_InterviewAppointment(Expression<Func<InterviewAppointment, bool>> where);

        IEnumerable<InterviewAppointment> SelectAllInterviewAppointments();

        bool SaveInterviewAppointmentForm(InterviewAppointment lookupTypeRepository);

        bool DeleteInterviewAppointmentForm(int lookupTypeId);

        Task<InterviewAppointment> SelectByInterviewAppointmentIdAsync(int lookupTypeId);

        Task<IEnumerable<InterviewAppointment>> SelectMany_InterviewAppointmentAsync(Expression<Func<InterviewAppointment, bool>> where);

        Task<InterviewAppointment> SelectSingle_InterviewAppointmentAsync(Expression<Func<InterviewAppointment, bool>> where);

        Task<IEnumerable<InterviewAppointment>> SelectAllInterviewAppointmentsAsync();

        Task<int> SaveInterviewAppointmentFormAsync(InterviewAppointment lookupTypeRepository);

        Task<int> DeleteInterviewAppointmentFormAsync(int lookupTypeId);
        #endregion

        #region Lookup

        Lookup SelectByLookupId(int lookupId, bool cacheRecord = false);

        IEnumerable<Lookup> SelectMany_Lookup(Expression<Func<Lookup, bool>> where);

        Lookup SelectSingle_Lookup(Expression<Func<Lookup, bool>> where);

        IEnumerable<Lookup> SelectAllLookups();

        bool SaveLookupForm(Lookup LookupRepository);

        bool DeleteLookupForm(int lookupId);

        Task<Lookup> SelectByLookupIdAsync(int lookupId);

        Task<IEnumerable<Lookup>> SelectMany_LookupAsync(Expression<Func<Lookup, bool>> where);

        Task<Lookup> SelectSingle_LookupAsync(Expression<Func<Lookup, bool>> where);

        Task<IEnumerable<Lookup>> SelectAllLookupsAsync();

        Task<int> SaveLookupFormAsync(Lookup LookupRepository);

        Task<int> DeleteLookupFormAsync(int lookupId);
        #endregion

        #region LookupType

        LookupType SelectByLookupTypeId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<LookupType> SelectMany_LookupType(Expression<Func<LookupType, bool>> where);

        LookupType SelectSingle_LookupType(Expression<Func<LookupType, bool>> where);

        IEnumerable<LookupType> SelectAllLookupTypes();

        bool SaveLookupTypeForm(LookupType lookupTypeRepository);

        bool DeleteLookupTypeForm(int lookupTypeId);

        Task<LookupType> SelectByLookupTypeIdAsync(int lookupTypeId);

        Task<IEnumerable<LookupType>> SelectMany_LookupTypeAsync(Expression<Func<LookupType, bool>> where);

        Task<LookupType> SelectSingle_LookupTypeAsync(Expression<Func<LookupType, bool>> where);

        Task<IEnumerable<LookupType>> SelectAllLookupTypesAsync();

        Task<int> SaveLookupTypeFormAsync(LookupType lookupTypeRepository);

        Task<int> DeleteLookupTypeFormAsync(int lookupTypeId);
        #endregion

        #region Message

        Message SelectByMessageId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<Message> SelectMany_Message(Expression<Func<Message, bool>> where);

        Message SelectSingle_Message(Expression<Func<Message, bool>> where);

        IEnumerable<Message> SelectAllMessages();

        bool SaveMessageForm(Message lookupTypeRepository);

        bool DeleteMessageForm(int lookupTypeId);

        Task<Message> SelectByMessageIdAsync(int lookupTypeId);

        Task<IEnumerable<Message>> SelectMany_MessageAsync(Expression<Func<Message, bool>> where);

        Task<Message> SelectSingle_MessageAsync(Expression<Func<Message, bool>> where);

        Task<IEnumerable<Message>> SelectAllMessagesAsync();

        Task<int> SaveMessageFormAsync(Message lookupTypeRepository);

        Task<int> DeleteMessageFormAsync(int lookupTypeId);
        #endregion

        #region Template

        Template SelectByTemplateId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<Template> SelectMany_Template(Expression<Func<Template, bool>> where);

        Template SelectSingle_Template(Expression<Func<Template, bool>> where);

        IEnumerable<Template> SelectAllTemplates();

        bool SaveTemplateForm(Template lookupTypeRepository);

        bool DeleteTemplateForm(int lookupTypeId);

        Task<Template> SelectByTemplateIdAsync(int lookupTypeId);

        Task<IEnumerable<Template>> SelectMany_TemplateAsync(Expression<Func<Template, bool>> where);

        Task<Template> SelectSingle_TemplateAsync(Expression<Func<Template, bool>> where);

        Task<IEnumerable<Template>> SelectAllTemplatesAsync();

        Task<int> SaveTemplateFormAsync(Template lookupTypeRepository);

        Task<int> DeleteTemplateFormAsync(int lookupTypeId);
        #endregion

        #region vw_LookupGrid
        List<vw_LookupGrid> Selectvw_LookupGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGrid> SelectAllvw_LookupGrids();
        Task<IEnumerable<vw_LookupGrid>> SelectAllvw_LookupGridsAsync();
        Task<List<vw_LookupGrid>> Selectvw_LookupGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGrid> SelectMany_vw_LookupGrid(Expression<Func<vw_LookupGrid, bool>> where);
        Task<IEnumerable<vw_LookupGrid>> SelectMany_vw_LookupGridAsync(Expression<Func<vw_LookupGrid, bool>> where);
        vw_LookupGrid SelectSingle_vw_LookupGrid(Expression<Func<vw_LookupGrid, bool>> where);
        Task<vw_LookupGrid> SelectSingle_vw_LookupGridAsync(Expression<Func<vw_LookupGrid, bool>> where);

        #endregion

        #region vw_LookupMenuOptionGroup
        List<vw_LookupMenuOptionGroup> Selectvw_LookupMenuOptionGroupsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupMenuOptionGroup> SelectAllvw_LookupMenuOptionGroups();
        Task<IEnumerable<vw_LookupMenuOptionGroup>> SelectAllvw_LookupMenuOptionGroupsAsync();
        Task<List<vw_LookupMenuOptionGroup>> Selectvw_LookupMenuOptionGroupsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupMenuOptionGroup> SelectMany_vw_LookupMenuOptionGroup(Expression<Func<vw_LookupMenuOptionGroup, bool>> where);
        Task<IEnumerable<vw_LookupMenuOptionGroup>> SelectMany_vw_LookupMenuOptionGroupAsync(Expression<Func<vw_LookupMenuOptionGroup, bool>> where);
        vw_LookupMenuOptionGroup SelectSingle_vw_LookupMenuOptionGroup(Expression<Func<vw_LookupMenuOptionGroup, bool>> where);
        Task<vw_LookupMenuOptionGroup> SelectSingle_vw_LookupMenuOptionGroupAsync(Expression<Func<vw_LookupMenuOptionGroup, bool>> where);

        #endregion

        #region vw_LookupUsersSignature
        List<vw_LookupUsersSignature> Selectvw_LookupUsersSignaturesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUsersSignature> SelectAllvw_LookupUsersSignatures();
        Task<IEnumerable<vw_LookupUsersSignature>> SelectAllvw_LookupUsersSignaturesAsync();
        Task<List<vw_LookupUsersSignature>> Selectvw_LookupUsersSignaturesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUsersSignature> SelectMany_vw_LookupUsersSignature(Expression<Func<vw_LookupUsersSignature, bool>> where);
        Task<IEnumerable<vw_LookupUsersSignature>> SelectMany_vw_LookupUsersSignatureAsync(Expression<Func<vw_LookupUsersSignature, bool>> where);
        vw_LookupUsersSignature SelectSingle_vw_LookupUsersSignature(Expression<Func<vw_LookupUsersSignature, bool>> where);
        Task<vw_LookupUsersSignature> SelectSingle_vw_LookupUsersSignatureAsync(Expression<Func<vw_LookupUsersSignature, bool>> where);

        #endregion

        #region vw_LookupGridUsers
        List<vw_LookupGridUsers> Selectvw_LookupGridUserssByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUsers> SelectAllvw_LookupGridUserss();
        Task<IEnumerable<vw_LookupGridUsers>> SelectAllvw_LookupGridUserssAsync();
        Task<List<vw_LookupGridUsers>> Selectvw_LookupGridUserssByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUsers> SelectMany_vw_LookupGridUsers(Expression<Func<vw_LookupGridUsers, bool>> where);
        Task<IEnumerable<vw_LookupGridUsers>> SelectMany_vw_LookupGridUsersAsync(Expression<Func<vw_LookupGridUsers, bool>> where);
        vw_LookupGridUsers SelectSingle_vw_LookupGridUsers(Expression<Func<vw_LookupGridUsers, bool>> where);
        Task<vw_LookupGridUsers> SelectSingle_vw_LookupGridUsersAsync(Expression<Func<vw_LookupGridUsers, bool>> where);

        #endregion

        #region vw_LookupGridRole
        List<vw_LookupGridRole> Selectvw_LookupGridRolesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridRole> SelectAllvw_LookupGridRoles();
        Task<IEnumerable<vw_LookupGridRole>> SelectAllvw_LookupGridRolesAsync();
        Task<List<vw_LookupGridRole>> Selectvw_LookupGridRolesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridRole> SelectMany_vw_LookupGridRole(Expression<Func<vw_LookupGridRole, bool>> where);
        Task<IEnumerable<vw_LookupGridRole>> SelectMany_vw_LookupGridRoleAsync(Expression<Func<vw_LookupGridRole, bool>> where);
        vw_LookupGridRole SelectSingle_vw_LookupGridRole(Expression<Func<vw_LookupGridRole, bool>> where);
        Task<vw_LookupGridRole> SelectSingle_vw_LookupGridRoleAsync(Expression<Func<vw_LookupGridRole, bool>> where);

        #endregion

        #region vw_LookupGridUserGroup
        List<vw_LookupGridUserGroup> Selectvw_LookupGridUserGroupsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserGroup> SelectAllvw_LookupGridUserGroups();
        Task<IEnumerable<vw_LookupGridUserGroup>> SelectAllvw_LookupGridUserGroupsAsync();
        Task<List<vw_LookupGridUserGroup>> Selectvw_LookupGridUserGroupsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserGroup> SelectMany_vw_LookupGridUserGroup(Expression<Func<vw_LookupGridUserGroup, bool>> where);
        Task<IEnumerable<vw_LookupGridUserGroup>> SelectMany_vw_LookupGridUserGroupAsync(Expression<Func<vw_LookupGridUserGroup, bool>> where);
        vw_LookupGridUserGroup SelectSingle_vw_LookupGridUserGroup(Expression<Func<vw_LookupGridUserGroup, bool>> where);
        Task<vw_LookupGridUserGroup> SelectSingle_vw_LookupGridUserGroupAsync(Expression<Func<vw_LookupGridUserGroup, bool>> where);

        #endregion

        #region vw_LookupGridUserAccessType
        List<vw_LookupGridUserAccessType> Selectvw_LookupGridUserAccessTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserAccessType> SelectAllvw_LookupGridUserAccessTypes();
        Task<IEnumerable<vw_LookupGridUserAccessType>> SelectAllvw_LookupGridUserAccessTypesAsync();
        Task<List<vw_LookupGridUserAccessType>> Selectvw_LookupGridUserAccessTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserAccessType> SelectMany_vw_LookupGridUserAccessType(Expression<Func<vw_LookupGridUserAccessType, bool>> where);
        Task<IEnumerable<vw_LookupGridUserAccessType>> SelectMany_vw_LookupGridUserAccessTypeAsync(Expression<Func<vw_LookupGridUserAccessType, bool>> where);
        vw_LookupGridUserAccessType SelectSingle_vw_LookupGridUserAccessType(Expression<Func<vw_LookupGridUserAccessType, bool>> where);
        Task<vw_LookupGridUserAccessType> SelectSingle_vw_LookupGridUserAccessTypeAsync(Expression<Func<vw_LookupGridUserAccessType, bool>> where);

        #endregion

        #region vw_LookupUserGroup
        List<vw_LookupUserGroup> Selectvw_LookupUserGroupsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUserGroup> SelectAllvw_LookupUserGroups();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupUserGroupsAsync();
        Task<List<vw_LookupUserGroup>> Selectvw_LookupUserGroupsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUserGroup> SelectMany_vw_LookupUserGroup(Expression<Func<vw_LookupUserGroup, bool>> where);
        Task<IEnumerable<vw_LookupUserGroup>> SelectMany_vw_LookupUserGroupAsync(Expression<Func<vw_LookupUserGroup, bool>> where);
        vw_LookupUserGroup SelectSingle_vw_LookupUserGroup(Expression<Func<vw_LookupUserGroup, bool>> where);
        Task<vw_LookupUserGroup> SelectSingle_vw_LookupUserGroupAsync(Expression<Func<vw_LookupUserGroup, bool>> where);

        #endregion

        #region vw_LookupGridUserType
        List<vw_LookupGridUserType> Selectvw_LookupGridUserTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserType> SelectAllvw_LookupGridUserTypes();
        Task<IEnumerable<vw_LookupGridUserType>> SelectAllvw_LookupGridUserTypesAsync();
        Task<List<vw_LookupGridUserType>> Selectvw_LookupGridUserTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridUserType> SelectMany_vw_LookupGridUserType(Expression<Func<vw_LookupGridUserType, bool>> where);
        Task<IEnumerable<vw_LookupGridUserType>> SelectMany_vw_LookupGridUserTypeAsync(Expression<Func<vw_LookupGridUserType, bool>> where);
        vw_LookupGridUserType SelectSingle_vw_LookupGridUserType(Expression<Func<vw_LookupGridUserType, bool>> where);
        Task<vw_LookupGridUserType> SelectSingle_vw_LookupGridUserTypeAsync(Expression<Func<vw_LookupGridUserType, bool>> where);

        #endregion

        #region vw_LookupRole
        List<vw_LookupRole> Selectvw_LookupRolesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupRole> SelectAllvw_LookupRoles();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupRolesAsync();
        Task<List<vw_LookupRole>> Selectvw_LookupRolesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupRole> SelectMany_vw_LookupRole(Expression<Func<vw_LookupRole, bool>> where);
        Task<IEnumerable<vw_LookupRole>> SelectMany_vw_LookupRoleAsync(Expression<Func<vw_LookupRole, bool>> where);
        vw_LookupRole SelectSingle_vw_LookupRole(Expression<Func<vw_LookupRole, bool>> where);
        Task<vw_LookupRole> SelectSingle_vw_LookupRoleAsync(Expression<Func<vw_LookupRole, bool>> where);

        #endregion

        #region vw_LookupTemplateType
        List<vw_LookupTemplateType> Selectvw_LookupTemplateTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupTemplateType> SelectAllvw_LookupTemplateTypes();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupTemplateTypesAsync();
        Task<List<vw_LookupTemplateType>> Selectvw_LookupTemplateTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupTemplateType> SelectMany_vw_LookupTemplateType(Expression<Func<vw_LookupTemplateType, bool>> where);
        Task<IEnumerable<vw_LookupTemplateType>> SelectMany_vw_LookupTemplateTypeAsync(Expression<Func<vw_LookupTemplateType, bool>> where);
        vw_LookupTemplateType SelectSingle_vw_LookupTemplateType(Expression<Func<vw_LookupTemplateType, bool>> where);
        Task<vw_LookupTemplateType> SelectSingle_vw_LookupTemplateTypeAsync(Expression<Func<vw_LookupTemplateType, bool>> where);

        #endregion

        #region vw_LookupUsers
        List<vw_LookupUsers> Selectvw_LookupUserssByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUsers> SelectAllvw_LookupUserss();
        Task<IEnumerable<vw_LookupUsers>> SelectAllvw_LookupUserssAsync();
        Task<List<vw_LookupUsers>> Selectvw_LookupUserssByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUsers> SelectMany_vw_LookupUsers(Expression<Func<vw_LookupUsers, bool>> where);
        Task<IEnumerable<vw_LookupUsers>> SelectMany_vw_LookupUsersAsync(Expression<Func<vw_LookupUsers, bool>> where);
        vw_LookupUsers SelectSingle_vw_LookupUsers(Expression<Func<vw_LookupUsers, bool>> where);
        Task<vw_LookupUsers> SelectSingle_vw_LookupUsersAsync(Expression<Func<vw_LookupUsers, bool>> where);

        #endregion

        #region vw_LookupDashboardOptionGroup
        List<vw_LookupDashboardOptionGroup> Selectvw_LookupDashboardOptionGroupsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardOptionGroup> SelectAllvw_LookupDashboardOptionGroups();
        Task<IEnumerable<vw_LookupDashboardOptionGroup>> SelectAllvw_LookupDashboardOptionGroupsAsync();
        Task<List<vw_LookupDashboardOptionGroup>> Selectvw_LookupDashboardOptionGroupsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardOptionGroup> SelectMany_vw_LookupDashboardOptionGroup(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where);
        Task<IEnumerable<vw_LookupDashboardOptionGroup>> SelectMany_vw_LookupDashboardOptionGroupAsync(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where);
        vw_LookupDashboardOptionGroup SelectSingle_vw_LookupDashboardOptionGroup(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where);
        Task<vw_LookupDashboardOptionGroup> SelectSingle_vw_LookupDashboardOptionGroupAsync(Expression<Func<vw_LookupDashboardOptionGroup, bool>> where);

        #endregion        

        #region vw_LookupUserAccessType
        List<vw_LookupUserAccessType> Selectvw_LookupUserAccessTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUserAccessType> SelectAllvw_LookupUserAccessTypes();
        Task<IEnumerable<vw_LookupUserAccessType>> SelectAllvw_LookupUserAccessTypesAsync();
        Task<List<vw_LookupUserAccessType>> Selectvw_LookupUserAccessTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUserAccessType> SelectMany_vw_LookupUserAccessType(Expression<Func<vw_LookupUserAccessType, bool>> where);
        Task<IEnumerable<vw_LookupUserAccessType>> SelectMany_vw_LookupUserAccessTypeAsync(Expression<Func<vw_LookupUserAccessType, bool>> where);
        vw_LookupUserAccessType SelectSingle_vw_LookupUserAccessType(Expression<Func<vw_LookupUserAccessType, bool>> where);
        Task<vw_LookupUserAccessType> SelectSingle_vw_LookupUserAccessTypeAsync(Expression<Func<vw_LookupUserAccessType, bool>> where);

        #endregion

        #region vw_LookupUserType
        List<vw_LookupUserType> Selectvw_LookupUserTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUserType> SelectAllvw_LookupUserTypes();
        Task<IEnumerable<vw_LookupUserType>> SelectAllvw_LookupUserTypesAsync();
        Task<List<vw_LookupUserType>> Selectvw_LookupUserTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUserType> SelectMany_vw_LookupUserType(Expression<Func<vw_LookupUserType, bool>> where);
        Task<IEnumerable<vw_LookupUserType>> SelectMany_vw_LookupUserTypeAsync(Expression<Func<vw_LookupUserType, bool>> where);
        vw_LookupUserType SelectSingle_vw_LookupUserType(Expression<Func<vw_LookupUserType, bool>> where);
        Task<vw_LookupUserType> SelectSingle_vw_LookupUserTypeAsync(Expression<Func<vw_LookupUserType, bool>> where);

        #endregion

        #region vw_TemplateGrid
        List<vw_TemplateGrid> Selectvw_TemplateGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_TemplateGrid> SelectAllvw_TemplateGrids();
        Task<IEnumerable<vw_TemplateGrid>> SelectAllvw_TemplateGridsAsync();
        Task<List<vw_TemplateGrid>> Selectvw_TemplateGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_TemplateGrid> SelectMany_vw_TemplateGrid(Expression<Func<vw_TemplateGrid, bool>> where);
        Task<IEnumerable<vw_TemplateGrid>> SelectMany_vw_TemplateGridAsync(Expression<Func<vw_TemplateGrid, bool>> where);
        vw_TemplateGrid SelectSingle_vw_TemplateGrid(Expression<Func<vw_TemplateGrid, bool>> where);
        Task<vw_TemplateGrid> SelectSingle_vw_TemplateGridAsync(Expression<Func<vw_TemplateGrid, bool>> where);

        #endregion

        #region vw_ExportLogGrid
        List<vw_ExportLogGrid> Selectvw_ExportLogGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_ExportLogGrid> SelectAllvw_ExportLogGrids();
        Task<IEnumerable<vw_ExportLogGrid>> SelectAllvw_ExportLogGridsAsync();
        Task<List<vw_ExportLogGrid>> Selectvw_ExportLogGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_ExportLogGrid> SelectMany_vw_ExportLogGrid(Expression<Func<vw_ExportLogGrid, bool>> where);
        Task<IEnumerable<vw_ExportLogGrid>> SelectMany_vw_ExportLogGridAsync(Expression<Func<vw_ExportLogGrid, bool>> where);
        vw_ExportLogGrid SelectSingle_vw_ExportLogGrid(Expression<Func<vw_ExportLogGrid, bool>> where);
        Task<vw_ExportLogGrid> SelectSingle_vw_ExportLogGridAsync(Expression<Func<vw_ExportLogGrid, bool>> where);

        #endregion

        #region vw_UserGrid
        List<vw_UserGrid> Selectvw_UserGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserGrid> SelectAllvw_UserGrids();
        Task<IEnumerable<vw_UserGrid>> SelectAllvw_UserGridsAsync();
        Task<List<vw_UserGrid>> Selectvw_UserGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserGrid> SelectMany_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where);
        Task<IEnumerable<vw_UserGrid>> SelectMany_vw_UserGridAsync(Expression<Func<vw_UserGrid, bool>> where);
        vw_UserGrid SelectSingle_vw_UserGrid(Expression<Func<vw_UserGrid, bool>> where);
        Task<vw_UserGrid> SelectSingle_vw_UserGridAsync(Expression<Func<vw_UserGrid, bool>> where);

        #endregion

        #region vw_LookupTypeGrid
        List<vw_LookupTypeGrid> Selectvw_LookupTypeGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupTypeGrid> SelectAllvw_LookupTypeGrids();
        Task<IEnumerable<vw_LookupTypeGrid>> SelectAllvw_LookupTypeGridsAsync();
        Task<List<vw_LookupTypeGrid>> Selectvw_LookupTypeGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupTypeGrid> SelectMany_vw_LookupTypeGrid(Expression<Func<vw_LookupTypeGrid, bool>> where);
        Task<IEnumerable<vw_LookupTypeGrid>> SelectMany_vw_LookupTypeGridAsync(Expression<Func<vw_LookupTypeGrid, bool>> where);
        vw_LookupTypeGrid SelectSingle_vw_LookupTypeGrid(Expression<Func<vw_LookupTypeGrid, bool>> where);
        Task<vw_LookupTypeGrid> SelectSingle_vw_LookupTypeGridAsync(Expression<Func<vw_LookupTypeGrid, bool>> where);

        #endregion

        #region vw_LookupMenuOption
        List<vw_LookupMenuOption> Selectvw_LookupMenuOptionsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupMenuOption> SelectAllvw_LookupMenuOptions();
        Task<IEnumerable<vw_LookupMenuOption>> SelectAllvw_LookupMenuOptionsAsync();
        Task<List<vw_LookupMenuOption>> Selectvw_LookupMenuOptionsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupMenuOption> SelectMany_vw_LookupMenuOption(Expression<Func<vw_LookupMenuOption, bool>> where);
        Task<IEnumerable<vw_LookupMenuOption>> SelectMany_vw_LookupMenuOptionAsync(Expression<Func<vw_LookupMenuOption, bool>> where);
        vw_LookupMenuOption SelectSingle_vw_LookupMenuOption(Expression<Func<vw_LookupMenuOption, bool>> where);
        Task<vw_LookupMenuOption> SelectSingle_vw_LookupMenuOptionAsync(Expression<Func<vw_LookupMenuOption, bool>> where);

        #endregion

        #region vw_LookupList
        List<vw_LookupList> Selectvw_LookupListsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupList> SelectAllvw_LookupLists();
        Task<IEnumerable<vw_LookupList>> SelectAllvw_LookupListsAsync();
        Task<List<vw_LookupList>> Selectvw_LookupListsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupList> SelectMany_vw_LookupList(Expression<Func<vw_LookupList, bool>> where);
        Task<IEnumerable<vw_LookupList>> SelectMany_vw_LookupListAsync(Expression<Func<vw_LookupList, bool>> where);
        vw_LookupList SelectSingle_vw_LookupList(Expression<Func<vw_LookupList, bool>> where);
        Task<vw_LookupList> SelectSingle_vw_LookupListAsync(Expression<Func<vw_LookupList, bool>> where);

        #endregion

        #region vw_LookupDashboardOption
        List<vw_LookupDashboardOption> Selectvw_LookupDashboardOptionsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardOption> SelectAllvw_LookupDashboardOptions();
        Task<IEnumerable<vw_LookupDashboardOption>> SelectAllvw_LookupDashboardOptionsAsync();
        Task<List<vw_LookupDashboardOption>> Selectvw_LookupDashboardOptionsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardOption> SelectMany_vw_LookupDashboardOption(Expression<Func<vw_LookupDashboardOption, bool>> where);
        Task<IEnumerable<vw_LookupDashboardOption>> SelectMany_vw_LookupDashboardOptionAsync(Expression<Func<vw_LookupDashboardOption, bool>> where);
        vw_LookupDashboardOption SelectSingle_vw_LookupDashboardOption(Expression<Func<vw_LookupDashboardOption, bool>> where);
        Task<vw_LookupDashboardOption> SelectSingle_vw_LookupDashboardOptionAsync(Expression<Func<vw_LookupDashboardOption, bool>> where);

        #endregion

        #region vw_LookupFromEmailAddress
        List<vw_LookupFromEmailAddress> Selectvw_LookupFromEmailAddresssByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupFromEmailAddress> SelectAllvw_LookupFromEmailAddresss();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupFromEmailAddresssAsync();
        Task<List<vw_LookupFromEmailAddress>> Selectvw_LookupFromEmailAddresssByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupFromEmailAddress> SelectMany_vw_LookupFromEmailAddress(Expression<Func<vw_LookupFromEmailAddress, bool>> where);
        Task<IEnumerable<vw_LookupFromEmailAddress>> SelectMany_vw_LookupFromEmailAddressAsync(Expression<Func<vw_LookupFromEmailAddress, bool>> where);
        vw_LookupFromEmailAddress SelectSingle_vw_LookupFromEmailAddress(Expression<Func<vw_LookupFromEmailAddress, bool>> where);
        Task<vw_LookupFromEmailAddress> SelectSingle_vw_LookupFromEmailAddressAsync(Expression<Func<vw_LookupFromEmailAddress, bool>> where);

        #endregion                

        #region UserExportLog

        UserExportLog SelectByUserExportLogId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<UserExportLog> SelectMany_UserExportLog(Expression<Func<UserExportLog, bool>> where);

        UserExportLog SelectSingle_UserExportLog(Expression<Func<UserExportLog, bool>> where);

        IEnumerable<UserExportLog> SelectAllUserExportLogs();

        bool SaveUserExportLogForm(UserExportLog lookupTypeRepository);

        bool DeleteUserExportLogForm(int lookupTypeId);

        Task<UserExportLog> SelectByUserExportLogIdAsync(int lookupTypeId);

        Task<IEnumerable<UserExportLog>> SelectMany_UserExportLogAsync(Expression<Func<UserExportLog, bool>> where);

        Task<UserExportLog> SelectSingle_UserExportLogAsync(Expression<Func<UserExportLog, bool>> where);

        Task<IEnumerable<UserExportLog>> SelectAllUserExportLogsAsync();

        Task<int> SaveUserExportLogFormAsync(UserExportLog lookupTypeRepository);

        Task<int> DeleteUserExportLogFormAsync(int lookupTypeId);
        #endregion

        #region vw_LookupTemplateAllowedType
        List<vw_LookupTemplateAllowedType> Selectvw_LookupTemplateAllowedTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupTemplateAllowedType> SelectAllvw_LookupTemplateAllowedTypes();
        Task<IEnumerable<vw_LookupTemplateAllowedType>> SelectAllvw_LookupTemplateAllowedTypesAsync();
        Task<List<vw_LookupTemplateAllowedType>> Selectvw_LookupTemplateAllowedTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupTemplateAllowedType> SelectMany_vw_LookupTemplateAllowedType(Expression<Func<vw_LookupTemplateAllowedType, bool>> where);
        Task<IEnumerable<vw_LookupTemplateAllowedType>> SelectMany_vw_LookupTemplateAllowedTypeAsync(Expression<Func<vw_LookupTemplateAllowedType, bool>> where);
        vw_LookupTemplateAllowedType SelectSingle_vw_LookupTemplateAllowedType(Expression<Func<vw_LookupTemplateAllowedType, bool>> where);
        Task<vw_LookupTemplateAllowedType> SelectSingle_vw_LookupTemplateAllowedTypeAsync(Expression<Func<vw_LookupTemplateAllowedType, bool>> where);

        #endregion

        #region vw_LookupGridAlertType
        List<vw_LookupGridAlertType> Selectvw_LookupGridAlertTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridAlertType> SelectAllvw_LookupGridAlertTypes();
        Task<IEnumerable<vw_LookupGridAlertType>> SelectAllvw_LookupGridAlertTypesAsync();
        Task<List<vw_LookupGridAlertType>> Selectvw_LookupGridAlertTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridAlertType> SelectMany_vw_LookupGridAlertType(Expression<Func<vw_LookupGridAlertType, bool>> where);
        Task<IEnumerable<vw_LookupGridAlertType>> SelectMany_vw_LookupGridAlertTypeAsync(Expression<Func<vw_LookupGridAlertType, bool>> where);
        vw_LookupGridAlertType SelectSingle_vw_LookupGridAlertType(Expression<Func<vw_LookupGridAlertType, bool>> where);
        Task<vw_LookupGridAlertType> SelectSingle_vw_LookupGridAlertTypeAsync(Expression<Func<vw_LookupGridAlertType, bool>> where);

        #endregion

        #region vw_LookupGridEmailTemplateType
        List<vw_LookupGridEmailTemplateType> Selectvw_LookupGridEmailTemplateTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridEmailTemplateType> SelectAllvw_LookupGridEmailTemplateTypes();
        Task<IEnumerable<vw_LookupGridEmailTemplateType>> SelectAllvw_LookupGridEmailTemplateTypesAsync();
        Task<List<vw_LookupGridEmailTemplateType>> Selectvw_LookupGridEmailTemplateTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridEmailTemplateType> SelectMany_vw_LookupGridEmailTemplateType(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where);
        Task<IEnumerable<vw_LookupGridEmailTemplateType>> SelectMany_vw_LookupGridEmailTemplateTypeAsync(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where);
        vw_LookupGridEmailTemplateType SelectSingle_vw_LookupGridEmailTemplateType(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where);
        Task<vw_LookupGridEmailTemplateType> SelectSingle_vw_LookupGridEmailTemplateTypeAsync(Expression<Func<vw_LookupGridEmailTemplateType, bool>> where);

        #endregion

        #region vw_LookupGridEmailType
        List<vw_LookupGridEmailType> Selectvw_LookupGridEmailTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridEmailType> SelectAllvw_LookupGridEmailTypes();
        Task<IEnumerable<vw_LookupGridEmailType>> SelectAllvw_LookupGridEmailTypesAsync();
        Task<List<vw_LookupGridEmailType>> Selectvw_LookupGridEmailTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridEmailType> SelectMany_vw_LookupGridEmailType(Expression<Func<vw_LookupGridEmailType, bool>> where);
        Task<IEnumerable<vw_LookupGridEmailType>> SelectMany_vw_LookupGridEmailTypeAsync(Expression<Func<vw_LookupGridEmailType, bool>> where);
        vw_LookupGridEmailType SelectSingle_vw_LookupGridEmailType(Expression<Func<vw_LookupGridEmailType, bool>> where);
        Task<vw_LookupGridEmailType> SelectSingle_vw_LookupGridEmailTypeAsync(Expression<Func<vw_LookupGridEmailType, bool>> where);

        #endregion

        #region vw_LookupGridTemplateAllowedType
        List<vw_LookupGridTemplateAllowedType> Selectvw_LookupGridTemplateAllowedTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupGridTemplateAllowedType> SelectAllvw_LookupGridTemplateAllowedTypes();
        Task<IEnumerable<vw_LookupGridTemplateAllowedType>> SelectAllvw_LookupGridTemplateAllowedTypesAsync();
        Task<List<vw_LookupGridTemplateAllowedType>> Selectvw_LookupGridTemplateAllowedTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupGridTemplateAllowedType> SelectMany_vw_LookupGridTemplateAllowedType(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where);
        Task<IEnumerable<vw_LookupGridTemplateAllowedType>> SelectMany_vw_LookupGridTemplateAllowedTypeAsync(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where);
        vw_LookupGridTemplateAllowedType SelectSingle_vw_LookupGridTemplateAllowedType(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where);
        Task<vw_LookupGridTemplateAllowedType> SelectSingle_vw_LookupGridTemplateAllowedTypeAsync(Expression<Func<vw_LookupGridTemplateAllowedType, bool>> where);

        #endregion

        #region vw_LookupEmailTemplateType
        List<vw_LookupEmailTemplateType> Selectvw_LookupEmailTemplateTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupEmailTemplateType> SelectAllvw_LookupEmailTemplateTypes();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupEmailTemplateTypesAsync();
        Task<List<vw_LookupEmailTemplateType>> Selectvw_LookupEmailTemplateTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupEmailTemplateType> SelectMany_vw_LookupEmailTemplateType(Expression<Func<vw_LookupEmailTemplateType, bool>> where);
        Task<IEnumerable<vw_LookupEmailTemplateType>> SelectMany_vw_LookupEmailTemplateTypeAsync(Expression<Func<vw_LookupEmailTemplateType, bool>> where);
        vw_LookupEmailTemplateType SelectSingle_vw_LookupEmailTemplateType(Expression<Func<vw_LookupEmailTemplateType, bool>> where);
        Task<vw_LookupEmailTemplateType> SelectSingle_vw_LookupEmailTemplateTypeAsync(Expression<Func<vw_LookupEmailTemplateType, bool>> where);

        #endregion

        #region vw_LookupEmailType
        List<vw_LookupEmailType> Selectvw_LookupEmailTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupEmailType> SelectAllvw_LookupEmailTypes();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupEmailTypesAsync();
        Task<List<vw_LookupEmailType>> Selectvw_LookupEmailTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupEmailType> SelectMany_vw_LookupEmailType(Expression<Func<vw_LookupEmailType, bool>> where);
        Task<IEnumerable<vw_LookupEmailType>> SelectMany_vw_LookupEmailTypeAsync(Expression<Func<vw_LookupEmailType, bool>> where);
        vw_LookupEmailType SelectSingle_vw_LookupEmailType(Expression<Func<vw_LookupEmailType, bool>> where);
        Task<vw_LookupEmailType> SelectSingle_vw_LookupEmailTypeAsync(Expression<Func<vw_LookupEmailType, bool>> where);

        #endregion

        #region vw_LookupAlertType
        List<vw_LookupAlertType> Selectvw_LookupAlertTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupAlertType> SelectAllvw_LookupAlertTypes();
        Task<IEnumerable<LookupEntity>> SelectAllvw_LookupAlertTypesAsync();
        Task<List<vw_LookupAlertType>> Selectvw_LookupAlertTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupAlertType> SelectMany_vw_LookupAlertType(Expression<Func<vw_LookupAlertType, bool>> where);
        Task<IEnumerable<vw_LookupAlertType>> SelectMany_vw_LookupAlertTypeAsync(Expression<Func<vw_LookupAlertType, bool>> where);
        vw_LookupAlertType SelectSingle_vw_LookupAlertType(Expression<Func<vw_LookupAlertType, bool>> where);
        Task<vw_LookupAlertType> SelectSingle_vw_LookupAlertTypeAsync(Expression<Func<vw_LookupAlertType, bool>> where);

        #endregion

        #region vw_LookupAccessType
        List<vw_LookupAccessType> Selectvw_LookupAccessTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupAccessType> SelectAllvw_LookupAccessTypes();
        Task<IEnumerable<vw_LookupAccessType>> SelectAllvw_LookupAccessTypesAsync();
        Task<List<vw_LookupAccessType>> Selectvw_LookupAccessTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupAccessType> SelectMany_vw_LookupAccessType(Expression<Func<vw_LookupAccessType, bool>> where);
        Task<IEnumerable<vw_LookupAccessType>> SelectMany_vw_LookupAccessTypeAsync(Expression<Func<vw_LookupAccessType, bool>> where);
        vw_LookupAccessType SelectSingle_vw_LookupAccessType(Expression<Func<vw_LookupAccessType, bool>> where);
        Task<vw_LookupAccessType> SelectSingle_vw_LookupAccessTypeAsync(Expression<Func<vw_LookupAccessType, bool>> where);

        #endregion

        #region vw_LookupObject
        List<vw_LookupObject> Selectvw_LookupObjectsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupObject> SelectAllvw_LookupObjects();
        Task<IEnumerable<vw_LookupObject>> SelectAllvw_LookupObjectsAsync();
        Task<List<vw_LookupObject>> Selectvw_LookupObjectsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupObject> SelectMany_vw_LookupObject(Expression<Func<vw_LookupObject, bool>> where);
        Task<IEnumerable<vw_LookupObject>> SelectMany_vw_LookupObjectAsync(Expression<Func<vw_LookupObject, bool>> where);
        vw_LookupObject SelectSingle_vw_LookupObject(Expression<Func<vw_LookupObject, bool>> where);
        Task<vw_LookupObject> SelectSingle_vw_LookupObjectAsync(Expression<Func<vw_LookupObject, bool>> where);

        #endregion

        #region vw_LookupObjectType
        List<vw_LookupObjectType> Selectvw_LookupObjectTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupObjectType> SelectAllvw_LookupObjectTypes();
        Task<IEnumerable<vw_LookupObjectType>> SelectAllvw_LookupObjectTypesAsync();
        Task<List<vw_LookupObjectType>> Selectvw_LookupObjectTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupObjectType> SelectMany_vw_LookupObjectType(Expression<Func<vw_LookupObjectType, bool>> where);
        Task<IEnumerable<vw_LookupObjectType>> SelectMany_vw_LookupObjectTypeAsync(Expression<Func<vw_LookupObjectType, bool>> where);
        vw_LookupObjectType SelectSingle_vw_LookupObjectType(Expression<Func<vw_LookupObjectType, bool>> where);
        Task<vw_LookupObjectType> SelectSingle_vw_LookupObjectTypeAsync(Expression<Func<vw_LookupObjectType, bool>> where);

        #endregion

        #region TemplateTag

        TemplateTag SelectByTemplateTagId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<TemplateTag> SelectMany_TemplateTag(Expression<Func<TemplateTag, bool>> where);

        TemplateTag SelectSingle_TemplateTag(Expression<Func<TemplateTag, bool>> where);

        IEnumerable<TemplateTag> SelectAllTemplateTags();

        bool SaveTemplateTagForm(TemplateTag lookupTypeRepository);

        bool DeleteTemplateTagForm(int lookupTypeId);

        Task<TemplateTag> SelectByTemplateTagIdAsync(int lookupTypeId);

        Task<IEnumerable<TemplateTag>> SelectMany_TemplateTagAsync(Expression<Func<TemplateTag, bool>> where);

        Task<TemplateTag> SelectSingle_TemplateTagAsync(Expression<Func<TemplateTag, bool>> where);

        Task<IEnumerable<TemplateTag>> SelectAllTemplateTagsAsync();

        Task<int> SaveTemplateTagFormAsync(TemplateTag lookupTypeRepository);

        Task<int> DeleteTemplateTagFormAsync(int lookupTypeId);
        #endregion

        #region vw_LookupDashboardObjectType
        List<vw_LookupDashboardObjectType> Selectvw_LookupDashboardObjectTypesByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardObjectType> SelectAllvw_LookupDashboardObjectTypes();
        Task<IEnumerable<vw_LookupDashboardObjectType>> SelectAllvw_LookupDashboardObjectTypesAsync();
        Task<List<vw_LookupDashboardObjectType>> Selectvw_LookupDashboardObjectTypesByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardObjectType> SelectMany_vw_LookupDashboardObjectType(Expression<Func<vw_LookupDashboardObjectType, bool>> where);
        Task<IEnumerable<vw_LookupDashboardObjectType>> SelectMany_vw_LookupDashboardObjectTypeAsync(Expression<Func<vw_LookupDashboardObjectType, bool>> where);
        vw_LookupDashboardObjectType SelectSingle_vw_LookupDashboardObjectType(Expression<Func<vw_LookupDashboardObjectType, bool>> where);
        Task<vw_LookupDashboardObjectType> SelectSingle_vw_LookupDashboardObjectTypeAsync(Expression<Func<vw_LookupDashboardObjectType, bool>> where);

        #endregion

        #region vw_LookupDashboardObject
        List<vw_LookupDashboardObject> Selectvw_LookupDashboardObjectsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardObject> SelectAllvw_LookupDashboardObjects();
        Task<IEnumerable<vw_LookupDashboardObject>> SelectAllvw_LookupDashboardObjectsAsync();
        Task<List<vw_LookupDashboardObject>> Selectvw_LookupDashboardObjectsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupDashboardObject> SelectMany_vw_LookupDashboardObject(Expression<Func<vw_LookupDashboardObject, bool>> where);
        Task<IEnumerable<vw_LookupDashboardObject>> SelectMany_vw_LookupDashboardObjectAsync(Expression<Func<vw_LookupDashboardObject, bool>> where);
        vw_LookupDashboardObject SelectSingle_vw_LookupDashboardObject(Expression<Func<vw_LookupDashboardObject, bool>> where);
        Task<vw_LookupDashboardObject> SelectSingle_vw_LookupDashboardObjectAsync(Expression<Func<vw_LookupDashboardObject, bool>> where);

        #endregion

        #region vw_LookupSetting
        List<vw_LookupSetting> Selectvw_LookupSettingsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupSetting> SelectAllvw_LookupSettings();
        Task<IEnumerable<vw_LookupSetting>> SelectAllvw_LookupSettingsAsync();
        Task<List<vw_LookupSetting>> Selectvw_LookupSettingsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupSetting> SelectMany_vw_LookupSetting(Expression<Func<vw_LookupSetting, bool>> where);
        Task<IEnumerable<vw_LookupSetting>> SelectMany_vw_LookupSettingAsync(Expression<Func<vw_LookupSetting, bool>> where);
        vw_LookupSetting SelectSingle_vw_LookupSetting(Expression<Func<vw_LookupSetting, bool>> where);
        Task<vw_LookupSetting> SelectSingle_vw_LookupSettingAsync(Expression<Func<vw_LookupSetting, bool>> where);

        #endregion

    }
}