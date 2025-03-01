using Gestion_Bunny.Modeles;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IScheduleService
{
    /// <summary>
    /// Retrieves all schedules from the database.
    /// </summary>
    List<Schedule> GetSchedules();

    /// <summary>
    /// Retrieves schedules for a specific employee.
    /// </summary>
    List<Schedule> GetEmployeeSchedules(int employeeId);

    /// <summary>
    /// Retrieves schedules for a specific employee and week.
    /// </summary>
    List<Schedule> GetEmployeeSchedulesForWeek(int employeeId, DateTime weekStart);

    /// <summary>
    /// Retrieves a specific schedule by its ID.
    /// </summary>
    Schedule GetScheduleById(int scheduleId);

    /// <summary>
    /// Adds a new schedule to the database.
    /// </summary>
    void AddSchedule(Schedule schedule);

    /// <summary>
    /// Updates an existing schedule in the database.
    /// </summary>
    void UpdateSchedule(Schedule schedule);

    /// <summary>
    /// Deletes a schedule from the database by its ID.
    /// </summary>
    void DeleteSchedule(int scheduleId);

    /// <summary>
    /// Retrieves schedules for a specific week.
    /// </summary>
    List<Schedule> GetSchedulesForWeek(DateTime weekStart);
}