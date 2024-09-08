import { Routes } from '@angular/router';
//component
import { TemplateGridComponent } from './template-grid/template-grid.component';
import { TemplateFormComponent } from './template-form/template-form.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';

export const TemplateRoutes: Routes = [
  {
    path: '',
    component: TemplateGridComponent
  },
  {
    path: 'add-template',
    component: TemplateFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'edit-template',
    component: TemplateFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
