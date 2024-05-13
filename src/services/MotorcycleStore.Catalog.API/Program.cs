using MotorcycleStore.Catalog.API.Configuration;
using MotorcycleStore.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddMongoDbConfig(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.RegisterServices();
builder.Services.AddMessageBusConfiguration(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();