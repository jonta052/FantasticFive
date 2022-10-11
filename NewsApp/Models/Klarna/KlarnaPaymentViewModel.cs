namespace NewsApp.Models.Klarna
{
    public class KlarnaPaymentViewModel
    {
        public KlarnaSessionResponse KlarnaSession { get; set; }

        public IEnumerable<KlarnaSessionRequest.OrderLine> OrderLines { get; set; }
    }
}
