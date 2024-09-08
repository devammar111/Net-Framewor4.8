import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LaddaModule } from 'angular2-ladda';
import { Ng2PaginationModule } from 'ng2-pagination'

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { SharedModule } from '../../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { LoginAnalysisRoutes } from './login-analysis.routing';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { ClickOutsideModule } from 'ng-click-outside';
//service
import { LoginAnalysisFormService } from './login-analysis-form/login-analysis-form.service';


//component
import { LoginAnalysisGridComponent } from './login-analysis-grid/login-analysis-grid.component';
import { LoginAnalysisDetailGridComponent } from './login-analysis-detail-grid/login-analysis-detail-grid.component';
import { LoginAnalysisFormComponent } from './login-analysis-form/login-analysis-form.component';

@NgModule({
    imports: [CommonModule,
        FormsModule,
        RouterModule.forChild(LoginAnalysisRoutes),
        SharedModule,
        ConfirmationPopoverModule.forRoot(),
        NgbModule.forRoot(),
        //DragulaModule,
        NgSelectModule,
        ClickOutsideModule,
        ReactiveFormsModule,
        Ng2PaginationModule,
       // ToastModule.forRoot(),
        LaddaModule.forRoot({
            style: "expand-left",
            spinnerSize: 30,
            spinnerColor: "red",
            spinnerLines: 12
        })
    ],
    declarations: [
        LoginAnalysisGridComponent,
      LoginAnalysisFormComponent,
      LoginAnalysisDetailGridComponent
        
    ],
    entryComponents: [LoginAnalysisFormComponent],
    providers: [
        AgGridSource,
      LoginAnalysisFormService,
      NgbDatepickerConfig
    ]
})

export class LoginAnalysisModule { }
