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
        return this._http.post(this.ApiUrl + '/login', authDto);
    }

    sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChangeSub.next(isAuthenticated);
    }

    logout = () => {
        localStorage.removeItem('role');
        localStorage.removeItem('token');
        this.sendAuthStateChangeNotification(false);
    }

    isLoggedIn(): boolean {
		return !!localStorage.getItem('token');
    }

    // TODO: CHANGE IT WHEN WE'LL ADD THE USERS REGISTRATION
    isAdmin() {
        return localStorage.getItem('role') == 'Administrator';
    }
}
