import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmailTemplateRoutingModule, EmailTempalteRoutes } from './email-template-routing.module';
import { EmailTemplateGridComponent } from './email-template-grid/email-template-grid.component';
import { FormsModule } from '@angular/forms';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { HttpModule } from '@angular/http';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { RouterModule } from '@angular/router';
import { EmailTemplateFormComponent } from './email-template-form/email-template-form.component';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';

import { AngularEditorModule } from '@kolkov/angular-editor';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(EmailTempalteRoutes),
    FormsModule,
    ConfirmationPopoverModule.forRoot(),
    HttpModule,
    SharedModule,
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

    AngularEditorModule,
  ],
  declarations: [EmailTemplateGridComponent, EmailTemplateFormComponent],
  providers: [
    AgGridSource
  ]
})
export class EmailTemplateModule { }
