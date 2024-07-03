import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { JoinChatRoomRequest } from '../../interfaces/dto/chats/JoinChatRoomRequest';

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

    join(request: JoinChatRoomRequest): Observable<any> {
        return this._http.post(this.ApiUrl + '/join', request)
    }
}
