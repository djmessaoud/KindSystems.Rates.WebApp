using Ardalis.Result;
using Kind.Systems.Rates.WebApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
namespace Kind.Systems.Rates.WebApp.Application.Queries
{
    public record GetLatestRatesQuery(string Base)
        : IRequest<Result<IReadOnlyList<ExchangeRateDto>>>;
}
