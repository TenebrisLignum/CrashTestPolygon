import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './components/landing/landing.component';
import { BlogsComponent } from './components/blogs/blogs.component';
import { LoginComponent } from './components/login/login.component';
import { LoginGuard } from './core/guards/login.guard';

const routes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'blogs', component: BlogsComponent },
    { path: 'login', component: LoginComponent, canActivate: [LoginGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
