import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ChangePasswordService } from './change-password.service';
import { RegistrationValidator } from './confirmValidator';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
  private id: string;
  private sub: any;
  private VerificationKey: string;
  private PasswordType: string;
  private FormType: string;
  public form: FormGroup;
  public isSucess: boolean = false;
  private PassNotMatch: boolean = false;
  isLoading: boolean = false;
  model: any = {};
  ConfirmPassword: any;
  text: string = "(!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~)";
  icon: string = "fa fa-times";
  icon_total_charecter: string = "fa fa-times";
  icon_spectial_charecter: string = "fa fa-times";
  icon_upper: string = "fa fa-times";
  icon_lower: string = "fa fa-times";
  icon_number: string = "fa fa-times";
  m_strUpperCase: string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  m_strLowerCase: string = "abcdefghijklmnopqrstuvwxyz";
  m_strNumber: string = "0123456789";
  m_strCharacters: string = "!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~";
  show: boolean = false;
  showPassword: boolean = false;
  showConfPassword: boolean = false;
  ptext: string = "show";
  ptype: string = "password";
  submitted = false;
  cptype: string = "password";;
  cptext: string = "show";;

  constructor(private fb: FormBuilder,
    private router: Router,
    private changePasswordService: ChangePasswordService,
    private toastr: ToastrService, vcr: ViewContainerRef,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute) {
      this.form = this.formBuilder.group({
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required]
      }, {
        validator: RegistrationValidator.validate.bind(this)
      });
  }

  ngOnInit() {
    this.sub = this.route.queryParams.subscribe((params: Params) => {
      this.id = params['id'];
      this.VerificationKey = params['verificationKey'];
      this.PasswordType = params['password'];
      this.FormType = params['FormType'];
    });
  }

  showList() {
    this.show = true;
  }

  hideList() {
    this.show = false;
  }

  inputChange() {
    if (this.model.Password) {
      if (this.model.Password.length >= 8) {
        this.icon_total_charecter = "fa fa-check";
      }
      else {
        this.icon_total_charecter = "fa fa-times";
      }

      //if (this.ConfirmPassword !== this.model.Password) {
      //  return {
      //    doesMatchPassword: true
      //  };
      //}


      let nUpperCount = this.countContain(this.model.Password, this.m_strUpperCase);
      if (nUpperCount >= 1) {
        this.icon_upper = "fa fa-check";
      }
      else {
        this.icon_upper = "fa fa-times";
      }

      let nLowerCount = this.countContain(this.model.Password, this.m_strLowerCase);
      if (nLowerCount >= 1) {
        this.icon_lower = "fa fa-check";
      }
      else {
        this.icon_lower = "fa fa-times";
      }
      let nChracterCount = this.countContain(this.model.Password, this.m_strCharacters);
      if (nChracterCount >= 1) {
        this.icon_spectial_charecter = "fa fa-check";
      }
      else {
        this.icon_spectial_charecter = "fa fa-times";
      }
      let nNumberCount = this.countContain(this.model.Password, this.m_strNumber);
      if (nNumberCount >= 1) {
        this.icon_number = "fa fa-check";
      }
      else {
        this.icon_number = "fa fa-times";
      }
      this.model.ConfirmPassword = this.model.Password;
    }
    else {
      this.icon_total_charecter = "fa fa-times";
      this.icon_number = "fa fa-times";
      this.icon_spectial_charecter = "fa fa-times";
      this.icon_lower = "fa fa-times";
      this.icon_upper = "fa fa-times";
      this.model.ConfirmPassword = '';
    }

  }
  countContain(strPassword, strCheck) {
    // Declare variables
    var nCount = 0;

    for (let i = 0; i < strPassword.length; i++) {
      if (strCheck.indexOf(strPassword.charAt(i)) > -1) {
        nCount++;
      }
    }

    return nCount;
  }

  showHidePassword() {
    this.showPassword = !this.showPassword;
    if (this.showPassword) {
      this.ptype = "text";
      this.ptext = "hide";
    }
    else {
      this.ptype = "password";
      this.ptext = "show";
    }
  }

  showHideConfPassword() {
      this.showConfPassword = !this.showConfPassword;
      if (this.showConfPassword) {
      this.cptype = "text";
      this.cptext = "hide";
    }
    else {
      this.cptype = "password";
      this.cptext = "show";
    }
  }

  onSubmit(valid) {
    this.submitted = true;
    let that = this;
    that.isLoading = true;
    if (valid) {
      var model = { Id: this.id, Token: this.VerificationKey, Password: this.model.Password, FormType: that.FormType};
      this.changePasswordService
        .confirmPasswordReset(model)
        .subscribe(
        result => {
          if (result) {
            that.isLoading = false;
            this.toastr.info('Your password is updated, Please check your email or Login to continue.');
            this.router.navigate(['authentication/login']);
          }
          else {
            that.isLoading = false;
            this.toastr.error('Some system error occurred, Try again later!');
          }
        },
        error => {
          if (error.Message) {
            that.isLoading = false;
            this.toastr.error(error);
          }
          else if (error.status == 400) {
            that.isLoading = false;
            this.toastr.error('Your session has expired.');
          }
          else {
            that.isLoading = false;
            this.toastr.error('Some system error occurred, Try again later!');
          }
        });
    }
  }
}
