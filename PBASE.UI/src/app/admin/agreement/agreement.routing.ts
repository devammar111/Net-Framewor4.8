import { Routes } from '@angular/router';
//component
import { AgreementListComponent } from './agreement-list/agreement-list.component';
//custom
import { CanDeactivateGuard } from '../../shared/guard/auth.guard';
import { AgreementFormComponent } from './agreement-form/agreement-form.component';
import { AgreementVersionFormComponent } from './agreement-form/agreement-version-form/agreement-version-form.component';


export const AgreementRoutes: Routes = [
  {
    path: '',
    component: AgreementListComponent
  },
  {
    path: 'add-agreement',
    component: AgreementFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
     path: 'edit-agreement',
     children: [
        {
           path: '',
           component: AgreementFormComponent,
           canDeactivate: [CanDeactivateGuard]
        },
        {
           path: 'add-agreement-version',
           component: AgreementVersionFormComponent,
           canDeactivate: [CanDeactivateGuard]
        },
        {
           path: 'edit-agreement-version',
           component: AgreementVersionFormComponent,
           canDeactivate: [CanDeactivateGuard]
        },
    ]
  }
];
