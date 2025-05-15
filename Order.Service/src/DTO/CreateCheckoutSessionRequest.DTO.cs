
public class CreateCheckoutSessionRequest
{
    public string OrderId { get; set; }
    public List<SessionItem> Items { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}


public class SessionItem
{
    public string ProductName { get; set; }
    public int UnitAmount { get; set; }
    public int Quantity { get; set; }
}
