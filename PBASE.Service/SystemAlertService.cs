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
    public partial class SystemAlertService : BaseService, ISystemAlertService
    {
        #region Initialization
        //private readonly Ivw_AgreementGridRepository vw_AgreementGridRepository;
        //private readonly Ivw_AgreementUserSubGridRepository vw_AgreementUserSubGridRepository;
        private readonly Ivw_SystemAlertMessagesRepository vw_SystemAlertMessagesRepository;
        private readonly Ivw_SystemAlertIsClosedRepository vw_SystemAlertIsClosedRepository;
        private readonly Ivw_SystemAlertGridRepository vw_SystemAlertGridRepository;
        private readonly ISystemAlertRepository SystemAlertRepository;
        private readonly IUnitOfWork unitOfWork;


        public SystemAlertService(
            ISystemAlertRepository SystemAlertRepository,
            Ivw_SystemAlertGridRepository vw_SystemAlertGridRepository,
            Ivw_SystemAlertIsClosedRepository vw_SystemAlertIsClosedRepository,
            Ivw_SystemAlertMessagesRepository vw_SystemAlertMessagesRepository,
            //Ivw_UserAgreementSubGridRepository vw_UserAgreementSubGridRepository,
            //Ivw_SystemAlertIsClosedRepository vw_SystemAlertIsClosedRepository,
            IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
            this.SystemAlertRepository = SystemAlertRepository;
            this.vw_SystemAlertGridRepository = vw_SystemAlertGridRepository;
            this.vw_SystemAlertIsClosedRepository = vw_SystemAlertIsClosedRepository;
            this.vw_SystemAlertMessagesRepository = vw_SystemAlertMessagesRepository;
            //this.vw_UserAgreementSubGridRepository = vw_UserAgreementSubGridRepository;
            //this.vw_SystemAlertIsClosedRepository = vw_SystemAlertIsClosedRepository;

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



        #region SystemAlert

        #region Sync Methods

        public SystemAlert SelectBySystemAlertId(int SystemAlertId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return SystemAlertRepository.SelectById(SystemAlertId);
            }
            else
            {
                SystemAlert SystemAlert = CacheService.Get<SystemAlert>("SelectBySystemAlertId" + SystemAlertId);
                if (SystemAlert == null)
                {
                    SystemAlert = SystemAlertRepository.SelectById(SystemAlertId);
                    CacheService.Add<SystemAlert>("SelectBySystemAlertId" + SystemAlertId, SystemAlert);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectBySystemAlertId" + SystemAlertId);
                }
                return SystemAlert;
            }
        }

        public IEnumerable<SystemAlert> SelectMany_SystemAlert(Expression<Func<SystemAlert, bool>> where)
        {
            return SystemAlertRepository.SelectMany(where);
        }

        public SystemAlert SelectSingle_SystemAlert(Expression<Func<SystemAlert, bool>> where)
        {
            return SystemAlertRepository.Select(where);
        }

        public IEnumerable<SystemAlert> SelectAllSystemAlerts()
        {
            return SystemAlertRepository.SelectAll();
        }

        public bool SaveSystemAlertForm(SystemAlert SystemAlert)
        {
            try
            {
                if (SystemAlert.FormMode == FormMode.Create)
                {
                    SystemAlertRepository.Add(SystemAlert);
                }
                else
                {
                    SystemAlertRepository.Update(SystemAlert);
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

        public bool DeleteSystemAlertForm(int SystemAlertId)
        {
            try
            {
                SystemAlert SystemAlert = SystemAlertRepository.SelectById(SystemAlertId);
                SystemAlertRepository.Delete(SystemAlert);

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

        public Task<SystemAlert> SelectBySystemAlertIdAsync(int SystemAlertId)
        {
            return SystemAlertRepository.SelectByIdAsync(SystemAlertId);
        }

        public Task<IEnumerable<SystemAlert>> SelectMany_SystemAlertAsync(Expression<Func<SystemAlert, bool>> where)
        {
            return SystemAlertRepository.SelectManyAsync(where);
        }

        public Task<SystemAlert> SelectSingle_SystemAlertAsync(Expression<Func<SystemAlert, bool>> where)
        {
            return SystemAlertRepository.SelectAsync(where);
        }

        public Task<IEnumerable<SystemAlert>> SelectAllSystemAlertsAsync()
        {
            return SystemAlertRepository.SelectAllAsync();
        }

        public Task<int> SaveSystemAlertFormAsync(SystemAlert SystemAlert)
        {
            try
            {
                if (SystemAlert.SystemAlertId == 0)
                {
                    SystemAlertRepository.Add(SystemAlert);
                }
                else
                {
                    SystemAlertRepository.Update(SystemAlert);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteSystemAlertFormAsync(int SystemAlertId)
        {
            try
            {
                SystemAlert SystemAlert = SystemAlertRepository.SelectById(SystemAlertId);
                SystemAlertRepository.Delete(SystemAlert);

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

        #region vw_SystemAlertGrid

        public List<vw_SystemAlertGrid> Selectvw_SystemAlertGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_SystemAlertGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_SystemAlertGrid>> Selectvw_SystemAlertGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_SystemAlertGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_SystemAlertGrid> SelectAllvw_SystemAlertGrids()
        {
            return vw_SystemAlertGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_SystemAlertGrid>> SelectAllvw_SystemAlertGridsAsync()
        {
            return vw_SystemAlertGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_SystemAlertGrid> SelectMany_vw_SystemAlertGrid(Expression<Func<vw_SystemAlertGrid, bool>> where)
        {
            return vw_SystemAlertGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_SystemAlertGrid>> SelectMany_vw_SystemAlertGridAsync(Expression<Func<vw_SystemAlertGrid, bool>> where)
        {
            return vw_SystemAlertGridRepository.SelectManyAsync(where);
        }
        public vw_SystemAlertGrid SelectSingle_vw_SystemAlertGrid(Expression<Func<vw_SystemAlertGrid, bool>> where)
        {
            return vw_SystemAlertGridRepository.Select(where);
        }
        public Task<vw_SystemAlertGrid> SelectSingle_vw_SystemAlertGridAsync(Expression<Func<vw_SystemAlertGrid, bool>> where)
        {
            return vw_SystemAlertGridRepository.SelectAsync(where);
        }

        #endregion vw_SystemAlertGrid

        #region vw_SystemAlertIsClosed

        public List<vw_SystemAlertIsClosed> Selectvw_SystemAlertIsClosedsByGridSetting(GridSetting gridSetting)
        {
            return vw_SystemAlertIsClosedRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_SystemAlertIsClosed>> Selectvw_SystemAlertIsClosedsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_SystemAlertIsClosedRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_SystemAlertIsClosed> SelectAllvw_SystemAlertIsCloseds()
        {
            return vw_SystemAlertIsClosedRepository.SelectAll();
        }
        public Task<IEnumerable<vw_SystemAlertIsClosed>> SelectAllvw_SystemAlertIsClosedsAsync()
        {
            return vw_SystemAlertIsClosedRepository.SelectAllAsync();
        }
        public IEnumerable<vw_SystemAlertIsClosed> SelectMany_vw_SystemAlertIsClosed(Expression<Func<vw_SystemAlertIsClosed, bool>> where)
        {
            return vw_SystemAlertIsClosedRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_SystemAlertIsClosed>> SelectMany_vw_SystemAlertIsClosedAsync(Expression<Func<vw_SystemAlertIsClosed, bool>> where)
        {
            return vw_SystemAlertIsClosedRepository.SelectManyAsync(where);
        }
        public vw_SystemAlertIsClosed SelectSingle_vw_SystemAlertIsClosed(Expression<Func<vw_SystemAlertIsClosed, bool>> where)
        {
            return vw_SystemAlertIsClosedRepository.Select(where);
        }
        public Task<vw_SystemAlertIsClosed> SelectSingle_vw_SystemAlertIsClosedAsync(Expression<Func<vw_SystemAlertIsClosed, bool>> where)
        {
            return vw_SystemAlertIsClosedRepository.SelectAsync(where);
        }

        #endregion vw_SystemAlertIsClosed                    

        #region vw_SystemAlertMessages

        public List<vw_SystemAlertMessages> Selectvw_SystemAlertMessagessByGridSetting(GridSetting gridSetting)
        {
            return vw_SystemAlertMessagesRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_SystemAlertMessages>> Selectvw_SystemAlertMessagessByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_SystemAlertMessagesRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_SystemAlertMessages> SelectAllvw_SystemAlertMessagess()
        {
            return vw_SystemAlertMessagesRepository.SelectAll();
        }
        public Task<IEnumerable<vw_SystemAlertMessages>> SelectAllvw_SystemAlertMessagessAsync()
        {
            return vw_SystemAlertMessagesRepository.SelectAllAsync();
        }
        public IEnumerable<vw_SystemAlertMessages> SelectMany_vw_SystemAlertMessages(Expression<Func<vw_SystemAlertMessages, bool>> where)
        {
            return vw_SystemAlertMessagesRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_SystemAlertMessages>> SelectMany_vw_SystemAlertMessagesAsync(Expression<Func<vw_SystemAlertMessages, bool>> where)
        {
            return vw_SystemAlertMessagesRepository.SelectManyAsync(where);
        }
        public vw_SystemAlertMessages SelectSingle_vw_SystemAlertMessages(Expression<Func<vw_SystemAlertMessages, bool>> where)
        {
            return vw_SystemAlertMessagesRepository.Select(where);
        }
        public Task<vw_SystemAlertMessages> SelectSingle_vw_SystemAlertMessagesAsync(Expression<Func<vw_SystemAlertMessages, bool>> where)
        {
            return vw_SystemAlertMessagesRepository.SelectAsync(where);
        }

        #endregion vw_SystemAlertMessages                    

    }

    public partial interface ISystemAlertService : IBaseService
    {


        #region SystemAlert

        SystemAlert SelectBySystemAlertId(int SystemAlertId, bool cacheRecord = false);

        IEnumerable<SystemAlert> SelectMany_SystemAlert(Expression<Func<SystemAlert, bool>> where);

        SystemAlert SelectSingle_SystemAlert(Expression<Func<SystemAlert, bool>> where);

        IEnumerable<SystemAlert> SelectAllSystemAlerts();

        bool SaveSystemAlertForm(SystemAlert SystemAlertRepository);

        bool DeleteSystemAlertForm(int SystemAlertId);

        Task<SystemAlert> SelectBySystemAlertIdAsync(int SystemAlertId);

        Task<IEnumerable<SystemAlert>> SelectMany_SystemAlertAsync(Expression<Func<SystemAlert, bool>> where);

        Task<SystemAlert> SelectSingle_SystemAlertAsync(Expression<Func<SystemAlert, bool>> where);

        Task<IEnumerable<SystemAlert>> SelectAllSystemAlertsAsync();

        Task<int> SaveSystemAlertFormAsync(SystemAlert SystemAlertRepository);

        Task<int> DeleteSystemAlertFormAsync(int SystemAlertId);
        #endregion

        #region vw_SystemAlertGrid
        List<vw_SystemAlertGrid> Selectvw_SystemAlertGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertGrid> SelectAllvw_SystemAlertGrids();
        Task<IEnumerable<vw_SystemAlertGrid>> SelectAllvw_SystemAlertGridsAsync();
        Task<List<vw_SystemAlertGrid>> Selectvw_SystemAlertGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertGrid> SelectMany_vw_SystemAlertGrid(Expression<Func<vw_SystemAlertGrid, bool>> where);
        Task<IEnumerable<vw_SystemAlertGrid>> SelectMany_vw_SystemAlertGridAsync(Expression<Func<vw_SystemAlertGrid, bool>> where);
        vw_SystemAlertGrid SelectSingle_vw_SystemAlertGrid(Expression<Func<vw_SystemAlertGrid, bool>> where);
        Task<vw_SystemAlertGrid> SelectSingle_vw_SystemAlertGridAsync(Expression<Func<vw_SystemAlertGrid, bool>> where);

        #endregion

        #region vw_SystemAlertIsClosed
        List<vw_SystemAlertIsClosed> Selectvw_SystemAlertIsClosedsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertIsClosed> SelectAllvw_SystemAlertIsCloseds();
        Task<IEnumerable<vw_SystemAlertIsClosed>> SelectAllvw_SystemAlertIsClosedsAsync();
        Task<List<vw_SystemAlertIsClosed>> Selectvw_SystemAlertIsClosedsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertIsClosed> SelectMany_vw_SystemAlertIsClosed(Expression<Func<vw_SystemAlertIsClosed, bool>> where);
        Task<IEnumerable<vw_SystemAlertIsClosed>> SelectMany_vw_SystemAlertIsClosedAsync(Expression<Func<vw_SystemAlertIsClosed, bool>> where);
        vw_SystemAlertIsClosed SelectSingle_vw_SystemAlertIsClosed(Expression<Func<vw_SystemAlertIsClosed, bool>> where);
        Task<vw_SystemAlertIsClosed> SelectSingle_vw_SystemAlertIsClosedAsync(Expression<Func<vw_SystemAlertIsClosed, bool>> where);

        #endregion

        #region vw_SystemAlertMessages
        List<vw_SystemAlertMessages> Selectvw_SystemAlertMessagessByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertMessages> SelectAllvw_SystemAlertMessagess();
        Task<IEnumerable<vw_SystemAlertMessages>> SelectAllvw_SystemAlertMessagessAsync();
        Task<List<vw_SystemAlertMessages>> Selectvw_SystemAlertMessagessByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_SystemAlertMessages> SelectMany_vw_SystemAlertMessages(Expression<Func<vw_SystemAlertMessages, bool>> where);
        Task<IEnumerable<vw_SystemAlertMessages>> SelectMany_vw_SystemAlertMessagesAsync(Expression<Func<vw_SystemAlertMessages, bool>> where);
        vw_SystemAlertMessages SelectSingle_vw_SystemAlertMessages(Expression<Func<vw_SystemAlertMessages, bool>> where);
        Task<vw_SystemAlertMessages> SelectSingle_vw_SystemAlertMessagesAsync(Expression<Func<vw_SystemAlertMessages, bool>> where);

        #endregion                
    }
}

