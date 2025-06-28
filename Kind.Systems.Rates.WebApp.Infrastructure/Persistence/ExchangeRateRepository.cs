using Kind.Systems.Rates.WebApp.Domain.Models;
using Kind.Systems.Rates.WebApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Infrastructure.Persistence
{
    public class ExchangeRateRepository(AppDbContext db)
       : IExchangeRateRepository
    {
        public async Task<IReadOnlyList<ExchangeRate>> GetLatestAsync(
            string baseCur, CancellationToken ct)
        {
            return await db.Rates
                .Where(r => r.Pair.Base == baseCur)
                .GroupBy(r => r.Pair.Quote)
                .Select(g => g
                    .OrderByDescending(x => x.RetrievedAt).First())
                .ToListAsync(ct);
        }

        public async Task UpsertAsync(IEnumerable<ExchangeRate> rates,
            CancellationToken ct)
        {
            db.Rates.AddRange(rates);
            await db.SaveChangesAsync(ct);
        }
    }
}
