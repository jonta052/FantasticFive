using Microsoft.AspNetCore.Mvc;
using NewsApp.Services;

namespace NewsApp.ViewComponents
{
public class EditorsChoiceViewComponent : ViewComponent
{
        private readonly IArticleService _articleService;
        public EditorsChoiceViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var editorsChoice = _articleService.EditorsChoice().Take(5).ToList();

            return View("Index", editorsChoice);
        }
    }
}
