import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source'
import { SharedModule } from '../../shared/shared.module';
import { PBSwitchModule } from '../../shared/pb-switch/pb-switch.module';
import { InvalidEmailLogRoutes } from './invalid-email-log.routing';     
//component
import { InvalidEmailLogGridComponent } from './invalid-email-log-grid/invalid-email-log-grid.component';


@NgModule({
  imports: [CommonModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild(InvalidEmailLogRoutes),
    HttpModule,
    NgSelectModule,
    ReactiveFormsModule,
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
    PBSwitchModule
  ],
  declarations: [
    InvalidEmailLogGridComponent
  ],
  providers: [
    AgGridSource
  ]
})

export class InvalidEmailLogModule { }
