import { RouteInfo } from './sidebar.metadata';

export const ROUTES: RouteInfo[] = [
  {
    path: '/dashboard',
    title: 'Dashboard',
    class: '',
    icon: 'fa fa-tachometer-alt',
    extralink: false,
    submitted: false,
    permissions: [-9990],
    submenu: []
  },
  {
    path: '/dashboard-tc',
    title: 'Agreements & Policies',
    class: '',
    icon: 'fa fa-list-alt',
    extralink: false,
    submitted: false,
    permissions: [0],
    submenu: []
  },
  {
    path: '/tests',
    title: 'Tests',
    class: '',
    icon: 'fa fa-list-alt',
    extralink: false,
    submitted: false,
    permissions: [-9962],
    submenu: []
  },
  {
    path: '',
    title: 'Maintenance',
    icon: 'fa fa-cogs',
    class: 'has-arrow',
    extralink: false,
    submitted: false,
    permissions: [-9989, -9988],
    submenu: [
      {
        path: '/templates',
        title: 'Templates',
        class: '',
        icon: 'fa fa-file',
        extralink: false,
        submitted: false,
        permissions: [-9989],
        submenu: []
      },
      {
        path: '/lookups',
        title: 'Lookups',
        class: '',
        icon: 'fa fa-folder-open',
        extralink: false,
        submitted: false,
        permissions: [-9988],
        submenu: []
      }
    ]
  },
  {
    path: '',
    title: 'Administration',
    icon: 'fa fa-users',
    class: 'has-arrow',
    extralink: false,
    submitted: false,
    permissions: [-9987, -9986, -9985, -9984, -9981, -9982, -9978, -9979, -9945],
    submenu: [
      {
        path: '/users',
        title: 'Users',
        class: '',
        icon: 'fa fa-user',
        extralink: false,
        submitted: false,
        permissions: [-9987],
        submenu: []
      },
      {
        path: '/login-analysis',
        title: 'Login Analysis',
        class: '',
        icon: 'fa fa-bars',
        extralink: false,
        submitted: false,
        permissions: [-9986],
        submenu: []
      },
      {
        path: '/system-settings',
        title: 'System Settings',
        class: '',
        icon: 'fa fa-cog',
        extralink: false,
        submitted: false,
        permissions: [-9985],
        submenu: []
      },
      {
        path: '/export-log',
        title: 'Export Log',
        class: '',
        icon: 'fa fa-file-download',
        extralink: false,
        submitted: false,
        permissions: [-9984],
        submenu: []
      },
      {
        path: '/email',
        title: 'Email',
        class: '',
        icon: 'fa fa-envelope',
        extralink: false,
        submitted: false,
        permissions: [-9981],
        submenu: []
      },
      {
        path: '/email-template',
        title: 'Email Template',
        class: '',
        icon: 'fa fa-envelope-open-text',
        extralink: false,
        submitted: false,
        permissions: [-9982],
        submenu: []
      },
      {
        path: '/agreement',
        title: 'Agreements',
        class: '',
        icon: 'fa fa-check-square',
        extralink: false,
        submitted: false,
        permissions: [-9978],
        submenu: []
      },
      {
        path: '/system-alerts',
        title: 'System Alerts',
        class: '',
        icon: 'fa fa-exclamation-triangle',
        extralink: false,
        submitted: false,
        permissions: [-9979],
        submenu: []
      },
      {
        path: '/system-administration',
        title: 'System Administration',
        class: '',
        icon: 'fas fa-tools',
        extralink: false,
        submitted: false,
        permissions: [-9945],
        submenu: []
      },
      {
        path: '/invalid-email-log',
        title: 'Invalid Email Log',
        class: '',
        icon: 'fa fa-lock',
        extralink: false,
        submitted: false,
        permissions: [1004],
        submenu: []
      }
    ]
  },
  
];
