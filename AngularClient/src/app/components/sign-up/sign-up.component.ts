import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth.service';
import { AlertService } from '../../core/services/alert.service';

@Component({
    selector: 'app-registration',
    templateUrl: './sign-up.component.html',
    styleUrl: './sign-up.component.scss'
})
export class SignupComponent {
    form: FormGroup;
    isRequestSent: boolean = false;

    constructor(
        private _authService: AuthService,
        private _alertService: AlertService,
        private _router: Router
    ) {
        this.form = new FormGroup({
            email: new FormControl("", [Validators.required]),
            username: new FormControl("", [Validators.required]),
            password: new FormControl("", [Validators.required]),
            confirmPassword: new FormControl("", [Validators.required])
        });
    }

    onSubmit() {
        this.isRequestSent = true;

        if (this.form.valid)
            this._authService.signup(this.form.value)
                .subscribe({
                    next: (res) => {
                        this._alertService.showSucsess("Welcome! Registration was sucsessful! Plese, log in now.");
                        this._router.navigate(['/login']);
                        this.isRequestSent = false;
                    },
                    error: (error) => {
                        this.isRequestSent = false;
                    }
                });
        else {
            this.isRequestSent = false;
            this._alertService.showWarning("Check your values");
        }
    }
}
