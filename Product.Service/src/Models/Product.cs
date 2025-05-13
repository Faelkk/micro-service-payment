using System.ComponentModel.DataAnnotations;

namespace Product.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "O preço do Produto é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public int Price { get; set; }
}