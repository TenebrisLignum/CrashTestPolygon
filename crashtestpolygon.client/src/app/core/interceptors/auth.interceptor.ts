import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import LocalStorageHelper from '../helpers/localstorage.helper';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(
        private _authService: AuthService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = LocalStorageHelper.getAccessToken();
        if (accessToken)
            req = this._addToken(req, accessToken);

        return next.handle(req).pipe(
            catchError((error) => {
                if (error instanceof HttpErrorResponse && error.status === 401) {
                    return this.handle401Error(req, next);
                } else {
                    return throwError(error);
                }
            })
        );
    }

    private _addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            let refresh_token = LocalStorageHelper.get('refresh_token') ?? '';

            return this._authService.refresh(refresh_token).pipe(
                switchMap((token: any) => {
                    this.isRefreshing = false;
                    LocalStorageHelper.updateTokens(token);
                    this.refreshTokenSubject.next(token.accessToken);
                    return next.handle(this._addToken(request, token.accessToken));
                }),
                catchError((error) => {
                    this.isRefreshing = false;
                    return throwError(error);
                })
            );
        } else {
            return this.refreshTokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(jwt => {
                    return next.handle(this._addToken(request, jwt));
                })
            );
        }
    }
}
