using MotorcycleStore.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMessageBusConfiguration(builder.Configuration);
builder.Services.AddMvcConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.RegisterServices();

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.Run();
