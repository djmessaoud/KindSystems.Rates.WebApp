using Kind.Systems.Rates.WebApp.Domain.Abstractions;
using Kind.Systems.Rates.WebApp.Domain.Models;
using Kind.Systems.Rates.WebApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Application.Commands
{
    // команда для обновления курсов валют
    public class RefreshRatesCommandHandler(
        IExchangeRateProvider provider,
        IExchangeRateRepository repo) :
        IRequestHandler<RefreshRatesCommand>
    {
        public async Task Handle(RefreshRatesCommand cmd, CancellationToken ct)
        {
            var dict = await provider.FetchAsync(cmd.Base, ct);
            var now = DateTime.UtcNow;

            var rates = dict.Select(kvp => new ExchangeRate
            {
                Pair = new CurrencyPair(cmd.Base, kvp.Key)
            }.Update(kvp.Value, now)).ToList();

            await repo.UpsertAsync(rates, ct);
        }
    }
}
