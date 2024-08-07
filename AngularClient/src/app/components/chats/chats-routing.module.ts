import { NgModule } from '@angular/core';
import { ChatsComponent } from './chats.component';
import { RouterModule, Routes } from '@angular/router';
import { ChatRoomsListComponent } from './chat-rooms-list/chat-rooms-list.component';
import { ChatRoomComponent } from './chat-room/chat-room.component';

const chatsRoutes: Routes = [
    {
        path: 'chats',
        component: ChatsComponent,
        children: [
            { path: '', component: ChatRoomsListComponent },
            { path: ':id', component: ChatRoomComponent },
        ]
    }
];


@NgModule({
    imports: [RouterModule.forChild(chatsRoutes)],
    exports: [RouterModule]
})

export class ChatsRoutingModule { }