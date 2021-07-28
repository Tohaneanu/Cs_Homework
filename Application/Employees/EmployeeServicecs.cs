using Application.Companies.Responses;
using Application.Employees.Request;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees
{
    public sealed class EmployeeServicecs : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeServicecs(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Result> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            Result<CompanyName> companyNameOrError = CompanyName.Create(request?.CompanyName);

            if (companyNameOrError.IsFailure)
            {
                return Result.Failure(companyNameOrError.Error);
            }

            var employee = new Employee(companyNameOrError.Value);

            await _employeeRepository.AddAsync(employee);

            return Result.Success();
        }

        public async Task<Result> DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return Result.Failure($"Employee with Id {employeeId} was not found");
            }

            await _employeeRepository.Delete(employee);

            return Result.Success();
        }

        public async Task<IList<EmployeeResponses>> GetAllEmployeesAsync()
        {
            var response = new List<EmployeeResponses>();

            var employees = await _employeeRepository.GetAllAsync();

            foreach (var employee in employees)
            {
                var employeeResponses = new EmployeeResponses
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName=employee.LastName,
                    Company =employee.Company,
                    Email=employee.Email,                
                };

                response.Add(employeeResponses);
            }

            return response;
        }

        public async Task<Result<EmployeeResponses>> GetEmployeeByAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return Result.Failure<EmployeeResponses>($"Employee with Id {id} was not found");
            }

            var response = new EmployeeResponses()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Company = employee.Company,
                Email = employee.Email,
            };

            return Result.Success(response);
        }

        public Task<Result> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request)
        {
            Result<CompanyName> companyNameOrError = CompanyName.Create(request?.CompanyName);

            if (companyNameOrError.IsFailure)
            {
                return Result.Failure(companyNameOrError.Error);
            }

            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
            {
                return Result.Failure($"Company with Id {companyId} was not found");
            }

            company.UpdateCompany(companyNameOrError.Value);

            await _companyRepository.Update(company);

            return Result.Success();
        }
    }
}
