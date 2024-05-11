using Microsoft.AspNetCore.Localization;
using MotorcycleStore.WebApp.MVC.Extensions;
using System.Globalization;

namespace MotorcycleStore.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static void AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration);
        services.AddControllersWithViews();
    }

    public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if (!env.IsDevelopment())
        //{
        //    app.UseExceptionHandler("/error/500");
        //    app.UseStatusCodePagesWithRedirects("/error/{0}");
        //    app.UseHsts();
        //}

        app.UseExceptionHandler("/error/500");
        app.UseStatusCodePagesWithRedirects("/error/{0}");
        app.UseHsts();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();        
        app.UseIdentityConfiguration();

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures =
            [
                new CultureInfo("en-US"),
                new CultureInfo("pt-BR")
            ],
            SupportedUICultures =
            [
                new CultureInfo("en-US"),
                new CultureInfo("pt-BR")
            ]
        });

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Catalog}/{action=Index}/{id?}");
        });
    }
}
