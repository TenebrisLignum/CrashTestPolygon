import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthDto } from '../../interfaces/dto/auth/AuthDto';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly ApiUrl = environment.urlAddress;

    constructor(
        private _http: HttpClient
    ) { }

    login(authDto: AuthDto): Observable<any> {
        return this._http.post(this.ApiUrl + '/login', authDto);
    }

    refresh(refreshToken: string): Observable<any> {
        let body = { refreshToken: refreshToken }
		return this._http.post(this.ApiUrl + '/refresh', body);
    }
}
