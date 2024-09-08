import { Routes } from '@angular/router';
//component
import { EmailTemplateGridComponent } from './email-template-grid/email-template-grid.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { EmailTemplateFormComponent } from './email-template-form/email-template-form.component';

export const EmailTempalteRoutes: Routes = [
  {
    path: '',
    component: EmailTemplateGridComponent
  },
  {
    path: 'add-email-template',
    component: EmailTemplateFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
    path: 'edit-email-template',
    component: EmailTemplateFormComponent,
    canDeactivate: [CanDeactivateGuard]
  }
];
export class EmailTemplateRoutingModule { }
