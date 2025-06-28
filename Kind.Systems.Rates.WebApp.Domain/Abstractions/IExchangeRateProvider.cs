using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Domain.Abstractions
{
    public interface IExchangeRateProvider
    {
        Task<IReadOnlyDictionary<string, decimal>> FetchAsync(string baseCur, CancellationToken ct);
    }
}
