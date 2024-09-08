import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { LaddaModule } from 'angular2-ladda';
import { NgxLoadingModule, ngxLoadingAnimationTypes } from 'ngx-loading';
import { NgSelectModule } from '@ng-select/ng-select';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
//module
import { SharedModule } from '../shared/shared.module';
import { ClickOutsideModule } from 'ng-click-outside';
import { DashboardTCComponent } from './dashboard-tc.component';
import { DashboardTCRoutes } from './dashboard-tc.routing';

@NgModule({
    imports: [FormsModule, CommonModule, RouterModule.forChild(DashboardTCRoutes),
        HttpModule,
        ConfirmationPopoverModule.forRoot(),
        SharedModule,
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
    declarations: [DashboardTCComponent],
})

export class DashboardTCModule {}
