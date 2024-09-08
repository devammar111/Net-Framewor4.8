import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LookupService } from "../../shared/lookup.service";
import { SignupService } from "./signup.service";
import { ToastrService } from 'ngx-toastr';
import { SignUp } from './signup';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html'
})
export class SignupComponent implements OnInit {
  registrationForm: FormGroup;
  model: SignUp = new SignUp();
  titles: any[] = [];
  submitted: boolean = false;
  isLoading: boolean = false;

  constructor(private fb: FormBuilder,
    private router: Router,
    private lookupService: LookupService,
    private signupService: SignupService,
    private toastr: ToastrService) {
    this.registrationForm = this.fb.group(
      {
        titleOther: [''],
        title: ['', Validators.required],
        lastName: ['', Validators.required],
        firstName: ['', Validators.required],
        reference: ['', Validators.required],
        professionalEmail: ['', Validators.required],
        isAgreeTermsCondition: ['', Validators.required]
      });
  }

  ngOnInit() {
    let that = this;
    that.lookupService
      .getTitlesApplication()
      .subscribe((res: any) => {
        that.titles = res
      });
  }

  onSubmit() {
    let that = this;
    that.isLoading = true;
    that.signupService
      .addUser(that.model)
      .subscribe(
        (res: any) => {
          if (res) {
            that.isLoading = false;
            that.toastr.info('Please check your email at (' + that.model.ProfessionalEmail + ') for furthuer information!');
            that.router.navigate(["/authentication/thank-you"], { queryParams: { email: that.model.ProfessionalEmail } });
          }
          else {
            that.isLoading = false;
            that.toastr.error('Some system error occurred, Try again later!');
          }
        },
        err => {
          that.isLoading = false;
          if (err.status == 400) {
            that.toastr.error('The email address has already been registered, please contact PBASE on 020 7831 0020');
          }
        });
  }
}
