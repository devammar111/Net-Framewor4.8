import { Routes } from '@angular/router';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { SystemAdministrationFormComponent } from './system-administration-form/system-administration-form.component';


export const SystemAdministrationRoutes: Routes = [
  {
    path: '',
    component: SystemAdministrationFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
