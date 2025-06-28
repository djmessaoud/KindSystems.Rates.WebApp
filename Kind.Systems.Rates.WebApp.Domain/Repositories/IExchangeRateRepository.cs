using Kind.Systems.Rates.WebApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Domain.Repositories
{
    public interface IExchangeRateRepository
    {

        Task<IReadOnlyList<ExchangeRate>> GetLatestAsync(string baseCur, CancellationToken ct);

        Task UpsertAsync(IEnumerable<ExchangeRate> rates, CancellationToken ct);
    }
}
