import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateArticleDto } from '../interfaces/dto/articles/CreateArticleDto';

@Injectable({
    providedIn: 'root'
})
export class ArticlesService {
    private readonly ApiUrl = environment.urlAddress + '/api/articles';

    constructor(private _http: HttpClient) { }

    list(): Observable<any> {
        return this._http.get(this.ApiUrl + '/list');
    }

    create(body: CreateArticleDto): Observable<any> {
        return this._http.post(this.ApiUrl, body);
    }

}
