using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopProject.Models;

namespace BookShopProject.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int id);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int id);
    }
}

