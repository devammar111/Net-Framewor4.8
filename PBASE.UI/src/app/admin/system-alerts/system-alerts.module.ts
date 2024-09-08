import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
//module
import { SharedModule } from '../../shared/shared.module';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { ClickOutsideModule } from 'ng-click-outside';
import { OwlDateTimeModule, OwlNativeDateTimeModule, OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';
//route
import { SystemAlertsRoutes } from './system-alerts.routing';
//custom
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
//component
import { SystemAlertsListComponent } from './system-alerts-list/system-alerts-list.component';
import { SystemAlertsFormComponent } from './system-alerts-form/system-alerts-form.component';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(SystemAlertsRoutes),
    FormsModule,
    ConfirmationPopoverModule.forRoot(),
    HttpModule,
    SharedModule,
    NgSelectModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    NgxMaterialTimepickerModule,
    NgbModule,
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
    AngularEditorModule,
    ClickOutsideModule
  ],
  declarations: [
    SystemAlertsListComponent,
    SystemAlertsFormComponent
  ],
  providers: [
    AgGridSource,
    { provide: OWL_DATE_TIME_LOCALE, useValue: 'en-GB' },
  ]
})
export class SystemAlertsModule {}
