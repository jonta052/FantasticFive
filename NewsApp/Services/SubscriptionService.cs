using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly ApplicationDbContext _db;

        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
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
    }
}
