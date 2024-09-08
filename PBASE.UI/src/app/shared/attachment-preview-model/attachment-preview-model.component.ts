import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, Input } from '@angular/core';
import { environment } from "../../../environments/environment";

@Component({
  selector: 'app-attachment-preview-model',
  templateUrl: './attachment-preview-model.component.html',
  styles: []
})
export class AttachmentPreviewModelComponent implements OnInit {

  isLoading: boolean = false;
  @Input() FileHandle: any;
  safeSrc: SafeResourceUrl;

  constructor(
    private sanitizer: DomSanitizer,
    public activeModal: NgbActiveModal
  ) { }

  ngOnInit() {
    let that = this;
    that.get();
  }

  get() {
    let that = this;
    that.safeSrc = that.sanitizer.bypassSecurityTrustResourceUrl("https://cdn.filestackcontent.com/preview/security=policy:" + environment.filePickerApi.policy + ",signature:" + environment.filePickerApi.signature + "/" + this.FileHandle);
  }

  close(data?: any) {
    let that = this;
    that.activeModal.close(data);
  }


}
