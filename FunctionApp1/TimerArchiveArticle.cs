using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TimerTrigger1.Services;

namespace TimerTrigger1
{
    public class TimerArchiveArticle
    {
        private readonly ILogger _logger;
        private readonly IArticleService _articleService;

        public TimerArchiveArticle(ILoggerFactory loggerFactory, IArticleService articleService)
        {
            _logger = loggerFactory.CreateLogger<TimerArchiveArticle>();
            _articleService = articleService;
        }

        [Function("TimerArchiveArticle")]
        public void Run([TimerTrigger("0 0 0 * * *", RunOnStartup = true)] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _articleService.SetArchivedArticles();
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
