using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace Order.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string PaymentStatus { get; set; } = "Pending";

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O preço total deve ser maior que zero.")]
    public int TotalPrice { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
