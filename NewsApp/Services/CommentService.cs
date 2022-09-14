using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IArticleService _articleService;

        public CommentService(ApplicationDbContext db, IArticleService articleService)
        {
            _db = db;
            _articleService = articleService;
        }

        public Comment CreateComment(Comment comment)
        {
            _db.Add(comment);
            _db.SaveChanges();
            return comment;
        }

        public void DeleteComment(int Id)
        {
            var comment = _db.Comments.Find(Id);
            _db.Remove(comment);
            _db.SaveChanges();

        }

        public Comment GetComment(int Id)
        {
            return _db.Comments.Find(Id);
        }

        public IEnumerable<Comment> GetCommentsByArticle(int articleId)
        {
            var article = _articleService.GetArticle(articleId);

            if (article == null)
            {
                return null;
            }
            return article.Comments.ToList();
        }

        public IEnumerable<Comment> GetCommentsByUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
