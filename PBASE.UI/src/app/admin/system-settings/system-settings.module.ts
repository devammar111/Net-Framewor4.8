import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { LaddaModule } from 'angular2-ladda';
import { SharedModule } from '../../shared/shared.module';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { SystemSettingsRoutes } from './system-settings.routing';
//component
import { SystemSettingsTabsComponent } from './system-settings-tabs/system-settings-tabs.component';
import { OptionsGridComponent } from './options-grid/options-grid.component';
import { OptionsFormComponent } from './options-form/options-form.component';
import { DashboardGridComponent } from './dashboard-grid/dashboard-grid.component';
import { DashboardFormComponent } from './dashboard-form/dashboard-form.component';
import { GroupsGridComponent } from './groups-grid/groups-grid.component';
import { GroupsFormComponent } from './groups-form/groups-form.component';

import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { PBSwitchModule } from '../../shared/pb-switch/pb-switch.module';
import { UserGroupTableComponent } from './user-group-table/user-group-table.component';
import { UserGroupDashboardTableComponent } from './user-group-dashboard-table/user-group-dashboard-table.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  wheelSpeed: 2,
  wheelPropagation: true,
  minScrollbarLength: 20
};


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PerfectScrollbarModule,
    SharedModule,
    RouterModule.forChild(SystemSettingsRoutes),
    ConfirmationPopoverModule.forRoot(),
    NgSelectModule,
    LaddaModule.forRoot({
      style: "expand-left",
      spinnerSize: 30,
      spinnerColor: "red",
      spinnerLines: 12
    }),
    NgxLoadingModule.forRoot({
      animationType: ngxLoadingAnimationTypes.doubleBounce,
      backdropBorderRadius: '4px',
      primaryColour: '#d7e7db',
      secondaryColour: '#57a170'
    }),
    NgxDatatableModule,
    PBSwitchModule,
  ],
  declarations: [
    OptionsGridComponent,
    SystemSettingsTabsComponent,
    OptionsFormComponent,
    UserGroupTableComponent,
    UserGroupDashboardTableComponent,
    DashboardGridComponent,
    DashboardFormComponent,
    GroupsGridComponent,
    GroupsFormComponent
  ],
  providers: [
    AgGridSource,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
  ]
})

export class SystemSettingsModule { }
