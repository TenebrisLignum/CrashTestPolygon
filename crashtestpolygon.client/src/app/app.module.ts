import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './components/landing/landing.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HeroSectionComponent } from './components/landing/hero-section/hero-section.component';
import { LandingModule } from './components/landing/landing.module';
import { HeaderComponent } from './components/header/header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
  HeaderComponent
	],
	imports: [
		LandingModule,
		BrowserModule,
		HttpClientModule,
		AppRoutingModule,
  FontAwesomeModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
