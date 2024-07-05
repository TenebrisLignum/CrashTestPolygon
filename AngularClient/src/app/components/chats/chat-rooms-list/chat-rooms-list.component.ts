import { Component } from '@angular/core';
import { ChatRoomsService } from '../../../core/services/chats/chat-rooms.service';
import { ChatRoomItemViewModel } from '../../../core/interfaces/view-models/chats/ChatRoomItemViewModel';
import { JoinChatRoomRequest } from '../../../core/interfaces/dto/chats/JoinChatRoomRequest';
import { Router } from '@angular/router';

@Component({
    selector: 'app-chat-rooms-list',
    templateUrl: './chat-rooms-list.component.html',
    styleUrl: './chat-rooms-list.component.scss'
})
export class ChatRoomsListComponent {

    chats: ChatRoomItemViewModel[] = [];

    constructor(
        private _chatRoomsService: ChatRoomsService,
        private _router: Router
    ) { }

    ngOnInit() {
        this._loadMyChats();
    }

    enter(id: string) {
        this._router.navigate(['chats/' + id]);
    }

    private _loadMyChats() {
        this._chatRoomsService.list().subscribe({
            next: (res: ChatRoomItemViewModel[]) => {
                this.chats = res;
            },
            error: (err) => {

            }
        })
    }
}
