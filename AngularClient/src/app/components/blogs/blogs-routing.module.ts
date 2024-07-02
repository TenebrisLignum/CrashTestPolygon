import { RouterModule, Routes } from "@angular/router";
import { BlogsComponent } from "./blogs.component";
import { AddArticleComponent } from "./add-article/add-article.component";
import { NgModule } from "@angular/core";
import { ArticlesListComponent } from "./articles-list/articles-list.component";
import { AdminGuard } from "../../core/guards/admin.guard";
import { EditArticleComponent } from "./edit-article/edit-article.component";

const blogsRoutes: Routes = [
    {
        path: 'blogs',
        component: BlogsComponent,
        children: [
            { path: '', component: ArticlesListComponent },
            { path: 'add', component: AddArticleComponent, canActivate: [AdminGuard] },
            { path: 'edit/:id', component: EditArticleComponent, canActivate: [AdminGuard] }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(blogsRoutes)],
    exports: [RouterModule]
})

export class BlogsRoutingModule { }
