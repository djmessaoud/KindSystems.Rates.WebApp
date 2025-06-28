using Kind.Systems.Rates.WebApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kind.Systems.Rates.WebApp.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> opts)
        : DbContext(opts)
    {
        public DbSet<ExchangeRate> Rates => Set<ExchangeRate>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<ExchangeRate>(eb =>
            {
                eb.HasKey(r => r.Id);

                eb.OwnsOne(r => r.Pair, pb =>
                {
                    pb.Property(p => p.Base).HasColumnName("Base");
                    pb.Property(p => p.Quote).HasColumnName("Quote");

                    // index lives on the owned type itself
                    pb.HasIndex(p => new { p.Base, p.Quote });
                });

                // optional: index on RetrievedAt alone
                eb.HasIndex(r => r.RetrievedAt);
            });
        }
    }
}
