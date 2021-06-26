using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Data;
using Northwind.Models;

namespace Northwind_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeeController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
        
        // GET: api/Employee
        [HttpGet("/GetEmployeeUsingEagerLoading")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeUsingEagerLoading(int employeeId)
        {
            //Linq Query
            // return await _context.Employees
            //                     .Include(e => e.Orders.Where(o => o.ShipCountry == "brazil"))
            //                     .Where(e => e.EmployeeId == employeeId)
            //                     .ToListAsync();
            // Expression Query
            return await (from emp in _context.Employees
                            join ord in _context.Orders on emp.EmployeeId equals ord.EmployeeId
                            where emp.EmployeeId == employeeId
                            select emp).ToListAsync();
        }

        // GET: api/Employee
        [HttpGet("/GetEmployeeUsingExplicitLoading")]
        public async Task<ActionResult<Employee>> GetEmployeeUsingExplicitLoading(int employeeId)
        {
            //Linq Query
            // return await _context.Employees
            //                     .Include(e => e.Orders.Where(o => o.ShipCountry == "brazil"))
            //                     .Where(e => e.EmployeeId == employeeId)
            //                     .ToListAsync();

            var employee = _context.Employees.Single(e => e.EmployeeId == employeeId);

            _context.Entry(employee)
                    .Collection(e => e.Orders)
                    .Query()
                    .Where(o => o.ShipCountry == "brazil")
                    .Load();

            return await Task.FromResult(employee);
        }

        // GET: api/Supplier
        [HttpGet("/GetSupplierUsingLazyLoading")]
        public async Task<ActionResult<Object>> GetSupplierUsingLazyLoading()
        {
            var products = _context.Products.ToList();
            var companyName = products.Last().Supplier.CompanyName;

            return await Task.FromResult(companyName);
            //return await Task.FromResult(new Supplier());
        }

        // GET: api/Employee
        [HttpGet("/GetEmployeesUsingQueryExpression")]
        public async Task<ActionResult<IEnumerable<String>>> GetEmployeesUsingQueryExpression()
        {
            //Grouping is little tricky 
            // var employees = (from emp in _context.Employees.ToList()
            //                  where emp.EmployeeId > 5 || emp.FirstName.Length > 5
            //                  group emp by emp.Title);

            // var results = new List<string>();

            // foreach (var emp in employees.ToList())
            // {
            //     results.Add($"Employee Title : {emp.Key} & Count : {emp.Count()}");
            // }

            // return await Task.FromResult(results);

            //and this is how you write joins in LINQ Query expression
            var results = await (
                            from emp in _context.Employees
                            join ord in _context.Orders on emp.EmployeeId equals ord.EmployeeId 
                            select new String($"{emp.EmployeeId} has order {ord.ShipName}")
                            ).ToListAsync();

            return results;
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
