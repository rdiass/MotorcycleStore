using MotorcycleStore.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration();
builder.Services.AddSwaggerConfiguration();

// Add services to the container.
var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();