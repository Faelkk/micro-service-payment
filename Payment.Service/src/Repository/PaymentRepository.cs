namespace Payment.Repository;


public class PaymentRepository : IPaymentRepository
{
    private readonly HttpClient _httpClient;

    public PaymentRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> UpdateOrderPaymentStatusAsync(string orderId, string status)
    {
        var payload = new { PaymentStatus = status };
        var response = await _httpClient.PatchAsJsonAsync($"/orders/{orderId}", payload);
        return response.IsSuccessStatusCode;
    }
}
