using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;

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
    public List<Schedule> GetSchedules()
    {
        try
        {
            return _context.Schedules
                .Include(s => s.User)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedules: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves schedules for a specific employee, including related employee data.
    /// </summary>
    public List<Schedule> GetEmployeeSchedules(int employeeId)
    {
        try
        {
            return _context.Schedules
                .Where(s => s.UserId == employeeId)
                .Include(s => s.User)
                .ToList();
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
    public List<Schedule> GetEmployeeSchedulesForWeek(int employeeId, DateTime weekStart)
    {
        try
        {
            var weekEnd = weekStart.AddDays(7); // Calculate the end of the week

            // Ensure weekStart and weekEnd are in UTC
            var utcWeekStart = EnsureUtc(weekStart);
            var utcWeekEnd = EnsureUtc(weekEnd);

            return _context.Schedules
                .Where(s => s.UserId == employeeId && s.ShiftStart >= utcWeekStart && s.ShiftStart < utcWeekEnd)
                .Include(s => s.User) 
                .OrderBy(s => s.ShiftStart) 
                .ToList();
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
    public Schedule GetScheduleById(int scheduleId)
    {
        try
        {
            return _context.Schedules
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == scheduleId);
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
    public void AddSchedule(Schedule schedule)
    {
        try
        {
            schedule.ShiftStart = EnsureUtc(schedule.ShiftStart);
            schedule.ShiftEnd = EnsureUtc(schedule.ShiftEnd);

            _context.Schedules.Add(schedule);
            _context.SaveChanges();
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
    public void UpdateSchedule(Schedule schedule)
    {
        try
        {
            // Ensure ShiftStart and ShiftEnd are in UTC
            schedule.ShiftStart = EnsureUtc(schedule.ShiftStart);
            schedule.ShiftEnd = EnsureUtc(schedule.ShiftEnd);

            _context.Entry(schedule).State = EntityState.Modified;
            _context.SaveChanges();
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
    public void DeleteSchedule(int scheduleId)
    {
        try
        {
            var schedule = _context.Schedules.Find(scheduleId);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                _context.SaveChanges();
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
    public List<Schedule> GetSchedulesForWeek(DateTime weekStart)
    {
        try
        {
            var weekEnd = weekStart.AddDays(7); // Calculate the end of the week

            // Ensure weekStart and weekEnd are in UTC
            var utcWeekStart = EnsureUtc(weekStart);
            var utcWeekEnd = EnsureUtc(weekEnd);

            return _context.Schedules
                .Where(s => s.ShiftStart >= utcWeekStart && s.ShiftStart < utcWeekEnd) 
                .Include(s => s.User)
                .OrderBy(s => s.ShiftStart)
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving schedules for week starting {weekStart}: {ex.Message}");
            throw;
        }
    }
}

