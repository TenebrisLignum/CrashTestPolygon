﻿using Domain.Entities.Articles;

namespace Domain.Repositories.Articles
{
    public interface IArticleRepository
    {
        Task<Article?> GetById(int id);
        IQueryable<Article> GetAsQuery();
        Task Insert(Article article);
        Task Update(Article article);
        Task Delete(int id);
    }
}
