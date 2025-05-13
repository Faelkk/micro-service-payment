using Microsoft.EntityFrameworkCore;

namespace Product.Context;

public interface IDatabaseContext
{
    DbSet<Models.Product> Products { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
