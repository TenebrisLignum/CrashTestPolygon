import { Component } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { Subject, takeUntil } from 'rxjs';
import { ArticlesService } from '../../../core/services/articles.service';
import { AuthService } from '../../../core/services/auth/auth.service';

@Component({
    selector: 'app-articles-list',
    templateUrl: './articles-list.component.html',
    styleUrl: './articles-list.component.scss'
})
export class ArticlesListComponent {
    faPlus = faPlus;
    isAdmin: boolean = false;

    private readonly destroy$ = new Subject<void>();
    articles: any[] = [];

    constructor(
        private _articlesService: ArticlesService,
        private _authService: AuthService
    ) {
        this.isAdmin = this._authService.isAdmin();
    }

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
