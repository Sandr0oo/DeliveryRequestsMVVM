using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestDelivery
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "DeliveryRequests.db" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            options.UseSqlite(connection);
        }
    }
}
