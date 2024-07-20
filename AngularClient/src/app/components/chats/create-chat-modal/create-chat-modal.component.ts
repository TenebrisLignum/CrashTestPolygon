import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-create-chat-modal',
    templateUrl: './create-chat-modal.component.html',
    styleUrl: './create-chat-modal.component.scss'
})
export class CreateChatModalComponent {
    @Output() dataChanged: EventEmitter<any> = new EventEmitter<any>();
    @Output() onCancel: EventEmitter<any> = new EventEmitter<any>();

    form: FormGroup;

    constructor() {
        this.form = new FormGroup({
            name: new FormControl("", [Validators.required]),
            isPrivate: new FormControl(false),
            password: new FormControl("")
        });
    }

    ngOnInit() {
        this.form.get('isPrivate')?.valueChanges.subscribe((isPrivate: boolean) => {
            const passwordControl = this.form.get('password');
            if (isPrivate) {
                passwordControl?.setValidators([Validators.required]);
            } else {
                passwordControl?.clearValidators();
            }
            passwordControl?.updateValueAndValidity();
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
