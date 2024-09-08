import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { IpInfoRoutingModule } from './ip-info.routing.module';
//component
import { IpInfoComponent } from './ip-info.component';

@NgModule({
  imports: [
    FormsModule,
    CommonModule,
    NgbModule,
    PerfectScrollbarModule,
    NgxJsonViewerModule,
    IpInfoRoutingModule
  ],
  declarations: [
    IpInfoComponent
  ]
})
export class IpInfoModule {}
