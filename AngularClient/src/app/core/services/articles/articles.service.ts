import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateArticleDto } from '../../interfaces/dto/articles/CreateArticleDto';
import { UpdateArticleDto } from '../../interfaces/dto/articles/UpdateArticleDto';
import { GetArticlesByFilterQueryDto } from '../../interfaces/dto/articles/GetArticlesByFilterQueryDto';

@Injectable({
    providedIn: 'root'
})
export class ArticlesService {
    private readonly ApiUrl = environment.urlAddress + '/api/articles';

    constructor(private _http: HttpClient) { }

    get(id: number): Observable<any> {
        return this._http.get(this.ApiUrl + '?id=' + id.toString())
    }

    list(filter: GetArticlesByFilterQueryDto): Observable<any> {
        let params = new HttpParams();

        if (filter.searchWord) {
            params = params.set('searchWord', filter.searchWord);
        }
        if (filter.pageSize) {
            params = params.set('pageSize', filter.pageSize.toString());
        }
        if (filter.page) {
            params = params.set('page', filter.page.toString());
        }
        return this._http.get(this.ApiUrl + '/list', { params });
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
