import { Component } from '@angular/core';
import { faEnvelope, faLocationDot, faSuitcase, faLink, faDownload } from '@fortawesome/free-solid-svg-icons';

@Component({
	selector: 'app-hero-section',
	templateUrl: './hero-section.component.html',
	styleUrl: './hero-section.component.scss'
})
export class HeroSectionComponent {
	faEnvelope = faEnvelope;
	faLocationDot = faLocationDot;
	faSuitcase = faSuitcase;
	faLink = faLink;
	faDownload = faDownload;
}
