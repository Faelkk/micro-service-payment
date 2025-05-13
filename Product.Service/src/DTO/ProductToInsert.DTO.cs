using System.ComponentModel.DataAnnotations;

namespace Product.DTO;

public class ProductToInsert
{
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
    public string name { get; set; }

    [Required(ErrorMessage = "O preço do produto é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
    public int price { get; set; }
}