using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models
{
    public class KlarnaOrder
    {
        public int Id { get; set; }

        [Required]
        public string KlarnaOrderId { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfInstallments { get; set; }

        [Required]
        public string Type { get; set; }
        public string FraudStatus { get; set; }
        public string RedirectUrl { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}
