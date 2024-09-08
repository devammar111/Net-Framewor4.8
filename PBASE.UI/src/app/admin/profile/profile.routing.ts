import { Routes } from '@angular/router';
import { ProfileComponent } from './profile.component';
import { ChangePasswordComponent } from '../users/change-password/change-password.component';
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { AgreementModelComponent } from './agreement-pt2-grid/agreement-model/agreement-model.component';

export const ProfileRoutes: Routes = [
  {
  path: '',
    component: ProfileComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'change-password',
    component: ChangePasswordComponent,
    canDeactivate: [CanDeactivateGuard]
  },  
  {
    path: 'view-agreement',
    component: AgreementModelComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
