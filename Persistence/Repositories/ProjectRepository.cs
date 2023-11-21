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
    public class ProjectRepository : IProjectRepository
    {
        private readonly TimesheetContext context;

        public ProjectRepository(TimesheetContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProjectEditDTO>> GetAllProjectAsync()
        {
            //var project = await this.context.Projects.Where(x => x.IsDaleted == false).Select(data => new ProjectEditDTO { ProjectId = data.ProjectId, ClientId = data.ClientId, InitiatedOn = data.InitiatedOn, Name = data.Name }).ToListAsync();

            //var clients = await this.context.Clients.Where(x => x.IsDaleted == false).Select(data => new ClientEditDTO { ClientId = data.ClientId, Name = data.Name }).ToListAsync();

            //var projectWithDesignation = project.Join(clients, c => c.ClientId, d => d.ClientId, (c, d) => new ProjectEditDTO
            //{
            //    ProjectId = c.ProjectId, ClientId = c.ClientId, InitiatedOn = c.InitiatedOn, Name=c.Name, 
            //});

            var teamMembers = await this.context.Projects.Where(x => x.IsDaleted == false).Select(data => new ProjectEditDTO { ProjectId = data.ProjectId, Name = data.Name, InitiatedOn = data.InitiatedOn ,ClientId = data.ClientId, ClientName = data.Client.Name}).ToListAsync();

            return teamMembers;

           
        }

        public async Task<Project?> GetProjectById(int projectId)
        {
            return await this.context.Projects.Where(x=> x.ProjectId == projectId && x.IsDaleted == false).FirstOrDefaultAsync();
        }

        public async Task<Project?> InsertProject(Project project)
        {
            await this.context.Projects.AddAsync(project);
            var result = await this.context.SaveChangesAsync();
            return project;
            
        }

        public async Task<Project?> UpdateProject(Project project)
        {
            var data = await this.context.Projects.Where(x => x.ProjectId == project.ProjectId).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Name = project.Name;
                data.ProjectId = project.ProjectId;
                data.ClientId = project.ClientId;
                data.InitiatedOn = project.InitiatedOn;
                data.Name = project.Name;
                data.ModifiedBy = project.ModifiedBy;
                data.ModifiedOn = project.ModifiedOn;
                data.ModifiedFrom = project.ModifiedFrom;
                var result = await this.context.SaveChangesAsync();
                return project;
            }
            return null;
        }

        public async Task<DataUpdateResponse> DeleteProject(Project project)
        {
            int result = 0;
            DataUpdateResponse response = new DataUpdateResponse();
        
            var data = await this.context.Projects.Where(x => x.ProjectId == project.ProjectId).FirstOrDefaultAsync();
            if(data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = project.IsDaleted;
                data.DeletedBy = project.DeletedBy;
                data.DeletedOn = project.DeletedOn;
                data.DeletedFrom = project.DeletedFrom;
                result = await this.context.SaveChangesAsync();
                response.Description = "Record deleted";

            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;
            return response;
        }
    }
}
