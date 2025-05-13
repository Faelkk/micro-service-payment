using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Carrega ocelot.json
builder.Configuration.AddJsonFile("ocelot.json");

// Adiciona Ocelot
builder.Services.AddOcelot();

var port = builder.Configuration["APIPORT"];
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Usa o middleware do Ocelot
await app.UseOcelot();

app.Run();
