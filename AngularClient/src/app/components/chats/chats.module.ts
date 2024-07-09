import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsComponent } from './chats.component';
import { ChatRoomComponent } from './chat-room/chat-room.component';
import { ChatsRoutingModule } from './chats-routing.module';
import { ChatRoomsListComponent } from './chat-rooms-list/chat-rooms-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LargeSpinnerComponent } from '../../core/shared/large-spinner/large-spinner.component';
import { JoinChatModalComponent } from './join-chat-modal/join-chat-modal.component';


@NgModule({
    declarations: [
        ChatsComponent,
        ChatRoomComponent,
        ChatRoomsListComponent,
        JoinChatModalComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        ChatsRoutingModule,
        FontAwesomeModule,
        FormsModule,
        ReactiveFormsModule,
        LargeSpinnerComponent
    ]
})
export class ChatsModule { }
