import { Component, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatMessagesService } from '../../../core/services/chats/chat-messages.service';
import { LoadChatMessagesRequest } from '../../../core/interfaces/dto/chats/LoadChatMessagesRequest';
import { ChatMessagesViewModel } from '../../../core/interfaces/view-models/chats/ChatMessagesViewModel';
import { ChatRoomsService } from '../../../core/services/chats/chat-rooms.service';
import { ChatRoomDetailViewModel } from '../../../core/interfaces/view-models/chats/ChatRoomDetailViewModel';
import { AlertService } from '../../../core/services/alert.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { SendChatMessageRequest } from '../../../core/interfaces/dto/chats/SendChatMessageRequest';

@Component({
    selector: 'app-chat-room',
    templateUrl: './chat-room.component.html',
    styleUrl: './chat-room.component.scss'
})
export class ChatRoomComponent {
    @ViewChild('messagesContainer') private messagesContainer: ElementRef;

    sendIcon = faPaperPlane;

    chatId: string;
    chatMessagesViewModel: ChatMessagesViewModel;
    chatRoomDetails: ChatRoomDetailViewModel;

    messageToSend: string;

    isChatRoomLoaded: boolean = false;
    isFirstMessagesLoaded: boolean = false;

    constructor(
        private _chatMessagesServie: ChatMessagesService,
        private _chatRoomsService: ChatRoomsService,
        private _route: ActivatedRoute,
        private _router: Router,
        private _alertService: AlertService
    ) { }

    ngOnInit() {
        this.chatId = this._route.snapshot.paramMap.get('id') as string;
        this._loadChatDetails(this.chatId);
    }

    ngAfterViewChecked() {
        this._scrollToBottom();
    }

    send() {
        let request = { text: this.messageToSend, chatRoomId: this.chatId } as SendChatMessageRequest;
        this._chatMessagesServie.send(request).subscribe({
            next: (res) => {
                this.messageToSend = '';
            },
            error: (err) => {

            }
        })
    }

    private _loadChatDetails(id: string) {
        this._chatRoomsService.get(id).subscribe({
            next: (res: ChatRoomDetailViewModel) => {
                this.chatRoomDetails = res;
                this.isChatRoomLoaded = true;
                this._loadMessages(this.chatId);
            },
            error: (err) => {
                this._alertService.showError(err);
                this._router.navigateByUrl('/');
            }
        });
    }

    private _loadMessages(chatRoomId: string, page: number = 1) {
        let params = { chatRoomId: chatRoomId, page: page } as LoadChatMessagesRequest;
        this._chatMessagesServie.load(params).subscribe({
            next: (res: ChatMessagesViewModel) => {
                this.chatMessagesViewModel = res;
                this.isFirstMessagesLoaded = true;
            },
            error: (err) => {

            }
        });
    }

    private _scrollToBottom() {
        try {
            this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
        } catch (err) { }
    }
}
