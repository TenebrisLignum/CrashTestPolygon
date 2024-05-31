import { Component } from '@angular/core';
import { ArticlesService } from '../../core/services/blogs.service';

@Component({
	selector: 'app-blogs',
	templateUrl: './blogs.component.html',
	styleUrl: './blogs.component.scss'
})
export class BlogsComponent {

	articles: any[] = [];

	constructor(
		private _articlesService: ArticlesService
	) { }

	ngOnInit() {
		this._getArticles();
	}

	private _getArticles() {
		this._articlesService.list().subscribe({
			next: (result) => {
				this.articles = result;
			},
			error: (error) => {
				console.log(error);
			}
		});
	}
}
