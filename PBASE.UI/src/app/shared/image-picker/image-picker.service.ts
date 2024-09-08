import { Injectable, Output, EventEmitter } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from "../../../environments/environment";
declare var filestack: any;

@Injectable()
export class ImagePickerService {
  filepicker: any;
  options: any;
  filepickerUnsecure: any;
  filepickerBaseUrl: string = 'https://www.filepicker.io/api/file/';
  transformOptions: any = {
    quality: {
      value: 80
    }
  };
  @Output() uploadDone: EventEmitter<any> = new EventEmitter<any>();
  constructor(private cookieService: CookieService) {
    var clientOption = {
      security: {
        policy: environment.filePickerApi.policy,
        signature: environment.filePickerApi.signature
      },
    }
    this.filepicker = filestack.init(environment.filePickerApi.key, clientOption);
    this.filepickerUnsecure = filestack.init(environment.filePickerApi.key);
    this.options = {
      fromSources: ['local_file_system'],
      storeTo: {
        location: environment.filePickerApi.location,
        path: environment.filePickerApi.path
      },
      disableTransformer: false,
      onOpen: opened => {
        this.cookieService.delete('session');
      },
      onUploadDone: (pickerResponse: any) => {
        this.uploadDone.emit(pickerResponse.filesUploaded);
      },
      accept:'image/*'
    };
  }

  open(options: any) {
    let that = this;
    that.options = Object.assign({}, that.options, options);
    that.filepicker.picker(that.options).open();
  }

  remove(fileHandle: string) {
    let that = this;
    const promise = new Promise((resolve, reject) => {
      that.filepicker.remove(fileHandle)
        .then((res: any) => {
          resolve(res);
        })
        .catch((err) => {
          reject(err);
        });
    });
    return promise;
  }

  get(fileHandle: string) {
    let that = this;
    const promise = new Promise((resolve, reject) => {
      that.filepicker.metadata(fileHandle)
        .then((fileMetData: any) => {
          resolve(fileMetData);
        })
        .catch((err) => {
          reject(err);
        });
    });
    return promise;
  }

  transform(url: string) {
    let that = this;
    const transformedUrl = that.filepicker.transform(url, that.transformOptions);
    return that.filepicker.storeURL(transformedUrl);
  }

  getUnsecure(fileHandle: string) {
    let that = this;
    const promise = new Promise((resolve, reject) => {
      that.filepickerUnsecure.metadata(fileHandle)
        .then((fileMetData: any) => {
          resolve(fileMetData);
        })
        .catch((err) => {
          reject(err);
        });
    });
    return promise;
  }

  download(fileHandle: string) {
    let that = this;
    const promise = new Promise((resolve, reject) => {
      that.filepicker.retrieve(fileHandle)
        .then((res: any) => {
          resolve(res);
        })
        .catch((err) => {
          reject(err);
        });
    });
    return promise;
  }
}
