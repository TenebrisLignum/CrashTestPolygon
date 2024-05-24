import { Component } from '@angular/core';
import { faTelegram, faGithub } from '@fortawesome/free-brands-svg-icons';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrl: './header.component.scss'
})
export class HeaderComponent {
	faTelegram = faTelegram;
	faGitHub = faGithub;

	onTelegram() {
		window.location.href = 'https://t.me/facel_essesse_nce';
	}

	onGitHub() {
		window.location.href = 'https://github.com/TenebrisLignum';
	}
}
