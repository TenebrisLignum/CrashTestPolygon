import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-large-spinner',
    standalone: true,
    imports: [],
    templateUrl: './large-spinner.component.html',
    styleUrl: './large-spinner.component.scss'
})
export class LargeSpinnerComponent {
    @Input() title = 'Loading...';
}
