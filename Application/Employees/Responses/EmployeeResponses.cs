using Domain.Companies;
using Domain.Employees.ValueObjects;
using System;

namespace Application.Companies.Responses
{
    public class EmployeeResponses
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Company Company { get; set; }
    }
}
