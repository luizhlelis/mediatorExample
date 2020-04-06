using System;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;

namespace MediatorExample.Domain.Model.Entities
{
    public class User : IIdentifiableEntity
    {
        public Guid Id { get; private set; }
        public string Cpf { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public User(){}

        public User(Guid id, string cpf, string name, string email, string phone)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}