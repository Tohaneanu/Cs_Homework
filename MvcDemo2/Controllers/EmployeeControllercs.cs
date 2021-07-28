
using Application.Employees;
using Application.Employees.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MvcDemo2.Controllers
{
    public class EmployeeControllercs:Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeControllercs(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            return View(employees);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.GetEmployeeByAsync(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }


            return View(result.Value);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeRequest request)
        {
            var result = await _employeeService.CreateEmployeeAsync(request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.FirstName), result.Error);
                ModelState.AddModelError(nameof(request.LastName), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByAsync(id);

            if (employee.IsFailure)
            {
                ModelState.AddModelError(nameof(id), employee.Error);
                return View();
            }

            return View(employee.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] UpdateEmployeeRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            
            var result = await _employeeService.UpdateEmployeeAsync(id, request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.FirstName), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.GetEmployeeByAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();

            }

            return View(result.Value);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));

        }
    
    }
}
