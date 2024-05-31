import { NgModule } from '@angular/core';
import { BlogsComponent } from './blogs.component';
import { BrowserModule } from '@angular/platform-browser';
import { ArticleItemComponent } from './article-item/article-item.component';

@NgModule({
	declarations: [
		BlogsComponent,
		ArticleItemComponent
	],
	imports: [
		BrowserModule
	],
	bootstrap: [
		BlogsComponent
	]
})

export class BlogsModule { }
