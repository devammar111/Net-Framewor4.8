import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LaddaModule } from 'angular2-ladda';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { CookieService } from 'ngx-cookie-service';
import { NotfoundComponent } from './404/not-found.component';
import { LockComponent } from './lock/lock.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ChangePasswordService } from './change-password/change-password.service';
import { LookupService } from '../shared/lookup.service';
import { SignupService } from './signup/signup.service';
import { SignupComponent } from './signup/signup.component';
import { NgSelectModule } from '@ng-select/ng-select';

import { AuthenticationRoutes } from './authentication.routing';
import { ThankYouPageComponent } from './thank-you/thank-you.component';
import { InputTrimModule } from 'ng2-trim-directive';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AuthenticationRoutes),
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    NgSelectModule,
    LaddaModule,
    InputTrimModule
  ],
  declarations: [
    NotfoundComponent,
    LoginComponent,
    SignupComponent,
    LockComponent,
    ForgotPasswordComponent,
    ChangePasswordComponent,
    ThankYouPageComponent
  ],
  providers: [
    ChangePasswordService,
    LookupService,
    SignupService,
    CookieService,
  ]
})
export class AuthenticationModule { }
