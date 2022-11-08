using Azure.Storage.Blobs;
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

        IEnumerable<Article> PopularArticles();
        IEnumerable<Article> LatestArticles();
        IEnumerable<Article> EditorsChoice();
        
        IEnumerable<Article> GetOneArticleForCategories();
        //BlobContainerClient InitBlobService();
        //void UploadToBlob(string fileName);
    }
}
