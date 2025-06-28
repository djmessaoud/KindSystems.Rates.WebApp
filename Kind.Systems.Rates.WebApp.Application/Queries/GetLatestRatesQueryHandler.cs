using Ardalis.Result;
using Kind.Systems.Rates.WebApp.Application.DTOs;
using Kind.Systems.Rates.WebApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Application.Queries
{
    public class GetLatestRatesQueryHandler(
        IExchangeRateRepository repo) :
        IRequestHandler<GetLatestRatesQuery,
            Result<IReadOnlyList<ExchangeRateDto>>>
    {
        public async Task<Result<IReadOnlyList<ExchangeRateDto>>> Handle(
            GetLatestRatesQuery q, CancellationToken ct)
        {
            var data = await repo.GetLatestAsync(q.Base, ct);

            var dto = data.Select(r =>
                new ExchangeRateDto(
                    r.Pair.Base, r.Pair.Quote,
                    r.Rate, r.RetrievedAt)).ToList();

            return Result.Success<IReadOnlyList<ExchangeRateDto>>(dto);
        }
    }
 }
