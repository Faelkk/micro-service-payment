
using Microsoft.AspNetCore.Mvc;
using Payment.Repository;
using Stripe;
using Stripe.Checkout;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{

    private readonly IPaymentRepository paymentRepository;


    private readonly IConfiguration configuration;

    public PaymentController(IPaymentRepository paymentRepository, IConfiguration configuration)
    {
        this.paymentRepository = paymentRepository;
        this.configuration = configuration;
    }


    [HttpPost("create-checkout-session")]
    public IActionResult CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = request.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                    },
                    UnitAmount = item.UnitAmount,
                    Currency = "brl"
                },
                Quantity = item.Quantity,
            }).ToList(),
            Mode = "payment",
            SuccessUrl = request.SuccessUrl,
            CancelUrl = request.CancelUrl,
            Metadata = new Dictionary<string, string>
{
    { "orderId", request.OrderId.ToString() }
}
        };

        var service = new SessionService();
        Session session = service.Create(options);

        Console.WriteLine($"Creating checkout session for OrderId: {request.OrderId}");


        return Ok(new { sessionId = session.Id, url = session.Url });
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var endpointSecret = configuration["Stripe:WebhookSecret"];


        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret
            );

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var sessionId = ((Session)stripeEvent.Data.Object).Id;
                var service = new SessionService();
                var session = service.Get(sessionId);
                Console.WriteLine($"Session ID: {session.Id}");
                Console.WriteLine($"Session Metadata: {System.Text.Json.JsonSerializer.Serialize(session.Metadata)}");


                if (session.Metadata != null && session.Metadata.TryGetValue("orderId", out var orderId))
                {
                    var updatePayload = new { PaymentStatus = "Paid" };
                    var success = await paymentRepository.UpdateOrderPaymentStatusAsync(orderId, "Paid");
                    if (!success)
                    {
                        return StatusCode(500, "Erro ao atualizar o status do pedido");
                    }

                    Console.WriteLine($"Pagamento concluído para sessão: {session.Id}");
                }
                else
                {
                    Console.WriteLine("Metadata ou orderId não encontrado");
                    return BadRequest();
                }
            }


            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}



