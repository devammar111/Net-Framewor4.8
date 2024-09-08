import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgSelectModule } from '@ng-select/ng-select';
//module
import { SharedModule } from '../../shared/shared.module';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
//component
import { TemplateFormComponent } from './template-form/template-form.component';
import { TemplateGridComponent } from './template-grid/template-grid.component';
//route
import { TemplateRoutes } from './template.routing';
//custom
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(TemplateRoutes),
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
    })
  ],
  declarations: [
    TemplateGridComponent,
    TemplateFormComponent
  ],
  providers: [
    AgGridSource
  ]
})
export class TemplateModule {}
