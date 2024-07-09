import { Component, ViewChild } from '@angular/core';
import { ChatRoomsService } from '../../../core/services/chats/chat-rooms.service';
import { ChatRoomItemViewModel } from '../../../core/interfaces/view-models/chats/ChatRoomItemViewModel';
import { Router } from '@angular/router';
import { JoinChatRoomRequest } from '../../../core/interfaces/dto/chats/JoinChatRoomRequest';

@Component({
    selector: 'app-chat-rooms-list',
    templateUrl: './chat-rooms-list.component.html',
    styleUrl: './chat-rooms-list.component.scss'
})
export class ChatRoomsListComponent {
    @ViewChild('joinDialog') dialog: any;

    isLoaded: boolean = false;

    chats: ChatRoomItemViewModel[] = [];

    constructor(
        private _chatRoomsService: ChatRoomsService,
        private _router: Router
    ) { }

    ngOnInit() {
        this._loadMyChats();
    }

    enter(id: string) {
        this._redirectToChatRoom(id);
    }

    onJoin() {
        this.dialog.nativeElement.showModal();
    }

    join($event: any) {
        this._chatRoomsService.join($event as JoinChatRoomRequest).subscribe({
            next: (res) => {
                this._redirectToChatRoom(res.chatRoomId);
            },
            error: (err) => {

            }
        })
    }

    closeJoinModal() {
        this.dialog.nativeElement.close();
    }

    private _loadMyChats() {
        this._chatRoomsService.list().subscribe({
            next: (res: ChatRoomItemViewModel[]) => {
                this.chats = res;
                this.isLoaded = true;
            },
            error: (err) => {
                this.isLoaded = true;
            }
        })
    }

    private _redirectToChatRoom(id: string) {
        this._router.navigate(['chats/' + id]);
    }
}
