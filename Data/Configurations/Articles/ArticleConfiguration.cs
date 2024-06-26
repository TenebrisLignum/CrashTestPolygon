﻿using Domain.Entities.Articles;
using Microsoft.EntityFrameworkCore;

namespace Data.Configurations.Articles
{
    public class ArticleConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .ToTable("Articles")
                .HasKey(c => c.Id);
        }
    }
}
