import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LaddaModule } from 'angular2-ladda';
import { NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { ClickOutsideModule } from 'ng-click-outside';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source'
import { SharedModule } from '../../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
//service
import { LookupFormService } from './lookup-form/lookup-form.service';
//component
import { LookupFormComponent } from './lookup-form/lookup-form.component';
import { LookupGridComponent } from './lookup-grid/lookup-grid.component';
import { LookupTypeGridComponent } from './lookup-type-grid/lookup-type-grid.component';
import { LookupRoutes } from './lookup.routing';

@NgModule({
  imports: [CommonModule,
    FormsModule,
    ConfirmationPopoverModule.forRoot(),
    RouterModule.forChild(LookupRoutes),
    SharedModule,
    NgbModule.forRoot(),
    NgSelectModule,
    ReactiveFormsModule,
    ClickOutsideModule,
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
  ],
  declarations: [
    LookupGridComponent,
    LookupTypeGridComponent,
    LookupFormComponent
  ],
  providers: [
    LookupFormService,
    NgbDatepickerConfig,
    AgGridSource
  ]
})

export class LookupModule {}
