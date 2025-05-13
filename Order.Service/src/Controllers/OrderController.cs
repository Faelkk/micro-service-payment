using Microsoft.AspNetCore.Mvc;
using Order.DTO;
using Order.Repository;

namespace Order.Controllers;

[Route("[Controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IOrderRepository orderRepository;
    public OrderController(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {

            var orders = orderRepository.GetAll();

            return Ok(orders);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

    [HttpGet("{id}")]

    public IActionResult GetById(int id)
    {
        try
        {
            var order = orderRepository.GetById(id);

            return Ok(order);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderToInsert order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var orderCreated = await orderRepository.Post(order);

            return Created("", orderCreated);
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderToUpdate order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var orderUpdated = await orderRepository.Update(id, order);
            return Ok(orderUpdated);
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

            await orderRepository.Remove(id);

            return NoContent();
        }
        catch (Exception Err)
        {
            return BadRequest(new { message = Err.Message.ToString() });
        }
    }
}