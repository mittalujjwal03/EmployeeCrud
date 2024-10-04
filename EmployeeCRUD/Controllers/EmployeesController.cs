using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeCRUD.DataAccessLayer;
using EmployeeCRUD.Models;

namespace EmployeeCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees  
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return View(employees);
        }

        // GET: Employees/Details/5  
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var employee = await GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create  
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Employees/Create  
        // POST: Employees/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Position,Salary,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Employee created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError("", "An error occurred while creating the employee. Please try again.");
                }
            }

            PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var employee = await GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Position,Salary,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Employee updated successfully."; // Success message
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError("", "An error occurred while updating the employee. Please try again.");
                }
            }

            PopulateDepartmentsDropDownList(employee.DepartmentId);
            return View(employee);
        }
        // GET: Employees/Delete/5  
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Department) 
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        private async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
        }

        private void PopulateDepartmentsDropDownList(int? selectedDepartmentId = null)
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", selectedDepartmentId);
        }
    }
}