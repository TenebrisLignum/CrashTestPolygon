import { Component, ViewChild } from '@angular/core';
import { faPencil, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Subject, takeUntil } from 'rxjs';
import { ArticlesService } from '../../../core/services/articles/articles.service';
import { AuthService } from '../../../core/services/auth/auth.service';
import { AlertService } from '../../../core/services/alert.service';
import { Router } from '@angular/router';
import { ArticleItemViewModel } from '../../../core/interfaces/view-models/articles/ArticleItemViewModel';
import { PagedList } from '../../../core/interfaces/view-models/PagedList';
import { GetArticlesByFilterQueryDto } from '../../../core/interfaces/dto/articles/GetArticlesByFilterQueryDto';

@Component({
    selector: 'app-articles-list',
    templateUrl: './articles-list.component.html',
    styleUrl: './articles-list.component.scss'
})
export class ArticlesListComponent {
    @ViewChild('dialog') dialog: any;

    isAdmin: boolean = false;
    isLoaded: boolean = false;

    articlesList: PagedList<ArticleItemViewModel>;
    articleToDeleteId: number;

    faPlus = faPlus;
    faEdit = faPencil;
    faDelete = faTrash;

    private readonly destroy$ = new Subject<void>();

    constructor(
        private _articlesService: ArticlesService,
        private _authService: AuthService,
        private _alertService: AlertService,
        private _router: Router
    ) {
        this.isAdmin = this._authService.isAdmin();
    }

    ngOnInit() {
        this._loadArticles();
    }

    ngOnDestroy() {
        this.destroy$.next();
        this.destroy$.complete();
    }

    onNextPage() {
        if (this.articlesList.hasNextPage)
            this._loadArticles(this.articlesList.page + 1);
    }

    onPreviousPage() {
        if (this.articlesList.hasPreviousPage)
            this._loadArticles(this.articlesList.page - 1);
    }

    onEdit(id: number) {
        this._router.navigateByUrl('blogs/edit/' + id.toString());
    }

    onDelete(id: number) {
        this.articleToDeleteId = id;
        this.dialog.nativeElement.showModal();
    }

    onDialogConfirm() {
        this._articlesService.delete(this.articleToDeleteId).subscribe({
            next: (res: number) => {
                this.articlesList.items = this.articlesList.items.filter(x => x.id != res);
                this.articleToDeleteId = 0;
                this.dialog.nativeElement.close();
                this._alertService.showSucsess("Article was deleted!");
            },
            error: (e) => {
                debugger
                this._alertService.showError(e.error.title);
            }
        })
    }

    onDialogCancel() {
        this.dialog.nativeElement.close();
    }

    private _loadArticles(page: number = 1) {
        this.isLoaded = false;
        let query = { page: page } as GetArticlesByFilterQueryDto;

        this._articlesService
            .list(query)
            .pipe(takeUntil(this.destroy$))
            .subscribe({
                next: (result: PagedList<ArticleItemViewModel>) => {
                    this.articlesList = result;
                    this.isLoaded = true;
                },
                error: (error) => {
                    this.isLoaded = true;
                    console.log(error);
                }
            });
    }
}
