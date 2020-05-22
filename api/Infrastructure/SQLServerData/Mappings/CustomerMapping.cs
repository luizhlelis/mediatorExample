using System;
using MediatorExample.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatorExample.Infrastructure.SqlServerData.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(table => table.Id);

            builder.Property(table => table.Id)
                .IsRequired(true);
            builder.Property(table => table.Name)
                .IsRequired(true);
            builder.Property(table => table.Email)
                .IsRequired(true);
            builder.Property(table => table.Phone)
                .IsRequired(true);

            builder.HasData
            (
                new Customer
                (
                    Guid.Parse("dd7b68b4-9bc0-4b9b-81d9-ddede17ce98d"),
                    "Luiz Lelis",
                    "luizhlelis@gmail.com",
                    "(31)99999-9999"
                )
            );
        }
    }
}
