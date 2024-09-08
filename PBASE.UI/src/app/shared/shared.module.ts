import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FilePickerComponent } from './file-picker/file-picker.component';
import { TabsComponent } from '../shared/tabs/tabs.component';
import { TabLinkComponent } from '../shared/tabs/tab-link.component';
import { UnsavedChanges } from './unsaved-changes/unsaved-changes.component';
import { RequestValidationComponent } from './request-validation/request-validation.component';
import { RequestValidationService } from './request-validation/request-validation.service';
import { ImagePickerComponent } from './image-picker/image-picker.component';
import { ElasticDirective } from './auto-grow/auto-grow.directive';
import { InputTrimModule } from 'ng2-trim-directive';
import { ToastrModule } from 'ngx-toastr';
import { ValidationMessageToasterComponent } from './http-Interceptors/validation-message-toaster/validation-message-toaster.component';
import { ScrollHandlerComponent } from './scroll-handler/scroll-handler.component';
import { StateManagementService } from './state-management/state-management.service';
import { AttachmentPreviewModelComponent } from './attachment-preview-model/attachment-preview-model.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    InputTrimModule,
    ToastrModule.forRoot({ toastComponent: ValidationMessageToasterComponent }),
    NgbModule,

  ],
  declarations: [
    FilePickerComponent,
    TabsComponent,
    ElasticDirective,
    TabLinkComponent,
    UnsavedChanges,
    RequestValidationComponent,
    ImagePickerComponent,
    ScrollHandlerComponent,
    ValidationMessageToasterComponent,
    AttachmentPreviewModelComponent
  ],
  exports: [
    FilePickerComponent,
    InputTrimModule,
    ImagePickerComponent,
    TabsComponent,
    UnsavedChanges,
    ScrollHandlerComponent,
    RequestValidationComponent,
  ],
  providers: [
    RequestValidationService,
    StateManagementService,
    FilePickerComponent
  ],
  entryComponents: [
    ValidationMessageToasterComponent,
    AttachmentPreviewModelComponent
  ]
})
export class SharedModule { }
