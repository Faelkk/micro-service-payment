public class CreateCheckoutSessionRequest
{
    public string OrderId { get; set; }
    public List<CreateCheckoutSessionItem> Items { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}
