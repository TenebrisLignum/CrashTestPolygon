import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingModule } from './components/landing/landing.module';
import { HeaderComponent } from './components/header/header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BlogsModule } from './components/blogs/blogs.module';

@NgModule({
	declarations: [
		AppComponent,
		HeaderComponent,
	],
	imports: [
		LandingModule,
		BrowserModule,
		BlogsModule,
		HttpClientModule,
		AppRoutingModule,
		FontAwesomeModule
        BrowserAnimationsModule,
        ToastrModule.forRoot()
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
