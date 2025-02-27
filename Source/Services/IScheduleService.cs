using Gestion_Bunny.Modeles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IScheduleService
{
    /// <summary>
    /// Retrieves all schedules from the database.
    /// </summary>
    Task<List<Schedule>> GetSchedulesAsync();

    /// <summary>
    /// Retrieves schedules for a specific employee.
    /// </summary>
    Task<List<Schedule>> GetEmployeeSchedulesAsync(int employeeId);

    /// <summary>
    /// Retrieves schedules for a specific employee and week.
    /// </summary>
    Task<List<Schedule>> GetEmployeeSchedulesForWeekAsync(int employeeId, DateTime weekStart);

    /// <summary>
    /// Retrieves a specific schedule by its ID.
    /// </summary>
    Task<Schedule> GetScheduleByIdAsync(int scheduleId);

    /// <summary>
    /// Adds a new schedule to the database.
    /// </summary>
    Task AddScheduleAsync(Schedule schedule);

    /// <summary>
    /// Updates an existing schedule in the database.
    /// </summary>
    Task UpdateScheduleAsync(Schedule schedule);

    /// <summary>
    /// Deletes a schedule from the database by its ID.
    /// </summary>
    Task DeleteScheduleAsync(int scheduleId);

    /// <summary>
    /// Retrieves schedules for a specific week.
    /// </summary>
    Task<List<Schedule>> GetSchedulesForWeekAsync(DateTime weekStart);
}