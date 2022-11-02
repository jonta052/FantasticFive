using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsApp.Data;
using NewsApp.Helpers;
using NewsApp.Models;
using NewsApp.Models.Email;
using NewsApp.Models.Klarna;
using NewsApp.Services;
using NuGet.Protocol;
using System.Linq;
using System.Security.Claims;

namespace NewsApp.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IKlarnaService _klarnaService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEmailService _emailService;


        public SubscriptionController(ApplicationDbContext db, UserManager<User> userManager,
            IKlarnaService klarnaService, ISubscriptionService subscriptionService,
            IEmailService emailService)
        {
            _db = db;
            _userManager = userManager;
            _klarnaService = klarnaService;
            _subscriptionService = subscriptionService;
            _emailService = emailService;
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
            if (_subscriptionService.HasSubscription(User))
            {
                return RedirectToAction("Index", "Home");
            }

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
            HttpContext.Session.SetString("SessionId", subId.ToString());
            
            if (_subscriptionService.HasSubscription(User))
            {
                return RedirectToAction("Index", "Home");
            }

            var klarnaSession = _klarnaService.CreateSession(subId).Result;
            HttpContext.Session.Set("KlarnaSession", klarnaSession);
            ViewBag.KlarnaSession = klarnaSession;
            KlarnaPaymentViewModel klarnaPaymentViewModel = new KlarnaPaymentViewModel();
            klarnaPaymentViewModel.KlarnaSession = klarnaSession.SessionResponse;
            klarnaPaymentViewModel.OrderLines = klarnaSession.OrderLines;
            return View("Klarna_Order", klarnaPaymentViewModel); 

        }

        public async Task<IActionResult>ProcessOrder(string authorizationToken)
        {
            var klarnaSession = HttpContext.Session.Get<KlarnaSessionResult>("KlarnaSession");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _klarnaService.CreateOrder(authorizationToken, userId, klarnaSession.OrderLines);
            HttpContext.Session.Remove("KlarnaSession");
            HttpContext.Session.Remove("subscriptionId");

            if (order== null)
            {
                return RedirectToAction(nameof(PaymentFailed));
            }
            return RedirectToAction(nameof(PaymentCompleted));
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
            var user = _userManager.GetUserAsync(User).Result;
            var sub = user.Subscription.OrderBy(s => s.Created).LastOrDefault();
            var subscriptionEmail = new SubscriptionEmail
            {
                SubscriberEmail = user.Email,
                SubscriberName = user.GetFullName(),
                SubscriptionTypeName = sub.Name

            };
                           
            TempData["Response"] = _emailService.SendEmail(subscriptionEmail).Result;

            return View();
        }

        [Authorize]
        public IActionResult SubDetails()
        {
            if (_subscriptionService.HasSubscription(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var details = _db.Subscriptions.Where(x => x.User.Id == user.Id).ToList();
                var categories = _db.UserCategories.ToList();

                var query=(from c in categories
                          join d in details
                          on c.UserId equals d.User.Id
                          select new CategorySubVM
                          {
                              Id=c.Id,
                              UserName=d.User.UserName,
                              CategoriesName=c.Category.Name,
                              SubTypeName=d.SubscriptionType.TypeName,
                              Price=d.Price,
                              Created=d.Created,
                              Expires=d.Expires,
                          }).ToList();

                return View(query);
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult SelectCategories()
        {
            if (_subscriptionService.HasSubscription(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var categories = _db.Categories.ToList();
                var userCategories = _db.UserCategories.Where(x => x.User.Id == user.Id).ToList();


                var query = (from c in categories
                             join u in userCategories
                             on c.Id equals u.CategoryId
                             into uc
                             from u in uc.DefaultIfEmpty()
                             select new UserCategoryVM
                             {
                                 Id = c.Id,
                                 CategoryName = c?.Name,
                                 UserCategoryId = u?.UserId
                             }).ToList();

                // List<UserCategoryVM> userCategoriesList = new List<UserCategoryVM>();
                //TempData["PreSelect"] = userCategories.Select(uc=>uc.CategoryId).ToList();
                
                return View(query);

            }
           
            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpPost]
        public IActionResult SelectCategories(List<int> categoriesId)
        {
            if (_subscriptionService.HasSubscription(User))
            {

                var categories = _db.Categories.ToList();

                var user = _userManager.GetUserAsync(User).Result;
                var userCategories = _db.UserCategories.Where(x => x.User.Id == user.Id).ToList();
                

                var addToUC = categoriesId.Except(userCategories.Select(uc => uc.CategoryId)).ToList();
                var removeFromUC = userCategories.Select(uc => uc.CategoryId).Except(categoriesId).ToList();

              

                List<UserCategories> removeCate = new List<UserCategories>();
                List<UserCategories> addCate = new List<UserCategories>();

                foreach (var item in removeFromUC)
                {
                    removeCate.Add( _db.UserCategories.FirstOrDefault(uc => uc.CategoryId == item));
                }
                foreach (var item in addToUC)
                {
                    addCate.Add(new UserCategories()
                    {
                        UserId = user.Id,
                        CategoryId = item
                    });
                }

              
                _db.UserCategories.AddRange(addCate);
                
                _db.UserCategories.RemoveRange(removeCate);

                _db.SaveChanges();

                return RedirectToAction("SubDetails", "Subscription");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
