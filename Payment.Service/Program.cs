using Order.Configuration;
using Payment.Repository;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<IPaymentRepository, PaymentRepository>(client =>
{
    client.BaseAddress = new Uri("http://api.gateway:5012");
});

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

var port = builder.Configuration["APIPORT"];
builder.WebHost.UseUrls($"http://*:{port}");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Configuration.AddEnvironmentVariables();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
