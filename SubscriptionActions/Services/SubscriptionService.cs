using SubscriptionActions.Data;
using SubscriptionActions.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace SubscriptionActions.Services
{
    internal class SubscriptionService : ISubscriptionService
    {
        private readonly AppDbContext _db;
        

        public SubscriptionService(AppDbContext db)
        {
            _db = db;
        }
        
        public void SetExpiredSubscription()
        {
            //var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var expiredSubscriptions = _db.Subscriptions.Where(s => s.Expires < DateTime.Now).ToList();
            foreach (var expiredSubscription in expiredSubscriptions)
            {
                expiredSubscription.Active = false;
                _db.Update(expiredSubscription);
            }
            _db.SaveChanges();
        }

        public async Task<IActionResult> ReminderEmail()
        {
            SubscriptionEmail subscriptionEmail = new SubscriptionEmail();

            var nowPlusTen = DateTime.Now.AddDays(10);
            //Check if there are less than 10 days left of the Subscription
            var soonExpiredSubscriptions = _db.Subscriptions.Where(s => s.Expires < nowPlusTen).ToList();
            List<string> sendToQueue = new List<string>();
            foreach (var soonExpiredSubscription in soonExpiredSubscriptions)
            {

                var user = _db.Users.Where(u => u.Id == soonExpiredSubscription.UserId).FirstOrDefault();

                subscriptionEmail.SubscriberEmail = user.Email;
                subscriptionEmail.SubscriberName = user.GetFullName();
                subscriptionEmail.SubscriptionTypeName = soonExpiredSubscription.Name + "true";
                sendToQueue.Add(JsonConvert.SerializeObject(subscriptionEmail));
                //var response = await _httpClient.PostAsJsonAsync("ConfirmHttp?code=fLNpR62m3yIlY_zbr9SN1xSAoU89_tXPgqiWOWWa1IinAzFui-qGcQ==", subscriptionEmail);
                
            }
            return await Queue.SendToQueue(sendToQueue);
        }

        public async Task<IActionResult> SubscriberEmail()
        {

            var articles = _db.Articles.ToList();
            var categories = _db.Categories.ToList();
            var userCategories = _db.UserCategories.ToList();
            //Don't put together a mail if the user has no entries in UserCategories
            var users = _db.Users.Where(u => _db.UserCategories.Any(uc => uc.UserId == u.Id)).ToList();
            List<string> sendToQueue = new List<string>();
            foreach (var user in users)
            {
                SubscriptionEmail subscriptionEmail = new SubscriptionEmail();
                //Only add categories belonging to the specific user
                List<UserCategories> userCategoriesList = userCategories.Where(uc => uc.UserId == user.Id).ToList();
                //userCategoriesList.AddRange(userCategories.Where(uc => uc.UserId == user.Id));

                foreach (var userCategory in userCategoriesList)
                {
                    List<Article> articleCategories = new List<Article>();

                    //Add the two newest articles from the category
                    articleCategories.AddRange(articles.Where(a => a.CategoryId == userCategory.CategoryId)
                        .OrderByDescending(a => a.DateStamp).Take(2));

                    foreach (var article in articleCategories)
                    {
                        subscriptionEmail.ArticleTitle.Add(article.Title);
                        subscriptionEmail.ArticleImageLink.Add(article.ImageLink);
                        subscriptionEmail.ArticleId.Add(article.Id);
                    }
                    var category = categories.Where(c => c.Id == userCategory.CategoryId).FirstOrDefault();
                    subscriptionEmail.CategoryName.Add(category.Name);
                }
                //We don't have different newsletters for Basic/Premium subscribers
                subscriptionEmail.SubscriptionTypeName = "Basic";
                subscriptionEmail.SubscriberEmail = user.Email;
                subscriptionEmail.SubscriberName = user.GetFullName();
                sendToQueue.Add(JsonConvert.SerializeObject(subscriptionEmail));
                //var response = await _httpClient.PostAsJsonAsync("ConfirmHttp?code=fLNpR62m3yIlY_zbr9SN1xSAoU89_tXPgqiWOWWa1IinAzFui-qGcQ==", subscriptionEmail);
            }
            return await Queue.SendToQueue(sendToQueue);
            //return null;
        }
    }
}
