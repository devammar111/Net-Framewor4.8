import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { LaddaModule } from 'angular2-ladda';
import { NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClickOutsideModule } from 'ng-click-outside';
//import { AutosizeModule } from 'ngx-autosize';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgSelectModule } from '@ng-select/ng-select';
//module
import { SharedModule } from '../shared/shared.module';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
//component
import { TestFormComponent } from './test-form/test-form.component';
import { TestGridComponent } from './test-grid/test-grid.component';
//route
import { TestRoutes } from './test.routing';
//custom
import { AgGridSource } from '../shared/ag-grid/ag-grid-source';
import { PBSwitchModule } from '../shared/pb-switch/pb-switch.module';
import { FormTabsComponent } from './form-tabs/form-tabs.component';;
import { TestSubGridComponent } from './test-sub-grid/test-sub-grid.component';
import { NoteTestGridComponent } from './note-test-grid/note-test-grid.component';
import { TestSubFormComponent } from './test-sub-form/test-sub-form.component';
import { NoteTestFormComponent } from './note-test-form/note-test-form.component'
import { TestSubTabOneGridComponent } from './test-form/test-sub-tab-one-grid/test-sub-tab-one-grid.component';
import { TestSubTabOneFormComponent } from './test-form/test-sub-tab-one-form/test-sub-tab-one-form.component';


@NgModule({
  imports: [CommonModule, RouterModule.forChild(TestRoutes),
      FormsModule,
      ReactiveFormsModule,
      ConfirmationPopoverModule.forRoot(),
    HttpModule,
      SharedModule,
      ClickOutsideModule,
      NgSelectModule,
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
      PBSwitchModule,
      //AutosizeModule

  ],
  declarations: [
      TestGridComponent,
      TestFormComponent,
      FormTabsComponent,
      TestSubGridComponent,
      NoteTestGridComponent ,
      TestSubFormComponent ,
      NoteTestFormComponent,
      TestSubTabOneGridComponent,
     TestSubTabOneFormComponent
   ],
  providers: [
      NgbDatepickerConfig,
    AgGridSource
  ]
})
export class TestModule {}
