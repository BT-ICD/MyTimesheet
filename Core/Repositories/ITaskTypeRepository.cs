using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITaskTypeRepository
    {
        Task<IEnumerable<TaskTypeEditDTO>> GetAllTaskType();
        Task<TaskType?> GetTaskTypeById(int taskId);
        Task<TaskType?> UpdateTaskType(TaskType taskType);
        Task<TaskType?> InsertTaskType(TaskType taskType);
        Task<DataUpdateResponse> DeleteTaskType(TaskType taskId);
    }
}
