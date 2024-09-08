import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ForgotPasswordService } from './forgot-password.service';

@Component({
   selector: 'app-forgot-password',
   templateUrl: './forgot-password.component.html'
})
export class ForgotPasswordComponent implements OnInit {
   public forgetForm: FormGroup;
   forgetModel: any = {};
   showMessage: boolean = false;
   isLoading: boolean = false;

   constructor(private fb: FormBuilder,
      private router: Router,
      private route: ActivatedRoute,
      private forgotService: ForgotPasswordService,
      private toastr: ToastrService) { }

   ngOnInit() {
      this.forgetForm = this.fb.group({
         uemail: [null, Validators.compose([Validators.required])]
      });
   }

   reset() {
      let that = this;
      that.isLoading = true;
      this.forgotService
         .resetPassword(this.forgetModel.email)
         .subscribe(
            result => {
               if (result) {
                  that.isLoading = false;
                  //this.toastr.info('Please check your email at (' + this.forgetModel.email + ') for furthuer information!');
                  this.showMessage = true;
               }
               else {
                  that.isLoading = false;
                  this.toastr.error('Some system error occurred, Try again later!');
                  this.showMessage = false;
               }
            },
            error => {
               that.isLoading = false;
               if (error.status == 404) {
                  this.toastr.error('Email Address not found!');
               }
               else {
                  this.toastr.error('Some system error occurred, Try again later!');
               }
               this.showMessage = false;
            });
   }
}
