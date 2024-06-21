export interface GetArticlesByFilterQueryDto {
    searchWord: string | null,
    pageSize: number | null,
    page: number | null
}