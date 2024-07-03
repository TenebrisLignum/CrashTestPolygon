import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { LoadChatMessagesRequest } from '../../interfaces/dto/chats/LoadChatMessagesRequest';

@Injectable({
    providedIn: 'root'
})
export class ChatMessagesService {
    private readonly ApiUrl = environment.urlAddress + '/api/chatmessages';

    constructor(
        private _http: HttpClient
    ) { }

    load(request: LoadChatMessagesRequest): Observable<any> {
        let params = new HttpParams();

        params = params.set('chatRoomId', request.chatRoomId);
        params = params.set('page', request.page ?? 1);

        return this._http.get(this.ApiUrl + '/load', { params: params });
    }
}
