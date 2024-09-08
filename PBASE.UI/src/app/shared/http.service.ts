import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { ACCESSTYPES } from './access-types';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(private http: HttpClient) {
  }

  get(url: string): Observable<any> {
    return this.http
      .get(url)
      .flatMap((res: any) => {
        return Observable.of(res);
      })
  }

  post(url: string, data: any): Observable<any> {
    return this
      .http
      .post(url, data)
      .flatMap((res: any) => {
        return Observable.of(res);
      });
  }

  put(url: string, data: any): Observable<any> {
    return this.http
      .put(url, data)
      .flatMap((res: any) => {
        return Observable.of(res);
      });
  }

  delete(url: string): Observable<any> {
    return this.http
      .delete(url)
      .flatMap((res: any) => {
        return Observable.of(res);
      });
  }

  userName(): string {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let name: string = (currentUser && currentUser.user) ? (currentUser.user ? currentUser.user.FirstName + ' ' + currentUser.user.LastName : '') : '';
    return name;
  }

  userProfileImageHandler(): string {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let ProfileImageAttachmentFileHandle: string = (currentUser && currentUser.ProfileImageAttachmentFileHandle) ? (currentUser.ProfileImageAttachmentFileHandle ? currentUser.ProfileImageAttachmentFileHandle : '') : '';
    return ProfileImageAttachmentFileHandle;
  }

  appInfo(): any {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let appInfo: any = {};
    appInfo.database = (currentUser && currentUser.database) ? currentUser.database : '';
    appInfo.version = (currentUser && currentUser.version) ? currentUser.version : '';
    appInfo.lastUpdated = (currentUser && currentUser.lastUpdated) ? currentUser.lastUpdated : '';
    return appInfo;
  }

  menus(): any[] {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let menus: any[] = (currentUser && currentUser.menus) ? currentUser.menus : [];
    return menus;
  }

  dashboardItems(): any[] {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let dashboardItems: any[] = (currentUser && currentUser.dashboardItems) ? currentUser.dashboardItems : [];
    return dashboardItems;
  }

  getAppInfo(id: number): any {
    let that = this;
    let menues = that.menus();
    let appinfo: any = {};
    let accessType: any;
    let menu = menues.find(x => x.Id == id);
    if (menu) {
      accessType = menu['AccessTypeId'];
      if (accessType) {
        if (accessType == ACCESSTYPES.ReadOnly) {
          appinfo.isReadonly = true;
          appinfo.show = true;
        }
        if (accessType == ACCESSTYPES.Show) {
          appinfo.show = true;
        }
        if (accessType == ACCESSTYPES.Hide) {
          appinfo.show = false;
        }
      }
    }
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser) {
      appinfo.isUserReadonly = currentUser.isReadonly;
      appinfo.isDeleteDisabled = currentUser.isDeleteDisabled;
    }
    if (appinfo.isDeleteDisabled) {
      appinfo.isDeleteHidden = true;
    }
    else if (!appinfo.isUserReadonly) {
       if ((menu && accessType) && (accessType == ACCESSTYPES.AddEdit || accessType == ACCESSTYPES.ReadOnly)) {
          appinfo.isDeleteHidden = true;
       }
    }
    return appinfo;
  }
} 
