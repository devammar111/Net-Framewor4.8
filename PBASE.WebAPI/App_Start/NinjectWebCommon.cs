[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PBASE.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PBASE.WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace PBASE.WebAPI.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using Service;
    using PBASE.Repository;
    using PBASE.Repository.Infrastructure;
    using Entity;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Infrastructure
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IBaseEntity>().To<BaseEntity>().InRequestScope();
            // Services
            kernel.Bind<IBaseService>().To<BaseService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IEmailService>().To<EmailService>().InRequestScope();
            kernel.Bind<ILookupService>().To<LookupService>().InRequestScope();
            kernel.Bind<IUserLogService>().To<UserLogService>().InRequestScope();
            kernel.Bind<IEmailTemplateService>().To<EmailTemplateService>().InRequestScope();
            kernel.Bind<IAgreementService>().To<AgreementService>().InRequestScope();
            kernel.Bind<ITestService>().To<TestService>().InRequestScope();
            kernel.Bind<ISystemAlertService>().To<SystemAlertService>().InRequestScope();

            // Repositories
            kernel.Bind<IUserDashboardOptionRoleRepository>().To<UserDashboardOptionRoleRepository>().InRequestScope();
            kernel.Bind<IAspNetUsersInvalidRepository>().To<AspNetUsersInvalidRepository>().InRequestScope();
            kernel.Bind<Ivw_InvalidEmailLogGridRepository>().To<vw_InvalidEmailLogGridRepository>().InRequestScope();
            kernel.Bind<Ivw_AgreementGridRepository>().To<vw_AgreementGridRepository>().InRequestScope();
            kernel.Bind<Ivw_AgreementVersionSubGridRepository>().To<vw_AgreementVersionSubGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupAlertTypeRepository>().To<vw_LookupAlertTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupAccessTypeRepository>().To<vw_LookupAccessTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupObjectRepository>().To<vw_LookupObjectRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupObjectTypeRepository>().To<vw_LookupObjectTypeRepository>().InRequestScope();
            kernel.Bind<ISystemAlertRepository>().To<SystemAlertRepository>().InRequestScope();
            kernel.Bind<Ivw_SystemAlertIsClosedRepository>().To<vw_SystemAlertIsClosedRepository>().InRequestScope();
            kernel.Bind<Ivw_SystemAlertMessagesRepository>().To<vw_SystemAlertMessagesRepository>().InRequestScope();
            kernel.Bind<Ivw_SystemAlertGridRepository>().To<vw_SystemAlertGridRepository>().InRequestScope();
            kernel.Bind<IAgreementRepository>().To<AgreementRepository>().InRequestScope();
            kernel.Bind<IAgreementUserTypeRepository>().To<AgreementUserTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_AgreementUserSubGridRepository>().To<vw_AgreementUserSubGridRepository>().InRequestScope();
            kernel.Bind<Ivw_RoleGridRepository>().To<vw_RoleGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUserAssignedRolesRepository>().To<vw_LookupUserAssignedRolesRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridAlertTypeRepository>().To<vw_LookupGridAlertTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUserRolesRepository>().To<vw_LookupUserRolesRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupFromEmailAddressRepository>().To<vw_LookupFromEmailAddressRepository>().InRequestScope();
            kernel.Bind<Ivw_UserMenuOptionRepository>().To<vw_UserMenuOptionRepository>().InRequestScope();
            kernel.Bind<Ivw_UserDashboardOptionRepository>().To<vw_UserDashboardOptionRepository>().InRequestScope();
            kernel.Bind<Ivw_UserMenuOptionRoleGridRepository>().To<vw_UserMenuOptionRoleGridRepository>().InRequestScope();
            kernel.Bind<Ivw_UserDashboardOptionRoleGridRepository>().To<vw_UserDashboardOptionRoleGridRepository>().InRequestScope();
            kernel.Bind<IUserMenuOptionRoleRepository>().To<UserMenuOptionRoleRepository>().InRequestScope();
            kernel.Bind<Ivw_UserGroupGridRepository>().To<vw_UserGroupGridRepository>().InRequestScope();
            kernel.Bind<Ivw_UserGroupObjectSubGridRepository>().To<vw_UserGroupObjectSubGridRepository>().InRequestScope();
            kernel.Bind<IUserGroupMenuOptionRepository>().To<UserGroupMenuOptionRepository>().InRequestScope();
            kernel.Bind<IUserGroupDashboardOptionRepository>().To<UserGroupDashboardOptionRepository>().InRequestScope();
            kernel.Bind<IUserGroupRepository>().To<UserGroupRepository>().InRequestScope();
            kernel.Bind<IEmailRepository>().To<EmailRepository>().InRequestScope();
            kernel.Bind<IApplicationInformationRepository>().To<ApplicationInformationRepository>().InRequestScope();
            kernel.Bind<IEmailAttachmentRepository>().To<EmailAttachmentRepository>().InRequestScope();
            kernel.Bind<IAspNetUserLogsRepository>().To<AspNetUserLogsRepository>().InRequestScope();
            kernel.Bind<Ivw_AspNetUserLogsGridRepository>().To<vw_AspNetUserLogsGridRepository>().InRequestScope();
            kernel.Bind<Ivw_EmailGridRepository>().To<vw_EmailGridRepository>().InRequestScope();
            kernel.Bind<Ivw_AspNetUserAccountLogsRepository>().To<vw_AspNetUserAccountLogsRepository>().InRequestScope();
            kernel.Bind<Ivw_UserGridRepository>().To<vw_UserGridRepository>().InRequestScope();
            kernel.Bind<IAttachmentRepository>().To<AttachmentRepository>().InRequestScope();
            kernel.Bind<IInternalFilterHeaderRepository>().To<InternalFilterHeaderRepository>().InRequestScope();
            kernel.Bind<IInternalGridSettingRepository>().To<InternalGridSettingRepository>().InRequestScope();
            kernel.Bind<IInternalReportRepository>().To<InternalReportRepository>().InRequestScope();
            kernel.Bind<IInternalReportFieldRepository>().To<InternalReportFieldRepository>().InRequestScope();
            kernel.Bind<IInterviewAppointmentRepository>().To<InterviewAppointmentRepository>().InRequestScope();
            kernel.Bind<ILookupRepository>().To<LookupRepository>().InRequestScope();
            kernel.Bind<ILookupTypeRepository>().To<LookupTypeRepository>().InRequestScope();
            kernel.Bind<IMessageRepository>().To<MessageRepository>().InRequestScope();
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>().InRequestScope();
            kernel.Bind<ISafeIPsRepository>().To<SafeIPsRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridRepository>().To<vw_LookupGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridUsersRepository>().To<vw_LookupGridUsersRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridRoleRepository>().To<vw_LookupGridRoleRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridUserGroupRepository>().To<vw_LookupGridUserGroupRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridUserAccessTypeRepository>().To<vw_LookupGridUserAccessTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridUserTypeRepository>().To<vw_LookupGridUserTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupRoleRepository>().To<vw_LookupRoleRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupTemplateTypeRepository>().To<vw_LookupTemplateTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupTypeGridRepository>().To<vw_LookupTypeGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupMenuOptionRepository>().To<vw_LookupMenuOptionRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupListRepository>().To<vw_LookupListRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupDashboardOptionRepository>().To<vw_LookupDashboardOptionRepository>().InRequestScope();
            kernel.Bind<IUserAccountsRepository>().To<UserAccountsRepository>().InRequestScope();
            kernel.Bind<IUserClaimsRepository>().To<UserClaimsRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUsersRepository>().To<vw_LookupUsersRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUserTypeRepository>().To<vw_LookupUserTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_TemplateGridRepository>().To<vw_TemplateGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUsersSignatureRepository>().To<vw_LookupUsersSignatureRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUserAccessTypeRepository>().To<vw_LookupUserAccessTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupMenuOptionGroupRepository>().To<vw_LookupMenuOptionGroupRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupDashboardOptionGroupRepository>().To<vw_LookupDashboardOptionGroupRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupUserGroupRepository>().To<vw_LookupUserGroupRepository>().InRequestScope();
            kernel.Bind<Ivw_ExportLogGridRepository>().To<vw_ExportLogGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupTemplateAllowedTypeRepository>().To<vw_LookupTemplateAllowedTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_EmailTemplateGridRepository>().To<vw_EmailTemplateGridRepository>().InRequestScope();
            kernel.Bind<IUserExportLogRepository>().To<UserExportLogRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridEmailTemplateTypeRepository>().To<vw_LookupGridEmailTemplateTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridEmailTypeRepository>().To<vw_LookupGridEmailTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridTemplateAllowedTypeRepository>().To<vw_LookupGridTemplateAllowedTypeRepository>().InRequestScope();
            kernel.Bind<IEmailTemplateRepository>().To<EmailTemplateRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupEmailTemplateTypeRepository>().To<vw_LookupEmailTemplateTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupEmailTypeRepository>().To<vw_LookupEmailTypeRepository>().InRequestScope();
            kernel.Bind<IEmailTemplateTagRepository>().To<EmailTemplateTagRepository>().InRequestScope();
            kernel.Bind<ITemplateTagRepository>().To<TemplateTagRepository>().InRequestScope();
            kernel.Bind<Ivw_UserAgreementSubGridRepository>().To<vw_UserAgreementSubGridRepository>().InRequestScope();
            kernel.Bind<Ivw_UserAgreementFormRepository>().To<vw_UserAgreementFormRepository>().InRequestScope();
            kernel.Bind<IUserAgreementRepository>().To<UserAgreementRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupTestTypeRepository>().To<vw_LookupTestTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_TestGridRepository>().To<vw_TestGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupGridTestTypeRepository>().To<vw_LookupGridTestTypeRepository>().InRequestScope();
            kernel.Bind<Ivw_TestSubGridRepository>().To<vw_TestSubGridRepository>().InRequestScope();
            kernel.Bind<Ivw_TestNoteGridRepository>().To<vw_TestNoteGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupDashboardObjectTypeRepository>().To<vw_LookupDashboardObjectTypeRepository>().InRequestScope();
            kernel.Bind<ITestRepository>().To<TestRepository>().InRequestScope();
            kernel.Bind<ITestNoteRepository>().To<TestNoteRepository>().InRequestScope();
            kernel.Bind<ITestNoteAttachmentRepository>().To<TestNoteAttachmentRepository>().InRequestScope();
            kernel.Bind<ITestSubRepository>().To<TestSubRepository>().InRequestScope();
            kernel.Bind<Ivw_UserGroupDashboardObjectSubGridRepository>().To<vw_UserGroupDashboardObjectSubGridRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupDashboardObjectRepository>().To<vw_LookupDashboardObjectRepository>().InRequestScope();
            kernel.Bind<Ivw_LookupSettingRepository>().To<vw_LookupSettingRepository>().InRequestScope();
            kernel.Bind<IInternalGridSettingDefaultRepository>().To<InternalGridSettingDefaultRepository>().InRequestScope();
            kernel.Bind<Ivw_AgreementPreviousVersionNumberRepository>().To<vw_AgreementPreviousVersionNumberRepository>().InRequestScope();
        }
    }
}
