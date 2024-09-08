import { Injectable } from '@angular/core';
import { Router, CanLoad, CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { HttpService } from "src/app/shared/http.service";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanLoad {

  constructor(private router: Router, private httpService: HttpService) { }

  canLoad() {
    if (localStorage.getItem('currentUser')) {
      // logged in so return true
      return true;
    }
    // not logged in so redirect to login page with the return url and return false
    this.router.navigate(['/authentication/login'], { queryParams: { returnUrl: this.router.url } });
    return false;
  }
}

export class CanDeactivateGuard implements CanDeactivate<FormComponent> {

  canDeactivate(component: any) {
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (component.form && component.form.dirty) {
      return component.form.dirty ? currentUser ? component.confirmationDialogService
        .confirm('Warning - Unsaved Changes',
          '<p>You have made changes to this page which have not been saved!</p><p>Are you sure you want to leave this page?</p>',
          'Leave Page')
        .then((confirmed) => {
          if (confirmed === true) {
            return true;
          }
          else {
            return false;
          }
        }) : true : true;
    }
    if (component.duelForm) {
      return component.duelForm.dirty ? currentUser ? component.confirmationDialogService
        .confirm('Warning - Unsaved Changes',
          '<p>You have made changes to this page which have not been saved!</p><p>Are you sure you want to leave this page?</p>',
          'Leave Page')
        .then((confirmed) => {
          if (confirmed === true) {
            return true;
          }
          else {
            return false;
          }
        }) : true : true;
    }
    return true;
  }
}

@Injectable({ providedIn: 'root' })
export class AuthguardService implements CanActivate {

  constructor(private router: Router, private httpService: HttpService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let access = route.data["access"] as Array<string>;
    if (access && access[0]) {
      var tabInfo = this.httpService.getAppInfo(Number(access[0]));
      if (tabInfo.show || tabInfo.isReadonly) {
        return true;
      }
    }

    this.router.navigate(['/authentication/login']);
    return false;
  }

}

@Injectable({ providedIn: 'root' })
export class RedirectSystemSettingGuard implements CanActivate {

  constructor(private router: Router, private httpService: HttpService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let access = route.data["access"] as Array<string>;
    if (access && access[0] && access[1]) {
      var tabInfo1 = this.httpService.getAppInfo(Number(access[0]));
      var tabInfo2 = this.httpService.getAppInfo(Number(access[1]));
      if (tabInfo1.show || tabInfo1.isReadonly) {
        this.router.navigate(['/system-settings/options']);
        return true;
      }
      else if (tabInfo2.show || tabInfo2.isReadonly) {
        this.router.navigate(['/system-settings/dashboard']);
        return true;
      }
      else {
        this.router.navigate(['/system-settings/groups']);
        return true;
      }
    }

    this.router.navigate(['/authentication/login']);
    return false;
  }

}

export interface FormComponent {
  form: FormGroup;
  duelForm: FormGroup;
}
