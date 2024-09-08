import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LaddaModule } from 'angular2-ladda';
import { SharedModule } from '../../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';

//import { DragulaModule } from 'ng2-dragula/ng2-dragula';
//import { ToastModule } from 'ng2-toastr/ng2-toastr';

import { ChangePasswordComponent } from '../users/change-password/change-password.component';
import { ChangePasswordService } from '../users/change-password/change-password.service';
import { ProfileComponent } from './profile.component';
import { ProfileRoutes } from './profile.routing';
import { AgreementPt2GridComponent } from './agreement-pt2-grid/agreement-pt2-grid.component';
import { AgGridSource } from 'src/app/shared/ag-grid/ag-grid-source';
import { AgreementModelComponent } from './agreement-pt2-grid/agreement-model/agreement-model.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';


@NgModule({
  imports: [CommonModule,
    SharedModule,
    RouterModule.forChild(ProfileRoutes),
    //DragulaModule,
    NgbModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    //ToastModule.forRoot(),
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
  declarations: [ProfileComponent, ChangePasswordComponent, AgreementPt2GridComponent, AgreementModelComponent],
  providers: [ChangePasswordService, AgreementPt2GridComponent, AgGridSource],
  entryComponents: [

  ]
})

export class ProfileModule {}
