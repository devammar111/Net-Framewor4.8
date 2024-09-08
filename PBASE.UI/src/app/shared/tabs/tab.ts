export class Tab {
    private _active: boolean;
  constructor(private _id: number = -1, public title?: String, public path?: any, public params: any = {}, public isShow: Boolean = true) {
    }

    set active(active: boolean) {
        this._active = active;
    }

    get active() {
        return this._active;
    }

    get id() {
        return this._id;
    }

    set id(id: number) {
        this._id = id;
    }
}
