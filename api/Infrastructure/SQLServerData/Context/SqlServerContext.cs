using System;
using Microsoft.EntityFrameworkCore;
using MediatorExample.Domain.Model.Entities;
using MediatorExample.Infrastructure.SqlServerData.Mappings;

namespace MediatorExample.Infrastructure.SqlServerData.Context
{
    public class SqlServerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }
    }
}
