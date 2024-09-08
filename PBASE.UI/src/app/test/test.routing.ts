import { Routes } from '@angular/router';
//component
import { TestGridComponent } from './test-grid/test-grid.component';
import { TestFormComponent } from './test-form/test-form.component';
import { FormTabsComponent } from './form-tabs/form-tabs.component';
import { TestSubGridComponent } from './test-sub-grid/test-sub-grid.component'
    ;
import { NoteTestGridComponent } from './note-test-grid/note-test-grid.component'
import { TestSubFormComponent } from './test-sub-form/test-sub-form.component'
    ;
import { NoteTestFormComponent } from './note-test-form/note-test-form.component'
//custom
import { CanDeactivateGuard } from '../shared/guard/auth.guard';
import { TestSubTabOneFormComponent } from './test-form/test-sub-tab-one-form/test-sub-tab-one-form.component';

export const TestRoutes: Routes = [
  {
    path: '',
    component: TestGridComponent
  },
  {
    path: 'add-test',
    component: TestFormComponent,
    canDeactivate: [CanDeactivateGuard]
  },
  {
      path: 'tab',
      component: FormTabsComponent,
      children: [
          {
              path: 'edit-test',
              children: [
                  {
                      path: '',
                      component: TestFormComponent,
                      canDeactivate: [CanDeactivateGuard],
                 },
                 {
                    path: 'add-test-sub-tab-one',
                    component: TestSubTabOneFormComponent,
                    canDeactivate: [CanDeactivateGuard],
                 },
                 {
                    path: 'edit-test-sub-tab-one',
                    component: TestSubTabOneFormComponent,
                    canDeactivate: [CanDeactivateGuard],
                 },

              ]
          },
          {
              path: 'testssub',
              children: [
                  {
                      path: '',
                      component: TestSubGridComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
                  {
                      path: 'add-test-sub',
                      component: TestSubFormComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
                  {
                      path: 'edit-test-sub',
                      component: TestSubFormComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
              ]
          },
          {
              path: 'testnote',
              children: [
                  {
                      path: '',
                      component: NoteTestGridComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
                  {
                      path: 'add-test-note',
                      component: NoteTestFormComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
                  {
                      path: 'edit-test-note',
                      component: NoteTestFormComponent,
                      canDeactivate: [CanDeactivateGuard],
                  },
              ]
          },
      ]
  },
];
