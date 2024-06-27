import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AlertService } from '../services/alert.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(
        private _router: Router,
        private _alertService: AlertService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((error: HttpErrorResponse) => {
                let errorMessage: string;
                switch (error.status) {
                    case 400:
                        debugger
                        this._alertService.showError(error.error.detail);
                        console.error('Bad Request - 400 error', error);
                        errorMessage = 'Access forbidden. Please contact support if you believe this is an error.';
                        break;
                    case 403:
                        this._router.navigate(['/forbidden']);
                        console.error('Access forbidden - 403 error', error);
                        errorMessage = 'Access forbidden. Please contact support if you believe this is an error.';
                        break;
                    case 404:
                        this._router.navigate(['/not-found']);
                        console.error('Not found - 404 error', error);
                        errorMessage = 'Not found. Please contact support if you believe this is an error.';
                        break;
                    case 500:
                        this._router.navigate(['/server-error']);
                        console.error('Internal server error - 500 error', error);
                        errorMessage = 'An unexpected error occurred. Please try again later.';
                        break;
                    default:
                        console.error('HTTP error', error);
                        errorMessage = error.message;
                        break;
                }
                return throwError(errorMessage);
            })
        );
    }
}
