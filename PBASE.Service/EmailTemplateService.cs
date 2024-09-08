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

namespace PBASE.Service
{
    public partial class EmailTemplateService : BaseService, IEmailTemplateService
    {
        #region Initialization
        private readonly IEmailTemplateRepository EmailTemplateRepository;
        private readonly IEmailTemplateTagRepository EmailTemplateTagRepository;
        private readonly Ivw_EmailTemplateGridRepository vw_EmailTemplateGridRepository;
        private readonly IUnitOfWork unitOfWork;


        public EmailTemplateService(
            IEmailTemplateRepository EmailTemplateRepository,
            IEmailTemplateTagRepository EmailTemplateTagRepository,
            Ivw_EmailTemplateGridRepository vw_EmailTemplateGridRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
            this.EmailTemplateRepository = EmailTemplateRepository;
            this.EmailTemplateTagRepository = EmailTemplateTagRepository;
            this.vw_EmailTemplateGridRepository = vw_EmailTemplateGridRepository;

        }

        public void Save()
        {
            unitOfWork.Commit(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        #endregion Initialization

        #region EmailTemplate

        #region Sync Methods

        public EmailTemplate SelectByEmailTemplateId(int EmailTemplateId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return EmailTemplateRepository.SelectById(EmailTemplateId);
            }
            else
            {
                EmailTemplate EmailTemplate = CacheService.Get<EmailTemplate>("SelectByEmailTemplateId" + EmailTemplateId);
                if (EmailTemplate == null)
                {
                    EmailTemplate = EmailTemplateRepository.SelectById(EmailTemplateId);
                    CacheService.Add<EmailTemplate>("SelectByEmailTemplateId" + EmailTemplateId, EmailTemplate);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByEmailTemplateId" + EmailTemplateId);
                }
                return EmailTemplate;
            }
        }

        public IEnumerable<EmailTemplate> SelectMany_EmailTemplate(Expression<Func<EmailTemplate, bool>> where)
        {
            return EmailTemplateRepository.SelectMany(where);
        }

        public EmailTemplate SelectSingle_EmailTemplate(Expression<Func<EmailTemplate, bool>> where)
        {
            return EmailTemplateRepository.Select(where);
        }

        public IEnumerable<EmailTemplate> SelectAllEmailTemplates()
        {
            return EmailTemplateRepository.SelectAll();
        }

        public bool SaveEmailTemplateForm(EmailTemplate EmailTemplate)
        {
            try
            {
                if (EmailTemplate.FormMode == FormMode.Create)
                {
                    EmailTemplateRepository.Add(EmailTemplate);
                }
                else
                {
                    EmailTemplateRepository.Update(EmailTemplate);
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

        public bool DeleteEmailTemplateForm(int EmailTemplateId)
        {
            try
            {
                EmailTemplate EmailTemplate = EmailTemplateRepository.SelectById(EmailTemplateId);
                EmailTemplateRepository.Delete(EmailTemplate);

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

        public Task<EmailTemplate> SelectByEmailTemplateIdAsync(int EmailTemplateId)
        {
            return EmailTemplateRepository.SelectByIdAsync(EmailTemplateId);
        }

        public Task<IEnumerable<EmailTemplate>> SelectMany_EmailTemplateAsync(Expression<Func<EmailTemplate, bool>> where)
        {
            return EmailTemplateRepository.SelectManyAsync(where);
        }

        public Task<EmailTemplate> SelectSingle_EmailTemplateAsync(Expression<Func<EmailTemplate, bool>> where)
        {
            return EmailTemplateRepository.SelectAsync(where);
        }

        public Task<IEnumerable<EmailTemplate>> SelectAllEmailTemplatesAsync()
        {
            return EmailTemplateRepository.SelectAllAsync();
        }

        public Task<int> SaveEmailTemplateFormAsync(EmailTemplate EmailTemplate)
        {
            try
            {
                if (EmailTemplate.EmailTemplateId == null)
                {
                    EmailTemplateRepository.Add(EmailTemplate);
                }
                else
                {
                    EmailTemplateRepository.Update(EmailTemplate);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteEmailTemplateFormAsync(int EmailTemplateId)
        {
            try
            {
                EmailTemplate EmailTemplate = EmailTemplateRepository.SelectById(EmailTemplateId);
                EmailTemplateRepository.Delete(EmailTemplate);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion EmailTemplate

        #region EmailTemplateTag

        #region Sync Methods

        public EmailTemplateTag SelectByEmailTemplateTagId(int EmailTemplateTagId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return EmailTemplateTagRepository.SelectById(EmailTemplateTagId);
            }
            else
            {
                EmailTemplateTag EmailTemplateTag = CacheService.Get<EmailTemplateTag>("SelectByEmailTemplateTagId" + EmailTemplateTagId);
                if (EmailTemplateTag == null)
                {
                    EmailTemplateTag = EmailTemplateTagRepository.SelectById(EmailTemplateTagId);
                    CacheService.Add<EmailTemplateTag>("SelectByEmailTemplateTagId" + EmailTemplateTagId, EmailTemplateTag);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByEmailTemplateTagId" + EmailTemplateTagId);
                }
                return EmailTemplateTag;
            }
        }

        public IEnumerable<EmailTemplateTag> SelectMany_EmailTemplateTag(Expression<Func<EmailTemplateTag, bool>> where)
        {
            return EmailTemplateTagRepository.SelectMany(where);
        }

        public EmailTemplateTag SelectSingle_EmailTemplateTag(Expression<Func<EmailTemplateTag, bool>> where)
        {
            return EmailTemplateTagRepository.Select(where);
        }

        public IEnumerable<EmailTemplateTag> SelectAllEmailTemplateTags()
        {
            return EmailTemplateTagRepository.SelectAll();
        }

        public bool SaveEmailTemplateTagForm(EmailTemplateTag EmailTemplateTag)
        {
            try
            {
                if (EmailTemplateTag.FormMode == FormMode.Create)
                {
                    EmailTemplateTagRepository.Add(EmailTemplateTag);
                }
                else
                {
                    EmailTemplateTagRepository.Update(EmailTemplateTag);
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

        public bool DeleteEmailTemplateTagForm(int EmailTemplateTagId)
        {
            try
            {
                EmailTemplateTag EmailTemplateTag = EmailTemplateTagRepository.SelectById(EmailTemplateTagId);
                EmailTemplateTagRepository.Delete(EmailTemplateTag);

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

        public Task<EmailTemplateTag> SelectByEmailTemplateTagIdAsync(int EmailTemplateTagId)
        {
            return EmailTemplateTagRepository.SelectByIdAsync(EmailTemplateTagId);
        }

        public Task<IEnumerable<EmailTemplateTag>> SelectMany_EmailTemplateTagAsync(Expression<Func<EmailTemplateTag, bool>> where)
        {
            return EmailTemplateTagRepository.SelectManyAsync(where);
        }

        public Task<EmailTemplateTag> SelectSingle_EmailTemplateTagAsync(Expression<Func<EmailTemplateTag, bool>> where)
        {
            return EmailTemplateTagRepository.SelectAsync(where);
        }

        public Task<IEnumerable<EmailTemplateTag>> SelectAllEmailTemplateTagsAsync()
        {
            return EmailTemplateTagRepository.SelectAllAsync();
        }

        public Task<int> SaveEmailTemplateTagFormAsync(EmailTemplateTag EmailTemplateTag)
        {
            try
            {
                if (EmailTemplateTag.EmailTemplateTagId == null)
                {
                    EmailTemplateTagRepository.Add(EmailTemplateTag);
                }
                else
                {
                    EmailTemplateTagRepository.Update(EmailTemplateTag);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteEmailTemplateTagFormAsync(int EmailTemplateTagId)
        {
            try
            {
                EmailTemplateTag EmailTemplateTag = EmailTemplateTagRepository.SelectById(EmailTemplateTagId);
                EmailTemplateTagRepository.Delete(EmailTemplateTag);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion EmailTemplateTag

        #region vw_EmailTemplateGrid

        public List<vw_EmailTemplateGrid> Selectvw_EmailTemplateGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_EmailTemplateGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_EmailTemplateGrid>> Selectvw_EmailTemplateGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_EmailTemplateGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_EmailTemplateGrid> SelectAllvw_EmailTemplateGrids()
        {
            return vw_EmailTemplateGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_EmailTemplateGrid>> SelectAllvw_EmailTemplateGridsAsync()
        {
            return vw_EmailTemplateGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_EmailTemplateGrid> SelectMany_vw_EmailTemplateGrid(Expression<Func<vw_EmailTemplateGrid, bool>> where)
        {
            return vw_EmailTemplateGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_EmailTemplateGrid>> SelectMany_vw_EmailTemplateGridAsync(Expression<Func<vw_EmailTemplateGrid, bool>> where)
        {
            return vw_EmailTemplateGridRepository.SelectManyAsync(where);
        }
        public vw_EmailTemplateGrid SelectSingle_vw_EmailTemplateGrid(Expression<Func<vw_EmailTemplateGrid, bool>> where)
        {
            return vw_EmailTemplateGridRepository.Select(where);
        }
        public Task<vw_EmailTemplateGrid> SelectSingle_vw_EmailTemplateGridAsync(Expression<Func<vw_EmailTemplateGrid, bool>> where)
        {
            return vw_EmailTemplateGridRepository.SelectAsync(where);
        }

        #endregion vw_EmailTemplateGrid

    }

    public partial interface IEmailTemplateService : IBaseService
    { 
        #region EmailTemplate

        EmailTemplate SelectByEmailTemplateId(int EmailTemplateId, bool cacheRecord = false);

        IEnumerable<EmailTemplate> SelectMany_EmailTemplate(Expression<Func<EmailTemplate, bool>> where);

        EmailTemplate SelectSingle_EmailTemplate(Expression<Func<EmailTemplate, bool>> where);

        IEnumerable<EmailTemplate> SelectAllEmailTemplates();

        bool SaveEmailTemplateForm(EmailTemplate EmailTemplateRepository);

        bool DeleteEmailTemplateForm(int EmailTemplateId);

        Task<EmailTemplate> SelectByEmailTemplateIdAsync(int EmailTemplateId);

        Task<IEnumerable<EmailTemplate>> SelectMany_EmailTemplateAsync(Expression<Func<EmailTemplate, bool>> where);

        Task<EmailTemplate> SelectSingle_EmailTemplateAsync(Expression<Func<EmailTemplate, bool>> where);

        Task<IEnumerable<EmailTemplate>> SelectAllEmailTemplatesAsync();

        Task<int> SaveEmailTemplateFormAsync(EmailTemplate EmailTemplateRepository);

        Task<int> DeleteEmailTemplateFormAsync(int EmailTemplateId);

        #endregion

        #region EmailTemplateTag

        EmailTemplateTag SelectByEmailTemplateTagId(int EmailTemplateTagId, bool cacheRecord = false);

        IEnumerable<EmailTemplateTag> SelectMany_EmailTemplateTag(Expression<Func<EmailTemplateTag, bool>> where);

        EmailTemplateTag SelectSingle_EmailTemplateTag(Expression<Func<EmailTemplateTag, bool>> where);

        IEnumerable<EmailTemplateTag> SelectAllEmailTemplateTags();

        bool SaveEmailTemplateTagForm(EmailTemplateTag EmailTemplateTagRepository);

        bool DeleteEmailTemplateTagForm(int EmailTemplateTagId);

        Task<EmailTemplateTag> SelectByEmailTemplateTagIdAsync(int EmailTemplateTagId);

        Task<IEnumerable<EmailTemplateTag>> SelectMany_EmailTemplateTagAsync(Expression<Func<EmailTemplateTag, bool>> where);

        Task<EmailTemplateTag> SelectSingle_EmailTemplateTagAsync(Expression<Func<EmailTemplateTag, bool>> where);

        Task<IEnumerable<EmailTemplateTag>> SelectAllEmailTemplateTagsAsync();

        Task<int> SaveEmailTemplateTagFormAsync(EmailTemplateTag EmailTemplateTagRepository);

        Task<int> DeleteEmailTemplateTagFormAsync(int EmailTemplateTagId);

        #endregion

        #region vw_EmailTemplateGrid
        List<vw_EmailTemplateGrid> Selectvw_EmailTemplateGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_EmailTemplateGrid> SelectAllvw_EmailTemplateGrids();
        Task<IEnumerable<vw_EmailTemplateGrid>> SelectAllvw_EmailTemplateGridsAsync();
        Task<List<vw_EmailTemplateGrid>> Selectvw_EmailTemplateGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_EmailTemplateGrid> SelectMany_vw_EmailTemplateGrid(Expression<Func<vw_EmailTemplateGrid, bool>> where);
        Task<IEnumerable<vw_EmailTemplateGrid>> SelectMany_vw_EmailTemplateGridAsync(Expression<Func<vw_EmailTemplateGrid, bool>> where);
        vw_EmailTemplateGrid SelectSingle_vw_EmailTemplateGrid(Expression<Func<vw_EmailTemplateGrid, bool>> where);
        Task<vw_EmailTemplateGrid> SelectSingle_vw_EmailTemplateGridAsync(Expression<Func<vw_EmailTemplateGrid, bool>> where);

        #endregion
    }
}

