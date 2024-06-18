import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor(
        private _toastr: ToastrService
    ) { }

    showSucsess(description: string, title = "Success!") {
        this._toastr.success(description, title, { timeOut: 5000 });
    }

    showInfo(description: string, title = "Pay attention!") {
        this._toastr.info(description, title, { timeOut: 5000 });
    }

    showWarning(description: string, title = "Warning!") {
        this._toastr.warning(description, title, { timeOut: 5000 });
    }

    showError(description: string, title = "Error!") {
        this._toastr.error(description, title, { timeOut: 5000 });
    }
}
