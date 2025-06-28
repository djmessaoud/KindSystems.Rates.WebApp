using Kind.Systems.Rates.WebApp.Application.Commands;
using Kind.Systems.Rates.WebApp.Application.Queries;
using Kind.Systems.Rates.WebApp.Components;
using Kind.Systems.Rates.WebApp.Domain.Repositories;
using Kind.Systems.Rates.WebApp.Infrastructure.External;
using Kind.Systems.Rates.WebApp.Infrastructure.Hosted;
using Kind.Systems.Rates.WebApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System;
using Kind.Systems.Rates.WebApp.Domain.Abstractions;

namespace Kind.Systems.Rates.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region UserSecrets
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }
            #endregion
            #region Services
            #region UI
            builder.Services.AddRazorComponents()
                            .AddInteractiveServerComponents();
            builder.Services.AddRadzenComponents();
            builder.Services.AddRadzenCookieThemeService(opts =>
            {
                opts.Name = "RatesTheme";
                opts.Duration = TimeSpan.FromDays(365);
            });
            #endregion UI

            #region Infrastructure
            builder.Services.AddControllers();

            
            builder.Services.AddMediatR(cfg => cfg
                .RegisterServicesFromAssemblyContaining<GetLatestRatesQuery>());
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

            builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
            builder.Services.AddHttpClient<IExchangeRateProvider, RateLayerProvider>();
            builder.Services.Configure<UpdaterOptions>(
                builder.Configuration.GetSection("Updater"));
            builder.Services.AddHostedService<RateUpdaterService>();
            #endregion Infrastructure
            #endregion Services


            var app = builder.Build();
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAntiforgery();
            
            app.MapControllers(); // API для функционала экспорта

            app.MapRazorComponents<App>()
               .AddInteractiveServerRenderMode();

            #region DbMigration

            await using (var scope = app.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();
            }
            #endregion
            app.Run();

        }
    }
}
