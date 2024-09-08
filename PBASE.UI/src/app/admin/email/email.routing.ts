import { Routes } from '@angular/router';
//component
import { EmailGridComponent } from './email-grid/email-grid.component';
import { EmailFormComponent } from './email-form/email-form.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';

export const EmailRoutes: Routes = [
  {
    path: '',
    component: EmailGridComponent
  },
  {
    path: 'edit-email',
    component: EmailFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
