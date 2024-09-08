import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
//import { ChangePasswordService } from './change-password.service';

@Component({
 selector: 'app-thank-you',
 templateUrl: './thank-you.component.html'
})
export class ThankYouPageComponent implements OnInit {
 private id: number;
 private sub: any;
 private VerificationKey: string;
 public email: any = "";
 public form: FormGroup;
 public isSucess: boolean = false;
 private PassNotMatch: boolean = false;

 constructor(private fb: FormBuilder,
   private router: Router,
   //private changePasswordService: ChangePasswordService,
   private toastr: ToastrService, vcr: ViewContainerRef,
   private route: ActivatedRoute) {
 }

 ngOnInit() {
   this.sub = this.route.queryParams.subscribe((params: Params) => {
     this.id = +params['id'];
     this.VerificationKey = params['verificationKey'];
     this.email = params['email'];     
   });
 }

}
