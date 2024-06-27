import { Component } from '@angular/core';
import { faTelegram, faGithub } from '@fortawesome/free-brands-svg-icons';
import { AuthService } from '../../core/services/auth/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss'
})
export class HeaderComponent {
    faTelegram = faTelegram;
    faGitHub = faGithub;

    isUserAuthenticated: boolean = false;

    constructor(
        private _authService: AuthService
    ) { }

    ngOnInit() {
        this.isUserAuthenticated = this._authService.isLoggedIn();
        this._authSubscribe();
    }

    onTelegram() {
        window.location.href = 'https://t.me/facel_essesse_nce';
    }

    onGitHub() {
        window.location.href = 'https://github.com/TenebrisLignum';
    }

    logout() {
        this._authService.logout()
        window.location.reload();
    }

    private _authSubscribe() {
        this._authService.authChanged
            .subscribe(res => {
                this.isUserAuthenticated = res;
            });
    }
}
