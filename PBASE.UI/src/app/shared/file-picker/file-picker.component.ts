import { Component, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';
import { FilePickerService } from './file-picker.service'
import { LoadingBarService } from '@ngx-loading-bar/core';
import { NGXLogger } from 'ngx-logger';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AttachmentPreviewModelComponent } from 'src/app/shared/attachment-preview-model/attachment-preview-model.component';
import { HttpService } from '../http.service';
declare var saveAs: any;

@Component({
  selector: 'app-file-picker',
  templateUrl: './file-picker.component.html',
  styleUrls: ['./file-picker.component.scss']
})

export class FilePickerComponent implements OnChanges {
  @Input() labeltext: string;
  @Input() className: string;
  @Input() required: boolean;
  @Input() fileHandle: string;
  @Input() fpOptions: any;
  @Output() notify: EventEmitter<any> = new EventEmitter<any>();
  filedata: any = {};
  options: any = {};
  isUploaded: boolean = false;
  dlgOptions: NgbModalOptions = {
    keyboard: false,
    size: 'lg',
    backdrop: 'static',
  };
  api: string = 'attachmentdownload/';

  constructor(
    private filePickerService: FilePickerService,
    private httpService: HttpService,
    private loadingBar: LoadingBarService,
    private modalService: NgbModal,
    private logger: NGXLogger
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    let that = this;
    that.filePickerService.uploadDone.subscribe(files => {
      that.uploadDone(files);
    });
    if (changes.fileHandle.currentValue) {
      that.loadingBar.start();
      that.filePickerService.get(changes.fileHandle.currentValue)
        .then(function (fileMetData: any) {
          that.loadingBar.complete();
          that.filedata.FileHandle = fileMetData.handle;
          that.filedata.FileName = fileMetData.filename;
          that.filedata.FileSize = fileMetData.size;
          that.filedata.MimeType = fileMetData.mimetype;
          that.isUploaded = true;
        })
        .catch(function (reason) {

          that.filePickerService.getUnsecure(changes.fileHandle.currentValue)
            .then(function (fileMetData: any) {
              that.loadingBar.complete();
              that.filedata.FileHandle = fileMetData.handle;
              that.filedata.FileName = fileMetData.filename;
              that.filedata.FileSize = fileMetData.size;
              that.filedata.MimeType = fileMetData.mimetype;
              that.isUploaded = true;
            })
            .catch(function (reason) {
              that.loadingBar.complete();
              that.logger.log("Error on file meta data!");
            });
        });
    }
  }

  upload() {
    let that = this;
    if (that.fpOptions) {
      that.options = that.fpOptions;
    }
    that.filePickerService.open(that.options);
  }

  uploadDone(files: any) {
    let that = this;
    if (files.length == 1) {
      let fileMetData = files[0];
      that.filedata.FileHandle = fileMetData.handle;
      that.filedata.FileName = fileMetData.filename;
      that.filedata.FileSize = fileMetData.size;
      that.filedata.MimeType = fileMetData.mimetype;
      that.fileHandle = fileMetData.handle;
      that.isUploaded = true;
      that.notify.emit(that.filedata);
    }
  }

  download() {
    let that = this;
    that.filePickerService.download(that.filedata.FileHandle)
      .then((res: any) => {
        saveAs(res, that.filedata.FileName);
      }).catch(function (reason) {
        that.logger.log("Error on file removing!");
      });
  }

  remove() {
    let that = this;
    that.filedata = {};
    that.fileHandle = "";
    that.isUploaded = false;
    that.notify.emit(that.filedata);
  }

  removeFromFilePicker() {
    let that = this;
    that.filePickerService.remove(that.fileHandle)
      .then(function () {
        that.logger.log("File removed!");
        that.filedata = {};
        that.fileHandle = "";
        that.isUploaded = false;
        that.notify.emit(that.filedata);
      }).catch(function (reason) {
        that.logger.log("Error on file removing!");
      });
  }

  downloadAttachment(attachmentId) {
    let that = this;
    that.httpService
      .get(that.api + attachmentId)
      .subscribe((res: any) => {
        var blob = that.filePickerService.b64toBlob(res.Content, res.ContentType);
        saveAs(blob, res.FileName);
      });
  }

  openAttachmentPreviewModel(AttachmentId) {
    let that = this;
    that.httpService
      .get('GetAttachmentFileHandler/' + (AttachmentId ? AttachmentId : '0'))
      .subscribe((res: any) => {
        const modalRef = that.modalService.open(AttachmentPreviewModelComponent, that.dlgOptions);
        modalRef.componentInstance.FileHandle = res;
      });
   
  }

  preview() {
    let that = this;
    const modalRef = that.modalService.open(AttachmentPreviewModelComponent, that.dlgOptions);
    modalRef.componentInstance.FileHandle = that.filedata.FileHandle;
  }

}
