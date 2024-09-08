import { Routes } from '@angular/router';
//component

//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { AboutComponent } from './about/about.component';

export const AboutRoutes: Routes = [
  {
    path: '',
    component: AboutComponent,
  }
];
