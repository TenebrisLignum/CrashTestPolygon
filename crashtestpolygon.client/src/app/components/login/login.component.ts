import { Component } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../core/services/alert.service';
import { AuthResponseDto } from '../../core/interfaces/dto/auth/AuthResponseDto';

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
        private _alertService: AlertService
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
                    next: (res: AuthResponseDto) => {
                        this.isRequestSent = false;
                        this._alertService.showSucsess("Welcome!");

                        this._addToLocalStorage("access_token", res.accessToken);
                    },
                    error: (error) => {
                        console.log(error);
                        this.isRequestSent = false;

                        this._alertService.showError(error.error.title);
                    }
                });
        else {
            this.isRequestSent = false;
            this._alertService.showWarning("Check your values");
        }
    }

    // TODO: create local storage helper
    private _addToLocalStorage(key: string, value: string) {
        localStorage.setItem(key, value)
    }
}
