using Microsoft.AspNetCore.Mvc.DataAnnotations;
using MotorcycleStore.WebApp.MVC.Extensions;
using MotorcycleStore.WebApp.MVC.Services;
using MotorcycleStore.WebApp.MVC.Services.Handlers;
using Polly;

namespace MotorcycleStore.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CnpjValidationAttributeAdapterProvider>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        
        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitAndRetryAsync())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }
}
