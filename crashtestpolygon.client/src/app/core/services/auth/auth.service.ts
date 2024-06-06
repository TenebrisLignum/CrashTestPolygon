import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { AuthDto } from '../../interfaces/dto/auth/AuthDto';
import LocalStorageHelper from '../../helpers/localstorage.helper';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly ApiUrl = environment.urlAddress;

    private authChangeSub = new Subject<boolean>()
    public authChanged = this.authChangeSub.asObservable();

    constructor(
        private _http: HttpClient
    ) { }

    login(authDto: AuthDto): Observable<any> {
        // TODO: CHANGE IT WHEN WE'LL ADD THE USERS REGISTRATION

        LocalStorageHelper.set('role', 'Administrator')
        return this._http.post(this.ApiUrl + '/login', authDto);
    }

    refresh(refreshToken: string): Observable<any> {
        let body = { refreshToken: refreshToken }
        return this._http.post(this.ApiUrl + '/refresh', body);
    }

    sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChangeSub.next(isAuthenticated);
    }

    logout = () => {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh_token');
        localStorage.removeItem('role');
        this.sendAuthStateChangeNotification(false);
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('access_token');
    }

    // TODO: CHANGE IT WHEN WE'LL ADD THE USERS REGISTRATION
    isAdmin() {
        return localStorage.getItem('role') == 'Administrator';
    }
}
