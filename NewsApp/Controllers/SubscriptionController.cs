using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: SubscriptionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SubscriptionController/Create
        public ActionResult Create()
        {
            

            return View(_db.SubscriptionTypes.ToList());
        }

        // POST: SubscriptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int subTypeId)
        {

            try
            {
                /*subscription.Expires = subscription.Created.AddDays(30);
                subscription.KlarnaOrder = true;
                subscription.IsActive = true;*/
                //subscription.Price = ()

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubscriptionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubscriptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubscriptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
