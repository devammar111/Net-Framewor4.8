import { Routes } from '@angular/router';
//component
import { UserFormComponent } from './user-form/user-form.component';
import { UserGridComponent } from './user-grid/user-grid.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';

export const UserRoutes: Routes = [
  {
    path: '',
    component: UserGridComponent
  },
  {
    path: 'edit-user',
    component: UserFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'add-user',
    component: UserFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
