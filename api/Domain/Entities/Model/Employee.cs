using System;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic.Entities;
using MediatR;

namespace MediatorExample.Domain.Entities.Model
{
    public class Employee : IIdentifiableEntity, INotification
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public double Salary { get; private set; }

        public Employee() { }

        public Employee(Guid id, string name, string email, string phone, double salary)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Salary = salary;
        }
    }
}
