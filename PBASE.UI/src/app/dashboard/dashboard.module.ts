import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ChartsModule } from 'ng2-charts';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
//component
import { DashboardComponent } from './dashboard.component';
//custom
import { DashboardsRoutes } from './dashboard.routing';  

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forChild(DashboardsRoutes),
    NgbModule,
    PerfectScrollbarModule,
    ChartsModule,
    NgxChartsModule
  ],
  declarations: [
    DashboardComponent
  ]
})
export class DashboardModule {}
