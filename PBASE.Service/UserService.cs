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
    public partial class UserService : BaseService, IUserService
    {
        #region Initialization
        private readonly Ivw_RoleGridRepository vw_RoleGridRepository;
        private readonly Ivw_LookupUserAssignedRolesRepository vw_LookupUserAssignedRolesRepository;
        private readonly Ivw_LookupUserRolesRepository vw_LookupUserRolesRepository;
        private readonly Ivw_UserMenuOptionRepository vw_UserMenuOptionRepository;
        private readonly Ivw_UserDashboardOptionRepository vw_UserDashboardOptionRepository;
        private readonly Ivw_UserMenuOptionRoleGridRepository vw_UserMenuOptionRoleGridRepository;
        private readonly Ivw_UserDashboardOptionRoleGridRepository vw_UserDashboardOptionRoleGridRepository;
        private readonly IUserMenuOptionRoleRepository userMenuOptionRoleRepository;
        private readonly IUserDashboardOptionRoleRepository userDashboardOptionRoleRepository;
        private readonly Ivw_UserGroupGridRepository vw_UserGroupGridRepository;
        private readonly Ivw_UserGroupObjectSubGridRepository vw_UserGroupObjectSubGridRepository;
        private readonly Ivw_UserGroupDashboardObjectSubGridRepository vw_UserGroupDashboardObjectSubGridRepository;
        private readonly IUserGroupMenuOptionRepository userGroupMenuOptionRepository;
        private readonly IUserGroupDashboardOptionRepository userGroupDashboardOptionRepository;
        private readonly IUserGroupRepository userGroupRepository;
        private readonly IAspNetUserLogsRepository aspNetUserLogsRepository;
        private readonly Ivw_AspNetUserLogsGridRepository vw_AspNetUserLogsGridRepository;
        private readonly Ivw_AspNetUserAccountLogsRepository vw_AspNetUserAccountLogsRepository;
        private readonly Ivw_UserGridRepository vw_UserGridRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserAccountsRepository UserAccountsRepository;
        private readonly IUserClaimsRepository UserClaimsRepository;
        private readonly Ivw_InvalidEmailLogGridRepository vw_InvalidEmailLogGridRepository;
        private readonly IAspNetUsersInvalidRepository AspNetUsersInvalidRepository;

        public UserService(
            Ivw_LookupUserRolesRepository vw_LookupUserRolesRepository,
            Ivw_UserMenuOptionRepository vw_UserMenuOptionRepository,
            Ivw_UserDashboardOptionRepository vw_UserDashboardOptionRepository,
            Ivw_LookupUserAssignedRolesRepository vw_LookupUserAssignedRolesRepository,
            Ivw_RoleGridRepository vw_RoleGridRepository,
            Ivw_UserMenuOptionRoleGridRepository vw_UserMenuOptionRoleGridRepository,
            Ivw_UserDashboardOptionRoleGridRepository vw_UserDashboardOptionRoleGridRepository,
            IUserMenuOptionRoleRepository userMenuOptionRoleRepository,
            IUserDashboardOptionRoleRepository userDashboardOptionRoleRepository,
            Ivw_UserGroupGridRepository vw_UserGroupGridRepository,
            Ivw_UserGroupObjectSubGridRepository vw_UserGroupObjectSubGridRepository,
            IUserGroupMenuOptionRepository userGroupMenuOptionRepository,
            IUserGroupDashboardOptionRepository userGroupDashboardOptionRepository,
            IAspNetUserLogsRepository aspNetUserLogsRepository,
            Ivw_AspNetUserLogsGridRepository vw_AspNetUserLogsGridRepository,
            Ivw_AspNetUserAccountLogsRepository vw_AspNetUserAccountLogsRepository,
            Ivw_UserGridRepository vw_UserGridRepository,
            IUserGroupRepository userGroupRepository,
            IUserAccountsRepository UserAccountsRepository,
            IUserClaimsRepository UserClaimsRepository,
            Ivw_UserGroupDashboardObjectSubGridRepository vw_UserGroupDashboardObjectSubGridRepository,
            Ivw_InvalidEmailLogGridRepository vw_InvalidEmailLogGridRepository,
            IAspNetUsersInvalidRepository AspNetUsersInvalidRepository,
        IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.vw_RoleGridRepository = vw_RoleGridRepository;
            this.vw_LookupUserAssignedRolesRepository = vw_LookupUserAssignedRolesRepository;
            this.vw_LookupUserRolesRepository = vw_LookupUserRolesRepository;
            this.vw_UserMenuOptionRepository = vw_UserMenuOptionRepository;
            this.vw_UserDashboardOptionRepository = vw_UserDashboardOptionRepository;
            this.vw_UserMenuOptionRoleGridRepository = vw_UserMenuOptionRoleGridRepository;
            this.vw_UserDashboardOptionRoleGridRepository = vw_UserDashboardOptionRoleGridRepository;
            this.userMenuOptionRoleRepository = userMenuOptionRoleRepository;
            this.userDashboardOptionRoleRepository = userDashboardOptionRoleRepository;
            this.vw_UserGroupGridRepository = vw_UserGroupGridRepository;
            this.vw_UserGroupObjectSubGridRepository = vw_UserGroupObjectSubGridRepository;
            this.userGroupMenuOptionRepository = userGroupMenuOptionRepository;
            this.userGroupDashboardOptionRepository = userGroupDashboardOptionRepository;
            this.aspNetUserLogsRepository = aspNetUserLogsRepository;
            this.vw_AspNetUserLogsGridRepository = vw_AspNetUserLogsGridRepository;
            this.vw_AspNetUserAccountLogsRepository = vw_AspNetUserAccountLogsRepository;
            this.userGroupRepository = userGroupRepository;
            this.UserAccountsRepository = UserAccountsRepository;
            this.vw_UserGridRepository = vw_UserGridRepository;
            this.UserClaimsRepository = UserClaimsRepository;
            this.vw_UserGroupDashboardObjectSubGridRepository = vw_UserGroupDashboardObjectSubGridRepository;
            this.vw_InvalidEmailLogGridRepository = vw_InvalidEmailLogGridRepository;
            this.AspNetUsersInvalidRepository = AspNetUsersInvalidRepository;
        }

        public void Save()
        {
            unitOfWork.Commit(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        public Task<int> SaveAsync()
        {
            return unitOfWork.CommitAsync(HttpContext.Current.User.Identity.GetUserId<int>());
        }

        public Task<int> SaveAsync(int userId)
        {
            return unitOfWork.CommitAsync(userId);
        }

        #endregion Initialization

        #region UserGroup

        #region Sync Methods

        public UserGroup SelectByUserGroupId(int UserGroupId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userGroupRepository.SelectById(UserGroupId);
            }
            else
            {
                UserGroup UserGroup = CacheService.Get<UserGroup>("SelectByUserGroupId" + UserGroupId);
                if (UserGroup == null)
                {
                    UserGroup = userGroupRepository.SelectById(UserGroupId);
                    CacheService.Add<UserGroup>("SelectByUserGroupId" + UserGroupId, UserGroup);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserGroupId" + UserGroupId);
                }
                return UserGroup;
            }
        }

        public IEnumerable<UserGroup> SelectMany_UserGroup(Expression<Func<UserGroup, bool>> where)
        {
            return userGroupRepository.SelectMany(where);
        }

        public UserGroup SelectSingle_UserGroup(Expression<Func<UserGroup, bool>> where)
        {
            return userGroupRepository.Select(where);
        }

        public IEnumerable<UserGroup> SelectAllUserGroups()
        {
            return userGroupRepository.SelectAll();
        }

        public bool SaveUserGroupForm(UserGroup UserGroup)
        {
            try
            {
                if (UserGroup.FormMode == FormMode.Create)
                {
                    userGroupRepository.Add(UserGroup);
                }
                else
                {
                    userGroupRepository.Update(UserGroup);
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

        public bool DeleteUserGroupForm(int UserGroupId)
        {
            try
            {
                UserGroup UserGroup = userGroupRepository.SelectById(UserGroupId);
                userGroupRepository.Delete(UserGroup);

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

        public Task<UserGroup> SelectByUserGroupIdAsync(int UserGroupId)
        {
            return userGroupRepository.SelectByIdAsync(UserGroupId);
        }

        public Task<IEnumerable<UserGroup>> SelectMany_UserGroupAsync(Expression<Func<UserGroup, bool>> where)
        {
            return userGroupRepository.SelectManyAsync(where);
        }

        public Task<UserGroup> SelectSingle_UserGroupAsync(Expression<Func<UserGroup, bool>> where)
        {
            return userGroupRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserGroup>> SelectAllUserGroupsAsync()
        {
            return userGroupRepository.SelectAllAsync();
        }

        public Task<int> SaveUserGroupFormAsync(UserGroup UserGroup)
        {
            try
            {
                if (UserGroup.UserGroupId == 0)
                {
                    userGroupRepository.Add(UserGroup);
                }
                else
                {
                    userGroupRepository.Update(UserGroup);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserGroupFormAsync(int UserGroupId)
        {
            try
            {
                UserGroup UserGroup = userGroupRepository.SelectById(UserGroupId);
                userGroupRepository.Delete(UserGroup);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion UserGroup

        #region UserGroupMenuOption

        #region Sync Methods

        public UserGroupMenuOption SelectByUserGroupMenuOptionId(int UserGroupMenuOptionId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userGroupMenuOptionRepository.SelectById(UserGroupMenuOptionId);
            }
            else
            {
                UserGroupMenuOption UserGroupMenuOption = CacheService.Get<UserGroupMenuOption>("SelectByUserGroupMenuOptionId" + UserGroupMenuOptionId);
                if (UserGroupMenuOption == null)
                {
                    UserGroupMenuOption = userGroupMenuOptionRepository.SelectById(UserGroupMenuOptionId);
                    CacheService.Add<UserGroupMenuOption>("SelectByUserGroupMenuOptionId" + UserGroupMenuOptionId, UserGroupMenuOption);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserGroupMenuOptionId" + UserGroupMenuOptionId);
                }
                return UserGroupMenuOption;
            }
        }

        public IEnumerable<UserGroupMenuOption> SelectMany_UserGroupMenuOption(Expression<Func<UserGroupMenuOption, bool>> where)
        {
            return userGroupMenuOptionRepository.SelectMany(where);
        }

        public UserGroupMenuOption SelectSingle_UserGroupMenuOption(Expression<Func<UserGroupMenuOption, bool>> where)
        {
            return userGroupMenuOptionRepository.Select(where);
        }

        public IEnumerable<UserGroupMenuOption> SelectAllUserGroupMenuOptions()
        {
            return userGroupMenuOptionRepository.SelectAll();
        }

        public bool SaveUserGroupMenuOptionForm(UserGroupMenuOption UserGroupMenuOption)
        {
            try
            {
                if (UserGroupMenuOption.FormMode == FormMode.Create)
                {
                    userGroupMenuOptionRepository.Add(UserGroupMenuOption);
                }
                else
                {
                    userGroupMenuOptionRepository.Update(UserGroupMenuOption);
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

        public bool DeleteUserGroupMenuOptionForm(int UserGroupMenuOptionId)
        {
            try
            {
                UserGroupMenuOption UserGroupMenuOption = userGroupMenuOptionRepository.SelectById(UserGroupMenuOptionId);
                userGroupMenuOptionRepository.Delete(UserGroupMenuOption);

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

        public Task<UserGroupMenuOption> SelectByUserGroupMenuOptionIdAsync(int UserGroupMenuOptionId)
        {
            return userGroupMenuOptionRepository.SelectByIdAsync(UserGroupMenuOptionId);
        }

        public Task<IEnumerable<UserGroupMenuOption>> SelectMany_UserGroupMenuOptionAsync(Expression<Func<UserGroupMenuOption, bool>> where)
        {
            return userGroupMenuOptionRepository.SelectManyAsync(where);
        }

        public Task<UserGroupMenuOption> SelectSingle_UserGroupMenuOptionAsync(Expression<Func<UserGroupMenuOption, bool>> where)
        {
            return userGroupMenuOptionRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserGroupMenuOption>> SelectAllUserGroupMenuOptionsAsync()
        {
            return userGroupMenuOptionRepository.SelectAllAsync();
        }

        public Task<int> SaveUserGroupMenuOptionFormAsync(UserGroupMenuOption UserGroupMenuOption)
        {
            try
            {
                if (UserGroupMenuOption.UserGroupMenuOptionId == 0)
                {
                    userGroupMenuOptionRepository.Add(UserGroupMenuOption);
                }
                else
                {
                    userGroupMenuOptionRepository.Update(UserGroupMenuOption);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> UserGroupMenuOptionTableActionsAsync(IEnumerable<vw_UserGroupObjectSubGrid> UserGroupMenuOptionList)
        {
            try
            {                
                foreach (var UserGroupMenuOption in UserGroupMenuOptionList)
                {
                    if (!UserGroupMenuOption.UserGroupMenuOptionId.HasValue && UserGroupMenuOption.AccessTypeId.HasValue)
                    {
                        //for add
                        UserGroupMenuOption entity = new UserGroupMenuOption();
                        entity.AccessTypeId = UserGroupMenuOption.AccessTypeId;
                        entity.UserGroupId = UserGroupMenuOption.UserGroupId;
                        entity.MenuOptionId = UserGroupMenuOption.ObjectId;
                        entity.IsArchived = false;
                        userGroupMenuOptionRepository.Add(entity);
                    }
                    else if (UserGroupMenuOption.UserGroupMenuOptionId.HasValue && UserGroupMenuOption.AccessTypeId.HasValue)
                    {
                        //for edit
                        UserGroupMenuOption entity = new UserGroupMenuOption();
                        entity = userGroupMenuOptionRepository.SelectById(UserGroupMenuOption.UserGroupMenuOptionId.Value);
                        entity.AccessTypeId = UserGroupMenuOption.AccessTypeId;
                        entity.UserGroupId = UserGroupMenuOption.UserGroupId;
                        entity.MenuOptionId = UserGroupMenuOption.ObjectId;
                        userGroupMenuOptionRepository.Update(entity);
                    }
                    else if (UserGroupMenuOption.UserGroupMenuOptionId.HasValue && !UserGroupMenuOption.AccessTypeId.HasValue)
                    {
                        //for delete
                        UserGroupMenuOption entity = new UserGroupMenuOption();
                        entity = userGroupMenuOptionRepository.SelectById(UserGroupMenuOption.UserGroupMenuOptionId.Value);
                        userGroupMenuOptionRepository.Delete(entity);
                    }
                    else
                    {
                        return Task.FromResult(1);
                    }
                }
                return SaveAsync();
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }
        }

        public Task<int> UserGroupDashboardMenuOptionTableActionsAsync(IEnumerable<vw_UserGroupDashboardObjectSubGrid> UserGroupDashboardOptionList)
        {
            try
            {
                foreach (var UserGroupDashboardOption in UserGroupDashboardOptionList)
                {
                    if (!UserGroupDashboardOption.UserGroupDashboardOptionId.HasValue && UserGroupDashboardOption.AccessTypeId.HasValue)
                    {
                        //for add
                        UserGroupDashboardOption entity = new UserGroupDashboardOption();
                        entity.AccessTypeId = UserGroupDashboardOption.AccessTypeId;
                        entity.UserGroupId = UserGroupDashboardOption.UserGroupId;
                        entity.DashboardOptionId = UserGroupDashboardOption.DashboardObjectId;
                        entity.IsArchived = false;
                        userGroupDashboardOptionRepository.Add(entity);
                    }
                    else if (UserGroupDashboardOption.UserGroupDashboardOptionId.HasValue && UserGroupDashboardOption.AccessTypeId.HasValue)
                    {
                        //for edit
                        UserGroupDashboardOption entity = new UserGroupDashboardOption();
                        entity = userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOption.UserGroupDashboardOptionId.Value);
                        entity.AccessTypeId = UserGroupDashboardOption.AccessTypeId;
                        entity.UserGroupId = UserGroupDashboardOption.UserGroupId;
                        entity.DashboardOptionId = UserGroupDashboardOption.DashboardObjectId;
                        userGroupDashboardOptionRepository.Update(entity);
                    }
                    else if (UserGroupDashboardOption.UserGroupDashboardOptionId.HasValue && !UserGroupDashboardOption.AccessTypeId.HasValue)
                    {
                        //for delete
                        UserGroupDashboardOption entity = new UserGroupDashboardOption();
                        entity = userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOption.UserGroupDashboardOptionId.Value);
                        userGroupDashboardOptionRepository.Delete(entity);
                    }
                    else
                    {
                        return Task.FromResult(1);
                    }
                }
                return SaveAsync();
            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }
        }

        public Task<int> DeleteUserGroupMenuOptionFormAsync(int UserGroupMenuOptionId)
        {
            try
            {
                UserGroupMenuOption UserGroupMenuOption = userGroupMenuOptionRepository.SelectById(UserGroupMenuOptionId);
                userGroupMenuOptionRepository.Delete(UserGroupMenuOption);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        public bool SaveUserGroupMenuOptionsForm(IEnumerable<int?> menuOptionIds, int userGroupId)
        {
            try
            {
                int? menuOptionId;
                var userMenuOptionIds = userGroupMenuOptionRepository.SelectMany(x => x.UserGroupId == userGroupId).Select(x => x.MenuOptionId);
                if (menuOptionIds.Any())
                {
                    foreach (var userMenuOptionId in menuOptionIds)
                    {
                        menuOptionId = userMenuOptionIds.FirstOrDefault(x => x == userMenuOptionId);
                        if (menuOptionId == null)
                        {
                            UserGroupMenuOption userMenuOption = new UserGroupMenuOption();
                            userMenuOption.UserGroupId = userGroupId;
                            userMenuOption.MenuOptionId = userMenuOptionId;
                            userMenuOption.IsArchived = false;
                            userMenuOption.UserGroupMenuOptionId = 0;
                            userGroupMenuOptionRepository.Add(userMenuOption);
                        }
                    }
                    Save();
                }
                if (userMenuOptionIds.Any())
                {
                    foreach (var userMenuOptionId in userMenuOptionIds)
                    {
                        menuOptionId = menuOptionIds.FirstOrDefault(x => x == userMenuOptionId);
                        if (menuOptionId == null)
                        {
                            UserGroupMenuOption userMenuOption = userGroupMenuOptionRepository.Select(x => x.MenuOptionId == userMenuOptionId && x.UserGroupId == userGroupId);
                            userGroupMenuOptionRepository.Delete(userMenuOption);
                        }
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

        #endregion UserGroupMenuOption

        #region UserGroupDashboardOption

        #region Sync Methods

        public UserGroupDashboardOption SelectByUserGroupDashboardOptionId(int UserGroupDashboardOptionId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOptionId);
            }
            else
            {
                UserGroupDashboardOption UserGroupDashboardOption = CacheService.Get<UserGroupDashboardOption>("SelectByUserGroupDashboardOptionId" + UserGroupDashboardOptionId);
                if (UserGroupDashboardOption == null)
                {
                    UserGroupDashboardOption = userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOptionId);
                    CacheService.Add<UserGroupDashboardOption>("SelectByUserGroupDashboardOptionId" + UserGroupDashboardOptionId, UserGroupDashboardOption);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserGroupDashboardOptionId" + UserGroupDashboardOptionId);
                }
                return UserGroupDashboardOption;
            }
        }

        public IEnumerable<UserGroupDashboardOption> SelectMany_UserGroupDashboardOption(Expression<Func<UserGroupDashboardOption, bool>> where)
        {
            return userGroupDashboardOptionRepository.SelectMany(where);
        }

        public UserGroupDashboardOption SelectSingle_UserGroupDashboardOption(Expression<Func<UserGroupDashboardOption, bool>> where)
        {
            return userGroupDashboardOptionRepository.Select(where);
        }

        public IEnumerable<UserGroupDashboardOption> SelectAllUserGroupDashboardOptions()
        {
            return userGroupDashboardOptionRepository.SelectAll();
        }

        public bool SaveUserGroupDashboardOptionForm(UserGroupDashboardOption UserGroupDashboardOption)
        {
            try
            {
                if (UserGroupDashboardOption.FormMode == FormMode.Create)
                {
                    userGroupDashboardOptionRepository.Add(UserGroupDashboardOption);
                }
                else
                {
                    userGroupDashboardOptionRepository.Update(UserGroupDashboardOption);
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

        public bool DeleteUserGroupDashboardOptionForm(int UserGroupDashboardOptionId)
        {
            try
            {
                UserGroupDashboardOption UserGroupDashboardOption = userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOptionId);
                userGroupDashboardOptionRepository.Delete(UserGroupDashboardOption);

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

        public Task<UserGroupDashboardOption> SelectByUserGroupDashboardOptionIdAsync(int UserGroupDashboardOptionId)
        {
            return userGroupDashboardOptionRepository.SelectByIdAsync(UserGroupDashboardOptionId);
        }

        public Task<IEnumerable<UserGroupDashboardOption>> SelectMany_UserGroupDashboardOptionAsync(Expression<Func<UserGroupDashboardOption, bool>> where)
        {
            return userGroupDashboardOptionRepository.SelectManyAsync(where);
        }

        public Task<UserGroupDashboardOption> SelectSingle_UserGroupDashboardOptionAsync(Expression<Func<UserGroupDashboardOption, bool>> where)
        {
            return userGroupDashboardOptionRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserGroupDashboardOption>> SelectAllUserGroupDashboardOptionsAsync()
        {
            return userGroupDashboardOptionRepository.SelectAllAsync();
        }

        public Task<int> SaveUserGroupDashboardOptionFormAsync(UserGroupDashboardOption UserGroupDashboardOption)
        {
            try
            {
                if (UserGroupDashboardOption.UserGroupDashboardOptionId == 0)
                {
                    userGroupDashboardOptionRepository.Add(UserGroupDashboardOption);
                }
                else
                {
                    userGroupDashboardOptionRepository.Update(UserGroupDashboardOption);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserGroupDashboardOptionFormAsync(int UserGroupDashboardOptionId)
        {
            try
            {
                UserGroupDashboardOption UserGroupDashboardOption = userGroupDashboardOptionRepository.SelectById(UserGroupDashboardOptionId);
                userGroupDashboardOptionRepository.Delete(UserGroupDashboardOption);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        public bool SaveUserGroupDashboardOptionsForm(IEnumerable<int?> dashboardOptionIds, int userGroupId)
        {
            try
            {
                int? dashboardOptionId = null;
                var userDashboardOptionIds = userGroupDashboardOptionRepository.SelectMany(x => x.UserGroupId == userGroupId).Select(x => x.DashboardOptionId);
                if (dashboardOptionIds != null && dashboardOptionIds.Any())
                {
                    foreach (var userDashboardOptionId in dashboardOptionIds)
                    {
                        dashboardOptionId = userDashboardOptionIds.FirstOrDefault(x => x == userDashboardOptionId);
                        if (dashboardOptionId == null)
                        {
                            UserGroupDashboardOption userDashboardOption = new UserGroupDashboardOption();
                            userDashboardOption.UserGroupId = userGroupId;
                            userDashboardOption.DashboardOptionId = userDashboardOptionId;
                            userDashboardOption.IsArchived = false;
                            userDashboardOption.UserGroupDashboardOptionId = 0;
                            userGroupDashboardOptionRepository.Add(userDashboardOption);
                        }
                    }
                    Save();
                }
                if (userDashboardOptionIds.Any())
                {
                    foreach (var userDashboardOptionId in userDashboardOptionIds)
                    {
                        if(dashboardOptionIds != null)
                        {
                            dashboardOptionId = dashboardOptionIds.FirstOrDefault(x => x == userDashboardOptionId);
                        }
                        if (dashboardOptionId == null)
                        {
                            UserGroupDashboardOption userDashboardOption = userGroupDashboardOptionRepository.Select(x => x.DashboardOptionId == userDashboardOptionId && x.UserGroupId == userGroupId);
                            userGroupDashboardOptionRepository.Delete(userDashboardOption);
                        }
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

        #endregion UserGroupDashboardOption

        #region UserMenuOptionRole

        #region Sync Methods

        public UserMenuOptionRole SelectByUserMenuOptionRoleId(int UserMenuOptionRoleId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userMenuOptionRoleRepository.SelectById(UserMenuOptionRoleId);
            }
            else
            {
                UserMenuOptionRole UserMenuOptionRole = CacheService.Get<UserMenuOptionRole>("SelectByUserMenuOptionRoleId" + UserMenuOptionRoleId);
                if (UserMenuOptionRole == null)
                {
                    UserMenuOptionRole = userMenuOptionRoleRepository.SelectById(UserMenuOptionRoleId);
                    CacheService.Add<UserMenuOptionRole>("SelectByUserMenuOptionRoleId" + UserMenuOptionRoleId, UserMenuOptionRole);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserMenuOptionRoleId" + UserMenuOptionRoleId);
                }
                return UserMenuOptionRole;
            }
        }

        public IEnumerable<UserMenuOptionRole> SelectMany_UserMenuOptionRole(Expression<Func<UserMenuOptionRole, bool>> where)
        {
            return userMenuOptionRoleRepository.SelectMany(where);
        }

        public UserMenuOptionRole SelectSingle_UserMenuOptionRole(Expression<Func<UserMenuOptionRole, bool>> where)
        {
            return userMenuOptionRoleRepository.Select(where);
        }

        public IEnumerable<UserMenuOptionRole> SelectAllUserMenuOptionRoles()
        {
            return userMenuOptionRoleRepository.SelectAll();
        }

        public bool SaveUserMenuOptionRoleForm(UserMenuOptionRole UserMenuOptionRole)
        {
            try
            {
                if (UserMenuOptionRole.FormMode == FormMode.Create)
                {
                    userMenuOptionRoleRepository.Add(UserMenuOptionRole);
                }
                else
                {
                    userMenuOptionRoleRepository.Update(UserMenuOptionRole);
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

        public bool DeleteUserMenuOptionRoleForm(int UserMenuOptionRoleId)
        {
            try
            {
                UserMenuOptionRole UserMenuOptionRole = userMenuOptionRoleRepository.SelectById(UserMenuOptionRoleId);
                userMenuOptionRoleRepository.Delete(UserMenuOptionRole);

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

        public Task<UserMenuOptionRole> SelectByUserMenuOptionRoleIdAsync(int UserMenuOptionRoleId)
        {
            return userMenuOptionRoleRepository.SelectByIdAsync(UserMenuOptionRoleId);
        }

        public Task<IEnumerable<UserMenuOptionRole>> SelectMany_UserMenuOptionRoleAsync(Expression<Func<UserMenuOptionRole, bool>> where)
        {
            return userMenuOptionRoleRepository.SelectManyAsync(where);
        }

        public Task<UserMenuOptionRole> SelectSingle_UserMenuOptionRoleAsync(Expression<Func<UserMenuOptionRole, bool>> where)
        {
            return userMenuOptionRoleRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserMenuOptionRole>> SelectAllUserMenuOptionRolesAsync()
        {
            return userMenuOptionRoleRepository.SelectAllAsync();
        }

        public Task<int> SaveUserMenuOptionRoleFormAsync(UserMenuOptionRole UserMenuOptionRole)
        {
            try
            {
                if (UserMenuOptionRole.UserMenuOptionRoleId == 0)
                {
                    userMenuOptionRoleRepository.Add(UserMenuOptionRole);
                }
                else
                {
                    userMenuOptionRoleRepository.Update(UserMenuOptionRole);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserMenuOptionRoleFormAsync(int UserMenuOptionRoleId)
        {
            try
            {
                UserMenuOptionRole UserMenuOptionRole = userMenuOptionRoleRepository.SelectById(UserMenuOptionRoleId);
                userMenuOptionRoleRepository.Delete(UserMenuOptionRole);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public bool SaveRoleFormWithUserMenuOptionRole(UserMenuOptionRole userMenuOptionRole, IEnumerable<int> aspNetRoleIds)
        {
            try
            {
                int? aspNetRoleId;
                var menuAspNetRoleIds = userMenuOptionRoleRepository.SelectMany(x => x.MenuOptionId == userMenuOptionRole.MenuOptionId).Select(x => x.AspNetRoleId);
                if (aspNetRoleIds.Any())
                {
                    foreach (var menuAspNetRoleId in aspNetRoleIds)
                    {
                        aspNetRoleId = menuAspNetRoleIds.FirstOrDefault(x => x == menuAspNetRoleId);
                        if (aspNetRoleId == null)
                        {
                            UserMenuOptionRole menuOptionRole = new UserMenuOptionRole();
                            menuOptionRole.MenuOptionId = userMenuOptionRole.MenuOptionId;
                            menuOptionRole.AspNetRoleId = menuAspNetRoleId;
                            menuOptionRole.IsArchived = false;
                            menuOptionRole.UserMenuOptionRoleId = 0;
                            userMenuOptionRoleRepository.Add(menuOptionRole);
                        }
                    }
                    Save();
                }
                if (menuAspNetRoleIds.Any())
                {
                    foreach (var menuAspNetRoleId in menuAspNetRoleIds)
                    {
                        aspNetRoleId = aspNetRoleIds.FirstOrDefault(x => x == menuAspNetRoleId);
                        if (aspNetRoleId == 0)
                        {
                            UserMenuOptionRole menuOptionRole = userMenuOptionRoleRepository.Select(x => x.AspNetRoleId == menuAspNetRoleId && x.MenuOptionId == userMenuOptionRole.MenuOptionId);
                            userMenuOptionRoleRepository.Delete(menuOptionRole);
                        }
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


        #endregion UserMenuOptionRole

        #region UserDashboardOptionRole

        #region Sync Methods

        public UserDashboardOptionRole SelectByUserDashboardOptionRoleId(int UserDashboardOptionRoleId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return userDashboardOptionRoleRepository.SelectById(UserDashboardOptionRoleId);
            }
            else
            {
                UserDashboardOptionRole UserDashboardOptionRole = CacheService.Get<UserDashboardOptionRole>("SelectByUserDashboardOptionRoleId" + UserDashboardOptionRoleId);
                if (UserDashboardOptionRole == null)
                {
                    UserDashboardOptionRole = userDashboardOptionRoleRepository.SelectById(UserDashboardOptionRoleId);
                    CacheService.Add<UserDashboardOptionRole>("SelectByUserDashboardOptionRoleId" + UserDashboardOptionRoleId, UserDashboardOptionRole);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserDashboardOptionRoleId" + UserDashboardOptionRoleId);
                }
                return UserDashboardOptionRole;
            }
        }

        public IEnumerable<UserDashboardOptionRole> SelectMany_UserDashboardOptionRole(Expression<Func<UserDashboardOptionRole, bool>> where)
        {
            return userDashboardOptionRoleRepository.SelectMany(where);
        }

        public UserDashboardOptionRole SelectSingle_UserDashboardOptionRole(Expression<Func<UserDashboardOptionRole, bool>> where)
        {
            return userDashboardOptionRoleRepository.Select(where);
        }

        public IEnumerable<UserDashboardOptionRole> SelectAllUserDashboardOptionRoles()
        {
            return userDashboardOptionRoleRepository.SelectAll();
        }

        public bool SaveUserDashboardOptionRoleForm(UserDashboardOptionRole UserDashboardOptionRole)
        {
            try
            {
                if (UserDashboardOptionRole.FormMode == FormMode.Create)
                {
                    userDashboardOptionRoleRepository.Add(UserDashboardOptionRole);
                }
                else
                {
                    userDashboardOptionRoleRepository.Update(UserDashboardOptionRole);
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

        public bool DeleteUserDashboardOptionRoleForm(int UserDashboardOptionRoleId)
        {
            try
            {
                UserDashboardOptionRole UserDashboardOptionRole = userDashboardOptionRoleRepository.SelectById(UserDashboardOptionRoleId);
                userDashboardOptionRoleRepository.Delete(UserDashboardOptionRole);

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

        public Task<UserDashboardOptionRole> SelectByUserDashboardOptionRoleIdAsync(int UserDashboardOptionRoleId)
        {
            return userDashboardOptionRoleRepository.SelectByIdAsync(UserDashboardOptionRoleId);
        }

        public Task<IEnumerable<UserDashboardOptionRole>> SelectMany_UserDashboardOptionRoleAsync(Expression<Func<UserDashboardOptionRole, bool>> where)
        {
            return userDashboardOptionRoleRepository.SelectManyAsync(where);
        }

        public Task<UserDashboardOptionRole> SelectSingle_UserDashboardOptionRoleAsync(Expression<Func<UserDashboardOptionRole, bool>> where)
        {
            return userDashboardOptionRoleRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserDashboardOptionRole>> SelectAllUserDashboardOptionRolesAsync()
        {
            return userDashboardOptionRoleRepository.SelectAllAsync();
        }

        public Task<int> SaveUserDashboardOptionRoleFormAsync(UserDashboardOptionRole UserDashboardOptionRole)
        {
            try
            {
                if (UserDashboardOptionRole.UserDashboardOptionRoleId == 0)
                {
                    userDashboardOptionRoleRepository.Add(UserDashboardOptionRole);
                }
                else
                {
                    userDashboardOptionRoleRepository.Update(UserDashboardOptionRole);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserDashboardOptionRoleFormAsync(int UserDashboardOptionRoleId)
        {
            try
            {
                UserDashboardOptionRole UserDashboardOptionRole = userDashboardOptionRoleRepository.SelectById(UserDashboardOptionRoleId);
                userDashboardOptionRoleRepository.Delete(UserDashboardOptionRole);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }


        public bool SaveRoleFormWithUserDashboardOptionRole(UserDashboardOptionRole userDashboardOptionRole, IEnumerable<int> aspNetRoleIds)
        {
            try
            {
                int? aspNetRoleId;
                var dashboardAspNetRoleIds = userDashboardOptionRoleRepository.SelectMany(x => x.DashboardOptionId == userDashboardOptionRole.DashboardOptionId).Select(x => x.AspNetRoleId);
                if (aspNetRoleIds.Any())
                {
                    foreach (var dashboardAspNetRoleId in aspNetRoleIds)
                    {
                        aspNetRoleId = dashboardAspNetRoleIds.FirstOrDefault(x => x == dashboardAspNetRoleId);
                        if (aspNetRoleId == null)
                        {
                            UserDashboardOptionRole dashboradOptionRole = new UserDashboardOptionRole();
                            dashboradOptionRole.DashboardOptionId = userDashboardOptionRole.DashboardOptionId;
                            dashboradOptionRole.AspNetRoleId = dashboardAspNetRoleId;
                            dashboradOptionRole.IsArchived = false;
                            dashboradOptionRole.UserDashboardOptionRoleId = 0;
                            userDashboardOptionRoleRepository.Add(dashboradOptionRole);
                        }
                    }
                    Save();
                }
                if (dashboardAspNetRoleIds.Any())
                {
                    foreach (var dashboardAspNetRoleId in dashboardAspNetRoleIds)
                    {
                        aspNetRoleId = aspNetRoleIds.FirstOrDefault(x => x == dashboardAspNetRoleId);
                        if (aspNetRoleId == 0)
                        {
                            UserDashboardOptionRole dashboradOptionRole = userDashboardOptionRoleRepository.Select(x => x.AspNetRoleId == dashboardAspNetRoleId && x.DashboardOptionId == userDashboardOptionRole.DashboardOptionId);
                            userDashboardOptionRoleRepository.Delete(dashboradOptionRole);
                        }
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

        #endregion UserDashboardOptionRole

        #region vw_UserMenuOptionRoleGrid

        public List<vw_UserMenuOptionRoleGrid> Selectvw_UserMenuOptionRoleGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserMenuOptionRoleGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserMenuOptionRoleGrid>> Selectvw_UserMenuOptionRoleGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserMenuOptionRoleGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserMenuOptionRoleGrid> SelectAllvw_UserMenuOptionRoleGrids()
        {
            return vw_UserMenuOptionRoleGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserMenuOptionRoleGrid>> SelectAllvw_UserMenuOptionRoleGridsAsync()
        {
            return vw_UserMenuOptionRoleGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserMenuOptionRoleGrid> SelectMany_vw_UserMenuOptionRoleGrid(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where)
        {
            return vw_UserMenuOptionRoleGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserMenuOptionRoleGrid>> SelectMany_vw_UserMenuOptionRoleGridAsync(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where)
        {
            return vw_UserMenuOptionRoleGridRepository.SelectManyAsync(where);
        }
        public vw_UserMenuOptionRoleGrid SelectSingle_vw_UserMenuOptionRoleGrid(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where)
        {
            return vw_UserMenuOptionRoleGridRepository.Select(where);
        }
        public Task<vw_UserMenuOptionRoleGrid> SelectSingle_vw_UserMenuOptionRoleGridAsync(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where)
        {
            return vw_UserMenuOptionRoleGridRepository.SelectAsync(where);
        }

        #endregion vw_UserMenuOptionRoleGrid

        #region vw_UserDashboardOptionRoleGrid

        public List<vw_UserDashboardOptionRoleGrid> Selectvw_UserDashboardOptionRoleGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserDashboardOptionRoleGrid>> Selectvw_UserDashboardOptionRoleGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserDashboardOptionRoleGrid> SelectAllvw_UserDashboardOptionRoleGrids()
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserDashboardOptionRoleGrid>> SelectAllvw_UserDashboardOptionRoleGridsAsync()
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserDashboardOptionRoleGrid> SelectMany_vw_UserDashboardOptionRoleGrid(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where)
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserDashboardOptionRoleGrid>> SelectMany_vw_UserDashboardOptionRoleGridAsync(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where)
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectManyAsync(where);
        }
        public vw_UserDashboardOptionRoleGrid SelectSingle_vw_UserDashboardOptionRoleGrid(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where)
        {
            return vw_UserDashboardOptionRoleGridRepository.Select(where);
        }
        public Task<vw_UserDashboardOptionRoleGrid> SelectSingle_vw_UserDashboardOptionRoleGridAsync(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where)
        {
            return vw_UserDashboardOptionRoleGridRepository.SelectAsync(where);
        }

        #endregion vw_UserDashboardOptionRoleGrid

        #region vw_RoleGrid

        public List<vw_RoleGrid> Selectvw_RoleGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_RoleGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_RoleGrid>> Selectvw_RoleGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_RoleGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_RoleGrid> SelectAllvw_RoleGrids()
        {
            return vw_RoleGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_RoleGrid>> SelectAllvw_RoleGridsAsync()
        {
            return vw_RoleGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_RoleGrid> SelectMany_vw_RoleGrid(Expression<Func<vw_RoleGrid, bool>> where)
        {
            return vw_RoleGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_RoleGrid>> SelectMany_vw_RoleGridAsync(Expression<Func<vw_RoleGrid, bool>> where)
        {
            return vw_RoleGridRepository.SelectManyAsync(where);
        }
        public vw_RoleGrid SelectSingle_vw_RoleGrid(Expression<Func<vw_RoleGrid, bool>> where)
        {
            return vw_RoleGridRepository.Select(where);
        }
        public Task<vw_RoleGrid> SelectSingle_vw_RoleGridAsync(Expression<Func<vw_RoleGrid, bool>> where)
        {
            return vw_RoleGridRepository.SelectAsync(where);
        }

        #endregion vw_RoleGrid

        #region vw_LookupUserAssignedRoles

        public List<vw_LookupUserAssignedRoles> Selectvw_LookupUserAssignedRolessByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUserAssignedRolesRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUserAssignedRoles>> Selectvw_LookupUserAssignedRolessByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUserAssignedRolesRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUserAssignedRoles> SelectAllvw_LookupUserAssignedRoless()
        {
            return vw_LookupUserAssignedRolesRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUserAssignedRoles>> SelectAllvw_LookupUserAssignedRolessAsync()
        {
            return vw_LookupUserAssignedRolesRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUserAssignedRoles> SelectMany_vw_LookupUserAssignedRoles(Expression<Func<vw_LookupUserAssignedRoles, bool>> where)
        {
            return vw_LookupUserAssignedRolesRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUserAssignedRoles>> SelectMany_vw_LookupUserAssignedRolesAsync(Expression<Func<vw_LookupUserAssignedRoles, bool>> where)
        {
            return vw_LookupUserAssignedRolesRepository.SelectManyAsync(where);
        }
        public vw_LookupUserAssignedRoles SelectSingle_vw_LookupUserAssignedRoles(Expression<Func<vw_LookupUserAssignedRoles, bool>> where)
        {
            return vw_LookupUserAssignedRolesRepository.Select(where);
        }
        public Task<vw_LookupUserAssignedRoles> SelectSingle_vw_LookupUserAssignedRolesAsync(Expression<Func<vw_LookupUserAssignedRoles, bool>> where)
        {
            return vw_LookupUserAssignedRolesRepository.SelectAsync(where);
        }

        #endregion vw_LookupUserAssignedRoles

        #region vw_LookupUserRoles

        public List<vw_LookupUserRoles> Selectvw_LookupUserRolessByGridSetting(GridSetting gridSetting)
        {
            return vw_LookupUserRolesRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_LookupUserRoles>> Selectvw_LookupUserRolessByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_LookupUserRolesRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_LookupUserRoles> SelectAllvw_LookupUserRoless()
        {
            return vw_LookupUserRolesRepository.SelectAll();
        }
        public Task<IEnumerable<vw_LookupUserRoles>> SelectAllvw_LookupUserRolessAsync()
        {
            return vw_LookupUserRolesRepository.SelectAllAsync();
        }
        public IEnumerable<vw_LookupUserRoles> SelectMany_vw_LookupUserRoles(Expression<Func<vw_LookupUserRoles, bool>> where)
        {
            return vw_LookupUserRolesRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_LookupUserRoles>> SelectMany_vw_LookupUserRolesAsync(Expression<Func<vw_LookupUserRoles, bool>> where)
        {
            return vw_LookupUserRolesRepository.SelectManyAsync(where);
        }
        public vw_LookupUserRoles SelectSingle_vw_LookupUserRoles(Expression<Func<vw_LookupUserRoles, bool>> where)
        {
            return vw_LookupUserRolesRepository.Select(where);
        }
        public Task<vw_LookupUserRoles> SelectSingle_vw_LookupUserRolesAsync(Expression<Func<vw_LookupUserRoles, bool>> where)
        {
            return vw_LookupUserRolesRepository.SelectAsync(where);
        }

        #endregion vw_LookupUserRoles

        #region vw_UserMenuOption

        public List<vw_UserMenuOption> Selectvw_UserMenuOptionsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserMenuOptionRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserMenuOption>> Selectvw_UserMenuOptionsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserMenuOptionRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserMenuOption> SelectAllvw_UserMenuOptions()
        {
            return vw_UserMenuOptionRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserMenuOption>> SelectAllvw_UserMenuOptionsAsync()
        {
            return vw_UserMenuOptionRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserMenuOption> SelectMany_vw_UserMenuOption(Expression<Func<vw_UserMenuOption, bool>> where)
        {
            return vw_UserMenuOptionRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserMenuOption>> SelectMany_vw_UserMenuOptionAsync(Expression<Func<vw_UserMenuOption, bool>> where)
        {
            return vw_UserMenuOptionRepository.SelectManyAsync(where);
        }
        public vw_UserMenuOption SelectSingle_vw_UserMenuOption(Expression<Func<vw_UserMenuOption, bool>> where)
        {
            return vw_UserMenuOptionRepository.Select(where);
        }
        public Task<vw_UserMenuOption> SelectSingle_vw_UserMenuOptionAsync(Expression<Func<vw_UserMenuOption, bool>> where)
        {
            return vw_UserMenuOptionRepository.SelectAsync(where);
        }

        #endregion vw_UserMenuOption

        #region vw_UserDashboardOption

        public List<vw_UserDashboardOption> Selectvw_UserDashboardOptionsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserDashboardOptionRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserDashboardOption>> Selectvw_UserDashboardOptionsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserDashboardOptionRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserDashboardOption> SelectAllvw_UserDashboardOptions()
        {
            return vw_UserDashboardOptionRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserDashboardOption>> SelectAllvw_UserDashboardOptionsAsync()
        {
            return vw_UserDashboardOptionRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserDashboardOption> SelectMany_vw_UserDashboardOption(Expression<Func<vw_UserDashboardOption, bool>> where)
        {
            return vw_UserDashboardOptionRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserDashboardOption>> SelectMany_vw_UserDashboardOptionAsync(Expression<Func<vw_UserDashboardOption, bool>> where)
        {
            return vw_UserDashboardOptionRepository.SelectManyAsync(where);
        }
        public vw_UserDashboardOption SelectSingle_vw_UserDashboardOption(Expression<Func<vw_UserDashboardOption, bool>> where)
        {
            return vw_UserDashboardOptionRepository.Select(where);
        }
        public Task<vw_UserDashboardOption> SelectSingle_vw_UserDashboardOptionAsync(Expression<Func<vw_UserDashboardOption, bool>> where)
        {
            return vw_UserDashboardOptionRepository.SelectAsync(where);
        }

        #endregion vw_UserDashboardOption

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

        #region vw_UserGroupGrid

        public List<vw_UserGroupGrid> Selectvw_UserGroupGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserGroupGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserGroupGrid>> Selectvw_UserGroupGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserGroupGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserGroupGrid> SelectAllvw_UserGroupGrids()
        {
            return vw_UserGroupGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserGroupGrid>> SelectAllvw_UserGroupGridsAsync()
        {
            return vw_UserGroupGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserGroupGrid> SelectMany_vw_UserGroupGrid(Expression<Func<vw_UserGroupGrid, bool>> where)
        {
            return vw_UserGroupGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserGroupGrid>> SelectMany_vw_UserGroupGridAsync(Expression<Func<vw_UserGroupGrid, bool>> where)
        {
            return vw_UserGroupGridRepository.SelectManyAsync(where);
        }
        public vw_UserGroupGrid SelectSingle_vw_UserGroupGrid(Expression<Func<vw_UserGroupGrid, bool>> where)
        {
            return vw_UserGroupGridRepository.Select(where);
        }
        public Task<vw_UserGroupGrid> SelectSingle_vw_UserGroupGridAsync(Expression<Func<vw_UserGroupGrid, bool>> where)
        {
            return vw_UserGroupGridRepository.SelectAsync(where);
        }

        #endregion vw_UserGroupGrid

        #region vw_UserGroupObjectSubGrid

        public List<vw_UserGroupObjectSubGrid> Selectvw_UserGroupObjectSubGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserGroupObjectSubGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserGroupObjectSubGrid>> Selectvw_UserGroupObjectSubGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserGroupObjectSubGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserGroupObjectSubGrid> SelectAllvw_UserGroupObjectSubGrids()
        {
            return vw_UserGroupObjectSubGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserGroupObjectSubGrid>> SelectAllvw_UserGroupObjectSubGridsAsync()
        {
            return vw_UserGroupObjectSubGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserGroupObjectSubGrid> SelectMany_vw_UserGroupObjectSubGrid(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where)
        {
            return vw_UserGroupObjectSubGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserGroupObjectSubGrid>> SelectMany_vw_UserGroupObjectSubGridAsync(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where)
        {
            return vw_UserGroupObjectSubGridRepository.SelectManyAsync(where);
        }
        public vw_UserGroupObjectSubGrid SelectSingle_vw_UserGroupObjectSubGrid(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where)
        {
            return vw_UserGroupObjectSubGridRepository.Select(where);
        }
        public Task<vw_UserGroupObjectSubGrid> SelectSingle_vw_UserGroupObjectSubGridAsync(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where)
        {
            return vw_UserGroupObjectSubGridRepository.SelectAsync(where);
        }

        #endregion vw_UserGroupObjectSubGrid

        #region vw_UserGroupDashboardObjectSubGrid

        public List<vw_UserGroupDashboardObjectSubGrid> Selectvw_UserGroupDashboardObjectSubGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_UserGroupDashboardObjectSubGrid>> Selectvw_UserGroupDashboardObjectSubGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_UserGroupDashboardObjectSubGrid> SelectAllvw_UserGroupDashboardObjectSubGrids()
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_UserGroupDashboardObjectSubGrid>> SelectAllvw_UserGroupDashboardObjectSubGridsAsync()
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_UserGroupDashboardObjectSubGrid> SelectMany_vw_UserGroupDashboardObjectSubGrid(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_UserGroupDashboardObjectSubGrid>> SelectMany_vw_UserGroupDashboardObjectSubGridAsync(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectManyAsync(where);
        }
        public vw_UserGroupDashboardObjectSubGrid SelectSingle_vw_UserGroupDashboardObjectSubGrid(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.Select(where);
        }
        public Task<vw_UserGroupDashboardObjectSubGrid> SelectSingle_vw_UserGroupDashboardObjectSubGridAsync(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where)
        {
            return vw_UserGroupDashboardObjectSubGridRepository.SelectAsync(where);
        }

        #endregion vw_UserGroupDashboardObjectSubGrid

        #region AspNetUserLogs

        #region Sync Methods

        public AspNetUserLogs SelectByAspNetUserLogsId(int aspNetUserLogsKey, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return aspNetUserLogsRepository.SelectById(aspNetUserLogsKey);
            }
            else
            {
                AspNetUserLogs aspNetUserLogs = CacheService.Get<AspNetUserLogs>("SelectByAspNetUserLogsKey" + aspNetUserLogsKey);
                if (aspNetUserLogs == null)
                {
                    aspNetUserLogs = aspNetUserLogsRepository.SelectById(aspNetUserLogsKey);
                    CacheService.Add<AspNetUserLogs>("SelectByAspNetUserLogsKey" + aspNetUserLogsKey, aspNetUserLogs);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByAspNetUserLogsKey" + aspNetUserLogsKey);
                }
                return aspNetUserLogs;
            }
        }

        public IEnumerable<AspNetUserLogs> SelectMany_AspNetUserLogs(Expression<Func<AspNetUserLogs, bool>> where)
        {
            return aspNetUserLogsRepository.SelectMany(where);
        }

        public AspNetUserLogs SelectSingle_AspNetUserLogs(Expression<Func<AspNetUserLogs, bool>> where)
        {
            return aspNetUserLogsRepository.Select(where);
        }

        public IEnumerable<AspNetUserLogs> SelectAllAspNetUserLogss()
        {
            return aspNetUserLogsRepository.SelectAll();
        }

        public bool SaveAspNetUserLogsForm(AspNetUserLogs aspNetUserLogs)
        {
            try
            {
                if (aspNetUserLogs.FormMode == FormMode.Create)
                {
                    aspNetUserLogsRepository.Add(aspNetUserLogs);
                }
                else
                {
                    aspNetUserLogsRepository.Update(aspNetUserLogs);
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

        public bool DeleteAspNetUserLogsForm(int aspNetUserLogsKey)
        {
            try
            {
                AspNetUserLogs aspNetUserLogs = aspNetUserLogsRepository.SelectById(aspNetUserLogsKey);
                aspNetUserLogsRepository.Delete(aspNetUserLogs);

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

        public Task<AspNetUserLogs> SelectByAspNetUserLogsIdAsync(int aspNetUserLogsKey)
        {
            return aspNetUserLogsRepository.SelectByIdAsync(aspNetUserLogsKey);
        }

        public Task<IEnumerable<AspNetUserLogs>> SelectMany_AspNetUserLogsAsync(Expression<Func<AspNetUserLogs, bool>> where)
        {
            return aspNetUserLogsRepository.SelectManyAsync(where);
        }

        public Task<AspNetUserLogs> SelectSingle_AspNetUserLogsAsync(Expression<Func<AspNetUserLogs, bool>> where)
        {
            return aspNetUserLogsRepository.SelectAsync(where);
        }

        public Task<IEnumerable<AspNetUserLogs>> SelectAllAspNetUserLogssAsync()
        {
            return aspNetUserLogsRepository.SelectAllAsync();
        }

        public Task<int> SaveAspNetUserLogsFormAsync(AspNetUserLogs aspNetUserLogs)
        {
            try
            {
                if (aspNetUserLogs.AspNetUserLogsKey == 0)
                {
                    aspNetUserLogsRepository.Add(aspNetUserLogs);
                }
                else
                {
                    aspNetUserLogsRepository.Update(aspNetUserLogs);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteAspNetUserLogsFormAsync(int aspNetUserLogsKey)
        {
            try
            {
                AspNetUserLogs aspNetUserLogs = aspNetUserLogsRepository.SelectById(aspNetUserLogsKey);
                aspNetUserLogsRepository.Delete(aspNetUserLogs);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion AspNetUserLogs

        #region vw_AspNetUserLogsGrid

        public List<vw_AspNetUserLogsGrid> Selectvw_AspNetUserLogsGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_AspNetUserLogsGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_AspNetUserLogsGrid>> Selectvw_AspNetUserLogsGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_AspNetUserLogsGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_AspNetUserLogsGrid> SelectAllvw_AspNetUserLogsGrids()
        {
            return vw_AspNetUserLogsGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_AspNetUserLogsGrid>> SelectAllvw_AspNetUserLogsGridsAsync()
        {
            return vw_AspNetUserLogsGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_AspNetUserLogsGrid> SelectMany_vw_AspNetUserLogsGrid(Expression<Func<vw_AspNetUserLogsGrid, bool>> where)
        {
            return vw_AspNetUserLogsGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_AspNetUserLogsGrid>> SelectMany_vw_AspNetUserLogsGridAsync(Expression<Func<vw_AspNetUserLogsGrid, bool>> where)
        {
            return vw_AspNetUserLogsGridRepository.SelectManyAsync(where);
        }
        public vw_AspNetUserLogsGrid SelectSingle_vw_AspNetUserLogsGrid(Expression<Func<vw_AspNetUserLogsGrid, bool>> where)
        {
            return vw_AspNetUserLogsGridRepository.Select(where);
        }
        public Task<vw_AspNetUserLogsGrid> SelectSingle_vw_AspNetUserLogsGridAsync(Expression<Func<vw_AspNetUserLogsGrid, bool>> where)
        {
            return vw_AspNetUserLogsGridRepository.SelectAsync(where);
        }

        #endregion vw_AspNetUserLogsGrid

        #region vw_AspNetUserAccountLogs

        public List<vw_AspNetUserAccountLogs> Selectvw_AspNetUserAccountLogssByGridSetting(GridSetting gridSetting)
        {
            return vw_AspNetUserAccountLogsRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_AspNetUserAccountLogs>> Selectvw_AspNetUserAccountLogssByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_AspNetUserAccountLogsRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_AspNetUserAccountLogs> SelectAllvw_AspNetUserAccountLogss()
        {
            return vw_AspNetUserAccountLogsRepository.SelectAll();
        }
        public Task<IEnumerable<vw_AspNetUserAccountLogs>> SelectAllvw_AspNetUserAccountLogssAsync()
        {
            return vw_AspNetUserAccountLogsRepository.SelectAllAsync();
        }
        public IEnumerable<vw_AspNetUserAccountLogs> SelectMany_vw_AspNetUserAccountLogs(Expression<Func<vw_AspNetUserAccountLogs, bool>> where)
        {
            return vw_AspNetUserAccountLogsRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_AspNetUserAccountLogs>> SelectMany_vw_AspNetUserAccountLogsAsync(Expression<Func<vw_AspNetUserAccountLogs, bool>> where)
        {
            return vw_AspNetUserAccountLogsRepository.SelectManyAsync(where);
        }
        public vw_AspNetUserAccountLogs SelectSingle_vw_AspNetUserAccountLogs(Expression<Func<vw_AspNetUserAccountLogs, bool>> where)
        {
            return vw_AspNetUserAccountLogsRepository.Select(where);
        }
        public Task<vw_AspNetUserAccountLogs> SelectSingle_vw_AspNetUserAccountLogsAsync(Expression<Func<vw_AspNetUserAccountLogs, bool>> where)
        {
            return vw_AspNetUserAccountLogsRepository.SelectAsync(where);
        }

        #endregion vw_AspNetUserAccountLogs

        #region UserAccounts

        #region Sync Methods

        public UserAccounts SelectByUserAccountsId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return UserAccountsRepository.SelectById(lookupTypeId);
            }
            else
            {
                UserAccounts lookupType = CacheService.Get<UserAccounts>("SelectByUserAccountsId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = UserAccountsRepository.SelectById(lookupTypeId);
                    CacheService.Add<UserAccounts>("SelectByUserAccountsId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserAccountsId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<UserAccounts> SelectMany_UserAccounts(Expression<Func<UserAccounts, bool>> where)
        {
            return UserAccountsRepository.SelectMany(where);
        }

        public UserAccounts SelectSingle_UserAccounts(Expression<Func<UserAccounts, bool>> where)
        {
            return UserAccountsRepository.Select(where);
        }

        public IEnumerable<UserAccounts> SelectAllUserAccountss()
        {
            return UserAccountsRepository.SelectAll();
        }

        public bool SaveUserAccountsForm(UserAccounts lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    UserAccountsRepository.Add(lookupType);
                }
                else
                {
                    UserAccountsRepository.Update(lookupType);
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

        public bool DeleteUserAccountsForm(int lookupTypeId)
        {
            try
            {
                UserAccounts lookupType = UserAccountsRepository.SelectById(lookupTypeId);
                UserAccountsRepository.Delete(lookupType);

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

        public Task<UserAccounts> SelectByUserAccountsIdAsync(int lookupTypeId)
        {
            return UserAccountsRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<UserAccounts>> SelectMany_UserAccountsAsync(Expression<Func<UserAccounts, bool>> where)
        {
            return UserAccountsRepository.SelectManyAsync(where);
        }

        public Task<UserAccounts> SelectSingle_UserAccountsAsync(Expression<Func<UserAccounts, bool>> where)
        {
            return UserAccountsRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserAccounts>> SelectAllUserAccountssAsync()
        {
            return UserAccountsRepository.SelectAllAsync();
        }

        public Task<int> SaveUserAccountsFormAsync(UserAccounts lookupType)
        {
            try
            {
                if (lookupType.UserAccountsId == 0)
                {
                    UserAccountsRepository.Add(lookupType);
                }
                else
                {
                    UserAccountsRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserAccountsFormAsync(int lookupTypeId)
        {
            try
            {
                UserAccounts lookupType = UserAccountsRepository.SelectById(lookupTypeId);
                UserAccountsRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion UserAccounts

        #region UserClaims

        #region Sync Methods

        public UserClaims SelectByUserClaimsId(int lookupTypeId, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return UserClaimsRepository.SelectById(lookupTypeId);
            }
            else
            {
                UserClaims lookupType = CacheService.Get<UserClaims>("SelectByUserClaimsId" + lookupTypeId);
                if (lookupType == null)
                {
                    lookupType = UserClaimsRepository.SelectById(lookupTypeId);
                    CacheService.Add<UserClaims>("SelectByUserClaimsId" + lookupTypeId, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByUserClaimsId" + lookupTypeId);
                }
                return lookupType;
            }
        }

        public IEnumerable<UserClaims> SelectMany_UserClaims(Expression<Func<UserClaims, bool>> where)
        {
            return UserClaimsRepository.SelectMany(where);
        }

        public UserClaims SelectSingle_UserClaims(Expression<Func<UserClaims, bool>> where)
        {
            return UserClaimsRepository.Select(where);
        }

        public IEnumerable<UserClaims> SelectAllUserClaimss()
        {
            return UserClaimsRepository.SelectAll();
        }

        public bool SaveUserClaimsForm(UserClaims lookupType)
        {
            try
            {
                if (lookupType.FormMode == FormMode.Create)
                {
                    UserClaimsRepository.Add(lookupType);
                }
                else
                {
                    UserClaimsRepository.Update(lookupType);
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

        public bool DeleteUserClaimsForm(int lookupTypeId)
        {
            try
            {
                UserClaims lookupType = UserClaimsRepository.SelectById(lookupTypeId);
                UserClaimsRepository.Delete(lookupType);

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

        public Task<UserClaims> SelectByUserClaimsIdAsync(int lookupTypeId)
        {
            return UserClaimsRepository.SelectByIdAsync(lookupTypeId);
        }

        public Task<IEnumerable<UserClaims>> SelectMany_UserClaimsAsync(Expression<Func<UserClaims, bool>> where)
        {
            return UserClaimsRepository.SelectManyAsync(where);
        }

        public Task<UserClaims> SelectSingle_UserClaimsAsync(Expression<Func<UserClaims, bool>> where)
        {
            return UserClaimsRepository.SelectAsync(where);
        }

        public Task<IEnumerable<UserClaims>> SelectAllUserClaimssAsync()
        {
            return UserClaimsRepository.SelectAllAsync();
        }

        public Task<int> SaveUserClaimsFormAsync(UserClaims lookupType)
        {
            try
            {
                if (lookupType.UserAccountID == 0)
                {
                    UserClaimsRepository.Add(lookupType);
                }
                else
                {
                    UserClaimsRepository.Update(lookupType);
                }

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteUserClaimsFormAsync(int lookupTypeId)
        {
            try
            {
                UserClaims lookupType = UserClaimsRepository.SelectById(lookupTypeId);
                UserClaimsRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion UserClaims

        #region vw_InvalidEmailLogGrid

        public List<vw_InvalidEmailLogGrid> Selectvw_InvalidEmailLogGridsByGridSetting(GridSetting gridSetting)
        {
            return vw_InvalidEmailLogGridRepository.SelectByGridSetting(gridSetting);
        }
        public Task<List<vw_InvalidEmailLogGrid>> Selectvw_InvalidEmailLogGridsByGridSettingAsync(GridSetting gridSetting)
        {
            return vw_InvalidEmailLogGridRepository.SelectByGridSettingAsync(gridSetting);
        }
        public IEnumerable<vw_InvalidEmailLogGrid> SelectAllvw_InvalidEmailLogGrids()
        {
            return vw_InvalidEmailLogGridRepository.SelectAll();
        }
        public Task<IEnumerable<vw_InvalidEmailLogGrid>> SelectAllvw_InvalidEmailLogGridsAsync()
        {
            return vw_InvalidEmailLogGridRepository.SelectAllAsync();
        }
        public IEnumerable<vw_InvalidEmailLogGrid> SelectMany_vw_InvalidEmailLogGrid(Expression<Func<vw_InvalidEmailLogGrid, bool>> where)
        {
            return vw_InvalidEmailLogGridRepository.SelectMany(where);
        }
        public Task<IEnumerable<vw_InvalidEmailLogGrid>> SelectMany_vw_InvalidEmailLogGridAsync(Expression<Func<vw_InvalidEmailLogGrid, bool>> where)
        {
            return vw_InvalidEmailLogGridRepository.SelectManyAsync(where);
        }
        public vw_InvalidEmailLogGrid SelectSingle_vw_InvalidEmailLogGrid(Expression<Func<vw_InvalidEmailLogGrid, bool>> where)
        {
            return vw_InvalidEmailLogGridRepository.Select(where);
        }
        public Task<vw_InvalidEmailLogGrid> SelectSingle_vw_InvalidEmailLogGridAsync(Expression<Func<vw_InvalidEmailLogGrid, bool>> where)
        {
            return vw_InvalidEmailLogGridRepository.SelectAsync(where);
        }

        #endregion vw_InvalidEmailLogGrid

        #region AspNetUsersInvalid

        #region Sync Methods

        public AspNetUsersInvalid SelectByAspNetUsersInvalidId(int id, bool cacheRecord = false)
        {
            if (!cacheRecord)
            {
                return AspNetUsersInvalidRepository.SelectById(id);
            }
            else
            {
                AspNetUsersInvalid lookupType = CacheService.Get<AspNetUsersInvalid>("SelectByAspNetUsersInvalidId" + id);
                if (lookupType == null)
                {
                    lookupType = AspNetUsersInvalidRepository.SelectById(id);
                    CacheService.Add<AspNetUsersInvalid>("SelectByAspNetUsersInvalidId" + id, lookupType);
                }
                else
                {
                    // One time cache only.
                    CacheService.Clear("SelectByAspNetUsersInvalidId" + id);
                }
                return lookupType;
            }
        }

        public IEnumerable<AspNetUsersInvalid> SelectMany_AspNetUsersInvalid(Expression<Func<AspNetUsersInvalid, bool>> where)
        {
            return AspNetUsersInvalidRepository.SelectMany(where);
        }

        public AspNetUsersInvalid SelectSingle_AspNetUsersInvalid(Expression<Func<AspNetUsersInvalid, bool>> where)
        {
            return AspNetUsersInvalidRepository.Select(where);
        }

        public IEnumerable<AspNetUsersInvalid> SelectAllAspNetUsersInvalids()
        {
            return AspNetUsersInvalidRepository.SelectAll();
        }

        public bool SaveAspNetUsersInvalidForm(AspNetUsersInvalid aspNetUsersInvalid)
        {
            try
            {
                if (aspNetUsersInvalid.FormMode == FormMode.Create)
                {
                    AspNetUsersInvalidRepository.Add(aspNetUsersInvalid);
                }
                else
                {
                    AspNetUsersInvalidRepository.Update(aspNetUsersInvalid);
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

        public bool DeleteAspNetUsersInvalidForm(int id)
        {
            try
            {
                AspNetUsersInvalid lookupType = AspNetUsersInvalidRepository.SelectById(id);
                AspNetUsersInvalidRepository.Delete(lookupType);

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

        public Task<AspNetUsersInvalid> SelectByAspNetUsersInvalidIdAsync(int id)
        {
            return AspNetUsersInvalidRepository.SelectByIdAsync(id);
        }

        public Task<IEnumerable<AspNetUsersInvalid>> SelectMany_AspNetUsersInvalidAsync(Expression<Func<AspNetUsersInvalid, bool>> where)
        {
            return AspNetUsersInvalidRepository.SelectManyAsync(where);
        }

        public Task<AspNetUsersInvalid> SelectSingle_AspNetUsersInvalidAsync(Expression<Func<AspNetUsersInvalid, bool>> where)
        {
            return AspNetUsersInvalidRepository.SelectAsync(where);
        }

        public Task<IEnumerable<AspNetUsersInvalid>> SelectAllAspNetUsersInvalidsAsync()
        {
            return AspNetUsersInvalidRepository.SelectAllAsync();
        }

        public Task<int> SaveAspNetUsersInvalidFormAsync(AspNetUsersInvalid aspNetUsersInvalid)
        {
            try
            {
                if (aspNetUsersInvalid.Id == 0)
                {
                    AspNetUsersInvalidRepository.Add(aspNetUsersInvalid);
                }
                else
                {
                    AspNetUsersInvalidRepository.Update(aspNetUsersInvalid);
                }

                return SaveAsync(1);

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        public Task<int> DeleteAspNetUsersInvalidFormAsync(int id)
        {
            try
            {
                AspNetUsersInvalid lookupType = AspNetUsersInvalidRepository.SelectById(id);
                AspNetUsersInvalidRepository.Delete(lookupType);

                return SaveAsync();

            }
            catch (Exception ex)
            {
                base.ProcessServiceException(ex);
                return Task.FromResult(-1);
            }

        }

        #endregion Async Methods

        #endregion AspNetUsersInvalid

    }

    public partial interface IUserService : IBaseService
    {
        // Interface Methods

        #region UserGroup

        UserGroup SelectByUserGroupId(int UserGroupId, bool cacheRecord = false);

        IEnumerable<UserGroup> SelectMany_UserGroup(Expression<Func<UserGroup, bool>> where);

        UserGroup SelectSingle_UserGroup(Expression<Func<UserGroup, bool>> where);

        IEnumerable<UserGroup> SelectAllUserGroups();

        bool SaveUserGroupForm(UserGroup UserGroupRepository);

        bool DeleteUserGroupForm(int UserGroupId);

        Task<UserGroup> SelectByUserGroupIdAsync(int UserGroupId);

        Task<IEnumerable<UserGroup>> SelectMany_UserGroupAsync(Expression<Func<UserGroup, bool>> where);

        Task<UserGroup> SelectSingle_UserGroupAsync(Expression<Func<UserGroup, bool>> where);

        Task<IEnumerable<UserGroup>> SelectAllUserGroupsAsync();

        Task<int> SaveUserGroupFormAsync(UserGroup UserGroupRepository);

        Task<int> DeleteUserGroupFormAsync(int UserGroupId);

        #endregion

        #region UserGroupMenuOption

        UserGroupMenuOption SelectByUserGroupMenuOptionId(int UserGroupMenuOptionId, bool cacheRecord = false);

        IEnumerable<UserGroupMenuOption> SelectMany_UserGroupMenuOption(Expression<Func<UserGroupMenuOption, bool>> where);

        UserGroupMenuOption SelectSingle_UserGroupMenuOption(Expression<Func<UserGroupMenuOption, bool>> where);

        IEnumerable<UserGroupMenuOption> SelectAllUserGroupMenuOptions();

        bool SaveUserGroupMenuOptionForm(UserGroupMenuOption UserGroupMenuOptionRepository);

        bool DeleteUserGroupMenuOptionForm(int UserGroupMenuOptionId);

        Task<UserGroupMenuOption> SelectByUserGroupMenuOptionIdAsync(int UserGroupMenuOptionId);

        Task<IEnumerable<UserGroupMenuOption>> SelectMany_UserGroupMenuOptionAsync(Expression<Func<UserGroupMenuOption, bool>> where);

        Task<UserGroupMenuOption> SelectSingle_UserGroupMenuOptionAsync(Expression<Func<UserGroupMenuOption, bool>> where);

        Task<IEnumerable<UserGroupMenuOption>> SelectAllUserGroupMenuOptionsAsync();

        Task<int> SaveUserGroupMenuOptionFormAsync(UserGroupMenuOption UserGroupMenuOptionRepository);
        Task<int> UserGroupMenuOptionTableActionsAsync(IEnumerable<vw_UserGroupObjectSubGrid> UserGroupMenuOptionList);
        Task<int> UserGroupDashboardMenuOptionTableActionsAsync(IEnumerable<vw_UserGroupDashboardObjectSubGrid> UserGroupDashboardOptionList);
        Task<int> DeleteUserGroupMenuOptionFormAsync(int UserGroupMenuOptionId);
        bool SaveUserGroupMenuOptionsForm(IEnumerable<int?> menuOptionIds, int userGroupId);
        #endregion

        #region UserGroupDashboardOption

        UserGroupDashboardOption SelectByUserGroupDashboardOptionId(int UserGroupDashboardOptionId, bool cacheRecord = false);

        IEnumerable<UserGroupDashboardOption> SelectMany_UserGroupDashboardOption(Expression<Func<UserGroupDashboardOption, bool>> where);

        UserGroupDashboardOption SelectSingle_UserGroupDashboardOption(Expression<Func<UserGroupDashboardOption, bool>> where);

        IEnumerable<UserGroupDashboardOption> SelectAllUserGroupDashboardOptions();

        bool SaveUserGroupDashboardOptionForm(UserGroupDashboardOption UserGroupDashboardOptionRepository);

        bool DeleteUserGroupDashboardOptionForm(int UserGroupDashboardOptionId);

        Task<UserGroupDashboardOption> SelectByUserGroupDashboardOptionIdAsync(int UserGroupDashboardOptionId);

        Task<IEnumerable<UserGroupDashboardOption>> SelectMany_UserGroupDashboardOptionAsync(Expression<Func<UserGroupDashboardOption, bool>> where);

        Task<UserGroupDashboardOption> SelectSingle_UserGroupDashboardOptionAsync(Expression<Func<UserGroupDashboardOption, bool>> where);

        Task<IEnumerable<UserGroupDashboardOption>> SelectAllUserGroupDashboardOptionsAsync();

        Task<int> SaveUserGroupDashboardOptionFormAsync(UserGroupDashboardOption UserGroupDashboardOptionRepository);

        Task<int> DeleteUserGroupDashboardOptionFormAsync(int UserGroupDashboardOptionId);
        bool SaveUserGroupDashboardOptionsForm(IEnumerable<int?> dashboardOptionIds, int userGroupId);
        #endregion

        #region UserMenuOptionRole

        UserMenuOptionRole SelectByUserMenuOptionRoleId(int UserMenuOptionRoleId, bool cacheRecord = false);

        IEnumerable<UserMenuOptionRole> SelectMany_UserMenuOptionRole(Expression<Func<UserMenuOptionRole, bool>> where);

        UserMenuOptionRole SelectSingle_UserMenuOptionRole(Expression<Func<UserMenuOptionRole, bool>> where);

        IEnumerable<UserMenuOptionRole> SelectAllUserMenuOptionRoles();

        bool SaveUserMenuOptionRoleForm(UserMenuOptionRole UserMenuOptionRoleRepository);

        bool DeleteUserMenuOptionRoleForm(int UserMenuOptionRoleId);

        Task<UserMenuOptionRole> SelectByUserMenuOptionRoleIdAsync(int UserMenuOptionRoleId);

        Task<IEnumerable<UserMenuOptionRole>> SelectMany_UserMenuOptionRoleAsync(Expression<Func<UserMenuOptionRole, bool>> where);

        Task<UserMenuOptionRole> SelectSingle_UserMenuOptionRoleAsync(Expression<Func<UserMenuOptionRole, bool>> where);

        Task<IEnumerable<UserMenuOptionRole>> SelectAllUserMenuOptionRolesAsync();

        Task<int> SaveUserMenuOptionRoleFormAsync(UserMenuOptionRole UserMenuOptionRoleRepository);

        Task<int> DeleteUserMenuOptionRoleFormAsync(int UserMenuOptionRoleId);
        bool SaveRoleFormWithUserMenuOptionRole(UserMenuOptionRole userMenuOptionRole, IEnumerable<int> aspNetRoleIds);
        #endregion

        #region UserDashboardOptionRole

        UserDashboardOptionRole SelectByUserDashboardOptionRoleId(int UserDashboardOptionRoleId, bool cacheRecord = false);

        IEnumerable<UserDashboardOptionRole> SelectMany_UserDashboardOptionRole(Expression<Func<UserDashboardOptionRole, bool>> where);

        UserDashboardOptionRole SelectSingle_UserDashboardOptionRole(Expression<Func<UserDashboardOptionRole, bool>> where);

        IEnumerable<UserDashboardOptionRole> SelectAllUserDashboardOptionRoles();

        bool SaveUserDashboardOptionRoleForm(UserDashboardOptionRole UserDashboardOptionRoleRepository);

        bool DeleteUserDashboardOptionRoleForm(int UserDashboardOptionRoleId);

        Task<UserDashboardOptionRole> SelectByUserDashboardOptionRoleIdAsync(int UserDashboardOptionRoleId);

        Task<IEnumerable<UserDashboardOptionRole>> SelectMany_UserDashboardOptionRoleAsync(Expression<Func<UserDashboardOptionRole, bool>> where);

        Task<UserDashboardOptionRole> SelectSingle_UserDashboardOptionRoleAsync(Expression<Func<UserDashboardOptionRole, bool>> where);

        Task<IEnumerable<UserDashboardOptionRole>> SelectAllUserDashboardOptionRolesAsync();

        Task<int> SaveUserDashboardOptionRoleFormAsync(UserDashboardOptionRole UserDashboardOptionRoleRepository);

        Task<int> DeleteUserDashboardOptionRoleFormAsync(int UserDashboardOptionRoleId);
        bool SaveRoleFormWithUserDashboardOptionRole(UserDashboardOptionRole userMenuOptionRole, IEnumerable<int> aspNetRoleIds);
        #endregion

        #region vw_UserMenuOptionRoleGrid
        List<vw_UserMenuOptionRoleGrid> Selectvw_UserMenuOptionRoleGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserMenuOptionRoleGrid> SelectAllvw_UserMenuOptionRoleGrids();
        Task<IEnumerable<vw_UserMenuOptionRoleGrid>> SelectAllvw_UserMenuOptionRoleGridsAsync();
        Task<List<vw_UserMenuOptionRoleGrid>> Selectvw_UserMenuOptionRoleGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserMenuOptionRoleGrid> SelectMany_vw_UserMenuOptionRoleGrid(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where);
        Task<IEnumerable<vw_UserMenuOptionRoleGrid>> SelectMany_vw_UserMenuOptionRoleGridAsync(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where);
        vw_UserMenuOptionRoleGrid SelectSingle_vw_UserMenuOptionRoleGrid(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where);
        Task<vw_UserMenuOptionRoleGrid> SelectSingle_vw_UserMenuOptionRoleGridAsync(Expression<Func<vw_UserMenuOptionRoleGrid, bool>> where);

        #endregion

        #region vw_UserDashboardOptionRoleGrid
        List<vw_UserDashboardOptionRoleGrid> Selectvw_UserDashboardOptionRoleGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserDashboardOptionRoleGrid> SelectAllvw_UserDashboardOptionRoleGrids();
        Task<IEnumerable<vw_UserDashboardOptionRoleGrid>> SelectAllvw_UserDashboardOptionRoleGridsAsync();
        Task<List<vw_UserDashboardOptionRoleGrid>> Selectvw_UserDashboardOptionRoleGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserDashboardOptionRoleGrid> SelectMany_vw_UserDashboardOptionRoleGrid(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where);
        Task<IEnumerable<vw_UserDashboardOptionRoleGrid>> SelectMany_vw_UserDashboardOptionRoleGridAsync(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where);
        vw_UserDashboardOptionRoleGrid SelectSingle_vw_UserDashboardOptionRoleGrid(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where);
        Task<vw_UserDashboardOptionRoleGrid> SelectSingle_vw_UserDashboardOptionRoleGridAsync(Expression<Func<vw_UserDashboardOptionRoleGrid, bool>> where);

        #endregion

        #region vw_RoleGrid
        List<vw_RoleGrid> Selectvw_RoleGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_RoleGrid> SelectAllvw_RoleGrids();
        Task<IEnumerable<vw_RoleGrid>> SelectAllvw_RoleGridsAsync();
        Task<List<vw_RoleGrid>> Selectvw_RoleGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_RoleGrid> SelectMany_vw_RoleGrid(Expression<Func<vw_RoleGrid, bool>> where);
        Task<IEnumerable<vw_RoleGrid>> SelectMany_vw_RoleGridAsync(Expression<Func<vw_RoleGrid, bool>> where);
        vw_RoleGrid SelectSingle_vw_RoleGrid(Expression<Func<vw_RoleGrid, bool>> where);
        Task<vw_RoleGrid> SelectSingle_vw_RoleGridAsync(Expression<Func<vw_RoleGrid, bool>> where);

        #endregion

        #region vw_LookupUserAssignedRoles
        List<vw_LookupUserAssignedRoles> Selectvw_LookupUserAssignedRolessByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUserAssignedRoles> SelectAllvw_LookupUserAssignedRoless();
        Task<IEnumerable<vw_LookupUserAssignedRoles>> SelectAllvw_LookupUserAssignedRolessAsync();
        Task<List<vw_LookupUserAssignedRoles>> Selectvw_LookupUserAssignedRolessByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUserAssignedRoles> SelectMany_vw_LookupUserAssignedRoles(Expression<Func<vw_LookupUserAssignedRoles, bool>> where);
        Task<IEnumerable<vw_LookupUserAssignedRoles>> SelectMany_vw_LookupUserAssignedRolesAsync(Expression<Func<vw_LookupUserAssignedRoles, bool>> where);
        vw_LookupUserAssignedRoles SelectSingle_vw_LookupUserAssignedRoles(Expression<Func<vw_LookupUserAssignedRoles, bool>> where);
        Task<vw_LookupUserAssignedRoles> SelectSingle_vw_LookupUserAssignedRolesAsync(Expression<Func<vw_LookupUserAssignedRoles, bool>> where);

        #endregion

        #region vw_LookupUserRoles
        List<vw_LookupUserRoles> Selectvw_LookupUserRolessByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_LookupUserRoles> SelectAllvw_LookupUserRoless();
        Task<IEnumerable<vw_LookupUserRoles>> SelectAllvw_LookupUserRolessAsync();
        Task<List<vw_LookupUserRoles>> Selectvw_LookupUserRolessByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_LookupUserRoles> SelectMany_vw_LookupUserRoles(Expression<Func<vw_LookupUserRoles, bool>> where);
        Task<IEnumerable<vw_LookupUserRoles>> SelectMany_vw_LookupUserRolesAsync(Expression<Func<vw_LookupUserRoles, bool>> where);
        vw_LookupUserRoles SelectSingle_vw_LookupUserRoles(Expression<Func<vw_LookupUserRoles, bool>> where);
        Task<vw_LookupUserRoles> SelectSingle_vw_LookupUserRolesAsync(Expression<Func<vw_LookupUserRoles, bool>> where);

        #endregion

        #region vw_UserMenuOption
        List<vw_UserMenuOption> Selectvw_UserMenuOptionsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserMenuOption> SelectAllvw_UserMenuOptions();
        Task<IEnumerable<vw_UserMenuOption>> SelectAllvw_UserMenuOptionsAsync();
        Task<List<vw_UserMenuOption>> Selectvw_UserMenuOptionsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserMenuOption> SelectMany_vw_UserMenuOption(Expression<Func<vw_UserMenuOption, bool>> where);
        Task<IEnumerable<vw_UserMenuOption>> SelectMany_vw_UserMenuOptionAsync(Expression<Func<vw_UserMenuOption, bool>> where);
        vw_UserMenuOption SelectSingle_vw_UserMenuOption(Expression<Func<vw_UserMenuOption, bool>> where);
        Task<vw_UserMenuOption> SelectSingle_vw_UserMenuOptionAsync(Expression<Func<vw_UserMenuOption, bool>> where);

        #endregion

        #region vw_UserDashboardOption
        List<vw_UserDashboardOption> Selectvw_UserDashboardOptionsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserDashboardOption> SelectAllvw_UserDashboardOptions();
        Task<IEnumerable<vw_UserDashboardOption>> SelectAllvw_UserDashboardOptionsAsync();
        Task<List<vw_UserDashboardOption>> Selectvw_UserDashboardOptionsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserDashboardOption> SelectMany_vw_UserDashboardOption(Expression<Func<vw_UserDashboardOption, bool>> where);
        Task<IEnumerable<vw_UserDashboardOption>> SelectMany_vw_UserDashboardOptionAsync(Expression<Func<vw_UserDashboardOption, bool>> where);
        vw_UserDashboardOption SelectSingle_vw_UserDashboardOption(Expression<Func<vw_UserDashboardOption, bool>> where);
        Task<vw_UserDashboardOption> SelectSingle_vw_UserDashboardOptionAsync(Expression<Func<vw_UserDashboardOption, bool>> where);

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

        #region vw_UserGroupGrid
        List<vw_UserGroupGrid> Selectvw_UserGroupGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserGroupGrid> SelectAllvw_UserGroupGrids();
        Task<IEnumerable<vw_UserGroupGrid>> SelectAllvw_UserGroupGridsAsync();
        Task<List<vw_UserGroupGrid>> Selectvw_UserGroupGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserGroupGrid> SelectMany_vw_UserGroupGrid(Expression<Func<vw_UserGroupGrid, bool>> where);
        Task<IEnumerable<vw_UserGroupGrid>> SelectMany_vw_UserGroupGridAsync(Expression<Func<vw_UserGroupGrid, bool>> where);
        vw_UserGroupGrid SelectSingle_vw_UserGroupGrid(Expression<Func<vw_UserGroupGrid, bool>> where);
        Task<vw_UserGroupGrid> SelectSingle_vw_UserGroupGridAsync(Expression<Func<vw_UserGroupGrid, bool>> where);

        #endregion

        #region vw_UserGroupObjectSubGrid
        List<vw_UserGroupObjectSubGrid> Selectvw_UserGroupObjectSubGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserGroupObjectSubGrid> SelectAllvw_UserGroupObjectSubGrids();
        Task<IEnumerable<vw_UserGroupObjectSubGrid>> SelectAllvw_UserGroupObjectSubGridsAsync();
        Task<List<vw_UserGroupObjectSubGrid>> Selectvw_UserGroupObjectSubGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserGroupObjectSubGrid> SelectMany_vw_UserGroupObjectSubGrid(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where);
        Task<IEnumerable<vw_UserGroupObjectSubGrid>> SelectMany_vw_UserGroupObjectSubGridAsync(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where);
        vw_UserGroupObjectSubGrid SelectSingle_vw_UserGroupObjectSubGrid(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where);
        Task<vw_UserGroupObjectSubGrid> SelectSingle_vw_UserGroupObjectSubGridAsync(Expression<Func<vw_UserGroupObjectSubGrid, bool>> where);

        #endregion

        #region vw_UserGroupDashboardObjectSubGrid
        List<vw_UserGroupDashboardObjectSubGrid> Selectvw_UserGroupDashboardObjectSubGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_UserGroupDashboardObjectSubGrid> SelectAllvw_UserGroupDashboardObjectSubGrids();
        Task<IEnumerable<vw_UserGroupDashboardObjectSubGrid>> SelectAllvw_UserGroupDashboardObjectSubGridsAsync();
        Task<List<vw_UserGroupDashboardObjectSubGrid>> Selectvw_UserGroupDashboardObjectSubGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_UserGroupDashboardObjectSubGrid> SelectMany_vw_UserGroupDashboardObjectSubGrid(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where);
        Task<IEnumerable<vw_UserGroupDashboardObjectSubGrid>> SelectMany_vw_UserGroupDashboardObjectSubGridAsync(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where);
        vw_UserGroupDashboardObjectSubGrid SelectSingle_vw_UserGroupDashboardObjectSubGrid(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where);
        Task<vw_UserGroupDashboardObjectSubGrid> SelectSingle_vw_UserGroupDashboardObjectSubGridAsync(Expression<Func<vw_UserGroupDashboardObjectSubGrid, bool>> where);

        #endregion

        #region aspNetUserLogs

        AspNetUserLogs SelectByAspNetUserLogsId(int aspNetUserLogsKey, bool cacheRecord = false);

        IEnumerable<AspNetUserLogs> SelectMany_AspNetUserLogs(Expression<Func<AspNetUserLogs, bool>> where);

        AspNetUserLogs SelectSingle_AspNetUserLogs(Expression<Func<AspNetUserLogs, bool>> where);

        IEnumerable<AspNetUserLogs> SelectAllAspNetUserLogss();

        bool SaveAspNetUserLogsForm(AspNetUserLogs aspNetUserLogs);

        bool DeleteAspNetUserLogsForm(int aspNetUserLogsKey);

        Task<AspNetUserLogs> SelectByAspNetUserLogsIdAsync(int aspNetUserLogsId);

        Task<IEnumerable<AspNetUserLogs>> SelectMany_AspNetUserLogsAsync(Expression<Func<AspNetUserLogs, bool>> where);

        Task<AspNetUserLogs> SelectSingle_AspNetUserLogsAsync(Expression<Func<AspNetUserLogs, bool>> where);

        Task<IEnumerable<AspNetUserLogs>> SelectAllAspNetUserLogssAsync();

        Task<int> SaveAspNetUserLogsFormAsync(AspNetUserLogs aspNetUserLogs);

        Task<int> DeleteAspNetUserLogsFormAsync(int aspNetUserLogsKey);

        #endregion

        #region vw_AspNetUserLogsGrid
        List<vw_AspNetUserLogsGrid> Selectvw_AspNetUserLogsGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_AspNetUserLogsGrid> SelectAllvw_AspNetUserLogsGrids();
        Task<IEnumerable<vw_AspNetUserLogsGrid>> SelectAllvw_AspNetUserLogsGridsAsync();
        Task<List<vw_AspNetUserLogsGrid>> Selectvw_AspNetUserLogsGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_AspNetUserLogsGrid> SelectMany_vw_AspNetUserLogsGrid(Expression<Func<vw_AspNetUserLogsGrid, bool>> where);
        Task<IEnumerable<vw_AspNetUserLogsGrid>> SelectMany_vw_AspNetUserLogsGridAsync(Expression<Func<vw_AspNetUserLogsGrid, bool>> where);
        vw_AspNetUserLogsGrid SelectSingle_vw_AspNetUserLogsGrid(Expression<Func<vw_AspNetUserLogsGrid, bool>> where);
        Task<vw_AspNetUserLogsGrid> SelectSingle_vw_AspNetUserLogsGridAsync(Expression<Func<vw_AspNetUserLogsGrid, bool>> where);

        #endregion

        #region vw_AspNetUserAccountLogs
        List<vw_AspNetUserAccountLogs> Selectvw_AspNetUserAccountLogssByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_AspNetUserAccountLogs> SelectAllvw_AspNetUserAccountLogss();
        Task<IEnumerable<vw_AspNetUserAccountLogs>> SelectAllvw_AspNetUserAccountLogssAsync();
        Task<List<vw_AspNetUserAccountLogs>> Selectvw_AspNetUserAccountLogssByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_AspNetUserAccountLogs> SelectMany_vw_AspNetUserAccountLogs(Expression<Func<vw_AspNetUserAccountLogs, bool>> where);
        Task<IEnumerable<vw_AspNetUserAccountLogs>> SelectMany_vw_AspNetUserAccountLogsAsync(Expression<Func<vw_AspNetUserAccountLogs, bool>> where);
        vw_AspNetUserAccountLogs SelectSingle_vw_AspNetUserAccountLogs(Expression<Func<vw_AspNetUserAccountLogs, bool>> where);
        Task<vw_AspNetUserAccountLogs> SelectSingle_vw_AspNetUserAccountLogsAsync(Expression<Func<vw_AspNetUserAccountLogs, bool>> where);

        #endregion

        #region UserAccounts

        UserAccounts SelectByUserAccountsId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<UserAccounts> SelectMany_UserAccounts(Expression<Func<UserAccounts, bool>> where);

        UserAccounts SelectSingle_UserAccounts(Expression<Func<UserAccounts, bool>> where);

        IEnumerable<UserAccounts> SelectAllUserAccountss();

        bool SaveUserAccountsForm(UserAccounts lookupTypeRepository);

        bool DeleteUserAccountsForm(int lookupTypeId);

        Task<UserAccounts> SelectByUserAccountsIdAsync(int lookupTypeId);

        Task<IEnumerable<UserAccounts>> SelectMany_UserAccountsAsync(Expression<Func<UserAccounts, bool>> where);

        Task<UserAccounts> SelectSingle_UserAccountsAsync(Expression<Func<UserAccounts, bool>> where);

        Task<IEnumerable<UserAccounts>> SelectAllUserAccountssAsync();

        Task<int> SaveUserAccountsFormAsync(UserAccounts lookupTypeRepository);

        Task<int> DeleteUserAccountsFormAsync(int lookupTypeId);
        #endregion

        #region UserClaims

        UserClaims SelectByUserClaimsId(int lookupTypeId, bool cacheRecord = false);

        IEnumerable<UserClaims> SelectMany_UserClaims(Expression<Func<UserClaims, bool>> where);

        UserClaims SelectSingle_UserClaims(Expression<Func<UserClaims, bool>> where);

        IEnumerable<UserClaims> SelectAllUserClaimss();

        bool SaveUserClaimsForm(UserClaims lookupTypeRepository);

        bool DeleteUserClaimsForm(int lookupTypeId);

        Task<UserClaims> SelectByUserClaimsIdAsync(int lookupTypeId);

        Task<IEnumerable<UserClaims>> SelectMany_UserClaimsAsync(Expression<Func<UserClaims, bool>> where);

        Task<UserClaims> SelectSingle_UserClaimsAsync(Expression<Func<UserClaims, bool>> where);

        Task<IEnumerable<UserClaims>> SelectAllUserClaimssAsync();

        Task<int> SaveUserClaimsFormAsync(UserClaims lookupTypeRepository);

        Task<int> DeleteUserClaimsFormAsync(int lookupTypeId);
        #endregion

        #region vw_InvalidEmailLogGrid
        List<vw_InvalidEmailLogGrid> Selectvw_InvalidEmailLogGridsByGridSetting(GridSetting gridSetting);
        IEnumerable<vw_InvalidEmailLogGrid> SelectAllvw_InvalidEmailLogGrids();
        Task<IEnumerable<vw_InvalidEmailLogGrid>> SelectAllvw_InvalidEmailLogGridsAsync();
        Task<List<vw_InvalidEmailLogGrid>> Selectvw_InvalidEmailLogGridsByGridSettingAsync(GridSetting gridSetting);
        IEnumerable<vw_InvalidEmailLogGrid> SelectMany_vw_InvalidEmailLogGrid(Expression<Func<vw_InvalidEmailLogGrid, bool>> where);
        Task<IEnumerable<vw_InvalidEmailLogGrid>> SelectMany_vw_InvalidEmailLogGridAsync(Expression<Func<vw_InvalidEmailLogGrid, bool>> where);
        vw_InvalidEmailLogGrid SelectSingle_vw_InvalidEmailLogGrid(Expression<Func<vw_InvalidEmailLogGrid, bool>> where);
        Task<vw_InvalidEmailLogGrid> SelectSingle_vw_InvalidEmailLogGridAsync(Expression<Func<vw_InvalidEmailLogGrid, bool>> where);

        #endregion

        #region AspNetUsersInvalid

        AspNetUsersInvalid SelectByAspNetUsersInvalidId(int id, bool cacheRecord = false);

        IEnumerable<AspNetUsersInvalid> SelectMany_AspNetUsersInvalid(Expression<Func<AspNetUsersInvalid, bool>> where);

        AspNetUsersInvalid SelectSingle_AspNetUsersInvalid(Expression<Func<AspNetUsersInvalid, bool>> where);

        IEnumerable<AspNetUsersInvalid> SelectAllAspNetUsersInvalids();

        bool SaveAspNetUsersInvalidForm(AspNetUsersInvalid aspNetUsersInvalid);

        bool DeleteAspNetUsersInvalidForm(int id);

        Task<AspNetUsersInvalid> SelectByAspNetUsersInvalidIdAsync(int id);

        Task<IEnumerable<AspNetUsersInvalid>> SelectMany_AspNetUsersInvalidAsync(Expression<Func<AspNetUsersInvalid, bool>> where);

        Task<AspNetUsersInvalid> SelectSingle_AspNetUsersInvalidAsync(Expression<Func<AspNetUsersInvalid, bool>> where);

        Task<IEnumerable<AspNetUsersInvalid>> SelectAllAspNetUsersInvalidsAsync();

        Task<int> SaveAspNetUsersInvalidFormAsync(AspNetUsersInvalid aspNetUsersInvalid);

        Task<int> DeleteAspNetUsersInvalidFormAsync(int id);
        #endregion
    }
}

