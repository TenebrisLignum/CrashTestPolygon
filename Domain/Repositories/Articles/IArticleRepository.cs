﻿using Domain.Entities.Articles;

namespace Domain.Repositories.Articles
{
    public interface IArticleRepository
    {
        Article? GetById(int id);
        Task Insert(Article article);
        void Update(Article article);
        void Delete(int id);
    }
}
