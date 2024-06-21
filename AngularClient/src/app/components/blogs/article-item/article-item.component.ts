import { Component, Input } from '@angular/core';
import { ArticleItemViewModel } from '../../../core/interfaces/view-models/articles/ArticleItemViewModel';

@Component({
    selector: 'app-article-item',
    templateUrl: './article-item.component.html',
    styleUrl: './article-item.component.scss'
})
export class ArticleItemComponent {
    @Input()
    article!: ArticleItemViewModel;

    constructor() { }

    ngOnInit() {

    }
}
