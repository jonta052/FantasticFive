using NewsApp.Models;

namespace NewsApp.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetCommentsByArticle(int articleId);
        IEnumerable<Comment> GetCommentsByUser(string userId);
        Comment GetComment(int Id);
        Comment CreateComment(Comment comment);
        void DeleteComment(int Id);
    }
}
