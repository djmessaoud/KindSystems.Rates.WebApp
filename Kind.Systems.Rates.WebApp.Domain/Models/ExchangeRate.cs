using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Domain.Models
{
    public class ExchangeRate
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public CurrencyPair Pair { get; init; } = default!;
        public decimal Rate { get; private set; }
        public DateTime RetrievedAt { get; private set; }

        public ExchangeRate Update(decimal newRate, DateTime when)
        {
            Rate = newRate;
            RetrievedAt = when;
            return this;
        }
    }
}
