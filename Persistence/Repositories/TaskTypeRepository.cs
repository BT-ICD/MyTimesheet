
 using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly TimesheetContext context;

        public TaskTypeRepository(TimesheetContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TaskTypeEditDTO>> GetAllTaskType()
        {
            return await context.TaskTypes.Where(x => x.IsDaleted == false).Select(data => new TaskTypeEditDTO { Id = data.Id, TypeShortName = data.TypeShortName, TypeDescription = data.TypeDescription }).ToListAsync();
        }
        public async Task<TaskType?> GetTaskTypeById(int taskId)
        {
            return await context.TaskTypes.Where(x => x.Id == taskId && x.IsDaleted == false).FirstOrDefaultAsync();
        }
        public async Task<TaskType?> InsertTaskType(TaskType taskType)
        {
            await context.TaskTypes.AddAsync(taskType);
            var result = await context.SaveChangesAsync();
            return taskType;
        }

        public async Task<TaskType?> UpdateTaskType(TaskType taskType)
        {
            var data = await context.TaskTypes.Where(x=> x.Id == taskType.Id ).FirstOrDefaultAsync();
            if (data != null)
            {
                data.TypeShortName = taskType.TypeShortName;
                data.TypeDescription = taskType.TypeDescription;
                data.ModifiedBy = taskType.ModifiedBy;
                data.ModifiedFrom = taskType.ModifiedFrom;
                data.ModifiedOn = taskType.ModifiedOn;
                var result = await context.SaveChangesAsync();
                return taskType;
            }
            return null;
        }
        public async Task<DataUpdateResponse> DeleteTaskType(TaskType taskId)
        {
            int result = 0;
            DataUpdateResponse response = new DataUpdateResponse();
            var data = await context.TaskTypes.Where(x => x.Id == taskId.Id && x.IsDaleted == false).FirstOrDefaultAsync();
            if (data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = taskId.IsDaleted;
                data.DeletedOn = taskId.DeletedOn;
                data.DeletedBy = taskId.DeletedBy;
                data.DeletedFrom = taskId.DeletedFrom;
                result = await context.SaveChangesAsync();
                response.Description = "Record Deleted";
            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;
            return response;
        }

    }
}
