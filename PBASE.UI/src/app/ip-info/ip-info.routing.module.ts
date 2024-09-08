import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { IpInfoComponent } from './ip-info.component';

export const IpInfoRoutes: Routes = [
  {
    path: '',
    component: IpInfoComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(IpInfoRoutes)],
  exports: [RouterModule]
})
export class IpInfoRoutingModule { } 
