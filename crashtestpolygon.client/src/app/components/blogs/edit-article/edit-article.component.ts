import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from '../../../core/services/alert.service';
import { ArticlesService } from '../../../core/services/articles.service';
import { UpdateArticleDto } from '../../../core/interfaces/dto/articles/UpdateArticleDto';
import { ArticleItemViewModel } from '../../../core/interfaces/view-models/articles/article';
import { text } from '@fortawesome/fontawesome-svg-core';

@Component({
    selector: 'app-update-article',
    templateUrl: './edit-article.component.html',
    styleUrl: './edit-article.component.scss'
})
export class EditArticleComponent {
    isRequestSent: boolean = false;
    form: FormGroup
    articleId: number;

    constructor(
        private _articlesService: ArticlesService,
        private _alertService: AlertService,
        private _router: Router,
        private _route: ActivatedRoute
    ) {
        this.form = new FormGroup({
            id: new FormControl([Validators.required]),
            title: new FormControl("", [Validators.required]),
            text: new FormControl("", [Validators.required])
        });
    }

    ngOnInit() {
        this.articleId = parseInt(this._route.snapshot.paramMap.get('id') as string);
        this._getArticle();
    }

    onSubmit() {
        this.isRequestSent = true;
        let body = this.form.value as UpdateArticleDto;

        this._articlesService.update(body).subscribe({
            next: (res) => {
                this._alertService.showSucsess("Article successfully updated!");
                this.isRequestSent = false;
                this._router.navigateByUrl('/blogs');
            },
            error: (e) => {
                this._alertService.showError(e.error.title);
            }
        }
        );
    }

    private _getArticle() {
        this._articlesService.get(this.articleId).subscribe(
            {
                next: (res: ArticleItemViewModel) => {
                    this.form.patchValue({
                        id: res.id,
                        title: res.title,
                        text: res.text
                    })
                },
                error: (e) => {

                }
            }
        )
    }
}
