using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleModel>> GetSchedulesAsync();
        Task<List<ScheduleModel>> GetEmployeeSchedulesAsync(int employeeId);
        Task<ScheduleModel> GetScheduleByIdAsync(int scheduleId);
        Task AddScheduleAsync(ScheduleModel schedule);
        Task UpdateScheduleAsync(ScheduleModel schedule);
        Task DeleteScheduleAsync(int scheduleId);
        Task<List<ScheduleModel>> GetSchedulesForWeekAsync(DateTime weekStart);  // Change to DateTime
    }
}
