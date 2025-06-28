using Kind.Systems.Rates.WebApp.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Infrastructure.External
{ 

public class RateLayerProvider : IExchangeRateProvider
    {
        private const string Key = "7ebceebcea401ab4ea7e3a33058068f3";
        private readonly string _key;
        private readonly HttpClient _http;
        private const string Url = "https://api.exchangerate.host/live?access_key={0}&source={1}";

        public RateLayerProvider(HttpClient http, IConfiguration configuration)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _key = configuration["ExchangeHostApiKey"] ?? throw new Exception("Ключ ExchangeRate API не найден");
        }

        public async Task<IReadOnlyDictionary<string, decimal>> FetchAsync(string baseCur, CancellationToken ct)
        {
            var json = await _http.GetFromJsonAsync<RateLayerDto>(
                           string.Format(Url, Key, baseCur), ct);

            if (json is null || !json.success || json.quotes is null)
                return new Dictionary<string, decimal>();

            // Убираем базовую валюту из ключей 
            int prefix = baseCur.Length;
            return json.quotes.ToDictionary(
                kvp => kvp.Key[prefix..], kvp => kvp.Value);
        }

        // DTO для ответа от ExchangeRate.host API (/live)
        private sealed class RateLayerDto
        {
            public bool success { get; set; }
            public Dictionary<string, decimal>? quotes { get; set; }
        }
    }
}
