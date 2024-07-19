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
    private connectionPromise: Promise<void>;

    private messageSubject = new Subject<ChatMessageViewModel>();
    public message$: Observable<ChatMessageViewModel> = this.messageSubject.asObservable();

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.urlAddress + '/chat-room-hub', { withCredentials: false })
            .build();

        this.hubConnection.on('ReceiveMessage', (message: ChatMessageViewModel) => {
            this.messageSubject.next(message);
        });

        this.connectionPromise = this.startConnection();
    }

    startConnection(): Promise<void> {
        return this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => {
                console.log('Error while starting connection: ' + err);
                return Promise.reject(err);
            });
    }

    joinChatRoom(chatRoomId: string): void {
        this.connectionPromise
            .then(() => this.hubConnection.invoke('JoinChatRoom', chatRoomId))
            .catch(err => console.error('Error while joining chat room: ' + err));
    }

    leaveChatRoom(chatRoomId: string): void {
        this.connectionPromise
            .then(() => this.hubConnection.invoke('LeaveChatRoom', chatRoomId))
            .catch(err => console.error('Error while leaving chat room: ' + err));
    }
}
