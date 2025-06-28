using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Application.Commands
{
    public record RefreshRatesCommand(string Base) : IRequest;
}
