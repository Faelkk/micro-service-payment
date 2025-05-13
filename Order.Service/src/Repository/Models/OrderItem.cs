using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models;

public class OrderItem
{

    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string ProductName { get; set; }


    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public int ProductPrice { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantity { get; set; }

    [Required]
    public int OrderId { get; set; }


    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}