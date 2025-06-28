using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Application.DTOs
{
    /// <summary>
    /// DTO для обменного курса валюты.
    /// </summary>
    public record ExchangeRateDto(
        string Base,
        string Quote,
        decimal Rate,
        DateTime RetrievedAt);
}
