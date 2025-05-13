using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.DTO;
using Product.Models;

namespace Product.Repository;

public class ProductRepository : IProductRepository
{

    private readonly IDatabaseContext databaseContext;

    public ProductRepository(IDatabaseContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public IEnumerable<ProductResponseDto> GetAll()
    {
        var products = databaseContext.Products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price
        });

        if (!products.Any())
        {
            throw new Exception("No products were found");
        }

        return products;
    }


    public ProductResponseDto GetById(int id)
    {
        var productById = databaseContext.Products.Where(p => p.Id == id).Select(p => new ProductResponseDto { Id = p.Id, Name = p.Name, Price = p.Price }).FirstOrDefault();

        if (productById == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }

        return productById;
    }

    public ProductResponseDto Post(ProductToInsert product)
    {

        var newProduct = new Models.Product { Name = product.name, Price = product.price };

        databaseContext.Products.Add(newProduct);
        databaseContext.SaveChanges();

        return new ProductResponseDto { Id = newProduct.Id, Name = newProduct.Name, Price = newProduct.Price };
    }

    public async Task<ProductResponseDto> Update(int id, ProductToUpdate product)
    {
        var existingProduct = await databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (existingProduct == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }


        existingProduct.Name = product.Name ?? existingProduct.Name;
        existingProduct.Price = product.Price ?? existingProduct.Price;

        await databaseContext.SaveChangesAsync();

        return new ProductResponseDto
        {
            Id = existingProduct.Id,
            Name = existingProduct.Name,
            Price = existingProduct.Price
        };
    }


    public async Task Remove(int id)
    {
        var productById = await databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (productById == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }

        databaseContext.Products.Remove(productById);
        await databaseContext.SaveChangesAsync();
    }

}