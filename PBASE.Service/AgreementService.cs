using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Repository;
using Microsoft.AspNet.Identity;
using PBASE.Entity.Enum;
using System.Threading.Tasks;
using System.Web;
using PBASE.Domain.Enum;

namespace PBASE.Service
{
    public partial class AgreementService : BaseService, IAgreementService
    {
        #region Initialization
        private readonly Ivw_AgreementGridRepository vw_AgreementGridRepository;
        private readonly Ivw_AgreementUserSubGridRepository vw_AgreementUserSubGridRepository;
        private readonly Ivw_UserAgreementSubGridRepository vw_UserAgreementSubGridRepository;
        private readonly Ivw_UserAgreementFormRepository vw_UserAgreementFormRepository;
        private readonly IAgreementRepository AgreementRepository;
        private readonly IAgreementUserTypeRepository AgreementUserTypeRepository;
        private readonly IUserAgreementRepository userAgreementRepository;
        private readonly Ivw_AgreementVersionSubGridRepository vw_AgreementVersionSubGridRepository;
        private readonly Ivw_AgreementPreviousVersionNumberRepository vw_AgreementPreviousVersionNumberRepository;
        private readonly IUnitOfWork unitOfWork;


        public AgreementService(
            Ivw_AgreementGridRepository vw_AgreementGridRepository,
            IAgreementRepository AgreementRepository,
            IAgreementUserTypeRepository AgreementUserTypeRepository,
            Ivw_AgreementUserSubGridRepository vw_AgreementUserSubGridRepository,
            Ivw_UserAgreementSubGridRepository vw_UserAgreementSubGridRepository,
            Ivw_UserAgreementFormRepository vw_UserAgreementFormRepository,
            IUserAgreementRepository userAgreementRepository,
            Ivw_AgreementVersionSubGridRepository vw_AgreementVersionSubGridRepository,
            Ivw_AgreementPreviousVersionNumberRepository vw_AgreementPreviousVersionNumberRepository,

        IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
            this.vw_AgreementGridRepository = vw_AgreementGridRepository;
            this.AgreementRepository = AgreementRepository;
            this.AgreementUserTypeRepository = AgreementUserTypeRepository;
            this.vw_AgreementUserSubGridRepository = vw_AgreementUserSubGridRepository;
            this.vw_UserAgreementSubGridRepository = vw_UserAgreementSubGridRepository;
            this.vw_UserAgreementFormRepository = vw_UserAgreementFormRepository;
            this.userAgreementRepository = userAgreementRepository;
            this.vw_AgreementVersionSubGridRepository = vw_AgreementVersionSubGridRepository;
            this.vw_AgreementPreviousVersionNumberRepository = vw_AgreementPreviousVersionNumberRepository;

        }

        public void Save()
        {
            int userId = HttpContext.Current.User.Identity.GetUserId<int>();
            userId = (userId != 0 ? userId : 1); //if user is not logged-in then pass userId = 1 (for reset-password scenario)
            unitOfWork.Commit(userId);
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        #endregion Initialization

        //#region Agreement

        //#region Sync Methods

        //public Agreement SelectByAgreementId(int AgreementId, bool cacheRecord = false)
        //{
        //    if (!cacheRecord)
        //    {
        //        return emailRepository.SelectById(AgreementId);
        //    }
        //    else
        //    {
        //        Agreement Agreement = CacheService.Get<Agreement>("SelectByAgreementId" + AgreementId);
        //        if (Agreement == null)
        //        {
        //            Agreement = emailRepository.SelectById(AgreementId);
        //            CacheService.Add<Agreement>("SelectByAgreementId" + AgreementId, Agreement);
        //        }
        //        else
        //        {
        //            // One time cache only.
        //            CacheService.Clear("SelectByAgreementId" + AgreementId);
        //        }
        //        return Agreement;
        //    }
        //}

        //public IEnumerable<Agreement> SelectMany_Agreement(Expression<Func<Agreement, bool>> where)
        //{
        //    return emailRepository.SelectMany(where);
        //}

        //public Agreement SelectSingle_Agreement(Expression<Func<Agreement, bool>> where)
        //{
        //    return emailRepository.Select(where);
        //}

        //public IEnumerable<Agreement> SelectAllAgreements()
        //{
        //    return emailRepository.SelectAll();
        //}

        //public bool SaveAgreementForm(Agreement Agreement)
        //{
        //    try
        //    {
        //        if (Agreement.FormMode == FormMode.Create)
        //        {
        //            emailRepository.Add(Agreement);
        //        }
        //        else
        //        {
        //            emailRepository.Update(Agreement);
        //        }

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //public bool SaveAgreementList(List<Agreement> Agreements, List<Agreement> AgreementList)
        //{
        //    try
        //    {
        //        foreach (var item in Agreements)
        //        {
        //            if (AgreementList.Find(x => x.AgreementId == item.AgreementId) != null && AgreementList.Find(x => x.AgreementId == item.AgreementId).MessageId != null)
        //                item.MessageId = AgreementList.Find(x => x.AgreementId == item.AgreementId).MessageId;
        //            item.SentDate = DateTime.Now;
        //            item.Status = AgreementStatus.SentSuccessfully;
        //            emailRepository.Update(item);
        //        }

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}


        //public bool DeleteAgreementForm(int AgreementId)
        //{
        //    try
        //    {
        //        Agreement Agreement = emailRepository.SelectById(AgreementId);
        //        emailRepository.Delete(Agreement);

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //#endregion Sync Methods

        //#region Async Methods

        //public Task<Agreement> SelectByAgreementIdAsync(int AgreementId)
        //{
        //    return emailRepository.SelectByIdAsync(AgreementId);
        //}

        //public Task<IEnumerable<Agreement>> SelectMany_AgreementAsync(Expression<Func<Agreement, bool>> where)
        //{
        //    return emailRepository.SelectManyAsync(where);
        //}

        //public Task<Agreement> SelectSingle_AgreementAsync(Expression<Func<Agreement, bool>> where)
        //{
        //    return emailRepository.SelectAsync(where);
        //}

        //public Task<IEnumerable<Agreement>> SelectAllAgreementsAsync()
        //{
        //    return emailRepository.SelectAllAsync();
        //}

        //public Task<int> SaveAgreementFormAsync(Agreement Agreement)
        //{
        //    try
        //    {
        //        if (Agreement.AgreementId == 0)
        //        {
        //            emailRepository.Add(Agreement);
        //        }
        //        else
        //        {
        //            emailRepository.Update(Agreement);
        //        }

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //public Task<int> DeleteAgreementFormAsync(int AgreementId)
        //{
        //    try
        //    {
        //        Agreement Agreement = emailRepository.SelectById(AgreementId);
        //        emailRepository.Delete(Agreement);

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //#endregion Async Methods

        //#endregion Agreement

        //#region ApplicationInformation

        //#region Sync Methods

        //public ApplicationInformation SelectByApplicationInformationId(int ApplicationInformationId, bool cacheRecord = false)
        //{
        //    if (!cacheRecord)
        //    {
        //        return ApplicationInformationRepository.SelectById(ApplicationInformationId);
        //    }
        //    else
        //    {
        //        ApplicationInformation ApplicationInformation = CacheService.Get<ApplicationInformation>("SelectByApplicationInformationId" + ApplicationInformationId);
        //        if (ApplicationInformation == null)
        //        {
        //            ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
        //            CacheService.Add<ApplicationInformation>("SelectByApplicationInformationId" + ApplicationInformationId, ApplicationInformation);
        //        }
        //        else
        //        {
        //            // One time cache only.
        //            CacheService.Clear("SelectByApplicationInformationId" + ApplicationInformationId);
        //        }
        //        return ApplicationInformation;
        //    }
        //}

        //public IEnumerable<ApplicationInformation> SelectMany_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where)
        //{
        //    return ApplicationInformationRepository.SelectMany(where);
        //}

        //public ApplicationInformation SelectSingle_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where)
        //{
        //    return ApplicationInformationRepository.Select(where);
        //}

        //public IEnumerable<ApplicationInformation> SelectAllApplicationInformations()
        //{
        //    return ApplicationInformationRepository.SelectAll();
        //}

        //public bool SaveApplicationInformationForm(ApplicationInformation ApplicationInformation)
        //{
        //    try
        //    {
        //        if (ApplicationInformation.FormMode == FormMode.Create)
        //        {
        //            ApplicationInformationRepository.Add(ApplicationInformation);
        //        }
        //        else
        //        {
        //            ApplicationInformationRepository.Update(ApplicationInformation);
        //        }

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //public bool DeleteApplicationInformationForm(int ApplicationInformationId)
        //{
        //    try
        //    {
        //        ApplicationInformation ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
        //        ApplicationInformationRepository.Delete(ApplicationInformation);

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //#endregion Sync Methods

        //#region Async Methods

        //public Task<ApplicationInformation> SelectByApplicationInformationIdAsync(int ApplicationInformationId)
        //{
        //    return ApplicationInformationRepository.SelectByIdAsync(ApplicationInformationId);
        //}

        //public Task<IEnumerable<ApplicationInformation>> SelectMany_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where)
        //{
        //    return ApplicationInformationRepository.SelectManyAsync(where);
        //}

        //public Task<ApplicationInformation> SelectSingle_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where)
        //{
        //    return ApplicationInformationRepository.SelectAsync(where);
        //}

        //public Task<IEnumerable<ApplicationInformation>> SelectAllApplicationInformationsAsync()
        //{
        //    return ApplicationInformationRepository.SelectAllAsync();
        //}

        //public Task<int> SaveApplicationInformationFormAsync(ApplicationInformation ApplicationInformation)
        //{
        //    try
        //    {
        //        if (ApplicationInformation.ApplicationInformationId == 0)
        //        {
        //            ApplicationInformationRepository.Add(ApplicationInformation);
        //        }
        //        else
        //        {
        //            ApplicationInformationRepository.Update(ApplicationInformation);
        //        }

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //public Task<int> DeleteApplicationInformationFormAsync(int ApplicationInformationId)
        //{
        //    try
        //    {
        //        ApplicationInformation ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
        //        ApplicationInformationRepository.Delete(ApplicationInformation);

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //#endregion Async Methods

        //#endregion ApplicationInformation

        //#region EmailAttachment

        //#region Sync Methods

        //public EmailAttachment SelectByEmailAttachmentId(int EmailAttachmentId, bool cacheRecord = false)
        //{
        //    if (!cacheRecord)
        //    {
        //        return emailAttachmentRepository.SelectById(EmailAttachmentId);
        //    }
        //    else
        //    {
        //        EmailAttachment EmailAttachment = CacheService.Get<EmailAttachment>("SelectByEmailAttachmentId" + EmailAttachmentId);
        //        if (EmailAttachment == null)
        //        {
        //            EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
        //            CacheService.Add<EmailAttachment>("SelectByEmailAttachmentId" + EmailAttachmentId, EmailAttachment);
        //        }
        //        else
        //        {
        //            // One time cache only.
        //            CacheService.Clear("SelectByEmailAttachmentId" + EmailAttachmentId);
        //        }
        //        return EmailAttachment;
        //    }
        //}

        //public IEnumerable<EmailAttachment> SelectMany_EmailAttachment(Expression<Func<EmailAttachment, bool>> where)
        //{
        //    return emailAttachmentRepository.SelectMany(where);
        //}

        //public EmailAttachment SelectSingle_EmailAttachment(Expression<Func<EmailAttachment, bool>> where)
        //{
        //    return emailAttachmentRepository.Select(where);
        //}

        //public IEnumerable<EmailAttachment> SelectAllEmailAttachments()
        //{
        //    return emailAttachmentRepository.SelectAll();
        //}

        //public bool SaveEmailAttachmentForm(EmailAttachment EmailAttachment)
        //{
        //    try
        //    {
        //        if (EmailAttachment.FormMode == FormMode.Create)
        //        {
        //            emailAttachmentRepository.Add(EmailAttachment);
        //        }
        //        else
        //        {
        //            emailAttachmentRepository.Update(EmailAttachment);
        //        }

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //public bool DeleteEmailAttachmentForm(int EmailAttachmentId)
        //{
        //    try
        //    {
        //        EmailAttachment EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
        //        emailAttachmentRepository.Delete(EmailAttachment);

        //        Save();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //    }

        //    return false;
        //}

        //#endregion Sync Methods

        //#region Async Methods

        //public Task<EmailAttachment> SelectByEmailAttachmentIdAsync(int EmailAttachmentId)
        //{
        //    return emailAttachmentRepository.SelectByIdAsync(EmailAttachmentId);
        //}

        //public Task<IEnumerable<EmailAttachment>> SelectMany_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where)
        //{
        //    return emailAttachmentRepository.SelectManyAsync(where);
        //}

        //public Task<EmailAttachment> SelectSingle_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where)
        //{
        //    return emailAttachmentRepository.SelectAsync(where);
        //}

        //public Task<IEnumerable<EmailAttachment>> SelectAllEmailAttachmentsAsync()
        //{
        //    return emailAttachmentRepository.SelectAllAsync();
        //}

        //public Task<int> SaveEmailAttachmentFormAsync(EmailAttachment EmailAttachment)
        //{
        //    try
        //    {
        //        if (EmailAttachment.EmailAttachmentId == 0)
        //        {
        //            emailAttachmentRepository.Add(EmailAttachment);
        //        }
        //        else
        //        {
        //            emailAttachmentRepository.Update(EmailAttachment);
        //        }

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //public Task<int> DeleteEmailAttachmentFormAsync(int EmailAttachmentId)
        //{
        //    try
        //    {
        //        EmailAttachment EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
        //        emailAttachmentRepository.Delete(EmailAttachment);

        //        return SaveAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        base.ProcessServiceException(ex);
        //        return Task.FromResult(-1);
        //    }

        //}

        //#endregion Async Methods

        //#endregion EmailAttachment

        #region Agreement

        #region Sync Methods

        public Agreement SelectByAgreementId(int agreementId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return AgreementRepository.SelectById(agreementId);
            }
            else
            {
                Agreement agreement = CacheService.Get<Agreement>("SelectByAgreementId" + agreementId);
                if (agreement == null)
                {
                    agreement = AgreementRepository.SelectById(agreementId);
                    CacheService.Add<Agreement>("SelectByAgreementId" + agreementId, agreement);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByAgreementId" + agreementId);
                }
                return agreement;
            }
        }

        public IEnumerable<Agreement> SelectMany_Agreement(Expression<Func<Agreement, bool>> where)
        {
            return AgreementRepository.SelectMany(where);
        }

        public Agreement SelectSingle_Agreement(Expression<Func<Agreement, bool>> where)
        {
            return AgreementRepository.Select(where);
        }

        public IEnumerable<Agreement> SelectAllAgreements()
        {
            return AgreementRepository.SelectAll();
        }

        public bool SaveAgreementForm(Agreement agreement)
        {
            try
            {
                if (agreement.FormMode == FormMode.Create)
                {
                    AgreementRepository.Add(agreement);
                }
                else
                {
                    AgreementRepository.Update(agreement);
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

        public bool DeleteAgreementForm(int agreementId)
        {
            try
            {
                Agreement agreement = AgreementRepository.SelectById(agreementId);
                AgreementRepository.Delete(agreement);

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

        public Task<Agreement> SelectByAgreementIdAsync(int agreementId)
        {
            return AgreementRepository.SelectByIdAsync(agreementId);
        }

        public Task<IEnumerable<Agreement>> SelectMany_AgreementAsync(Expression<Func<Agreement, bool>> where)
        {
            return AgreementRepository.SelectManyAsync(where);
        }

        public Task<Agreement> SelectSingle_AgreementAsync(Expression<Func<Agreement, bool>> where)
        {
            return AgreementRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Agreement>> SelectAllAgreementsAsync()
        {
            return AgreementRepository.SelectAllAsync();
        }

        public Task<int> SaveAgreementFormAsync(Agreement agreement)
        {
            try
            {
                if (agreement.AgreementId == 0)
                {
                    AgreementRepository.Add(agreement);
                }
                else
                {
                    AgreementRepository.Update(agreement);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteAgreementFormAsync(int agreementId)
        {
            try
            {
                Agreement agreement = AgreementRepository.SelectById(agreementId);
                AgreementRepository.Delete(agreement);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Agreement 

        #region AgreementUserType

        #region Sync Methods

        public AgreementUserType SelectByAgreementUserTypeId(int AgreementUserTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return AgreementUserTypeRepository.SelectById(AgreementUserTypeId);
            }
            else
            {
                AgreementUserType agreementUserType = CacheService.Get<AgreementUserType>("SelectByAgreementUserTypeId" + AgreementUserTypeId);
                if (agreementUserType == null)
                {
                    agreementUserType = AgreementUserTypeRepository.SelectById(AgreementUserTypeId);
                    CacheService.Add<AgreementUserType>("SelectByAgreementUserTypeId" + AgreementUserTypeId, agreementUserType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByAgreementUserTypeId" + AgreementUserTypeId);
                }
                return agreementUserType;
            }
        }

        public IEnumerable<AgreementUserType> SelectMany_AgreementUserType(Expression<Func<AgreementUserType, bool>> where)
        {
            return AgreementUserTypeRepository.SelectMany(where);
        }

        public AgreementUserType SelectSingle_AgreementUserType(Expression<Func<AgreementUserType, bool>> where)
        {
            return AgreementUserTypeRepository.Select(where);
        }

        public IEnumerable<AgreementUserType> SelectAllAgreementUserTypes()
        {
            return AgreementUserTypeRepository.SelectAll();
        }

        public bool SaveAgreementUserTypeForm(AgreementUserType agreementUserType)
        {
            try
            {
                if (agreementUserType.FormMode == FormMode.Create)
                {
                    AgreementUserTypeRepository.Add(agreementUserType);
                }
                else
                {
                    AgreementUserTypeRepository.Update(agreementUserType);
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

        public bool DeleteAgreementUserTypeForm(int AgreementUserTypeId)
        {
            try
            {
                AgreementUserType agreementUserType = AgreementUserTypeRepository.SelectById(AgreementUserTypeId);
                AgreementUserTypeRepository.Delete(agreementUserType);

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

        public Task<AgreementUserType> SelectByAgreementUserTypeIdAsync(int AgreementUserTypeId)
        {
            return AgreementUserTypeRepository.SelectByIdAsync(AgreementUserTypeId);
        }

        public Task<IEnumerable<AgreementUserType>> SelectMany_AgreementUserTypeAsync(Expression<Func<AgreementUserType, bool>> where)
        {
            return AgreementUserTypeRepository.SelectManyAsync(where);
        }

        public Task<AgreementUserType> SelectSingle_AgreementUserTypeAsync(Expression<Func<AgreementUserType, bool>> where)
        {
            return AgreementUserTypeRepository.SelectAsync(where);
        }

        public Task<IEnumerable<AgreementUserType>> SelectAllAgreementUserTypesAsync()
        {
            return AgreementUserTypeRepository.SelectAllAsync();
        }

        public Task<int> SaveAgreementUserTypeFormAsync(AgreementUserType agreementUserType)
        {
            try
            {
                if (agreementUserType.AgreementUserTypeId == 0)
                {
                    AgreementUserTypeRepository.Add(agreementUserType);
                }
                else
                {
                    AgreementUserTypeRepository.Update(agreementUserType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteAgreementUserTypeFormAsync(int AgreementUserTypeId)
        {
            try
            {
                AgreementUserType agreementUserType = AgreementUserTypeRepository.SelectById(AgreementUserTypeId);
                AgreementUserTypeRepository.Delete(agreementUserType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }
        public bool SaveMultiAgreementUserType(Agreement agreement, IEnumerable<int> agreementUserTypeIds)
        {
            try
            {
                if (agreement.AgreementId > 0)
                {
                    var agreementUserTypes = AgreementUserTypeRepository.SelectMany(x => x.AgreementId == agreement.AgreementId);
                    if (agreementUserTypes != null)
                    {
                        foreach (var userMenuOption in agreementUserTypes)
                        {
                            AgreementUserTypeRepository.Delete(userMenuOption);
                        }

                        Save();
                    }
                }
                if (agreementUserTypeIds != null)
                {
                    foreach (var agreementUserTypeId in agreementUserTypeIds)
                    {
                        AgreementUserType agreementUserTypeAdd = new AgreementUserType();
                        agreementUserTypeAdd.UserTypeId = agreementUserTypeId;
                        agreementUserTypeAdd.AgreementId = agreement.AgreementId;
                        agreementUserTypeAdd.IsArchived = false;
                        AgreementUserTypeRepository.Add(agreementUserTypeAdd);
                    }
                    Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                base.LastErrorMessage = ex.Message;
            }

            return false;
        }

        #endregion Async Methods

        #endregion AgreementUserType

        #region userAgreement

        #region Sync Methods

        public UserAgreement SelectByUserAgreementId(int userAgreementId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userAgreementRepository.SelectById(userAgreementId);
            }
            else
            {
                UserAgreement userAgreement = CacheService.Get<UserAgreement>("SelectByUserAgreementId" + userAgreementId);
                if (userAgreement == null)
                {
                    userAgreement = userAgreementRepository.SelectById(userAgreementId);
                    CacheService.Add<UserAgreement>("SelectByUserAgreementId" + userAgreementId, userAgreement);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserAgreementId" + userAgreementId);
                }
                return userAgreement;
            }
        }

        public IEnumerable<UserAgreement> SelectMany_UserAgreement(Expression<Func<UserAgreement, bool>> where)
        {
            return userAgreementRepository.SelectMany(where);
        }

        public UserAgreement SelectSingle_UserAgreement(Expression<Func<UserAgreement, bool>> where)
        {
            return userAgreementRepository.Select(where);
        }

        public IEnumerable<UserAgreement> SelectAllUserAgreements()
        {
            return userAgreementRepository.SelectAll();
        }

        public bool SaveUserAgreementForm(UserAgreement userAgreement)
        {
            try
            {
                if (userAgreement.FormMode == FormMode.Create)
                {
                    userAgreementRepository.Add(userAgreement);
                }
                else
                {
                    userAgreementRepository.Update(userAgreement);
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

        public bool DeleteUserAgreementForm(int userAgreementId)
        {
            try
            {
                UserAgreement userAgreement = userAgreementRepository.SelectById(userAgreementId);
                userAgreementRepository.Delete(userAgreement);

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

        public Task<UserAgreement> SelectByUserAgreementIdAsync(int userAgreementId)
        {
            return userAgreementRepository.SelectByIdAsync(userAgreementId);
        }

        public Task<IEnumerable<UserAgreement>> SelectMany_UserAgreementAsync(Expression<Func<UserAgreement, bool>> where)
        {
            return userAgreementRepository.SelectManyAsync(where);
        }

        public Task<UserAgreement> SelectSingle_UserAgreementAsync(Expression<Func<UserAgreement, bool>> where)
        {
            return userAgreementRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserAgreement>> SelectAllUserAgreementsAsync()
        {
            return userAgreementRepository.SelectAllAsync();
        }

        public Task<int> SaveUserAgreementFormAsync(UserAgreement userAgreement)
        {
            try
            {
                if (userAgreement.UserAgreementId == 0)
                {
                    userAgreementRepository.Add(userAgreement);
                }
                else
                {
                    userAgreementRepository.Update(userAgreement);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserAgreementFormAsync(int userAgreementId)
        {
            try
            {
                UserAgreement userAgreement = userAgreementRepository.SelectById(userAgreementId);
                userAgreementRepository.Delete(userAgreement);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion userAgreement 

        #region vw_AgreementGrid

        public List<vw_AgreementGrid> Selectvw_AgreementGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_AgreementGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_AgreementGrid>> Selectvw_AgreementGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_AgreementGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_AgreementGrid> SelectAllvw_AgreementGrids()
        {
            return vw_AgreementGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_AgreementGrid>> SelectAllvw_AgreementGridsAsync()
        {
            return vw_AgreementGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_AgreementGrid> SelectMany_vw_AgreementGrid(Expression<Func<vw_AgreementGrid, bool>> where)
        {
            return vw_AgreementGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_AgreementGrid>> SelectMany_vw_AgreementGridAsync(Expression<Func<vw_AgreementGrid, bool>> where)
        {
            return vw_AgreementGridRepository.SelectManyAsync(where);
        }
        public vw_AgreementGrid SelectSingle_vw_AgreementGrid(Expression<Func<vw_AgreementGrid, bool>> where)
        {
            return vw_AgreementGridRepository.Select(where);
        }
        public Task<vw_AgreementGrid> SelectSingle_vw_AgreementGridAsync(Expression<Func<vw_AgreementGrid, bool>> where)
        {
            return vw_AgreementGridRepository.SelectAsync(where);
        }

        #endregion vw_AgreementGrid

        #region vw_AgreementUserSubGrid

        public List<vw_AgreementUserSubGrid> Selectvw_AgreementUserSubGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_AgreementUserSubGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_AgreementUserSubGrid>> Selectvw_AgreementUserSubGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_AgreementUserSubGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_AgreementUserSubGrid> SelectAllvw_AgreementUserSubGrids()
        {
            return vw_AgreementUserSubGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_AgreementUserSubGrid>> SelectAllvw_AgreementUserSubGridsAsync()
        {
            return vw_AgreementUserSubGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_AgreementUserSubGrid> SelectMany_vw_AgreementUserSubGrid(Expression<Func<vw_AgreementUserSubGrid, bool>> where)
        {
            return vw_AgreementUserSubGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_AgreementUserSubGrid>> SelectMany_vw_AgreementUserSubGridAsync(Expression<Func<vw_AgreementUserSubGrid, bool>> where)
        {
            return vw_AgreementUserSubGridRepository.SelectManyAsync(where);
        }
        public vw_AgreementUserSubGrid SelectSingle_vw_AgreementUserSubGrid(Expression<Func<vw_AgreementUserSubGrid, bool>> where)
        {
            return vw_AgreementUserSubGridRepository.Select(where);
        }
        public Task<vw_AgreementUserSubGrid> SelectSingle_vw_AgreementUserSubGridAsync(Expression<Func<vw_AgreementUserSubGrid, bool>> where)
        {
            return vw_AgreementUserSubGridRepository.SelectAsync(where);
        }

      #endregion vw_AgreementUserSubGrid

        #region vw_AgreementVersionSubGrid

      public List<vw_AgreementVersionSubGrid> Selectvw_AgreementVersionSubGridsByGridSetting(GridSetting gridSetting)
      {
         return vw_AgreementVersionSubGridRepository.SelectByGridSetting(gridSetting);
      }
      public Task<List<vw_AgreementVersionSubGrid>> Selectvw_AgreementVersionSubGridsByGridSettingAsync(GridSetting gridSetting)
      {
         return vw_AgreementVersionSubGridRepository.SelectByGridSettingAsync(gridSetting);
      }
      public IEnumerable<vw_AgreementVersionSubGrid> SelectAllvw_AgreementVersionSubGrids()
      {
         return vw_AgreementVersionSubGridRepository.SelectAll();
      }
      public Task<IEnumerable<vw_AgreementVersionSubGrid>> SelectAllvw_AgreementVersionSubGridsAsync()
      {
         return vw_AgreementVersionSubGridRepository.SelectAllAsync();
      }
      public IEnumerable<vw_AgreementVersionSubGrid> SelectMany_vw_AgreementVersionSubGrid(Expression<Func<vw_AgreementVersionSubGrid, bool>> where)
      {
         return vw_AgreementVersionSubGridRepository.SelectMany(where);
      }
      public Task<IEnumerable<vw_AgreementVersionSubGrid>> SelectMany_vw_AgreementVersionSubGridAsync(Expression<Func<vw_AgreementVersionSubGrid, bool>> where)
      {
         return vw_AgreementVersionSubGridRepository.SelectManyAsync(where);
      }
      public vw_AgreementVersionSubGrid SelectSingle_vw_AgreementVersionSubGrid(Expression<Func<vw_AgreementVersionSubGrid, bool>> where)
      {
         return vw_AgreementVersionSubGridRepository.Select(where);
      }
      public Task<vw_AgreementVersionSubGrid> SelectSingle_vw_AgreementVersionSubGridAsync(Expression<Func<vw_AgreementVersionSubGrid, bool>> where)
      {
         return vw_AgreementVersionSubGridRepository.SelectAsync(where);
      }

        #endregion vw_AgreementVersionSubGrid

        #region vw_AgreementPreviousVersionNumber

        public List<vw_AgreementPreviousVersionNumber> Selectvw_AgreementPreviousVersionNumbersByGridSetting(GridSetting gridSetting)
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_AgreementPreviousVersionNumber>> Selectvw_AgreementPreviousVersionNumbersByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_AgreementPreviousVersionNumber> SelectAllvw_AgreementPreviousVersionNumbers()
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectAll();
        }
        public Task<IEnumerable<vw_AgreementPreviousVersionNumber>> SelectAllvw_AgreementPreviousVersionNumbersAsync()
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectAllAsync();
        }
        public IEnumerable<vw_AgreementPreviousVersionNumber> SelectMany_vw_AgreementPreviousVersionNumber(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where)
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_AgreementPreviousVersionNumber>> SelectMany_vw_AgreementPreviousVersionNumberAsync(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where)
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectManyAsync(where);
        }
        public vw_AgreementPreviousVersionNumber SelectSingle_vw_AgreementPreviousVersionNumber(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where)
        {
            return vw_AgreementPreviousVersionNumberRepository.Select(where);
        }
        public Task<vw_AgreementPreviousVersionNumber> SelectSingle_vw_AgreementPreviousVersionNumberAsync(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where)
        {
            return vw_AgreementPreviousVersionNumberRepository.SelectAsync(where);
        }

        #endregion vw_AgreementPreviousVersionNumber

        #region vw_UserAgreementSubGrid

        public List<vw_UserAgreementSubGrid> Selectvw_UserAgreementSubGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserAgreementSubGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserAgreementSubGrid>> Selectvw_UserAgreementSubGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserAgreementSubGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserAgreementSubGrid> SelectAllvw_UserAgreementSubGrids()
        {
            return vw_UserAgreementSubGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserAgreementSubGrid>> SelectAllvw_UserAgreementSubGridsAsync()
        {
            return vw_UserAgreementSubGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserAgreementSubGrid> SelectMany_vw_UserAgreementSubGrid(Expression<Func<vw_UserAgreementSubGrid, bool>> where)
        {
            return vw_UserAgreementSubGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserAgreementSubGrid>> SelectMany_vw_UserAgreementSubGridAsync(Expression<Func<vw_UserAgreementSubGrid, bool>> where)
        {
            return vw_UserAgreementSubGridRepository.SelectManyAsync(where);
        }
        public vw_UserAgreementSubGrid SelectSingle_vw_UserAgreementSubGrid(Expression<Func<vw_UserAgreementSubGrid, bool>> where)
        {
            return vw_UserAgreementSubGridRepository.Select(where);
        }
        public Task<vw_UserAgreementSubGrid> SelectSingle_vw_UserAgreementSubGridAsync(Expression<Func<vw_UserAgreementSubGrid, bool>> where)
        {
            return vw_UserAgreementSubGridRepository.SelectAsync(where);
        }

        #endregion vw_UserAgreementSubGrid

        #region vw_UserAgreementForm

        public List<vw_UserAgreementForm> Selectvw_UserAgreementFormsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserAgreementFormRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserAgreementForm>> Selectvw_UserAgreementFormsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserAgreementFormRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserAgreementForm> SelectAllvw_UserAgreementForms()
        {
            return vw_UserAgreementFormRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserAgreementForm>> SelectAllvw_UserAgreementFormsAsync()
        {
            return vw_UserAgreementFormRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserAgreementForm> SelectMany_vw_UserAgreementForm(Expression<Func<vw_UserAgreementForm, bool>> where)
        {
            return vw_UserAgreementFormRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserAgreementForm>> SelectMany_vw_UserAgreementFormAsync(Expression<Func<vw_UserAgreementForm, bool>> where)
        {
            return vw_UserAgreementFormRepository.SelectManyAsync(where);
        }
        public vw_UserAgreementForm SelectSingle_vw_UserAgreementForm(Expression<Func<vw_UserAgreementForm, bool>> where)
        {
            return vw_UserAgreementFormRepository.Select(where);
        }
        public Task<vw_UserAgreementForm> SelectSingle_vw_UserAgreementFormAsync(Expression<Func<vw_UserAgreementForm, bool>> where)
        {
            return vw_UserAgreementFormRepository.SelectAsync(where);
        }

        #endregion vw_UserAgreementForm                    
        public int? sp_AgreementVersionNew(int userId,int agreemtntId, ref int? outputMessage)
        {
            return AgreementRepository.sp_AgreementVersionNew(userId, agreemtntId, ref outputMessage);
        }
    }

    public partial interface IAgreementService : IBaseService
    {
        // Interface Methods

        //#region Email

        //Email SelectByEmailId(int EmailId, bool cacheRecord = false);

        //IEnumerable<Email> SelectMany_Email(Expression<Func<Email, bool>> where);

        //Email SelectSingle_Email(Expression<Func<Email, bool>> where);

        //IEnumerable<Email> SelectAllEmails();

        //bool SaveEmailForm(Email EmailRepository);

        //bool DeleteEmailForm(int EmailId);

        //Task<Email> SelectByEmailIdAsync(int EmailId);

        //Task<IEnumerable<Email>> SelectMany_EmailAsync(Expression<Func<Email, bool>> where);

        //Task<Email> SelectSingle_EmailAsync(Expression<Func<Email, bool>> where);

        //Task<IEnumerable<Email>> SelectAllEmailsAsync();

        //Task<int> SaveEmailFormAsync(Email EmailRepository);

        //bool SaveEmailList(List<Email> Emails, List<Email> EmailList);


        //Task<int> DeleteEmailFormAsync(int EmailId);

        //#endregion

        //#region ApplicationInformation

        //ApplicationInformation SelectByApplicationInformationId(int ApplicationInformationId, bool cacheRecord = false);

        //IEnumerable<ApplicationInformation> SelectMany_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where);

        //ApplicationInformation SelectSingle_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where);

        //IEnumerable<ApplicationInformation> SelectAllApplicationInformations();

        //bool SaveApplicationInformationForm(ApplicationInformation ApplicationInformationRepository);

        //bool DeleteApplicationInformationForm(int ApplicationInformationId);

        //Task<ApplicationInformation> SelectByApplicationInformationIdAsync(int ApplicationInformationId);

        //Task<IEnumerable<ApplicationInformation>> SelectMany_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where);

        //Task<ApplicationInformation> SelectSingle_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where);

        //Task<IEnumerable<ApplicationInformation>> SelectAllApplicationInformationsAsync();

        //Task<int> SaveApplicationInformationFormAsync(ApplicationInformation ApplicationInformationRepository);

        //Task<int> DeleteApplicationInformationFormAsync(int ApplicationInformationId);

        //#endregion

        //#region EmailAttachment

        //EmailAttachment SelectByEmailAttachmentId(int EmailAttachmentId, bool cacheRecord = false);

        //IEnumerable<EmailAttachment> SelectMany_EmailAttachment(Expression<Func<EmailAttachment, bool>> where);

        //EmailAttachment SelectSingle_EmailAttachment(Expression<Func<EmailAttachment, bool>> where);

        //IEnumerable<EmailAttachment> SelectAllEmailAttachments();

        //bool SaveEmailAttachmentForm(EmailAttachment emailAttachment);

        //bool DeleteEmailAttachmentForm(int EmailAttachmentId);

        //Task<EmailAttachment> SelectByEmailAttachmentIdAsync(int EmailAttachmentId);

        //Task<IEnumerable<EmailAttachment>> SelectMany_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where);

        //Task<EmailAttachment> SelectSingle_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where);

        //Task<IEnumerable<EmailAttachment>> SelectAllEmailAttachmentsAsync();

        //Task<int> SaveEmailAttachmentFormAsync(EmailAttachment emailAttachment);

        //Task<int> DeleteEmailAttachmentFormAsync(int EmailAttachmentId);

        //#endregion

        #region Agreement

        Agreement SelectByAgreementId(int agreementId, bool cacheRecord = false);

        IEnumerable<Agreement> SelectMany_Agreement(Expression<Func<Agreement, bool>> where);

        Agreement SelectSingle_Agreement(Expression<Func<Agreement, bool>> where);

        IEnumerable<Agreement> SelectAllAgreements();

        bool SaveAgreementForm(Agreement AgreementRepository);

        bool DeleteAgreementForm(int agreementId);

        Task<Agreement> SelectByAgreementIdAsync(int agreementId);

        Task<IEnumerable<Agreement>> SelectMany_AgreementAsync(Expression<Func<Agreement, bool>> where);

        Task<Agreement> SelectSingle_AgreementAsync(Expression<Func<Agreement, bool>> where);

        Task<IEnumerable<Agreement>> SelectAllAgreementsAsync();

        Task<int> SaveAgreementFormAsync(Agreement AgreementRepository);

        Task<int> DeleteAgreementFormAsync(int agreementId);
        #endregion

        #region AgreementUserType

        AgreementUserType SelectByAgreementUserTypeId(int agreementId, bool cacheRecord = false);

        IEnumerable<AgreementUserType> SelectMany_AgreementUserType(Expression<Func<AgreementUserType, bool>> where);

        AgreementUserType SelectSingle_AgreementUserType(Expression<Func<AgreementUserType, bool>> where);

        IEnumerable<AgreementUserType> SelectAllAgreementUserTypes();

        bool SaveAgreementUserTypeForm(AgreementUserType AgreementUserTypeRepository);

        bool DeleteAgreementUserTypeForm(int agreementId);

        Task<AgreementUserType> SelectByAgreementUserTypeIdAsync(int agreementId);

        Task<IEnumerable<AgreementUserType>> SelectMany_AgreementUserTypeAsync(Expression<Func<AgreementUserType, bool>> where);

        Task<AgreementUserType> SelectSingle_AgreementUserTypeAsync(Expression<Func<AgreementUserType, bool>> where);

        Task<IEnumerable<AgreementUserType>> SelectAllAgreementUserTypesAsync();

        Task<int> SaveAgreementUserTypeFormAsync(AgreementUserType AgreementUserTypeRepository);

        Task<int> DeleteAgreementUserTypeFormAsync(int agreementUserTypeId);
        bool SaveMultiAgreementUserType(Agreement agreement, IEnumerable<int> AgreementUserTypeIds);
        #endregion

        #region vw_AgreementGrid
        List<vw_AgreementGrid> Selectvw_AgreementGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_AgreementGrid> SelectAllvw_AgreementGrids();
        Task<IEnumerable<vw_AgreementGrid>> SelectAllvw_AgreementGridsAsync();
        Task<List<vw_AgreementGrid>> Selectvw_AgreementGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_AgreementGrid> SelectMany_vw_AgreementGrid(Expression<Func<vw_AgreementGrid, bool>> where);
        Task<IEnumerable<vw_AgreementGrid>> SelectMany_vw_AgreementGridAsync(Expression<Func<vw_AgreementGrid, bool>> where);
        vw_AgreementGrid SelectSingle_vw_AgreementGrid(Expression<Func<vw_AgreementGrid, bool>> where);
        Task<vw_AgreementGrid> SelectSingle_vw_AgreementGridAsync(Expression<Func<vw_AgreementGrid, bool>> where);

        #endregion

        #region vw_AgreementUserSubGrid
        List<vw_AgreementUserSubGrid> Selectvw_AgreementUserSubGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_AgreementUserSubGrid> SelectAllvw_AgreementUserSubGrids();
        Task<IEnumerable<vw_AgreementUserSubGrid>> SelectAllvw_AgreementUserSubGridsAsync();
        Task<List<vw_AgreementUserSubGrid>> Selectvw_AgreementUserSubGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_AgreementUserSubGrid> SelectMany_vw_AgreementUserSubGrid(Expression<Func<vw_AgreementUserSubGrid, bool>> where);
        Task<IEnumerable<vw_AgreementUserSubGrid>> SelectMany_vw_AgreementUserSubGridAsync(Expression<Func<vw_AgreementUserSubGrid, bool>> where);
        vw_AgreementUserSubGrid SelectSingle_vw_AgreementUserSubGrid(Expression<Func<vw_AgreementUserSubGrid, bool>> where);
        Task<vw_AgreementUserSubGrid> SelectSingle_vw_AgreementUserSubGridAsync(Expression<Func<vw_AgreementUserSubGrid, bool>> where);

      #endregion

        #region vw_AgreementVersionSubGrid
      List<vw_AgreementVersionSubGrid> Selectvw_AgreementVersionSubGridsByGridSetting(GridSetting gridSetting);
      IEnumerable<vw_AgreementVersionSubGrid> SelectAllvw_AgreementVersionSubGrids();
      Task<IEnumerable<vw_AgreementVersionSubGrid>> SelectAllvw_AgreementVersionSubGridsAsync();
      Task<List<vw_AgreementVersionSubGrid>> Selectvw_AgreementVersionSubGridsByGridSettingAsync(GridSetting gridSetting);
      IEnumerable<vw_AgreementVersionSubGrid> SelectMany_vw_AgreementVersionSubGrid(Expression<Func<vw_AgreementVersionSubGrid, bool>> where);
      Task<IEnumerable<vw_AgreementVersionSubGrid>> SelectMany_vw_AgreementVersionSubGridAsync(Expression<Func<vw_AgreementVersionSubGrid, bool>> where);
      vw_AgreementVersionSubGrid SelectSingle_vw_AgreementVersionSubGrid(Expression<Func<vw_AgreementVersionSubGrid, bool>> where);
      Task<vw_AgreementVersionSubGrid> SelectSingle_vw_AgreementVersionSubGridAsync(Expression<Func<vw_AgreementVersionSubGrid, bool>> where);

        #endregion

        #region vw_AgreementPreviousVersionNumber
        List<vw_AgreementPreviousVersionNumber> Selectvw_AgreementPreviousVersionNumbersByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_AgreementPreviousVersionNumber> SelectAllvw_AgreementPreviousVersionNumbers();
        Task<IEnumerable<vw_AgreementPreviousVersionNumber>> SelectAllvw_AgreementPreviousVersionNumbersAsync();
        Task<List<vw_AgreementPreviousVersionNumber>> Selectvw_AgreementPreviousVersionNumbersByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_AgreementPreviousVersionNumber> SelectMany_vw_AgreementPreviousVersionNumber(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where);
        Task<IEnumerable<vw_AgreementPreviousVersionNumber>> SelectMany_vw_AgreementPreviousVersionNumberAsync(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where);
        vw_AgreementPreviousVersionNumber SelectSingle_vw_AgreementPreviousVersionNumber(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where);
        Task<vw_AgreementPreviousVersionNumber> SelectSingle_vw_AgreementPreviousVersionNumberAsync(Expression<Func<vw_AgreementPreviousVersionNumber, bool>> where);

        #endregion

        #region vw_UserAgreementSubGrid
        List<vw_UserAgreementSubGrid> Selectvw_UserAgreementSubGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserAgreementSubGrid> SelectAllvw_UserAgreementSubGrids();
        Task<IEnumerable<vw_UserAgreementSubGrid>> SelectAllvw_UserAgreementSubGridsAsync();
        Task<List<vw_UserAgreementSubGrid>> Selectvw_UserAgreementSubGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserAgreementSubGrid> SelectMany_vw_UserAgreementSubGrid(Expression<Func<vw_UserAgreementSubGrid, bool>> where);
        Task<IEnumerable<vw_UserAgreementSubGrid>> SelectMany_vw_UserAgreementSubGridAsync(Expression<Func<vw_UserAgreementSubGrid, bool>> where);
        vw_UserAgreementSubGrid SelectSingle_vw_UserAgreementSubGrid(Expression<Func<vw_UserAgreementSubGrid, bool>> where);
        Task<vw_UserAgreementSubGrid> SelectSingle_vw_UserAgreementSubGridAsync(Expression<Func<vw_UserAgreementSubGrid, bool>> where);

        #endregion

        #region vw_UserAgreementForm
        List<vw_UserAgreementForm> Selectvw_UserAgreementFormsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserAgreementForm> SelectAllvw_UserAgreementForms();
        Task<IEnumerable<vw_UserAgreementForm>> SelectAllvw_UserAgreementFormsAsync();
        Task<List<vw_UserAgreementForm>> Selectvw_UserAgreementFormsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserAgreementForm> SelectMany_vw_UserAgreementForm(Expression<Func<vw_UserAgreementForm, bool>> where);
        Task<IEnumerable<vw_UserAgreementForm>> SelectMany_vw_UserAgreementFormAsync(Expression<Func<vw_UserAgreementForm, bool>> where);
        vw_UserAgreementForm SelectSingle_vw_UserAgreementForm(Expression<Func<vw_UserAgreementForm, bool>> where);
        Task<vw_UserAgreementForm> SelectSingle_vw_UserAgreementFormAsync(Expression<Func<vw_UserAgreementForm, bool>> where);

        #endregion                

        #region UserAgreement

        UserAgreement SelectByUserAgreementId(int agreementId, bool cacheRecord = false);

        IEnumerable<UserAgreement> SelectMany_UserAgreement(Expression<Func<UserAgreement, bool>> where);

        UserAgreement SelectSingle_UserAgreement(Expression<Func<UserAgreement, bool>> where);

        IEnumerable<UserAgreement> SelectAllUserAgreements();

        bool SaveUserAgreementForm(UserAgreement UserAgreementRepository);

        bool DeleteUserAgreementForm(int agreementId);

        Task<UserAgreement> SelectByUserAgreementIdAsync(int agreementId);

        Task<IEnumerable<UserAgreement>> SelectMany_UserAgreementAsync(Expression<Func<UserAgreement, bool>> where);

        Task<UserAgreement> SelectSingle_UserAgreementAsync(Expression<Func<UserAgreement, bool>> where);

        Task<IEnumerable<UserAgreement>> SelectAllUserAgreementsAsync();

        Task<int> SaveUserAgreementFormAsync(UserAgreement UserAgreementRepository);

        Task<int> DeleteUserAgreementFormAsync(int UserAgreementId);

        #endregion
        int? sp_AgreementVersionNew(int userId, int agreementId, ref int? newAgreementId);
    }
}

