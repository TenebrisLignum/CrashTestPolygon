import { Component } from '@angular/core';
import { ArticlesService } from '../../../core/services/articles/articles.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateArticleDto } from '../../../core/interfaces/dto/articles/CreateArticleDto';
import { AlertService } from '../../../core/services/alert.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-article',
    templateUrl: './add-article.component.html',
    styleUrl: './add-article.component.scss'
})
export class AddArticleComponent {
    isRequestSent: boolean = false;
    form: FormGroup

    constructor(
        private _articlesService: ArticlesService,
        private _alertService: AlertService,
        private _router: Router
    ) {
        this.form = new FormGroup({
            title: new FormControl("", [Validators.required]),
            text: new FormControl("", [Validators.required])
        });
    }

    ngOnInit() {

    }

    onSubmit() {
        this.isRequestSent = true;
        let body = this.form.value as CreateArticleDto;

        this._articlesService.create(body).subscribe({
            next: (res) => {
                this._alertService.showSucsess("Article successfully created!");
                this.isRequestSent = false;
                this._router.navigateByUrl('/blogs');
            },
            error: (e) => {
                this._alertService.showError(e.error.title);
            }
        }
        );
    }
}
