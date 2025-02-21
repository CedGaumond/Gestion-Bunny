using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using Gestion_Bunny.Services;
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

    public async Task<List<ScheduleModel>> GetSchedulesAsync()
    {
        return await _context.Schedules // Use "Schedules" here
            .Include(s => s.Employee)
            .ToListAsync();
    }

    public async Task<List<ScheduleModel>> GetEmployeeSchedulesAsync(int employeeId)
    {
        return await _context.Schedules // Use "Schedules" here
            .Where(s => s.EmployeeId == employeeId)
            .Include(s => s.Employee)
            .ToListAsync();
    }

    public async Task<ScheduleModel> GetScheduleByIdAsync(int scheduleId)
    {
        return await _context.Schedules
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.Id == scheduleId);
    }

    public async Task AddScheduleAsync(ScheduleModel schedule)
    {
        // Ensure that the ShiftStart and ShiftEnd are in UTC before saving
        if (schedule.ShiftStart.Kind == DateTimeKind.Local)
        {
            schedule.ShiftStart = schedule.ShiftStart.ToUniversalTime();  
        }

        if (schedule.ShiftEnd.Kind == DateTimeKind.Local)
        {
            schedule.ShiftEnd = schedule.ShiftEnd.ToUniversalTime(); 
        }

        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateScheduleAsync(ScheduleModel schedule)
    {
        // Ensure that the ShiftStart and ShiftEnd are in UTC before saving
        if (schedule.ShiftStart.Kind == DateTimeKind.Local)
        {
            schedule.ShiftStart = schedule.ShiftStart.ToUniversalTime();  // Convert to UTC
        }

        if (schedule.ShiftEnd.Kind == DateTimeKind.Local)
        {
            schedule.ShiftEnd = schedule.ShiftEnd.ToUniversalTime();      // Convert to UTC
        }

        _context.Entry(schedule).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }


    public async Task DeleteScheduleAsync(int scheduleId)
    {
        var schedule = await _context.Schedules.FindAsync(scheduleId);
        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ScheduleModel>> GetSchedulesForWeekAsync(DateTime weekStart)
    {
        var weekEnd = weekStart.AddDays(7); // Calculate the end of the week

        // Ensure weekStart and weekEnd are in UTC
        var utcWeekStart = weekStart.ToUniversalTime();
        var utcWeekEnd = weekEnd.ToUniversalTime();

        var schedules = await _context.Schedules
            .Where(s => s.ShiftStart >= utcWeekStart && s.ShiftStart < utcWeekEnd)
            .Include(s => s.Employee)  // Include related employee
            .OrderBy(s => s.ShiftStart)
            .ToListAsync();

        return schedules;
    }
}
