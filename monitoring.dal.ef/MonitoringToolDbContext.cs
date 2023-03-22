using Microsoft.EntityFrameworkCore;
using monitoring.business.model;
using monitoring.dal.ef.MyProject.Infrastructure.Persistence;
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

            public MonitoringToolDbContext(DbContextOptions<MonitoringToolDbContext> options) : base(options)
            {
            }
    }
}
