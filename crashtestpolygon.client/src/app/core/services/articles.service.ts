import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateArticleDto } from '../interfaces/dto/articles/CreateArticleDto';
import { UpdateArticleDto } from '../interfaces/dto/articles/UpdateArticleDto';

@Injectable({
    providedIn: 'root'
})
export class ArticlesService {
    private readonly ApiUrl = environment.urlAddress + '/api/articles';

    constructor(private _http: HttpClient) { }

    get(id: number): Observable<any> {
        return this._http.get(this.ApiUrl + '?id=' + id.toString())
    }

    list(): Observable<any> {
        return this._http.get(this.ApiUrl + '/list');
    }

    create(body: CreateArticleDto): Observable<any> {
        return this._http.post(this.ApiUrl, body);
    }

    update(body: UpdateArticleDto): Observable<any> {
        return this._http.put(this.ApiUrl, body);
    }

    delete(id: number): Observable<any> {
        return this._http.delete(this.ApiUrl + '?id=' + id.toString());
    }

}
