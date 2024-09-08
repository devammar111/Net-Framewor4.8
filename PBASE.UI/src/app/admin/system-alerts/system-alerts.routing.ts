import { Routes } from '@angular/router';
//component
import { SystemAlertsListComponent } from './system-alerts-list/system-alerts-list.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { SystemAlertsFormComponent } from './system-alerts-form/system-alerts-form.component';


export const SystemAlertsRoutes: Routes = [
  {
    path: '',
    component: SystemAlertsListComponent
  },
  {
    path: 'edit-system-alerts',
    component: SystemAlertsFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'add-system-alerts',
    component: SystemAlertsFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
