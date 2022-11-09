using SubscriptionActions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ExpiredSubscription
{
    public class UpdateSubscription
    {
        private readonly ILogger _logger;
        private readonly ISubscriptionService _subscriptionService;
        public UpdateSubscription(ILoggerFactory loggerFactory, ISubscriptionService subscriptionService)
        {
            _logger = loggerFactory.CreateLogger<UpdateSubscription>();
            _subscriptionService = subscriptionService;
        }

        [Function("UpdateSubscription")]
        //Triggers 6:30 every day
        public void Run([TimerTrigger("0 30 6 * * *", RunOnStartup = true)] MyInfo myTimer)
        {
            _subscriptionService.SetExpiredSubscription();
            _subscriptionService.ReminderEmail();
            _subscriptionService.SubscriberEmail();

            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
