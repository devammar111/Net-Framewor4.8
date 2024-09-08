import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';

//import { DragulaModule } from 'ng2-dragula/ng2-dragula';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { SharedModule } from '../../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';

import { ExportLogRoutes } from './export-log.routing';

//component
import { ExportLogGridComponent } from './export-log-grid/export-log-grid.component';

@NgModule({
  imports: [CommonModule, RouterModule.forChild(ExportLogRoutes),
    FormsModule,
    ConfirmationPopoverModule.forRoot(),
    HttpModule,
    //DragulaModule,
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
  ],
  declarations: [
    ExportLogGridComponent,
  ],
  providers: [
    AgGridSource
  ]
})

export class ExportLogModule {}
