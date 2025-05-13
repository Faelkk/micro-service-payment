namespace Order.DTO;

public class OrderToInsert
{
    public string PaymentStatus { get; set; } = "Pending";
    public List<OrderItemToInsert> Items { get; set; } = new();
}