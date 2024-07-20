import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { AuthDto } from '../../interfaces/dto/auth/AuthDto';
import { SignUpDto } from '../../interfaces/dto/auth/SignUpDto';
import JWTHelper from '../../helpers/jwt.helper';
import { ACCESS_TONKEN_KEY, ADMINISTRATOR_ROLE_STRING } from '../../../../consts';

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

    signup(sigUpDto: SignUpDto): Observable<any> {
        return this._http.post(this.ApiUrl + '/signup', sigUpDto);

    }

    logout = () => {
        localStorage.removeItem(ACCESS_TONKEN_KEY);
        this.sendAuthStateChangeNotification(false);
    }

    sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
        this.authChangeSub.next(isAuthenticated);
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem(ACCESS_TONKEN_KEY);
    }

    isAdmin() {
        return JWTHelper.isUserInRole(ADMINISTRATOR_ROLE_STRING);
    }
}