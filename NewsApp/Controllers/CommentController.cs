using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Controllers
{
    //[Authorize(Roles = $"{Roles.Administrator}, {Roles.User}")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;
        public CommentController
          (ICommentService commentService,
          IArticleService articleService,
          UserManager<User> userManager)
        {
            _commentService = commentService;
            _articleService = articleService;
            _userManager = userManager;
        }



        // GET: CommentController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: CommentController/Details/5


        //[AllowAnonymous]
        public IActionResult Details(int id)
        {
            var comment = _commentService.GetComment(id);
            return View(comment);
        }

        // GET: CommentController/Create
        [Authorize]
        public IActionResult Create(int id)
        {
            var article = _articleService.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }
            var comment = new Comment
            {
                ArticleId = article.Id,
                Article = article
            };
            
            return View(comment);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(int articleId, string commentBody)
        {
            TempData["JumpToOpinions"] = "hej";
            if (!string.IsNullOrEmpty(commentBody))
            {
                var comment = new Comment
                {
                    ArticleId = articleId,
                    Body = commentBody
                };

                var user = _userManager.GetUserAsync(User).Result;
                comment.UserId = user.Id;
                _commentService.CreateComment(comment);
                return RedirectToAction("Details", "Article", new { id = articleId });
            }
            else
            {
                return RedirectToAction("Details", "Article", new { id = articleId });
            }
        }



        // GET: CommentController/Delete/5
        [Authorize]
        public IActionResult Delete(int id)
        {
            var UserId = _userManager.GetUserId(User);
            var comment = _commentService.GetComment(id);

            if (comment == null || comment.User.Id != UserId)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Delete(int id, Comment comment)
        {
            try
            {
                _commentService.DeleteComment(id);

            }
            catch
            {
                Console.WriteLine("Error!");
            }
            return RedirectToAction("Details", "Article", new { id = comment.ArticleId });

        }
    }
}
