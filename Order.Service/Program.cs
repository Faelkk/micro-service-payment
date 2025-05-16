using Microsoft.EntityFrameworkCore;
using Order.Context;
using Order.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddHttpClient<IOrderRepository, OrderRepository>(client =>
{
    client.BaseAddress = new Uri("http://api.gateway:5012");
});


builder.Services.AddOpenApi();


var port = builder.Configuration["APIPORT"];
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
