using System;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;

namespace MediatorExample.Domain.Model.Entities
{
    public class Customer : IIdentifiableEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Customer(){}

        public Customer(Guid id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}