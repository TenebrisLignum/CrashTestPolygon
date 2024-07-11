import { Component, ViewChild } from '@angular/core';
import { ChatRoomsService } from '../../../core/services/chats/chat-rooms.service';
import { ChatRoomItemViewModel } from '../../../core/interfaces/view-models/chats/ChatRoomItemViewModel';
import { Router } from '@angular/router';
import { JoinChatRoomRequest } from '../../../core/interfaces/dto/chats/JoinChatRoomRequest';
import { CreateChatRoomRequest } from '../../../core/interfaces/dto/chats/CreateChatRoomRequest';
import { AlertService } from '../../../core/services/alert.service';

@Component({
    selector: 'app-chat-rooms-list',
    templateUrl: './chat-rooms-list.component.html',
    styleUrl: './chat-rooms-list.component.scss'
})
export class ChatRoomsListComponent {
    @ViewChild('joinDialog') joinDialog: any;
    @ViewChild('createDialog') createDialog: any;

    isLoaded: boolean = false;

    chats: ChatRoomItemViewModel[] = [];

    constructor(
        private _chatRoomsService: ChatRoomsService,
        private _router: Router,
        private _alertService: AlertService
    ) { }

    ngOnInit() {
        this._loadMyChats();
    }

    enter(id: string) {
        this._redirectToChatRoom(id);
    }

    onJoin() {
        this.joinDialog.nativeElement.showModal();
    }

    join($event: any) {
        this._chatRoomsService.join($event as JoinChatRoomRequest).subscribe({
            next: (res) => {
                this._redirectToChatRoom(res.chatRoomId);
                this._alertService.showSucsess("Welcome!");
            },
            error: (err) => {

            }
        });
    }

    closeJoinModal() {
        this.joinDialog.nativeElement.close();
    }

    onCreate() {
        this.createDialog.nativeElement.showModal();
    }

    create($event: any) {
        this._chatRoomsService.create($event as CreateChatRoomRequest).subscribe({
            next: (res) => {
                this._redirectToChatRoom(res.chatRoomId);
                this._alertService.showSucsess("Chat room was successfully created!");
            },
            error: (err) => {

            }
        });
    }

    closeCreateModal() {
        this.createDialog.nativeElement.close();
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
