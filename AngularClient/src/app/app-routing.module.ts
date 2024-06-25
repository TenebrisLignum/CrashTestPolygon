import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { LoginComponent } from './components/login/login.component';
import { LoginGuard } from './core/guards/login.guard';
import { Error404Component } from './core/shared/error-404/error-404.component';
import { Error403Component } from './core/shared/error-403/error-403.component';
import { Error500Component } from './core/shared/error-500/error-500.component';
import { Error401Component } from './core/shared/error-401/error-401.component';

const routes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'blogs', loadChildren: () => import('./components/blogs/blogs.module').then(x => x.BlogsModule) },
    { path: 'login', component: LoginComponent, canActivate: [LoginGuard] },
    { path: 'unauthorized', component: Error401Component },
    { path: 'forbidden', component: Error403Component },
    { path: 'not-found', component: Error404Component },
    { path: 'server-error', component: Error500Component },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
