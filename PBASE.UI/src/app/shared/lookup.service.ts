import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

@Injectable({
  providedIn: 'root',
})
export class LookupService {
  lookupApiUrl: string = "Lookup/";// Web API URL  
  lookupApiUrl2: string = "lookup1/";// Web API URL  
  lookupsApiUrl: string = "Lookups/";// Web API URL  
  lookupRoleApiUrl: string = "lookuprole/";// Web API URL  
  constructor(private http: HttpClient) {
  }

  //Fill lookup
  templateTypes: Observable<any[]>;
  applicationTitles: Observable<any[]>;
  AlertType: Observable<any[]>;
  allUsers: Observable<any[]>;
  allRoles: Observable<any[]>;
  userAccessTypes: Observable<any[]>;
  EmailType: Observable<any[]>;
  EmailTemplateType: Observable<any[]>;
  allUserGroups: Observable<any[]>;
  allMenuOptionGroups: Observable<any[]>;
  allDashboardOptionGroups: Observable<any[]>;
 
  ////Variables
  defaultCurrencyId: any = 4;
  TPARole: any = 'TPA';
  TPARoleId: any = 2;
  userAccessGroupId: any = 3463;
  defaultMessageTypeId: any = 3259;
  defaultLockoutEnabled: any = false;

  //Services
  getTitlesApplication() {
    if (!this.applicationTitles) {
      this.applicationTitles = this.http.get(this.lookupApiUrl + "vw_LookupTitleApplication")
        .map((res: any) => <any[]>res)
        .publishReplay(1)
        .refCount();
    }
    return this.applicationTitles;
  }

  getTemplateTypes() {
    if (!this.templateTypes) {
      this.templateTypes = this.http.get(this.lookupApiUrl + "vw_lookuptemplatetype")
        .map((res) => <any[]>res)
        .publishReplay(1)
        .refCount();
    }
    return this.templateTypes;
  }

  getAllUsers() {
    this.allUsers = this.http.get(this.lookupApiUrl + "vw_lookupuserssignature")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return this.allUsers;
  }

  getAllRoles() {
    if (!this.allRoles) {
      this.allRoles = this.http.get(this.lookupRoleApiUrl + "vw_lookupsroles")
        .map((res) => <any[]>res)
        .publishReplay(1)
        .refCount();
    }
    return this.allRoles;
  }

  getUserAccessTypes() {
    if (!this.userAccessTypes) {
      this.userAccessTypes = this.http.get(this.lookupApiUrl + "vw_LookupUserAccessType")
        .map((res) => <any[]>res)
        .publishReplay(1)
        .refCount();
    }
    return this.userAccessTypes;
  }

  getAllUserGroups(id) {
    this.allUserGroups = this.http.get(this.lookupApiUrl + "vw_LookupUserGroup/" + id)
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return this.allUserGroups;
  }

  getMenuOptionGroups(aspNetRoleId) {
    this.allMenuOptionGroups = this.http.get(this.lookupApiUrl + "vw_LookupMenuOptionGroup/" + aspNetRoleId)
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return this.allMenuOptionGroups;
  }

  getDashboardOptionGroups(aspNetRoleId) {
    this.allDashboardOptionGroups = this.http.get(this.lookupApiUrl + "vw_LookupDashboardOptionGroup/" + aspNetRoleId)
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return this.allDashboardOptionGroups;
  }

  getGridUserGroups() {
    let UserGroup = this.http.get(this.lookupsApiUrl + "vw_LookupGridUserGroup")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return UserGroup;
  }
  getGridTestType() {
      let UserGroup = this.http.get(this.lookupsApiUrl + "vw_LookupGridTestType")
          .map((res) => <any[]>res)
          .publishReplay(1)
          .refCount();
      return UserGroup;
  }

  getGridEmailTemplateTypes() {
    let EmailTemplateType = this.http.get(this.lookupsApiUrl + "vw_LookupGridEmailTemplateType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return EmailTemplateType;
  }
  getGridAlertTypes() {
    let AlertType = this.http.get(this.lookupApiUrl2 + "vw_LookupGridAlertType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return AlertType;
  }

  getGridEmailTypes() {
    let EmailType = this.http.get(this.lookupsApiUrl + "vw_LookupGridEmailType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return EmailType;
  }

  getGridObjectTypes() {
    let ObjectType = this.http.get(this.lookupsApiUrl + "vw_LookupObjectType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return ObjectType;
  }

  getGridTemplateAllowedTypes() {
    let TemplateAllowedType = this.http.get(this.lookupsApiUrl + "vw_LookupGridTemplateAllowedType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return TemplateAllowedType;
  }

  getGridUserAccessTypes() {
    let UserAccessType = this.http.get(this.lookupsApiUrl + "vw_LookupGridUserAccessType")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return UserAccessType;
  }

  getGridRoles() {
    let Role = this.http.get(this.lookupsApiUrl + "vw_LookupGridRole")
      .map((res) => <any[]>res)
      .publishReplay(1)
      .refCount();
    return Role;
  }

  getBooleanOptions() {
    var options = [
      { LookupId: true, LookupValue: 'Yes' },
      { LookupId: false, LookupValue: 'No' }
    ];
    return options;
  }
} 
