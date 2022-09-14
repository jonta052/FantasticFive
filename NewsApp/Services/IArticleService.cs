using NewsApp.Models;
namespace NewsApp.Services

{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();
        Article GetArticle(int id);
        Article CreateArticle(Article article);
        Article UpdateArticle(Article article);
        void DeleteArticle(int id);
    }
}
