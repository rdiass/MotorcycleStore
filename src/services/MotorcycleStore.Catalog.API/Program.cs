using MotorcycleStore.Catalog.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();
builder.Services.AddMongoDbConfig(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();