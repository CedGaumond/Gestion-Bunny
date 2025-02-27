using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ScheduleService : IScheduleService
{
    private readonly ApplicationDbContext _context;

    public ScheduleService(ApplicationDbContext context)
    {
        _context = context;
    }

    private DateTime EnsureUtc(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            // Assume the input is in the server's local time zone
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            return dateTime.ToUniversalTime();
        }

        // Already UTC
        return dateTime;
    }

    /// <summary>
    /// Retrieves all schedules from the database, including related employee data.
    /// </summary>
    public async Task<List<Schedule>> GetSchedulesAsync()
    {
        try
        {
            return await _context.Schedules
                .Include(s => s.Employee) // Include the related Employee entity
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., using a logging framework)
            Console.WriteLine($"Error retrieving schedules: {ex.Message}");
            throw; // Re-throw the exception to be handled by the caller
        }
    }

    /// <summary>
    /// Retrieves schedules for a specific employee, including related employee data.
    /// </summary>
    public async Task<List<Schedule>> GetEmployeeSchedulesAsync(int employeeId)
    {
        try
        {
            return await _context.Schedules
                .Where(s => s.EmployeeId == employeeId) // Filter by EmployeeId
                .Include(s => s.Employee) // Include the related Employee entity
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedules for employee {employeeId}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves schedules for a specific employee and week, including related employee data.
    /// </summary>
    public async Task<List<Schedule>> GetEmployeeSchedulesForWeekAsync(int employeeId, DateTime weekStart)
    {
        try
        {
            var weekEnd = weekStart.AddDays(7); // Calculate the end of the week

            // Ensure weekStart and weekEnd are in UTC
            var utcWeekStart = EnsureUtc(weekStart);
            var utcWeekEnd = EnsureUtc(weekEnd);

            return await _context.Schedules
                .Where(s => s.EmployeeId == employeeId && s.ShiftStart >= utcWeekStart && s.ShiftStart < utcWeekEnd) // Filter by EmployeeId and week range
                .Include(s => s.Employee) // Include the related Employee entity
                .OrderBy(s => s.ShiftStart) // Order by ShiftStart
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedules for employee {employeeId} and week starting {weekStart}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a specific schedule by its ID, including related employee data.
    /// </summary>
    public async Task<Schedule> GetScheduleByIdAsync(int scheduleId)
    {
        try
        {
            return await _context.Schedules
                .Include(s => s.Employee) // Include the related Employee entity
                .FirstOrDefaultAsync(s => s.Id == scheduleId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedule with ID {scheduleId}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Adds a new schedule to the database. Converts ShiftStart and ShiftEnd to UTC if they are in local time.
    /// </summary>
    public async Task AddScheduleAsync(Schedule schedule)
    {
        try
        {
            string dateString = "2025-02-27 2:25:59";
            DateTime localDateTime = DateTime.Parse(dateString);
            DateTime utcDateTime = localDateTime.ToUniversalTime();

            schedule.ShiftStart = utcDateTime;
            schedule.ShiftEnd = utcDateTime;

            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding schedule: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing schedule in the database. Converts ShiftStart and ShiftEnd to UTC if they are in local time.
    /// </summary>
    public async Task UpdateScheduleAsync(Schedule schedule)
    {
        try
        {
            // Ensure ShiftStart and ShiftEnd are in UTC
            schedule.ShiftStart = EnsureUtc(schedule.ShiftStart);
            schedule.ShiftEnd = EnsureUtc(schedule.ShiftEnd);

            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating schedule with ID {schedule.Id}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Deletes a schedule from the database by its ID.
    /// </summary>
    public async Task DeleteScheduleAsync(int scheduleId)
    {
        try
        {
            var schedule = await _context.Schedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting schedule with ID {scheduleId}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves schedules for a specific week, starting from the provided weekStart date.
    /// </summary>
    public async Task<List<Schedule>> GetSchedulesForWeekAsync(DateTime weekStart)
    {
        try
        {
            var weekEnd = weekStart.AddDays(7); // Calculate the end of the week

            // Ensure weekStart and weekEnd are in UTC
            var utcWeekStart = EnsureUtc(weekStart);
            var utcWeekEnd = EnsureUtc(weekEnd);

            return await _context.Schedules
                .Where(s => s.ShiftStart >= utcWeekStart && s.ShiftStart < utcWeekEnd) // Filter by the week range
                .Include(s => s.Employee) // Include the related Employee entity
                .OrderBy(s => s.ShiftStart) // Order by ShiftStart
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedules for week starting {weekStart}: {ex.Message}");
            throw;
        }
    }
}

