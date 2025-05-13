using Microsoft.AspNetCore.Mvc;
using Product.DTO;
using Product.Repository;


namespace Product.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {

            var products = productRepository.GetAll();

            return Ok(products);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int Id)
    {
        try
        {

            var productById = productRepository.GetById(Id);

            return Ok(productById);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductToInsert product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var productCreated = productRepository.Post(product);

            return Created("", productCreated);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductToUpdate product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedProduct = await productRepository.Update(id, product);
            return Ok(updatedProduct);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {

            await productRepository.Remove(id);

            return NoContent();
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

}