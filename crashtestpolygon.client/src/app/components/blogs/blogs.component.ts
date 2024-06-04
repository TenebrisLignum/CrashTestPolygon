import { Component } from '@angular/core';
import { ArticlesService } from '../../core/services/articles.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
	selector: 'app-blogs',
	templateUrl: './blogs.component.html',
	styleUrl: './blogs.component.scss'
})
export class BlogsComponent {
	private readonly destroy$ = new Subject<void>();
	articles: any[] = [];

	constructor(
		private _articlesService: ArticlesService
	) { }

	ngOnInit() {
		this._getArticles();
	}

	ngOnDestroy() {
		this.destroy$.next();
		this.destroy$.complete();
	}

	private _getArticles() {
		this._articlesService.list()
			.pipe(takeUntil(this.destroy$))
			.subscribe({
				next: (result) => {
					this.articles = result;
				},
				error: (error) => {
					console.log(error);
				}
			});
	}
}
