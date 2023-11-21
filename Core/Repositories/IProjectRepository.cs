using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectEditDTO>> GetAllProjectAsync();
        Task<Project?> GetProjectById(int projectId);

        Task<Project?> InsertProject(Project project);
        Task<Project?> UpdateProject(Project project);
        Task<DataUpdateResponse> DeleteProject(Project project);
    }
}
