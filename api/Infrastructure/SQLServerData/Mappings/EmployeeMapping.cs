using System;
using MediatorExample.Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatorExample.Infrastructure.SqlServerData.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(table => table.Id);

            builder.Property(table => table.Id)
                .IsRequired(true);
            builder.Property(table => table.Name)
                .IsRequired(true);
            builder.Property(table => table.Email)
                .IsRequired(true);
            builder.Property(table => table.Phone)
                .IsRequired(true);
            builder.Property(table => table.Salary)
                .IsRequired(true);

            builder.HasData
            (
                new Employee
                (
                    Guid.Parse("dd7b68b4-9bc0-4b9b-81d9-ddede17ce98d"),
                    "Luiz Lelis",
                    "luizhlelis@gmail.com",
                    "(31)99999-9999",
                    10000
                )
            );
        }
    }
}
