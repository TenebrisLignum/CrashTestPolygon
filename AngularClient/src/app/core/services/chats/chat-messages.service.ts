import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { LoadChatMessagesRequest } from '../../interfaces/dto/chats/LoadChatMessagesRequest';
import { SendChatMessageRequest } from '../../interfaces/dto/chats/SendChatMessageRequest';

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
        if (request.lastMessageId != null)
            params = params.set('lastMessageId', request.lastMessageId);

        return this._http.get(this.ApiUrl + '/load', { params: params });
    }

    send(request: SendChatMessageRequest): Observable<any> {
        return this._http.post(this.ApiUrl + '/send', request);
    }
}
