
using Application.RepositoryInterfaces;
using Domain.Employees;
using Infrastructure.Context;
using System.Linq;

namespace Infrastructure.Repositories
{
    public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }
        protected override IQueryable<Employee> DefaultIncludes(IQueryable<Employee> queryable)
        {
            return queryable.Includes(p=>p.Company);
        }
    }
}
