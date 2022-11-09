using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerTrigger1.Data;

namespace TimerTrigger1.Services
{
    public class ArticleService : IArticleService
    {
        private readonly AppDbContext _db;

        public ArticleService(AppDbContext db)
        {
            _db = db;
        }

        public void SetArchivedArticles()
        {
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var articles = _db.Articles.Where(a => a.DateStamp < thirtyDaysAgo && !a.Archived).ToList();

            foreach (var item in articles)
            {
                item.Archived = true;
                _db.Update(item);
            }
            _db.SaveChanges();
        }
    }
}
