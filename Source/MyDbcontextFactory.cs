using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

public class MyDbContextFactory : 
IDesignTimeDbContextFactory<EmployeeContext>
{
    public EmployeeContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeContext>();
        string connectionString = DatabaseConfiguration.GetConnectionString();
        optionsBuilder.UseNpgsql(connectionString);

        return new EmployeeContext(optionsBuilder.Options);
    }
}