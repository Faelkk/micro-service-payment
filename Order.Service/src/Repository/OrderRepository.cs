using Microsoft.EntityFrameworkCore;
using Order.DTO;
using Order.Models;

namespace Order.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IDatabaseContext databaseContext;
    private readonly HttpClient httpClient;

    public OrderRepository(IDatabaseContext databaseContext, HttpClient httpClient)
    {
        this.databaseContext = databaseContext;
        this.httpClient = httpClient;
    }



    public IEnumerable<OrderResponseDto> GetAll()
    {
        var orders = databaseContext.Orders.Select(o => new OrderResponseDto
        {
            Id = o.Id,
            PaymentStatus = o.PaymentStatus,
            CreatedAt = o.CreatedAt,
            TotalPrice = o.TotalPrice,
            Items = o.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.ProductPrice
            }).ToList()
        }).ToList();

        if (!orders.Any())
        {
            throw new Exception("No products were found");
        }

        return orders;
    }



    public OrderResponseDto GetById(int id)
    {
        var order = databaseContext.Orders
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found.");
        }

        return new OrderResponseDto
        {
            Id = order.Id,
            PaymentStatus = order.PaymentStatus,
            CreatedAt = order.CreatedAt,
            TotalPrice = order.TotalPrice,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.ProductPrice
            }).ToList()
        };
    }

    public async Task<OrderResponseDto> Post(OrderToInsert orderDto)
    {
        if (orderDto.Items == null || !orderDto.Items.Any())
            throw new ArgumentException("Order must contain at least one item.");

        var orderItems = new List<OrderItem>();
        int totalPrice = 0;

        var checkoutItems = new List<SessionItem>();

        foreach (var item in orderDto.Items)
        {
            var response = await httpClient.GetAsync($"/products/{item.ProductId}").ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Product with ID {item.ProductId} not found.");

            var productJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var product = System.Text.Json.JsonSerializer.Deserialize<ProductDto>(productJson, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = item.Quantity
            };

            totalPrice += product.Price * item.Quantity;
            orderItems.Add(orderItem);

            checkoutItems.Add(new SessionItem
            {
                ProductName = product.Name,
                UnitAmount = product.Price,
                Quantity = item.Quantity
            });
        }

        var order = new Models.Order
        {
            PaymentStatus = orderDto.PaymentStatus,
            CreatedAt = DateTime.UtcNow,
            TotalPrice = totalPrice,
            Items = orderItems
        };

        databaseContext.Orders.Add(order);
        await databaseContext.SaveChangesAsync().ConfigureAwait(false);


        var checkoutRequest = new CreateCheckoutSessionRequest
        {
            Items = checkoutItems,
            OrderId = order.Id.ToString(),
            SuccessUrl = $"http://localhost:3000/payment/success?orderId={order.Id}",
            CancelUrl = $"http://localhost:3000/payment/cancel?orderId={order.Id}"
        };

        var responseCheckout = await httpClient.PostAsJsonAsync("/payments/create-checkout-session", checkoutRequest);

        if (!responseCheckout.IsSuccessStatusCode)
            throw new Exception("Failed to create checkout session.");

        var checkoutResponse = await responseCheckout.Content.ReadFromJsonAsync<CheckoutSessionResponse>();

        return new OrderResponseDto
        {
            Id = order.Id,
            PaymentStatus = order.PaymentStatus,
            CreatedAt = order.CreatedAt,
            TotalPrice = order.TotalPrice,
            CheckoutUrl = checkoutResponse?.Url,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.ProductPrice
            }).ToList()
        };
    }


    public async Task<OrderResponseDto> Update(int id, OrderToUpdate dto)
    {
        var order = databaseContext.Orders
            .Include(o => o.Items)
            .FirstOrDefault(o => o.Id == id);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found.");
        }

        order.PaymentStatus = dto.PaymentStatus;
        await databaseContext.SaveChangesAsync();

        return new OrderResponseDto
        {
            Id = order.Id,
            PaymentStatus = order.PaymentStatus,
            CreatedAt = order.CreatedAt,
            TotalPrice = order.TotalPrice,
            Items = order.Items.Select(i => new OrderItemResponseDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.ProductPrice
            }).ToList()
        };
    }



    public async Task Remove(int id)
    {
        var order = databaseContext.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found.");
        }

        databaseContext.Orders.Remove(order);
        await databaseContext.SaveChangesAsync();
    }


}