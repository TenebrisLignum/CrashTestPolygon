import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class ChatRoomsService {
    private readonly ApiUrl = environment.urlAddress + '/api/chatrooms';

    constructor(
        private _http: HttpClient
    ) { }

    list(): Observable<any> {
        return this._http.get(this.ApiUrl + "/my-chats")
    }
}
