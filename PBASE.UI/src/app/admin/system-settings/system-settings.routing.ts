import { Routes } from '@angular/router';
import { SystemSettingsTabsComponent } from './system-settings-tabs/system-settings-tabs.component';
import { OptionsGridComponent } from './options-grid/options-grid.component';
import { DashboardGridComponent } from './dashboard-grid/dashboard-grid.component';
import { OptionsFormComponent } from './options-form/options-form.component';
import { DashboardFormComponent } from './dashboard-form/dashboard-form.component';
import { GroupsFormComponent } from './groups-form/groups-form.component';
import { GroupsGridComponent } from './groups-grid/groups-grid.component';
import { CanDeactivateGuard, AuthguardService, RedirectSystemSettingGuard } from '../../shared/guard/auth.guard';


export const SystemSettingsRoutes: Routes = [
  {
    path: '',
    component: SystemSettingsTabsComponent,
    children: [
      { path: '', redirectTo: '', pathMatch: 'full', canActivate: [RedirectSystemSettingGuard], data: { access: [-9980, -9947] } },
      {
        path: 'options',
        component: OptionsGridComponent,
        canActivate: [AuthguardService],
        data: { access: [-9980] },
      },
      {
        path: 'options/edit-options',
        component: OptionsFormComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [AuthguardService],
        data: { access: [-9980] },
      },
      {
        path: 'dashboard',
        component: DashboardGridComponent,
        canActivate: [AuthguardService],
        data: { access: [-9947] },
      },
      {
        path: 'dashboard/edit-dashboard',
        component: DashboardFormComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [AuthguardService],
        data: { access: [-9947] },
      },
      {
        path: 'groups',
        component: GroupsGridComponent
      },
      {
        path: 'groups/edit-groups',
        component: GroupsFormComponent,
        canDeactivate: [CanDeactivateGuard]
      },
      {
        path: 'groups/add-groups',
        component: GroupsFormComponent,
        canDeactivate: [CanDeactivateGuard]
      }
    ]
  }
];
