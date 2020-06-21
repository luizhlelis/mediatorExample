using System;
using System.Collections.Generic;
using MediatorExample.Domain.Data.NoSqlServerRepositoryContract.Generic.Entities;
using MongoDB.Bson;

namespace MediatorExample.Domain.Entities.Model
{
    public class Office : IIdentifiableMongoDocument
    {
        public ObjectId Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public double MonthlySalaryExpenses { get; set; }
        public List<EmployeeSummary> EmployeeList { get; set; }

        public Office() { }

        public Office(ObjectId id, string name, string address, double monthlyEmployeeExpenses)
        {
            Id = id;
            Name = name;
            Address = address;
            MonthlySalaryExpenses = monthlyEmployeeExpenses;
        }
    }

    public class EmployeeSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
