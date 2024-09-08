import { Routes } from '@angular/router';
//component
import { LookupTypeGridComponent } from './lookup-type-grid/lookup-type-grid.component';
import { LookupGridComponent } from './lookup-grid/lookup-grid.component';
import { LookupFormComponent } from './lookup-form/lookup-form.component';
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';

export const LookupRoutes: Routes = [
  {
    path: '',
    component: LookupTypeGridComponent
  },
  {
    path: 'lookup-grid',
    component: LookupGridComponent
  },
  {
    path: 'lookup-grid/edit-lookup',
    component: LookupFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'lookup-grid/add-lookup',
    component: LookupFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
