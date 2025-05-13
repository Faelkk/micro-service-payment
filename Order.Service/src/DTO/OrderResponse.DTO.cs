using Order.Models;

namespace Order.DTO;

public class OrderResponseDto
{
    public int Id { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int TotalPrice { get; set; }

    public List<OrderItemResponseDto> Items { get; set; } = new();
}
