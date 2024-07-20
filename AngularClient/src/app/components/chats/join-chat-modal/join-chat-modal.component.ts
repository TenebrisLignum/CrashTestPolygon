import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-join-chat-modal',
    templateUrl: './join-chat-modal.component.html',
    styleUrl: './join-chat-modal.component.scss'
})
export class JoinChatModalComponent {
    @Output() dataChanged: EventEmitter<any> = new EventEmitter<any>();
    @Output() onCancel: EventEmitter<any> = new EventEmitter<any>();

    form: FormGroup;

    constructor() {
        this.form = new FormGroup({
            chatRoomName: new FormControl("", [Validators.required]),
            password: new FormControl("")
        });
    }

    submit() {
        if (!this.form.invalid)
            this.dataChanged.emit(this.form.value);
    }

    cancel() {
        this.form.reset();
        this.onCancel.emit();
    }
}
