import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingModule } from './components/landing/landing.module';
import { HeaderComponent } from './components/header/header.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BlogsModule } from './components/blogs/blogs.module';
import { LoginComponent } from './components/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { interceptorProviders } from './core/interceptors/interceptorProviders';
import { FeaturesComponent } from './components/features/features.component';
import { JwtModule } from '@auth0/angular-jwt';
import LocalStorageHelper from './core/helpers/localstorage.helper';
import { SignupComponent } from './components/sign-up/sign-up.component';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        LoginComponent,
        FeaturesComponent,
        SignupComponent
    ],
    imports: [
        LandingModule,
        BrowserModule,
        BlogsModule,
        HttpClientModule,
        AppRoutingModule,
        FontAwesomeModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        JwtModule.forRoot({
            config: {
                tokenGetter: LocalStorageHelper.getAccessToken,
                allowedDomains: ["localhost:44320"]
            }
        })
    ],
    providers: interceptorProviders,
    bootstrap: [AppComponent]
})
export class AppModule { }
