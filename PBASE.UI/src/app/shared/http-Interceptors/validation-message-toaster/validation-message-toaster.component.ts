import { Component, Input } from '@angular/core';
import { Toast, ToastrService, ToastPackage } from 'ngx-toastr';
import { trigger, state, style } from '@angular/animations';

@Component({
  selector: '[pink-toast-component]',
  templateUrl: './validation-message-toaster.component.html',
  animations: [
    trigger('flyInOut', [
      state('inactive', style({
        display: 'none',
        opacity: 0
      })),
    ]),
  ],
  preserveWhitespaces: false,
})
export class ValidationMessageToasterComponent extends Toast {
  // used for demo purposes
  undoString = 'undo';
  show: boolean[] = [];
  showButton: boolean[] = [];
  messages: any = [];
  isMessage = false;

  // constructor is only necessary when not using AoT
  constructor(
    protected toastrService: ToastrService,
    public toastPackage: ToastPackage,
  ) {
    super(toastrService, toastPackage);
    let message = this.toastPackage.message;
    if (typeof (message) == 'string') {
      this.isMessage = true;
    }
  }

  formate(value, index) {
    let that = this;
    let msgArr = value.split("_");
    if (msgArr.length > 1) {
      that.showButton[index] = true;
    }
    return msgArr[0];
  }

  expand(index, value, message, event) {
    let that = this;
    event.stopPropagation();
    that.show[index] = !that.show[index];
    let msgArr = value.split("_");
    if (this.messages.length <= 0) {
      message.forEach(function (msg) {
        that.messages.push({ message: msg });
      });
    }
    if (msgArr.length > 1) {
      that.messages[index].AppInvalid = msgArr[1];
      that.messages[index].TextInvalid = msgArr[2];
    }
  }

  stop(event) {
    event.stopPropagation();
  }

}
