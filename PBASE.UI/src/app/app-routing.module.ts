import { Routes } from '@angular/router';
import { NgxPermissionsGuard } from 'ngx-permissions';
//component
import { FullComponent } from './layouts/full/full.component';
import { BlankComponent } from './layouts/blank/blank.component';
//custom
import { AuthGuard } from './shared/guard/auth.guard';

export const Approutes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: 'dashboard',
        loadChildren: './dashboard/dashboard.module#DashboardModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9990',
            redirectTo: 'error/404'
          },
        }
      },
      {
          path: 'dashboard-tc',
          loadChildren: './dashboard-tc/dashboard-tc.module#DashboardTCModule',
          canLoad: [AuthGuard, NgxPermissionsGuard],
          data: {
              permissions: {
                  only: '0',
                  redirectTo: 'error/404'
              },
          }
      },
      {
          path: 'tests',
          loadChildren: './test/test.module#TestModule',
          canLoad: [AuthGuard, NgxPermissionsGuard],
          data: {
              permissions: {
                  only: '-9962',
                  redirectTo: 'error/404'
              },
          }
      },
      {
        path: 'users',
        loadChildren: './admin/users/user.module#UserModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9987',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'templates',
        loadChildren: './admin/template/template.module#TemplateModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9989',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'lookups',
        loadChildren: './admin/lookup/lookup.module#LookupModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9988',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'system-settings',
        loadChildren: './admin/system-settings/system-settings.module#SystemSettingsModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9985',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'login-analysis',
        loadChildren: './admin/login-analysis/login-analysis.module#LoginAnalysisModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9986',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'profile',
        loadChildren: './admin/profile/profile.module#ProfileModule',
        canLoad: [AuthGuard]
      },
      {
        path: 'export-log',
        loadChildren: './admin/export-log/export-log.module#ExportLogModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9984',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'email',
        loadChildren: './admin/email/email.module#EmailModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9981',
            redirectTo: 'error/404'
          },
        }
      },

      {
        path: 'email-template',
        loadChildren: './admin/email-template/email-template.module#EmailTemplateModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9982',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'agreement',
        loadChildren: './admin/agreement/agreement.module#AgreementModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9978',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'system-alerts',
        loadChildren: './admin/system-alerts/system-alerts.module#SystemAlertsModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9979',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'system-administration',
        loadChildren: './admin/system-administration/system-administration.module#SystemAdministrationModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '-9945',
            redirectTo: 'error/404'
          },
        }
      },
      {
        path: 'about',
        loadChildren: './admin/about/about.module#AboutModule',
        canLoad: [AuthGuard]
      },
      {
        path: 'invalid-email-log',
        loadChildren: './admin/invalid-email-log/invalid-email-log.module#InvalidEmailLogModule',
        canLoad: [AuthGuard, NgxPermissionsGuard],
        data: {
          permissions: {
            only: '1004',
            redirectTo: 'error/404'
          },
        }
      }
    ]
  },
  {
    path: '',
    component: BlankComponent,
    children: [
      {
        path: 'authentication',
        loadChildren: './authentication/authentication.module#AuthenticationModule'
      },
    ]
  },
  {
    path: '',
    component: BlankComponent,
    children: [
      {
        path: 'ip-info',
        loadChildren: './ip-info/ip-info.module#IpInfoModule'
      },
    ]
  },
  {
    path: '**',
    redirectTo: '/authentication/404'
  }
];
