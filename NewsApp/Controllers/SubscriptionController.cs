using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Helpers;
using NewsApp.Models;
using NewsApp.Models.Klarna;
using NewsApp.Services;
using NuGet.Protocol;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IKlarnaService _klarnaService;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ApplicationDbContext db, UserManager<User> userManager,
            IKlarnaService klarnaService, ISubscriptionService subscriptionService)
        {
            _db = db;
            _userManager = userManager;
            _klarnaService = klarnaService;
            _subscriptionService = subscriptionService;
        }
        // GET: SubscriptionController
        public IActionResult Index()
        {
            
            var listOfSubscriptionTypes = _db.SubscriptionTypes.ToList();
            //ViewBag.Names = 
               var subTypes = (from n in listOfSubscriptionTypes select n).ToList();
            ViewBag.Length = listOfSubscriptionTypes.Count();
           var subscriptions  = (from s in _db.Subscriptions
                                     join st in _db.SubscriptionTypes on s.SubscriptionType.Id equals st.Id
                                     select new
                                     {
                                         TypeName = st.TypeName,
                                         Active = s.Active
                                     }
                                     ).GroupBy(x => x.TypeName).Select(x => new
                                     {  
                                         Active = x.Count(d => d.Active == true),
                                        TypeName = x.FirstOrDefault().TypeName

                                     }).ToList();
            ViewBag.Subscriptions = subscriptions;
            ViewBag.Length = subscriptions.Count();

            return View(listOfSubscriptionTypes);
        }

        // GET: SubscriptionController/Details/5
        /*public IActionResult Details(int id)
        {
            return View();
        }*/

        [Authorize(Roles = $"{Roles.Administrator}")]
        public IActionResult Create()
        {
            SubscriptionType subscriptionType = new SubscriptionType();

            return View(subscriptionType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}")]
        public IActionResult Create(SubscriptionType subscriptionType)
        {
            _db.SubscriptionTypes.Add(subscriptionType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SubscriptionController/Create
        public IActionResult CreateUserSubscription()
        {
            var subscriptionTypes = _db.SubscriptionTypes.ToList();
            //add price here
            var selectList = new SelectList(subscriptionTypes, "Id", "TypeName");
            ViewBag.SubType = selectList;
            
            return View();
        }

        // POST: SubscriptionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUserSubscription(int subId)
        {
            /*var subscriptionTypes = _db.SubscriptionTypes.ToList();
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
            }*/
            //_db.Subscriptions.Add(subscription);
            //_db.SaveChanges();
            var klarnaSession = _klarnaService.CreateSession(subId).Result;
            HttpContext.Session.Set("KlarnaSession", klarnaSession);
            ViewBag.KlarnaSession = klarnaSession;
            KlarnaPaymentViewModel klarnaPaymentViewModel = new KlarnaPaymentViewModel();
            klarnaPaymentViewModel.KlarnaSession = klarnaSession.SessionResponse;
            klarnaPaymentViewModel.OrderLines = klarnaSession.OrderLines;
            return View("Klarna_Order", klarnaPaymentViewModel); 

        }

        public async Task<IActionResult>ProcessOrder(string autorizationToken)
        {
            var klarnaSession = HttpContext.Session.Get<KlarnaSessionResult>("KlarnaSession");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _klarnaService.CreateOrder(autorizationToken, userId, klarnaSession.OrderLines);
            HttpContext.Session.Remove("KlarnaSession");

            if(order== null)
            {
                return RedirectToAction("PaymentFailed");
            }
            return RedirectToAction("PaymentCompleted");
        }

        // GET: SubscriptionController/Edit/5
        [Authorize(Roles = $"{Roles.Administrator}")]
        public IActionResult Edit(int id)
        {
            var thisSubType = _db.SubscriptionTypes.Where(s => s.Id == id).FirstOrDefault();
            return View(thisSubType);
        }

        // POST: SubscriptionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}")]
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
        [Authorize(Roles = $"{Roles.Administrator}")]
        public IActionResult Delete(int id)
        {
            var thisSubType = _db.SubscriptionTypes.Where(s => s.Id == id).FirstOrDefault();
            return View(thisSubType);
        }

        // POST: SubscriptionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Roles.Administrator}")]
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
        public IActionResult PaymentFailed()
        {

            return View();
        }
        public IActionResult PaymentCompleted()
        {

            return View();
        }
    }
}
