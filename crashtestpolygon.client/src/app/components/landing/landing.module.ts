import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './landing.component';
import { HeroSectionComponent } from './hero-section/hero-section.component';

@NgModule({
	declarations: [
		LandingComponent,
		HeroSectionComponent
	],
	imports: [
		CommonModule
	],
	exports: [
		LandingComponent
	]
})
export class LandingModule { }
