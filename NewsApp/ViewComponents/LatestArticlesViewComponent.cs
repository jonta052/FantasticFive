using Microsoft.AspNetCore.Mvc;
using NewsApp.Services;

namespace NewsApp.ViewComponents
{
public class LatestArticlesViewComponent: ViewComponent
{
        private readonly IArticleService _articleService;
        public LatestArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var latestArticles = _articleService.LatestArticles().Take(5).ToList();

            return View("Index", latestArticles);
        }
    }
}
