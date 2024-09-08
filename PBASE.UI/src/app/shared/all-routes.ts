export interface IRouteInfo {
  route: string;
  label: string;
}

export class RouteInfo implements IRouteInfo {
  route: string;
  label: string;
  constructor(route?: string, label?: string) {
    this.route = route || '';
    this.label = label || '';
  }
}

export interface IGrid {
  vwName: string;
  routeInfo: RouteInfo;
}

export class Grid implements IGrid {
  vwName: string;
  routeInfo: RouteInfo;
  constructor(vwName?: string, routeInfo?: RouteInfo) {
    this.vwName = vwName || '';
    this.routeInfo = routeInfo || new RouteInfo();
  }
}

export interface IForm {
  heading: string;
  routeInfo: RouteInfo;
}

export class Form implements IForm {
  heading: string;
  routeInfo: RouteInfo;
  constructor(heading?: string, routeInfo?: RouteInfo) {
    this.heading = heading || '';
    this.routeInfo = routeInfo || new RouteInfo();
  }
}

export interface IMenu {
  grid: Grid;
  addForm: Form;
  editForm: Form;
}

export class Menu implements IMenu {
  id: string;
  api: string;
  grid: Grid;
  addForm: Form;
  editForm: Form;
  constructor(id: string, api: string, grid?: Grid, addForm?: Form, editForm?: Form) {
    this.id = id || "";
    this.api = api || '';
    this.grid = grid || new Grid();
    this.addForm = addForm || new Form();
    this.editForm = editForm || new Form();
  }
}

export const pBMenus: Array<Menu> = [];
//Dashboard
pBMenus.push(new Menu("Dashboard",
  '',
  new Grid('dashboard', new RouteInfo('dashboard', 'Dashboard'))//Grid
));
//Users
pBMenus.push(new Menu("Users",
  '',
  new Grid('vw_UserGrid', new RouteInfo('users', 'Users')),//Grid
  new Form('User', new RouteInfo('add-user', 'Add User')),//AddForm
  new Form('User', new RouteInfo('edit-user', 'Edit User'))//EditForm
));
//Profile
pBMenus.push(new Menu("Profile",
  'Account/',
  new Grid('', new RouteInfo('', 'Profile')),//Grid
  new Form('Profile', new RouteInfo('add-user', 'Add User')),//AddForm
  new Form('User', new RouteInfo('edit-user', 'Edit User'))//EditForm
));
//ChangePassword
pBMenus.push(new Menu("ChangePassword",
  'Account/ChangePassword/',
  new Grid('', new RouteInfo('', 'Change Password')),//Grid
  new Form('Blank', new RouteInfo('Blank', 'Blank')),//AddForm
  new Form('Blank', new RouteInfo('Blank', 'Blank'))//EditForm
));
//Tests
pBMenus.push(new Menu("Tests",
    'test/',
    new Grid('vw_TestGrid', new RouteInfo('tests', 'Tests')),//Grid
    new Form('Test', new RouteInfo('add-test', 'Add Test')),//AddForm
    new Form('Test', new RouteInfo('tab/edit-test', 'Edit Test'))//EditForm
));
//Tests
pBMenus.push(new Menu("TestsSub",
    'testSub/',
    new Grid('vw_TestSubGrid', new RouteInfo('tests/tab/testssub', 'Test Sub')),//Grid
    new Form('Test Sub', new RouteInfo('add-test-sub', 'Add Test Sub')),//AddForm
    new Form('Test Sub', new RouteInfo('edit-test-sub', 'Edit Test Sub'))//EditForm
));
//Tests Sub Tab One
pBMenus.push(new Menu("TestsSubTabOne",
   'testSub/',
   new Grid('vw_TestSubGrid', new RouteInfo('tests/tab/edit-test', 'Test Sub')),//Grid
   new Form('Test Sub', new RouteInfo('add-test-sub-tab-one', 'Add Test Sub')),//AddForm
   new Form('Test Sub', new RouteInfo('edit-test-sub-tab-one', 'Edit Test Sub'))//EditForm
));

//Tests
pBMenus.push(new Menu("TestsNote",
    'testNote/',
    new Grid('vw_TestNoteGrid', new RouteInfo('tests/tab/testnote', 'Test Note')),//Grid
    new Form('Test Note', new RouteInfo('add-test-note', 'Add Test Note')),//AddForm
    new Form('Test Note', new RouteInfo('edit-test-note', 'Edit Test Note'))//EditForm
));
//Templates
pBMenus.push(new Menu("Templates",
  'template/',
  new Grid('vw_TemplateGrid', new RouteInfo('templates', 'Templates')),//Grid
  new Form('Template', new RouteInfo('add-template', 'Add Template')),//AddForm
  new Form('Template', new RouteInfo('edit-template', 'Edit Template'))//EditForm
));
//Lookup Types
pBMenus.push(new Menu("LookupType",
  '',
  new Grid('vw_LookupTypeGrid', new RouteInfo('lookups', 'Lookups'))//Grid
));
//Lookup
pBMenus.push(new Menu("Lookups",
  'lookup/',
  new Grid('vw_LookupGrid', new RouteInfo('lookup-grid', 'Lookup')),//Grid
  new Form('Lookup', new RouteInfo('add-lookup', 'Add Lookup')),//AddForm
  new Form('Lookup', new RouteInfo('edit-lookup', 'Edit Lookup'))//EditForm
));
//Export Log
pBMenus.push(new Menu("ExportLog",
  '',
  new Grid('vw_ExportLogGrid', new RouteInfo('export-log', 'Export Log')),//Grid
));
//System Setting
pBMenus.push(new Menu("Options",
  'UserMenuOptionRole/', //api/UserMenuOptionRole
  new Grid('vw_UserMenuOptionRoleGrid', new RouteInfo('system-settings/options', 'Objects')),//Grid
  new Form('Options', new RouteInfo('add-options', 'Add Object')),//AddForm
  new Form('Options', new RouteInfo('edit-options', 'Edit Object'))//EditForm
));
pBMenus.push(new Menu("DashboardOptions",
  'UserDashboardOptionRole/', //api/UserDashboardOptionRole
  new Grid('vw_UserDashboardOptionRoleGrid', new RouteInfo('system-settings/dashboard', 'Dashboard Objects')),//Grid
  new Form('Dashboard', new RouteInfo('add-dashboard', 'Add Dashboard Object')),//AddForm
  new Form('Dashboard', new RouteInfo('edit-dashboard', 'Edit Dashboard Object'))//EditForm
));
pBMenus.push(new Menu("Groups",
  'UserGroup/', //api/UserGroup
  new Grid('vw_UserGroupGrid', new RouteInfo('system-settings/groups', 'Groups')),//Grid
  new Form('Groups', new RouteInfo('add-groups', 'Add Group')),//AddForm
  new Form('Groups', new RouteInfo('edit-groups', 'Edit Group'))//EditForm
));
//Login Analysis
pBMenus.push(new Menu("LoginAnalysis",
  '',
  new Grid('vw_AspNetUserLogsGrid', new RouteInfo('login-analysis', 'Login Analysis')),//Grid
  new Form('loginAnalysis', new RouteInfo('', 'Add Options')),//AddForm
  new Form('loginAnalysis', new RouteInfo('login-analysis-detail', 'Login Analysis Detail'))//EditForm
));
//Email
pBMenus.push(new Menu("Email",
  'Email/',
  new Grid('vw_EmailGrid', new RouteInfo('email', 'Email')),//Grid
  new Form('Email', new RouteInfo('add-email', 'Add Email')),//AddForm
  new Form('Email', new RouteInfo('edit-email', 'Edit Email'))//EditForm
));
// Email Template
pBMenus.push(new Menu("EmailTemplate",
  'EmailTemplate/',
  new Grid('vw_EmailTemplateGrid', new RouteInfo('email-template', 'Email Template')),//Grid
  new Form('Email Template', new RouteInfo('add-email-template', 'Add Email Template')),//AddForm
  new Form('Email Template', new RouteInfo('edit-email-template', 'Edit Email Template'))//EditForm
));
pBMenus.push(new Menu("Agreement",
  'agreement/',
  new Grid('vw_AgreementGrid', new RouteInfo('agreement', 'Agreements')),//Grid
  new Form('Agreement', new RouteInfo('add-agreement', 'Add Agreement')),//AddForm
  new Form('Agreement', new RouteInfo('edit-agreement', 'Edit Agreement'))//EditForm
));
pBMenus.push(new Menu("AgreementUser",
  'agreement/',
  new Grid('vw_AgreementUserSubGrid', new RouteInfo('agreement', 'Agreement User')),//Grid
));
pBMenus.push(new Menu("AgreementVersion",
   'agreement/',
   new Grid('vw_AgreementVersionSubGrid', new RouteInfo('agreement/edit-agreement', 'Version History')),//Grid
   new Form('Version History', new RouteInfo('add-agreement-version', 'Add Version History')),//AddForm
   new Form('Version History', new RouteInfo('edit-agreement-version', 'Edit Version History'))//EditForm
));
pBMenus.push(new Menu("SystemAlerts",
  'SystemAlertsForm/',
  new Grid('vw_SystemAlertGrid', new RouteInfo('system-alerts', 'System Alerts')),//Grid
  new Form('System Alerts', new RouteInfo('add-system-alerts', 'Add System Alerts')),//AddForm
  new Form('System Alerts', new RouteInfo('edit-system-alerts', 'Edit System Alerts'))//EditForm
));
pBMenus.push(new Menu("SystemAdministration",
  'ApplicationInformation/',
  new Grid('vw_SystemAlertGrid', new RouteInfo('system-administration', 'System Administration')),//Grid
  new Form('System Administration', new RouteInfo('add-system-administration', 'Add System Administration')),//AddForm
  new Form('System Administration', new RouteInfo('system-administration', 'System Administration'))//EditForm
));
pBMenus.push(new Menu("TermsAndConditions",
    'userAgreement/',
    new Grid('vw_UserAgreementForm', new RouteInfo('dashboard-tc', 'Terms & Conditions')),//Grid
    new Form('UserAgreement', new RouteInfo('add-dashboard-tc', 'Add Terms & Conditions')),//AddForm
    new Form('UserAgreement', new RouteInfo('edit-dashboard-tc', 'Edit Terms & Conditions'))//EditForm
));
//Invalid Email Log
let invalidEmailLog = new Menu("InvalidEmailLog",
  "vw_InvalidEmailLogGrid/",
  new Grid("vw_InvalidEmailLogGrid", new RouteInfo("invalid-email-log", "Invalid Email Log")), //Grid
  new Form("", new RouteInfo("", "")), //AddForm
  new Form("", new RouteInfo("", "")) //EditForm
);
pBMenus.push(invalidEmailLog);

export enum InfoMessage {
  SaveMessage = "Data successfully saved",
  DeleteMessage = "Data has been deleted successfully",
  ErrorMessage = "Something went wrong",
}
