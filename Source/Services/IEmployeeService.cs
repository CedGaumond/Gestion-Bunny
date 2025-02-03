
using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services;
public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int employeeId);
    
    // Add method to fetch random employee
    Task<Employee> GetRandomEmployeeAsync();
}
