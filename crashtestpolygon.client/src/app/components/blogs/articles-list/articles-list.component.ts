import { Component } from '@angular/core';
import { faEdit, faPencil, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Subject, takeUntil } from 'rxjs';
import { ArticlesService } from '../../../core/services/articles.service';
import { AuthService } from '../../../core/services/auth/auth.service';
import { AlertService } from '../../../core/services/alert.service';
import { Router } from '@angular/router';
import { ArticleItemViewModel } from '../../../core/interfaces/view-models/articles/article';

@Component({
    selector: 'app-articles-list',
    templateUrl: './articles-list.component.html',
    styleUrl: './articles-list.component.scss'
})
export class ArticlesListComponent {
    faPlus = faPlus;
    faEdit = faPencil;
    faDelete = faTrash;

    isAdmin: boolean = false;

    private readonly destroy$ = new Subject<void>();
    articles: ArticleItemViewModel[] = [];

    constructor(
        private _articlesService: ArticlesService,
        private _authService: AuthService,
        private _alertService: AlertService,
        private _router: Router
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

    onEdit(id: number) {
        this._router.navigateByUrl('blogs/edit/' + id.toString());
    }

    onDelete(id: number) {
        this._articlesService.delete(id).subscribe({
            next: (res) => {
                this.articles = this.articles.filter(x => x.id != id);
                this._alertService.showSucsess("Article was deleted!");
            },
            error: (e) => {
                debugger
                this._alertService.showError(e.error.title);
            }
        })
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
