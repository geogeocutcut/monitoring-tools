using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using monitoring.business.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring.dal.ef
{
    public class MonitoringToolDbContext : DbContext
    {
            public DbSet<ApiEndpoint> ApiEndpoints { get; set; }

            public MonitoringToolDbContext(DbContextOptions<MonitoringToolDbContext> optionsBuilder) : base(optionsBuilder)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ApiEndpoint>()
                    .ToTable("api_endpoints",schema:"monitoring")
                    .HasKey(a => a.Id);

                modelBuilder.Entity<ApiEndpoint>()
                    .Property(a => a.Id)
                    .HasColumnName("id");

                modelBuilder.Entity<ApiEndpoint>()
                    .Property(a => a.Name)
                    .HasColumnName("name");

                modelBuilder.Entity<ApiEndpoint>()
                    .Property(a => a.Description)
                    .HasColumnName("description");
            }
    }
}
