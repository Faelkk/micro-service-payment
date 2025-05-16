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


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var server = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var database = Environment.GetEnvironmentVariable("DB_NAME");
        var dbuser = Environment.GetEnvironmentVariable("DB_USER");
        var dbpass = Environment.GetEnvironmentVariable("DB_PASSWORD");

        var connectionString = $"Host={server};Port={port};Database={database};Username={dbuser};Password={dbpass}";

        optionsBuilder.UseNpgsql(connectionString);
    }
}
