namespace Payment.Repository;

public interface IPaymentRepository
{
    Task<bool> UpdateOrderPaymentStatusAsync(string orderId, string status);
}