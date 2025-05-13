using Microsoft.EntityFrameworkCore;

namespace Product.Context;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<Models.Product> Products { get; set; } = null;

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    // Implementação do método assíncrono
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var server = Environment.GetEnvironmentVariable("DBSERVER");
        var database = Environment.GetEnvironmentVariable("DBNAME");
        var dbuser = Environment.GetEnvironmentVariable("DBUSER");
        var dbpass = Environment.GetEnvironmentVariable("DBPASSWORD");

        // var connectionString = $"Server={server};Database={database};User={dbuser};Password={dbpass};TrustServerCertificate=True";

        var connectionString = $"Server=db-product;Database=productdb;User=sa;Password=ProductDbPassword!;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

    }
}
