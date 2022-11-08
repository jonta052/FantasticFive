using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System.Text.Json;

namespace ConfirmationEMail
{
    public class NewsAppQueue
    {
        [FunctionName("NewsAppQueue")]
        [return: SendGrid(ApiKey = "SendGridApiKey")]

        public SendGridMessage Run([QueueTrigger("mailtobesent", Connection = "AzureQueueStorage")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            SubscriptionEmail subscriptionEmail = JsonSerializer.Deserialize<SubscriptionEmail>(myQueueItem, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            
            bool result = (subscriptionEmail?.SubscriptionTypeName).Contains("true");
            var subscriptionType = (subscriptionEmail?.SubscriptionTypeName).Replace("true", "");
            
            //Newsletter
            if (subscriptionEmail?.ArticleTitle[0] != "newSubscriber")
                {
                var categories = subscriptionEmail?.CategoryName;
                var articles = subscriptionEmail?.ArticleTitle;
                var site = subscriptionEmail?.ArticleId;
                var imageLink = subscriptionEmail?.ArticleImageLink;
                int j = 0;
                string hr;

                string newsletter = "<h1>Newsletter</h1>\r\n";

                foreach (var category in categories)
                {
                    newsletter += $"<h2 style=\"margin-bottom:0px\">{category}:</h2>\r\n<div style=\"border-style:solid;border-radius:10px;width:400px;border-color:green;padding:10px\">\r\n\r\n";
                    //Add the two articles belonging to the category
                    for (int i = j; i < 2 + j; i++)
                    {
                        //Put a line only after the first article for each category
                        if (i % 2 == 0)
                        {
                            hr = "<hr>";
                        }
                        else { hr = ""; }
                        newsletter += $"<p>{articles[i]}</p>\r\n<a href=\"https://newsfive.azurewebsites.net/Article/Details/{site[i]}\">\r\n<img alt=\"Azure\" src=\"{imageLink[i]}\"\r\n width=300\">\r\n</a>{hr}\r\n";
                    }
                    newsletter += "</div>";
                    j += 2;
                }

                var msg = new SendGridMessage()
                {

                    From = new EmailAddress("jonta72@hotmail.com", "NewsFive"),
                    Subject = "Newsletter",
                    PlainTextContent = $"I have no idea where this text will show up.",
                    HtmlContent = newsletter
                };
                msg.AddTo(new EmailAddress(subscriptionEmail?.SubscriberEmail, subscriptionEmail?.SubscriberName));
                return msg;
            }

            //Subscription email
            else if (result != true)
            {
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("jonta72@hotmail.com", "NewsFive"),
                    Subject = $"{subscriptionEmail?.SubscriptionTypeName} subscription order verified",
                    PlainTextContent = $"and easy to do anywhere, especially with C#",
                    HtmlContent = $"<h1>Welcome to Newsfive</h1>\r\n<div style=\"border-style:solid;border-radius:10px;width:400px;border-color:green\">\r\n<p style=\"margin-left:10px\">With the <b>{subscriptionEmail?.SubscriptionTypeName} subscription</b>," +
                    $" you can now choose from our list of specialized newsletters that will be sent to you every day.</p>\r\n</div>"
                };
                msg.AddTo(new EmailAddress(subscriptionEmail?.SubscriberEmail, subscriptionEmail?.SubscriberName));
                return msg;
            }
            //Reminder email
            else
            {
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("jonta72@hotmail.com", "NewsFive"),
                    Subject = "Your subscription will soon expire",
                    PlainTextContent = $"I have no idea where this text will show up.",
                    HtmlContent = $"<h1>Subscription reminder</h1>\r\n<div style=\"border-style:solid;border-radius:10px;width:400px;border-color:red\">\r\n<p style=\"margin-left:10px\">Your " +
                    $"<b>{subscriptionType} subscription</b>, will soon expire,\r\n   if you are satisfied with our services you should consider resubscribing.</p>\r\n</div>"
                };
                msg.AddTo(new EmailAddress(subscriptionEmail?.SubscriberEmail, subscriptionEmail?.SubscriberName));
                return msg;
            }

        }
    }
}
