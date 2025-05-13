using Product.DTO;

namespace Product.Repository;

public interface IProductRepository
{
    IEnumerable<ProductResponseDto> GetAll();

    ProductResponseDto GetById(int id);

    ProductResponseDto Post(ProductToInsert product);

    Task<ProductResponseDto> Update(int id, ProductToUpdate product);

    Task Remove(int id);
}