using Microsoft.AspNetCore.Mvc;
using NewsApp.Services;
using NewsApp.Models;
using NewsApp.ViewComponents;



namespace NewsApp.ViewComponents
{
    public class PopularArticlesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;
        public PopularArticlesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var popularArticles = _articleService.PopularArticles().Take(5).ToList();

            return View("Index",popularArticles);
        }

    }
}
