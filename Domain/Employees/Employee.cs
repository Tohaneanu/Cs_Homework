using System;
using Domain.Base;
using Domain.Companies;
using Domain.Employees.ValueObjects;

namespace Domain.Employees
{
    public class Employee : BasicEntity
    {
        private Result<Name> name;
        private Result<EmployeeEmail> email;
        private Address address;

        private Employee()
        {

        }

        public Employee(Address address, Guid companyId)
        {
            Id = Guid.NewGuid();
            Address = address;
            CompanyId = companyId;
        }
        public Employee(Name name, EmployeeEmail email,Address address, Guid companyId)
        {
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
            Address = address;
            CompanyId = companyId;

        }

        public Employee(Result<Name> name, Result<EmployeeEmail> email, Address address, Guid companyId)
        {
            this.name = name;
            this.email = email;
            this.address = address;
            CompanyId = companyId;
        }

        public Name Name { get; set; }
        public  EmployeeEmail Email { get; set; }
        public Address Address { get; private set; }
        public Guid CompanyId { get; private set; }
        public Company Company { get; set; }

    }
}
