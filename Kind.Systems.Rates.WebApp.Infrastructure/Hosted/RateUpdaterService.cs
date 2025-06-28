using Kind.Systems.Rates.WebApp.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Infrastructure.Hosted
{
    public class RateUpdaterService(
        IServiceScopeFactory sf,
        IOptions<UpdaterOptions> opt) : BackgroundService
    {
        // обновляет курсы валют каждые 5 минут 
        protected override async Task ExecuteAsync(CancellationToken stop)
        {
            var delay = TimeSpan.FromMinutes(opt.Value.IntervalMinutes);

            while (!stop.IsCancellationRequested)
            {
                await Task.Delay(delay, stop);

                await using var scope = sf.CreateAsyncScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                // Запускаем обновление курсов для всех базовых валют (пока умолчанию USD)
                foreach (var b in opt.Value.BaseCurrencies)
                    await mediator.Send(new RefreshRatesCommand(b), stop);
            }
        }
    }

    public record UpdaterOptions
    {
        public int IntervalMinutes { get; init; } = 5;
        public string[] BaseCurrencies { get; init; } = ["USD"];
    }
}
