using Order.DTO;

namespace Order.Repository;

public interface IOrderRepository
{
    IEnumerable<OrderResponseDto> GetAll();

    OrderResponseDto GetById(int id);

    Task<OrderResponseDto> Post(OrderToInsert product);

    Task<OrderResponseDto> Update(int id, OrderToUpdate product);

    Task Remove(int id);
}