// Sidebar route metadata
export interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
  extralink: boolean;
  submitted: boolean;
  permissions: number[];
  submenu: RouteInfo[];
}
