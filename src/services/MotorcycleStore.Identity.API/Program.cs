using Microsoft.AspNetCore.Identity;
using MS.Identity.API.Models;
using MS.Identity.API.Settings;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services        
        .AddAuthorization()
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
        (
            mongoDbSettings.ConnectionString, mongoDbSettings.Name
        )
        .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() {
        Version = "v1",
        Title = "MotorcycleStore Identity API", 
        Description = "This API it is used for create new users and login",
        Contact = new() { Name = "Rafael Santos", Email = "rafaeldias.a@hotmail.com" },
        License = new() { Name = "MIT", Url = new("https://opensource.org/licenses/MIT") }
    });
});

// Add services to the container.
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app
    .UseRouting()
    .UseEndpoints(r =>
    {
        r.MapDefaultControllerRoute();
    });

app.Run();