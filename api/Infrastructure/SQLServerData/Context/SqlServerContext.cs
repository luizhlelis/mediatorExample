using System;
using Microsoft.EntityFrameworkCore;
using MediatorExample.Domain.Model.Entities;
using MediatorExample.Infrastructure.SQLServerData.Mappings;

namespace MediatorExample.Infrastructure.SQLServerData.Context
{
    public class SqlServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(
                @"Server=db;Database=MediatorExampleDb;User Id=sa;Password=1Secure*Password1;Connection Timeout=120;MultipleActiveResultSets=true"
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
