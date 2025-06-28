using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Domain.Models
{
    public class CurrencyPair
    {

        private CurrencyPair() { }
        public CurrencyPair(string baseCur, string quoteCur)
        {
            Base = baseCur.ToUpperInvariant();
            Quote = quoteCur.ToUpperInvariant();
        }
        public string Base { get; }      // USD
        public string Quote { get; }      // EUR

       
        public override bool Equals(object? obj) =>
            obj is CurrencyPair p && p.Base == Base && p.Quote == Quote;
        public override int GetHashCode() => HashCode.Combine(Base, Quote);
        public override string ToString() => $"{Base}/{Quote}";
    }
}
