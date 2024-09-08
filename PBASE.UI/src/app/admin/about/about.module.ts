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
import { ClickOutsideModule } from 'ng-click-outside';
//component
import { AboutComponent } from './about/about.component';
//route
import { AboutRoutes } from './about.routing';
//custom
import { AgGridSource } from '../../shared/ag-grid/ag-grid-source';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [CommonModule, RouterModule.forChild(AboutRoutes),
        FormsModule,
        ConfirmationPopoverModule.forRoot(),
        HttpModule,
        SharedModule,
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
      AngularEditorModule,
      ClickOutsideModule
    ],
    declarations: [
    AboutComponent
  ],
    providers: [
        AgGridSource,
    ]
})
export class AboutModule {}
