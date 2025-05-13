using Microsoft.EntityFrameworkCore;

public interface IDatabaseContext
{
    DbSet<Order.Models.Order> Orders { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}