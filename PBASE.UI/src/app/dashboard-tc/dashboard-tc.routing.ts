import { Routes } from '@angular/router';

import { DashboardTCComponent } from './dashboard-tc.component';

export const DashboardTCRoutes: Routes = [{
    path: '',
    children: [
        {
            path: '',
            component: DashboardTCComponent,
            data: {
                heading: 'Terms & Conditions'
            },

        },
    ]
}];
