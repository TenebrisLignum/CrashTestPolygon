import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsComponent } from './chats.component';
import { ChatRoomComponent } from './chat-room/chat-room.component';
import { ChatsRoutingModule } from './chats-routing.module';
import { ChatRoomsListComponent } from './chat-rooms-list/chat-rooms-list.component';
import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule } from '@angular/forms';
import { LargeSpinnerComponent } from '../../core/shared/large-spinner/large-spinner.component';


@NgModule({
    declarations: [
        ChatsComponent,
        ChatRoomComponent,
        ChatRoomsListComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        ChatsRoutingModule,
        FontAwesomeModule,
        FormsModule,
        LargeSpinnerComponent
    ]
})
export class ChatsModule { }
