import { Component, OnChanges, SimpleChanges, Input, Output, EventEmitter } from '@angular/core';
import { LoadingBarService } from '@ngx-loading-bar/core';
import { NGXLogger } from 'ngx-logger';
import { ImagePickerService } from './image-picker.service';
declare var saveAs: any;
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AttachmentPreviewModelComponent } from '../attachment-preview-model/attachment-preview-model.component';

@Component({
  selector: 'app-image-picker',
  templateUrl: './image-picker.component.html',
  styleUrls: ['./image-picker.component.css']
})
export class ImagePickerComponent implements OnChanges {

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

  constructor(private imagePickerService: ImagePickerService,
    private loadingBar: LoadingBarService,
    private sanitizer: DomSanitizer,
    private modalService: NgbModal,
    private logger: NGXLogger) { }

  ngOnChanges(changes: SimpleChanges) {
    let that = this;
    that.imagePickerService.uploadDone.subscribe(files => {
      that.uploadDone(files);
    });
    if (changes.fileHandle.currentValue) {
      that.loadingBar.start();
      that.imagePickerService.get(changes.fileHandle.currentValue)
        .then(function (fileMetData: any) {
          that.loadingBar.complete();
          that.filedata.FileHandle = fileMetData.handle;
          that.filedata.FileName = fileMetData.filename;
          that.filedata.FileSize = fileMetData.size;
          that.filedata.MimeType = fileMetData.mimetype;
          that.isUploaded = true;
        })
        .catch(function (reason) {
          that.imagePickerService.getUnsecure(changes.fileHandle.currentValue)
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
    that.imagePickerService.open(that.options);
  }

  uploadDone(files: any) {
    let that = this;
    if (files.length == 1) {
      let fileMetData = files[0];
      that.imagePickerService.transform(fileMetData.url)
        .then((metaData: any) => {
          fileMetData = metaData;
          that.filedata.FileHandle = fileMetData.handle;
          that.filedata.FileName = fileMetData.filename;
          that.filedata.FileSize = fileMetData.size;
          that.filedata.MimeType = fileMetData.mimetype;
          that.fileHandle = fileMetData.handle;
          that.isUploaded = true;
          that.notify.emit(that.filedata);
        })
    }
  }

  download() {
    let that = this;
    that.imagePickerService.download(that.filedata.FileHandle)
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
    that.imagePickerService.remove(that.fileHandle)
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

  preview() {
    let that = this;
    const modalRef = that.modalService.open(AttachmentPreviewModelComponent, that.dlgOptions);
    modalRef.componentInstance.FileHandle = that.filedata.FileHandle;
  }

}

