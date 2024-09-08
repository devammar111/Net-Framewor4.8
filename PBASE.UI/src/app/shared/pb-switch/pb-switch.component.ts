import { Component, Input, Output, EventEmitter, HostListener, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
const UI_SWITCH_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => PBSwitchComponent),
  multi: true
};
@Component({
  selector: 'pb-switch',
  template: `
  <div class="btn btn-group btn-block" >
  <div [class.btn-on]="checked===true" (click)="disabled ? $event.stopPropagation() : onYes();" [class.disabled]="disabled"  [class.btn-null]="checked===null||checked===false"  class="btn">Yes</div>
  <div [class.btn-null-active]="checked===null" (click)="disabled ? $event.stopPropagation() : onNull();" [class.disabled]="disabled" class="btn btn-null"></div>
  <div [class.btn-off]="checked===false" (click)="disabled ? $event.stopPropagation() : onNo();" [class.disabled]="disabled"  [class.btn-null]="checked===null||checked===true"  class="btn">No</div>
  </div>
  `,
  providers: [UI_SWITCH_CONTROL_VALUE_ACCESSOR]
})
export class PBSwitchComponent implements ControlValueAccessor {
  private onTouchedCallback = (v: any) => {
  };
  private onChangeCallback = (v: any) => {
  };
  private _checked: boolean;
  private _disabled: boolean;
  private _isNullable: boolean;
  @Input() set checked(v: boolean) {
    this._checked = v;
  }
  get checked() {
    return this._checked;
  }
  @Input() set isNullable(v: boolean) {
    this._isNullable = (v = "false" ? false : true);
  }
  get isNullable() {
    return this._isNullable;
  }
  @Input() set disabled(v: boolean) {
    this._disabled = v;
  };
  get disabled() {
    return this._disabled;
  }
  @Output() change = new EventEmitter<boolean>();
  onYes() {
    let that = this;
    if (that._disabled == false || that._disabled == null) {
      that._checked = true;
    }
    this.change.emit(this.checked);
    this.onChangeCallback(this.checked);
    this.onTouchedCallback(this.checked);

  }
  onNo() {
    let that = this;
    if (that._disabled == false || that._disabled == null) {
      that._checked = false;
    }
    this.change.emit(this.checked);
    this.onChangeCallback(this.checked);
    this.onTouchedCallback(this.checked);
  }
  onNull() {
    let that = this;
    if (that._disabled == false || that._disabled == null) {
      that._checked = null;
    }
    this.change.emit(this.checked);
    this.onChangeCallback(this.checked);
    this.onTouchedCallback(this.checked);
  }
  writeValue(obj: any): void {
    if (obj !== this.checked) {
      this.checked = obj;
    }
  }
  registerOnChange(fn: any) {
    this.onChangeCallback = fn;
  }
  registerOnTouched(fn: any) {
    this.onTouchedCallback = fn;
  }
}
