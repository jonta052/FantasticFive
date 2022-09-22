namespace NewsApp.Models
{
    public class KlarnaOrder
    {
        public int Id { get; set; }
        public string KlarnaOrderId { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfInstallments { get; set; }
        public string Type { get; set; }
        public string FraudStatus { get; set; }
        public string RedirectUrl { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual User User { get; set; }

    }
}
