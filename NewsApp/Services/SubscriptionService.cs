using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Models;
using System.Security.Claims;

namespace NewsApp.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public SubscriptionService(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Subscription CreateSubscription(int subscriptionId, User user, KlarnaOrder klarnaOrder, double totalAmount)
        {
            Subscription subscription = new Subscription();
           
                    var subscriptionTypes = (from s in _db.SubscriptionTypes
                                            where s.Id == subscriptionId
                                            select s).FirstOrDefault();

                    subscription.Price = (decimal)totalAmount;
                    subscription.SubscriptionType = subscriptionTypes;
                    subscription.Name = subscriptionTypes.TypeName;

                    subscription.User = user;
                    subscription.KlarnaOrder = klarnaOrder;
                    var created = subscription.Created;
                    subscription.Expires = created.AddDays(30);
                    subscription.Active = true;
            
            

            _db.Subscriptions.Add(subscription);
            _db.SaveChanges();
            return subscription;
        }
        public bool HasSubscription(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);

            return _db.Subscriptions.Any(s => s.User.Id == userId);
        }
  

        
    }
}
