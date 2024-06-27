import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../core/services/alert.service';
import LocalStorageHelper from '../../core/helpers/localstorage.helper';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent {
    loginForm: FormGroup;
    isRequestSent: boolean = false;

    constructor(
        private _authService: AuthService,
        private _alertService: AlertService,
        private _router: Router
    ) {
        this.loginForm = new FormGroup({
            email: new FormControl("", [Validators.required]),
            password: new FormControl("", [Validators.required])
        });
    }

    onSubmit() {
        this.isRequestSent = true;
        if (this.loginForm.valid)
            this._authService.login(this.loginForm.value)
                .subscribe({
                    next: (res) => {
                        debugger
                        // TODO: CHANGE IT WHEN WE'LL ADD THE USERS REGISTRATION

                        LocalStorageHelper.set('role', 'Administrator')
                        LocalStorageHelper.setAccessToken(res.token);
                        this._alertService.showSucsess("Welcome!");
                        this._authService.sendAuthStateChangeNotification(true);

                        this._router.navigate(['/']);
                        this.isRequestSent = false;

                    },
                    error: (error) => {
                        debugger

                        // this._alertService.showError(error.error.title);

                        this.isRequestSent = false;
                    }
                });
        else {
            this.isRequestSent = false;
            this._alertService.showWarning("Check your values");
        }
    }
}
