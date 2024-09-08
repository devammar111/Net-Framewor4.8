import { Injectable, Output, EventEmitter } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from "../../../environments/environment";
declare var filestack: any;

@Injectable()
export class FilePickerService {
  filepicker: any;
  options: any;
  filepickerUnsecure: any;
  filepickerBaseUrl: string = 'https://www.filestackapi.com/api/file/';
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
      disableTransformer: true,
      onOpen: opened => {
        this.cookieService.delete('session');
      },
      onUploadDone: (pickerResponse: any) => {
        this.uploadDone.emit(pickerResponse.filesUploaded);
      }
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

  b64toBlob(b64Data, contentType, sliceSize?) {
    contentType = contentType || '';
    sliceSize = sliceSize || 512;

    let byteCharacters = atob(b64Data);
    let byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      let slice = byteCharacters.slice(offset, offset + sliceSize);

      let byteNumbers = new Array(slice.length);
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }

      let byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }
    let blob = new Blob(byteArrays, { type: contentType });
    return blob;
  }

  generateLink(fileHandle: string) {
    if (fileHandle && typeof fileHandle === 'string') {
      return this.filepickerBaseUrl + fileHandle + '?policy=' + environment.filePickerApi.policy + '&signature=' + environment.filePickerApi.signature;
    }
  }

  generateLinkPreview(fileHandle: string) {
    if (fileHandle && typeof fileHandle === 'string') {
      const returnUrl = "https://cdn.filestackcontent.com/preview/security=policy:" + environment.filePickerApi.policy + ",signature:" + environment.filePickerApi.signature + "/" + fileHandle;
      return returnUrl;
    }
  }

}
