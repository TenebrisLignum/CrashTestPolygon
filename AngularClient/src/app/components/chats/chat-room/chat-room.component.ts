import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatMessagesService } from '../../../core/services/chats/chat-messages.service';
import { LoadChatMessagesRequest } from '../../../core/interfaces/dto/chats/LoadChatMessagesRequest';
import { ChatMessagesViewModel, ChatMessageViewModel } from '../../../core/interfaces/view-models/chats/ChatMessagesViewModel';
import { ChatRoomsService } from '../../../core/services/chats/chat-rooms.service';
import { ChatRoomDetailViewModel } from '../../../core/interfaces/view-models/chats/ChatRoomDetailViewModel';
import { AlertService } from '../../../core/services/alert.service';
import { faPaperPlane } from '@fortawesome/free-solid-svg-icons';
import { SendChatMessageRequest } from '../../../core/interfaces/dto/chats/SendChatMessageRequest';
import { ChatRoomHub } from '../../../core/hubs/chat-room-hub';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-chat-room',
    templateUrl: './chat-room.component.html',
    styleUrl: './chat-room.component.scss'
})
export class ChatRoomComponent {
    @ViewChild('messagesContainer') private messagesContainer: ElementRef;

    private messageSubscription: Subscription;

    sendIcon = faPaperPlane;

    chatId: string;
    chatMessagesVM: ChatMessagesViewModel = { messages: {}, lastMessageId: "" } as ChatMessagesViewModel;
    chatRoomDetails: ChatRoomDetailViewModel;

    messageToSend: string;

    isChatRoomLoaded: boolean = false;
    isFirstMessagesLoaded: boolean = false;
    isMessagesOver: boolean = false;
    isScrolledToBottom: boolean = false;

    isRequestSent: boolean = false;
    isMessageRequestSent: boolean = false;

    constructor(
        private _chatMessagesServie: ChatMessagesService,
        private _chatRoomsService: ChatRoomsService,
        private _chatRoomHub: ChatRoomHub,
        private _route: ActivatedRoute,
        private _router: Router,
        private _alertService: AlertService
    ) { }

    ngOnInit() {
        this.chatId = this._route.snapshot.paramMap.get('id') as string;
        this._chatRoomHub.startConnection();
        this._chatRoomHub.joinChatRoom(this.chatId);
        this.messageSubscription = this._chatRoomHub.message$.subscribe(
            (message: ChatMessageViewModel) => {
                this.chatMessagesVM.messages.push(message);
            }
        );

        this._loadChatDetails(this.chatId);
    }

    ngAfterViewChecked() {
        this._scrollToBottom();
    }

    ngOnDestroy(): void {
        if (this.messageSubscription) {
            this.messageSubscription.unsubscribe();
        };
        this._chatRoomHub.leaveChatRoom(this.chatId);
    }

    send() {
        this.isMessageRequestSent = true;

        let request = { text: this.messageToSend, chatRoomId: this.chatId } as SendChatMessageRequest;
        this._chatMessagesServie.send(request).subscribe({
            next: (res) => {
                this.messageToSend = '';
                this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
                this.isMessageRequestSent = false;
            }
        })
    }

    loadNextMessages() {
        this._loadMessages(this.chatId, this.chatMessagesVM.lastMessageId);
    }

    @HostListener('scroll', ['$event'])
    onScroll($event: any): void {
        if (this.messagesContainer.nativeElement.scrollTop < 300) {
            this._loadMessages(this.chatId, this.chatMessagesVM.lastMessageId);
        }
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

    private _loadMessages(chatRoomId: string, lastMessageId: string | null = null) {
        if (this.isMessagesOver || this.isRequestSent)
            return;

        this.isRequestSent = true;

        let params = { chatRoomId: chatRoomId, lastMessageId: lastMessageId } as LoadChatMessagesRequest;
        this._chatMessagesServie.load(params).subscribe({
            next: (res: ChatMessagesViewModel) => {
                if (res.messages.length == 0) {
                    this.isMessagesOver = true;
                    return;
                };

                if (!this.isFirstMessagesLoaded) {
                    this.chatMessagesVM = res;
                    this.isFirstMessagesLoaded = true;
                }
                else {
                    this.chatMessagesVM.messages = res.messages.concat(this.chatMessagesVM.messages);
                    this.chatMessagesVM.lastMessageId = res.lastMessageId;
                }

                this.isRequestSent = false;
            },
            error: (err) => {
                this.isRequestSent = false;
            }
        });
    }

    private _scrollToBottom() {
        if (!this.isScrolledToBottom) {
            this.messagesContainer.nativeElement.scrollTop = this.messagesContainer.nativeElement.scrollHeight;
            this.isScrolledToBottom = true;
        }
    }
}
