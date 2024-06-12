import { NgModule } from '@angular/core';
import { BlogsComponent } from './blogs.component';
import { BrowserModule } from '@angular/platform-browser';
import { ArticleItemComponent } from './article-item/article-item.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AddArticleComponent } from './add-article/add-article.component';
import { BlogsRoutingModule } from './blogs-routing.module';
import { ArticlesListComponent } from './articles-list/articles-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EditArticleComponent } from './edit-article/edit-article.component';

@NgModule({
    declarations: [
        BlogsComponent,
        ArticleItemComponent,
        AddArticleComponent,
        ArticlesListComponent,
        EditArticleComponent
    ],
    imports: [
        BlogsRoutingModule,
        BrowserModule,
        FontAwesomeModule,
        ReactiveFormsModule
    ]
})

export class BlogsModule { }
