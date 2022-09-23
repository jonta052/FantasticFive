using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Models;
using NuGet.Protocol;

namespace NewsApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public SubscriptionController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // GET: SubscriptionController
        public IActionResult Index()
        {
            var listOfSubscriptionTypes = _db.SubscriptionTypes.ToList();
            return View(listOfSubscriptionTypes);
        }

        // GET: SubscriptionController/Details/5
        /*public IActionResult Details(int id)
        {
            return View();
        }*/

        // GET: SubscriptionController/Create
        public IActionResult Create()
        {
            var subscriptionTypes = _db.SubscriptionTypes.ToList();
            //add price here
            var selectList = new SelectList(subscriptionTypes, "Id", "TypeName");
            ViewBag.SubType = selectList;
            return View();
        }

        // POST: SubscriptionController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(int subId)
        {
            var subscriptionTypes = _db.SubscriptionTypes.ToList();
            var user = _userManager.GetUserAsync(User).Result;

            var selectList = new SelectList(subscriptionTypes, "Id", "TypeName");

            ViewBag.SubType = selectList;

            var usr = User;
            Subscription subscription = new Subscription();
            var created = subscription.Created;
            subscription.Expires = created.AddDays(30);
            subscription.User = user;
            //subscription.KlarnaOrder = true;
            subscription.Active = true;

            foreach (var item in selectList)
            {
                if (item.Value == subId.ToString())
                {
                    var thisSubscription = (from s in subscriptionTypes
                                            where s.Id == decimal.Parse(item.Value)
                                            select s).FirstOrDefault();

                    subscription.Price = thisSubscription.Price;

                    subscription.SubscriptionType = thisSubscription;

                    subscription.Name = thisSubscription.TypeName;
                }
            }
            _db.Subscriptions.Add(subscription);
            _db.SaveChanges();
            
            return View();

        }

        // GET: SubscriptionController/Edit/5
        public IActionResult Edit(int id)
        {
            var thisSubType = _db.SubscriptionTypes.Where(s => s.Id == id).FirstOrDefault();
            return View(thisSubType);
        }

        // POST: SubscriptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SubscriptionType subType)
        {
            try
            {
                _db.Update(subType);
                _db.SaveChanges();
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
            var thisSubType = _db.SubscriptionTypes.Where(s => s.Id == id).FirstOrDefault();
            return View(thisSubType);
        }

        // POST: SubscriptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, SubscriptionType subType)
        {
            try
            {
                _db.Remove(subType);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
