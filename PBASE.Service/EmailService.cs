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
    public partial class EmailService : BaseService, IEmailService
    {
        #region Initialization
        private readonly IEmailRepository emailRepository;
        private readonly IApplicationInformationRepository ApplicationInformationRepository;
        private readonly IEmailAttachmentRepository emailAttachmentRepository;
        private readonly Ivw_EmailGridRepository vw_EmailGridRepository;
        private readonly IUnitOfWork unitOfWork;


        public EmailService(
            IEmailRepository emailRepository,
            IApplicationInformationRepository ApplicationInformationRepository,
            IEmailAttachmentRepository emailAttachmentRepository,
            Ivw_EmailGridRepository vw_EmailGridRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
            this.emailRepository = emailRepository;
            this.ApplicationInformationRepository = ApplicationInformationRepository;
            this.emailAttachmentRepository = emailAttachmentRepository;
            this.vw_EmailGridRepository = vw_EmailGridRepository;

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

        #region Email

        #region Sync Methods

        public Email SelectByEmailId(int EmailId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return emailRepository.SelectById(EmailId);
            }
            else
            {
                Email Email = CacheService.Get<Email>("SelectByEmailId" + EmailId);
                if (Email == null)
                {
                    Email = emailRepository.SelectById(EmailId);
                    CacheService.Add<Email>("SelectByEmailId" + EmailId, Email);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByEmailId" + EmailId);
                }
                return Email;
            }
        }

        public IEnumerable<Email> SelectMany_Email(Expression<Func<Email, bool>> where)
        {
            return emailRepository.SelectMany(where);
        }

        public Email SelectSingle_Email(Expression<Func<Email, bool>> where)
        {
            return emailRepository.Select(where);
        }

        public IEnumerable<Email> SelectAllEmails()
        {
            return emailRepository.SelectAll();
        }

        public bool SaveEmailForm(Email Email)
        {
            try
            {
                if (Email.FormMode == FormMode.Create)
                {
                    emailRepository.Add(Email);
                }
                else
                {
                    emailRepository.Update(Email);
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

        public bool SaveEmailList(List<Email> Emails, List<Email> EmailList)
        {
            try
            {
                foreach (var item in Emails)
                {
                    if (EmailList.Find(x => x.EmailId == item.EmailId) != null && EmailList.Find(x => x.EmailId == item.EmailId).MessageId != null)
                        item.MessageId = EmailList.Find(x => x.EmailId == item.EmailId).MessageId;
                    item.SentDate = DateTime.Now;
                    item.Status = EmailStatus.SentSuccessfully;
                    emailRepository.Update(item);
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


        public bool DeleteEmailForm(int EmailId)
        {
            try
            {
                Email Email = emailRepository.SelectById(EmailId);
                emailRepository.Delete(Email);

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

        public Task<Email> SelectByEmailIdAsync(int EmailId)
        {
            return emailRepository.SelectByIdAsync(EmailId);
        }

        public Task<IEnumerable<Email>> SelectMany_EmailAsync(Expression<Func<Email, bool>> where)
        {
            return emailRepository.SelectManyAsync(where);
        }

        public Task<Email> SelectSingle_EmailAsync(Expression<Func<Email, bool>> where)
        {
            return emailRepository.SelectAsync(where);
        }

        public Task<IEnumerable<Email>> SelectAllEmailsAsync()
        {
            return emailRepository.SelectAllAsync();
        }

        public Task<int> SaveEmailFormAsync(Email Email)
        {
            try
            {
                if (Email.EmailId == 0)
                {
                    emailRepository.Add(Email);
                }
                else
                {
                    emailRepository.Update(Email);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteEmailFormAsync(int EmailId)
        {
            try
            {
                Email Email = emailRepository.SelectById(EmailId);
                emailRepository.Delete(Email);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion Email
        
        #region ApplicationInformation

        #region Sync Methods

        public ApplicationInformation SelectByApplicationInformationId(int ApplicationInformationId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return ApplicationInformationRepository.SelectById(ApplicationInformationId);
            }
            else
            {
                ApplicationInformation ApplicationInformation = CacheService.Get<ApplicationInformation>("SelectByApplicationInformationId" + ApplicationInformationId);
                if (ApplicationInformation == null)
                {
                    ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
                    CacheService.Add<ApplicationInformation>("SelectByApplicationInformationId" + ApplicationInformationId, ApplicationInformation);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByApplicationInformationId" + ApplicationInformationId);
                }
                return ApplicationInformation;
            }
        }

        public IEnumerable<ApplicationInformation> SelectMany_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where)
        {
            return ApplicationInformationRepository.SelectMany(where);
        }

        public ApplicationInformation SelectSingle_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where)
        {
            return ApplicationInformationRepository.Select(where);
        }

        public IEnumerable<ApplicationInformation> SelectAllApplicationInformations()
        {
            return ApplicationInformationRepository.SelectAll();
        }

        public bool SaveApplicationInformationForm(ApplicationInformation ApplicationInformation)
        {
            try
            {
                if (ApplicationInformation.FormMode == FormMode.Create)
                {
                    ApplicationInformationRepository.Add(ApplicationInformation);
                }
                else
                {
                    ApplicationInformationRepository.Update(ApplicationInformation);
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

        public bool DeleteApplicationInformationForm(int ApplicationInformationId)
        {
            try
            {
                ApplicationInformation ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
                ApplicationInformationRepository.Delete(ApplicationInformation);

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

        public Task<ApplicationInformation> SelectByApplicationInformationIdAsync(int ApplicationInformationId)
        {
            return ApplicationInformationRepository.SelectByIdAsync(ApplicationInformationId);
        }

        public Task<IEnumerable<ApplicationInformation>> SelectMany_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where)
        {
            return ApplicationInformationRepository.SelectManyAsync(where);
        }

        public Task<ApplicationInformation> SelectSingle_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where)
        {
            return ApplicationInformationRepository.SelectAsync(where);
        }

        public Task<IEnumerable<ApplicationInformation>> SelectAllApplicationInformationsAsync()
        {
            return ApplicationInformationRepository.SelectAllAsync();
        }

        public Task<int> SaveApplicationInformationFormAsync(ApplicationInformation ApplicationInformation)
        {
            try
            {
                if (ApplicationInformation.ApplicationInformationId == 0)
                {
                    ApplicationInformationRepository.Add(ApplicationInformation);
                }
                else
                {
                    ApplicationInformationRepository.Update(ApplicationInformation);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteApplicationInformationFormAsync(int ApplicationInformationId)
        {
            try
            {
                ApplicationInformation ApplicationInformation = ApplicationInformationRepository.SelectById(ApplicationInformationId);
                ApplicationInformationRepository.Delete(ApplicationInformation);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion ApplicationInformation

        #region EmailAttachment

        #region Sync Methods

        public EmailAttachment SelectByEmailAttachmentId(int EmailAttachmentId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return emailAttachmentRepository.SelectById(EmailAttachmentId);
            }
            else
            {
                EmailAttachment EmailAttachment = CacheService.Get<EmailAttachment>("SelectByEmailAttachmentId" + EmailAttachmentId);
                if (EmailAttachment == null)
                {
                    EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
                    CacheService.Add<EmailAttachment>("SelectByEmailAttachmentId" + EmailAttachmentId, EmailAttachment);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByEmailAttachmentId" + EmailAttachmentId);
                }
                return EmailAttachment;
            }
        }

        public IEnumerable<EmailAttachment> SelectMany_EmailAttachment(Expression<Func<EmailAttachment, bool>> where)
        {
            return emailAttachmentRepository.SelectMany(where);
        }

        public EmailAttachment SelectSingle_EmailAttachment(Expression<Func<EmailAttachment, bool>> where)
        {
            return emailAttachmentRepository.Select(where);
        }

        public IEnumerable<EmailAttachment> SelectAllEmailAttachments()
        {
            return emailAttachmentRepository.SelectAll();
        }

        public bool SaveEmailAttachmentForm(EmailAttachment EmailAttachment)
        {
            try
            {
                if (EmailAttachment.FormMode == FormMode.Create)
                {
                    emailAttachmentRepository.Add(EmailAttachment);
                }
                else
                {
                    emailAttachmentRepository.Update(EmailAttachment);
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

        public bool DeleteEmailAttachmentForm(int EmailAttachmentId)
        {
            try
            {
                EmailAttachment EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
                emailAttachmentRepository.Delete(EmailAttachment);

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

        public Task<EmailAttachment> SelectByEmailAttachmentIdAsync(int EmailAttachmentId)
        {
            return emailAttachmentRepository.SelectByIdAsync(EmailAttachmentId);
        }

        public Task<IEnumerable<EmailAttachment>> SelectMany_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where)
        {
            return emailAttachmentRepository.SelectManyAsync(where);
        }

        public Task<EmailAttachment> SelectSingle_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where)
        {
            return emailAttachmentRepository.SelectAsync(where);
        }

        public Task<IEnumerable<EmailAttachment>> SelectAllEmailAttachmentsAsync()
        {
            return emailAttachmentRepository.SelectAllAsync();
        }

        public Task<int> SaveEmailAttachmentFormAsync(EmailAttachment EmailAttachment)
        {
            try
            {
                if (EmailAttachment.EmailAttachmentId == 0)
                {
                    emailAttachmentRepository.Add(EmailAttachment);
                }
                else
                {
                    emailAttachmentRepository.Update(EmailAttachment);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteEmailAttachmentFormAsync(int EmailAttachmentId)
        {
            try
            {
                EmailAttachment EmailAttachment = emailAttachmentRepository.SelectById(EmailAttachmentId);
                emailAttachmentRepository.Delete(EmailAttachment);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion EmailAttachment

        #region vw_EmailGrid

        public List<vw_EmailGrid> Selectvw_EmailGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_EmailGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_EmailGrid>> Selectvw_EmailGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_EmailGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_EmailGrid> SelectAllvw_EmailGrids()
        {
            return vw_EmailGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_EmailGrid>> SelectAllvw_EmailGridsAsync()
        {
            return vw_EmailGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_EmailGrid> SelectMany_vw_EmailGrid(Expression<Func<vw_EmailGrid, bool>> where)
        {
            return vw_EmailGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_EmailGrid>> SelectMany_vw_EmailGridAsync(Expression<Func<vw_EmailGrid, bool>> where)
        {
            return vw_EmailGridRepository.SelectManyAsync(where);
        }
        public vw_EmailGrid SelectSingle_vw_EmailGrid(Expression<Func<vw_EmailGrid, bool>> where)
        {
            return vw_EmailGridRepository.Select(where);
        }
        public Task<vw_EmailGrid> SelectSingle_vw_EmailGridAsync(Expression<Func<vw_EmailGrid, bool>> where)
        {
            return vw_EmailGridRepository.SelectAsync(where);
        }

        #endregion vw_EmailGrid

    }

    public partial interface IEmailService : IBaseService
    {
        // Interface Methods

        #region Email

        Email SelectByEmailId(int EmailId, bool cacheRecord = false);

        IEnumerable<Email> SelectMany_Email(Expression<Func<Email, bool>> where);

        Email SelectSingle_Email(Expression<Func<Email, bool>> where);

        IEnumerable<Email> SelectAllEmails();

        bool SaveEmailForm(Email EmailRepository);

        bool DeleteEmailForm(int EmailId);

        Task<Email> SelectByEmailIdAsync(int EmailId);

        Task<IEnumerable<Email>> SelectMany_EmailAsync(Expression<Func<Email, bool>> where);

        Task<Email> SelectSingle_EmailAsync(Expression<Func<Email, bool>> where);

        Task<IEnumerable<Email>> SelectAllEmailsAsync();

        Task<int> SaveEmailFormAsync(Email EmailRepository);

        bool SaveEmailList(List<Email> Emails, List<Email> EmailList);


        Task<int> DeleteEmailFormAsync(int EmailId);

        #endregion

        #region ApplicationInformation

        ApplicationInformation SelectByApplicationInformationId(int ApplicationInformationId, bool cacheRecord = false);

        IEnumerable<ApplicationInformation> SelectMany_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where);

        ApplicationInformation SelectSingle_ApplicationInformation(Expression<Func<ApplicationInformation, bool>> where);

        IEnumerable<ApplicationInformation> SelectAllApplicationInformations();

        bool SaveApplicationInformationForm(ApplicationInformation ApplicationInformationRepository);

        bool DeleteApplicationInformationForm(int ApplicationInformationId);

        Task<ApplicationInformation> SelectByApplicationInformationIdAsync(int ApplicationInformationId);

        Task<IEnumerable<ApplicationInformation>> SelectMany_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where);

        Task<ApplicationInformation> SelectSingle_ApplicationInformationAsync(Expression<Func<ApplicationInformation, bool>> where);

        Task<IEnumerable<ApplicationInformation>> SelectAllApplicationInformationsAsync();

        Task<int> SaveApplicationInformationFormAsync(ApplicationInformation ApplicationInformationRepository);

        Task<int> DeleteApplicationInformationFormAsync(int ApplicationInformationId);

        #endregion

        #region EmailAttachment

        EmailAttachment SelectByEmailAttachmentId(int EmailAttachmentId, bool cacheRecord = false);

        IEnumerable<EmailAttachment> SelectMany_EmailAttachment(Expression<Func<EmailAttachment, bool>> where);

        EmailAttachment SelectSingle_EmailAttachment(Expression<Func<EmailAttachment, bool>> where);

        IEnumerable<EmailAttachment> SelectAllEmailAttachments();

        bool SaveEmailAttachmentForm(EmailAttachment emailAttachment);

        bool DeleteEmailAttachmentForm(int EmailAttachmentId);

        Task<EmailAttachment> SelectByEmailAttachmentIdAsync(int EmailAttachmentId);

        Task<IEnumerable<EmailAttachment>> SelectMany_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where);

        Task<EmailAttachment> SelectSingle_EmailAttachmentAsync(Expression<Func<EmailAttachment, bool>> where);

        Task<IEnumerable<EmailAttachment>> SelectAllEmailAttachmentsAsync();

        Task<int> SaveEmailAttachmentFormAsync(EmailAttachment emailAttachment);

        Task<int> DeleteEmailAttachmentFormAsync(int EmailAttachmentId);

        #endregion

        #region vw_EmailGrid
        List<vw_EmailGrid> Selectvw_EmailGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_EmailGrid> SelectAllvw_EmailGrids();
        Task<IEnumerable<vw_EmailGrid>> SelectAllvw_EmailGridsAsync();
        Task<List<vw_EmailGrid>> Selectvw_EmailGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_EmailGrid> SelectMany_vw_EmailGrid(Expression<Func<vw_EmailGrid, bool>> where);
        Task<IEnumerable<vw_EmailGrid>> SelectMany_vw_EmailGridAsync(Expression<Func<vw_EmailGrid, bool>> where);
        vw_EmailGrid SelectSingle_vw_EmailGrid(Expression<Func<vw_EmailGrid, bool>> where);
        Task<vw_EmailGrid> SelectSingle_vw_EmailGridAsync(Expression<Func<vw_EmailGrid, bool>> where);

        #endregion
    }
}

