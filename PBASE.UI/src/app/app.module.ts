import * as $ from 'jquery';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
    CommonModule,
    APP_BASE_HREF
} from '@angular/common';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { httpInterceptorProviders } from './shared/http-Interceptors/index';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { LaddaModule } from 'angular2-ladda';
import { NgxMaskModule } from 'ngx-mask'
import { NgbModule, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ngx-toastr';
import { CryptoService } from "./shared/crypto/crypto.service";
import { FullComponent } from './layouts/full/full.component';
import { BlankComponent } from './layouts/blank/blank.component';
import { UserIdleModule } from 'angular-user-idle';
import { ConfirmationPopoverModule } from 'angular-confirmation-popover';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';
import { NgxPermissionsModule, NgxPermissionsService } from 'ngx-permissions';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';
import { CookieService } from 'ngx-cookie-service';


import { NavigationComponent } from './shared/header-navigation/navigation.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { BreadcrumbComponent } from './shared/breadcrumb/breadcrumb.component';
import { ConfirmationDialogComponent } from './shared/confirmation-dialog/confirmation-dialog.component';
import { AuthGuard, CanDeactivateGuard } from './shared/guard/auth.guard';
import { DatePickerConvertor } from "./shared/datepicker/datepicker.convertor";
import { Approutes } from './app-routing.module';
import { AppComponent } from './app.component';
import { SpinnerComponent } from './shared/spinner.component';
import { LoginService } from './authentication/login/login.service';
import { FilePickerService } from './shared/file-picker/file-picker.service';
import { ImagePickerService } from './shared/image-picker/image-picker.service';
import { ConfirmationDialogService } from './shared/confirmation-dialog/confirmation-dialog.service';
import { HttpService } from './shared/http.service';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { BreadcrumbService } from './shared/breadcrumb/breadcrumb.service';
import { environment } from '../environments/environment';
  
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true,
    wheelSpeed: 2,
    wheelPropagation: true,
    minScrollbarLength: 20
};

export function createNgxLoader(ngx: NgxPermissionsService, httpService: HttpService) {
  return () => {
    let menus = httpService.menus();
    if (menus && menus.length != 0) {
      let filteredMenus = menus.filter(x => x.AccessTypeId != null);
      let permissions: any[] = filteredMenus.map(x => x.Id);
      return ngx.loadPermissions(permissions);
    }
  };
}

@NgModule({
  declarations: [
    AppComponent,
    SpinnerComponent,
    FullComponent,
    BlankComponent,
    NavigationComponent,
    BreadcrumbComponent,
    SidebarComponent,
    ConfirmationDialogComponent,
  ],
  imports: [
    SharedModule,
    ConfirmationPopoverModule.forRoot(),
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    NgbModule.forRoot(),
    NgbDatepickerModule,
    NgxPermissionsModule.forRoot(),
    RouterModule.forRoot(Approutes, { useHash: false, scrollPositionRestoration: 'top' }),
    NgxMaskModule.forRoot(),
    PerfectScrollbarModule,
    LoadingBarHttpClientModule,
    LaddaModule,
    UserIdleModule.forRoot(environment.userIdle),
    LoggerModule.forRoot({
      serverLoggingUrl: 'errorlog',
      level: NgxLoggerLevel.ERROR,
      serverLogLevel: NgxLoggerLevel.ERROR,
      disableConsoleLogging: true
    }),
    SnotifyModule
    
  ],
  providers: [
    FilePickerService,
    ImagePickerService,
    LoginService,
    BreadcrumbService,
    ConfirmationDialogService,
    httpInterceptorProviders,
    DatePickerConvertor,
    CryptoService,
    NgxPermissionsService,
    CookieService,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    SnotifyService,
    {
      provide: APP_INITIALIZER,
      useFactory: createNgxLoader,
      deps: [NgxPermissionsService, HttpService],
      multi: true
    },
    AuthGuard,
    CanDeactivateGuard,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
    {
      provide: APP_BASE_HREF,
      useValue: '/'
    }
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
