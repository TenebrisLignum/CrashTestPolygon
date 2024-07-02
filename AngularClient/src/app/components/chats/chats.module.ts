import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatsComponent } from './chats.component';
import { ChatRoomComponent } from './chat-room/chat-room.component';
import { ChatsRoutingModule } from './chats-routing.module';
import { ChatRoomsListComponent } from './chat-rooms-list/chat-rooms-list.component';


@NgModule({
    declarations: [
        ChatsComponent,
        ChatRoomComponent,
        ChatRoomsListComponent
    ],
    imports: [
        CommonModule,
        ChatsRoutingModule
    ]
})
export class ChatsModule { }
