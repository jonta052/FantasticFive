using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionActions.Models
{
    internal class UserCategories
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
