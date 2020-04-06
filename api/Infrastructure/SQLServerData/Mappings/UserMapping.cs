using System;
using MediatorExample.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatorExample.Infrastructure.SQLServerData.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(table => table.Id);

            builder.Property(table => table.Id)
                .IsRequired(true);
            builder.Property(table => table.Cpf)
                .IsRequired(true);
            builder.Property(table => table.Name)
                .IsRequired(true);
            builder.Property(table => table.Email)
                .IsRequired(true);
            builder.Property(table => table.Phone)
                .IsRequired(true);

            builder.HasData
            (
                new User
                (
                    Guid.NewGuid(),
                    "111.111.111-11",
                    "Luiz Lelis",
                    "luizhlelis@gmail.com",
                    "(31)99999-9999"
                )
            );
        }
    }
}
