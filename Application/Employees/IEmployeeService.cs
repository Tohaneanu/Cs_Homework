
using Application.Companies.Responses;
using Application.Employees.Request;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Employees
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeResponses>> GetAllEmployeesAsync();
        Task<Result<EmployeeResponses>> GetEmployeeByAsync(Guid id);
        Task<Result> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<Result> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request);
        Task<Result> DeleteEmployeeAsync(Guid employeeId);
    }
}
