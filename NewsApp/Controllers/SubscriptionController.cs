using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubscriptionController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: SubscriptionController
        public IActionResult Index()
        {
            var listOfSubscriptions = _db.Subscriptions.ToList();
            return View(listOfSubscriptions);
        }

        // GET: SubscriptionController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: SubscriptionController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: SubscriptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string subscriptionType)
        {
            var subscriptionTypes = _db.SubscriptionTypes.ToList();
            IEnumerable<SelectListItem> items =

        from item in subscriptionTypes

        select new SelectListItem

        {

            Text = item.TypeName.ToString(),

            Value = item.Price.ToString()

        };
            ViewBag.SubType = items;
            /*if (!ModelState.IsValid)
            { }*/
            try
            {
                //subscription = (from s in _db.Subscriptions where s.Id == subId select s).FirstOrDefault();
                Subscription subscription = new Subscription();
                subscription.Expires = subscription.Created.AddDays(30);
                //subscription.KlarnaOrder = true;
                subscription.Active = true;

                foreach (var item in items)
                {
                    if (item.Selected == true)
                    {
                        subscription.Price = decimal.Parse(item.Value);
                    }
                }

                _db.Subscriptions.Add(subscription);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }


        }

        // GET: SubscriptionController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubscriptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubscriptionController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubscriptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
