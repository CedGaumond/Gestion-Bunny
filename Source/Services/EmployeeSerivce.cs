using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeContext _context;

    public EmployeeService(EmployeeContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _context.Employee.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Employee.FindAsync(employeeId);
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _context.Employee.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        var employee = await _context.Employee.FindAsync(employeeId);
        if (employee != null)
        {
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    // New method to get a random employee
    public async Task<Employee> GetRandomEmployeeAsync()
    {
        // Get a count of all employees in the database
        var count = await _context.Employee.CountAsync();
        
        // Generate a random index based on the number of employees
        var randomIndex = new Random().Next(count);
        
        // Retrieve the employee at that index
        return await _context.Employee.Skip(randomIndex).Take(1).FirstOrDefaultAsync();
    }
}
