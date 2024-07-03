import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChatMessagesService } from '../../../core/services/chats/chat-messages.service';
import { LoadChatMessagesRequest } from '../../../core/interfaces/dto/chats/LoadChatMessagesRequest';
import { ChatMessagesViewModel } from '../../../core/interfaces/view-models/chats/ChatMessagesViewModel';

@Component({
    selector: 'app-chat-room',
    templateUrl: './chat-room.component.html',
    styleUrl: './chat-room.component.scss'
})
export class ChatRoomComponent {

    chatId: string;
    chatMessagesViewModel: ChatMessagesViewModel;

    constructor(
        private _chatMessagesServie: ChatMessagesService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.chatId = this._route.snapshot.paramMap.get('id') as string;
        this._loadMessages(this.chatId);
    }

    private _loadMessages(chatRoomId: string, page: number = 1) {
        let params = { chatRoomId: chatRoomId, page: page } as LoadChatMessagesRequest;
        this._chatMessagesServie.load(params).subscribe({
            next: (res: ChatMessagesViewModel) => {
                this.chatMessagesViewModel = res;
                debugger
            },
            error: (err) => {

            }
        });
    }
}
