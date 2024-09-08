import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { LookupService } from "../../../shared/lookup.service";
import { ToastrService } from 'ngx-toastr';
import { BreadcrumbService } from '../../../shared/breadcrumb/breadcrumb.service';
import { Breadcrumb } from '../../../shared/breadcrumb/breadcrumb.model';
import { ConfirmationDialogService } from 'src/app/shared/confirmation-dialog/confirmation-dialog.service';
import { CryptoService } from '../../../shared/crypto/crypto.service';
import { Menu, pBMenus, InfoMessage } from 'src/app/shared/all-routes';
import { HttpService } from '../../../shared/http.service';

@Component({
   selector: 'app-groups-form',
   templateUrl: './groups-form.component.html',
   styleUrls: ['./groups-form.component.scss']
})

export class GroupsFormComponent implements OnInit {
   @ViewChild('groupsForm') form;
   data: any = {};
   allRoles: Array<any> = [];
   isEdit: boolean = false;
   isSaving: boolean | number = false;
   isDeleting: boolean | number = false;
   submitted = false;
   isLoading: boolean = false;
   appInfo: any = {};
   queryParams: any = {};
   pBRoutes: Menu = pBMenus.find(x => x.id == "Groups");
   isEditClicked = false;
   isEditClickedDashboard = false;

   constructor(
      private router: Router,
      private lookupService: LookupService,
      private route: ActivatedRoute,
      private toastr: ToastrService,
      private breadcrumbService: BreadcrumbService,
      private cryptoService: CryptoService,
      private httpService: HttpService,
      private confirmationDialogService: ConfirmationDialogService,

   ) {

   }

   ngOnInit() {
      let that = this;
      that.queryParams = that.route.snapshot.queryParams;
      that.appInfo = that.httpService.getAppInfo(-9985);

      that.get();
      that.setBreadcrumbs();
   }

   get() {
      let that = this;
      that.isLoading = true;
      that.isEdit = true;
      that.httpService
         .get(that.pBRoutes.api + (that.queryParams.id ? that.queryParams.id : '0'))
         .subscribe((res: any) => {
           that.data = res;
           that.filterRoles();
           that.isLoading = false;
         }
         );
   }
   OnIsClicked($event) {
      this.isEditClicked = $event.isEditClicked;
   }
   OnIsClickedDashboard($event) {
      this.isEditClickedDashboard = $event.isEditClickedDashboard;
   }

  filterRoles() {
    let that = this;
    let currentUserTypeId = JSON.parse(localStorage.getItem('currentUser')).user.UserTypeId;
    if (currentUserTypeId != 3) {
      that.data.allRoles = that.data.allRoles.filter(x => x.LookupId != 3);
    }
  }

   sortOptions(options, value) {
      let sortedOptions = options.sort((a, b) => a.LookupValue.localeCompare(b.LookupValue));
      if (value == "dashboard") {
         this.data.DashboardOptionIds = sortedOptions.map(x => x.LookupId);
      }
      else {
         this.data.MenuOptionIds = sortedOptions.map(x => x.LookupId);
      }
   }

   submit(valid: boolean) {
      let that = this;
      that.submitted = true;
      if (valid && !that.appInfo.isUserReadonly) {
         that.isSaving = true;
         that.queryParams.id ? that.update() : that.save();
      }
      else {
         valid == true ? that.toastr.error('You cannot save your changes') : "";
      }
   }

   save() {
      let that = this;
      that.httpService
         .post(that.pBRoutes.api, that.data)
         .subscribe(id => {
            that.form.control.markAsPristine();
            that.goToEdit(InfoMessage.SaveMessage, id);
         }, erorr => {
            that.isSaving = false;
         });
   }

   update() {
      let that = this;
      that.httpService
         .put(that.pBRoutes.api + that.queryParams.id, that.data)
         .subscribe(template => {
            that.form.control.markAsPristine();
            that.goBack(InfoMessage.SaveMessage);
         }, erorr => {
            that.isSaving = false;
         });
   }

   delete() {
      let that = this;
      that.isDeleting = true;
      that.httpService
         .delete(that.pBRoutes.api + that.queryParams.id)
         .subscribe(template => {
            that.form.control.markAsPristine();
            that.goBack(InfoMessage.DeleteMessage);
         }, erorr => {
            that.isDeleting = false;
         });
   }

   goBack(message?: string) {
      let that = this;
      that.router.navigated = false;
      that.router
         .navigate(['/' + that.pBRoutes.grid.routeInfo.route], {
            queryParams: {
               localName: that.cryptoService.encryptString(that.pBRoutes.grid.vwName)
            }
         }).then(() => {
            if (message) {
               that.toastr.success(message);
            }
         });
   }
   goToEdit(message?: string, id?: string) {
      let that = this;
      that.router.navigated = false;
      that.router
         .navigate(['/' + that.pBRoutes.grid.routeInfo.route + '/' + that.pBRoutes.editForm.routeInfo.route], {
            queryParams: {
               id: id
            }
         })
         .then(() => {
            if (message) {
               that.toastr.success(message);
            }
         });
   }

   setBreadcrumbs() {
      let that = this;
      let breadcrumbs: Breadcrumb[] = [];
      breadcrumbs.push({
         label: that.pBRoutes.grid.routeInfo.label,
         url: '/' + that.pBRoutes.grid.routeInfo.route,
         params: {
            localName: that.cryptoService.encryptString(that.pBRoutes.grid.vwName)
         }
      });
      if (that.queryParams.id) {
         breadcrumbs.push({
            label: that.pBRoutes.editForm.routeInfo.label,
            url: '',
            params: {}
         });
      }
      else {
         breadcrumbs.push({
            label: that.pBRoutes.addForm.routeInfo.label,
            url: '',
            params: {}
         });
      }
      this.breadcrumbService.set(breadcrumbs);
   }

}
