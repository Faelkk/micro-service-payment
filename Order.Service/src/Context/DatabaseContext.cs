using Microsoft.EntityFrameworkCore;

namespace Order.Context;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<Models.Order> Orders { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

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

        var connectionString = $"Server=db-order;Database=orderdb;User=sa;Password=OrderDbPassword!;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

    }
}