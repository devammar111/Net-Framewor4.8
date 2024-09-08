import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { LaddaModule } from 'angular2-ladda';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source'
import { SharedModule } from '../../shared/shared.module';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { PBSwitchModule } from '../../shared/pb-switch/pb-switch.module';

//import { DragulaModule } from 'ng2-dragula/ng2-dragula';
import { UserRoutes } from './user.routing';

//service
import { ChangePasswordService } from './change-password/change-password.service';
//component
import { UserFormComponent } from './user-form/user-form.component';
import { UserGridComponent } from './user-grid/user-grid.component';


@NgModule({
  imports: [CommonModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild(UserRoutes),
    //DragulaModule,
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
    UserFormComponent,
    UserGridComponent,

  ],
  providers: [
    ChangePasswordService,
    AgGridSource
  ]
})

export class UserModule {}
