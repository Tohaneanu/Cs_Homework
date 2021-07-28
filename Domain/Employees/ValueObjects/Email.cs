using Domain.Base;
using System;


namespace Domain.Employees.ValueObjects
{
    public sealed class EmployeeEmail : ValueObject<EmployeeEmail>
    {
        public readonly string Value;

        private EmployeeEmail(string value)
        {
            Value = value;
        }

        public static Result<EmployeeEmail> Create(string employeeEmail)
        {
            if (string.IsNullOrWhiteSpace(employeeEmail))
            {
                return Result.Failure<EmployeeEmail>("Employee email was not provided");
            }

            return Result.Success(new EmployeeEmail(employeeEmail));
        }

        protected override bool EqualsCore(EmployeeEmail other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static explicit operator EmployeeEmail(string employeeEmail)
        {
            return Create(employeeEmail).Value;
        }

        public static implicit operator string(EmployeeEmail employeeEmail)
        {
            return employeeEmail.Value;
        }
    }
    
}
