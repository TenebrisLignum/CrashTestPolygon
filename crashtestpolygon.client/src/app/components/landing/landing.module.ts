import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './landing.component';
import { HeroSectionComponent } from './hero-section/hero-section.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
	declarations: [
		LandingComponent,
		HeroSectionComponent
	],
	imports: [
		CommonModule,
		FontAwesomeModule
	],
	exports: [
		LandingComponent
	]
})
export class LandingModule { }
