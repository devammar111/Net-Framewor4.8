import { Routes } from '@angular/router';
import { LoginAnalysisGridComponent } from './login-analysis-grid/login-analysis-grid.component';
import { LoginAnalysisDetailGridComponent } from './login-analysis-detail-grid/login-analysis-detail-grid.component';

export const LoginAnalysisRoutes: Routes = [
  {
    path: '',
    component: LoginAnalysisGridComponent
  },
  {
    path: 'login-analysis-detail',
    component: LoginAnalysisDetailGridComponent
  }
];
