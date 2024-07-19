import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { ChatMessageViewModel } from '../interfaces/view-models/chats/ChatMessagesViewModel';
import { Observable, Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ChatRoomHub {
    private hubConnection: signalR.HubConnection;

    private messageSubject = new Subject<ChatMessageViewModel>();
    public message$: Observable<ChatMessageViewModel> = this.messageSubject.asObservable();

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.urlAddress + '/chat-room-hub', { withCredentials: false })
            .build();

        this.hubConnection.on('ReceiveMessage', (message: ChatMessageViewModel) => {
            this.messageSubject.next(message);
        });
    }

    startConnection(): void {
        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err));
    }
}
