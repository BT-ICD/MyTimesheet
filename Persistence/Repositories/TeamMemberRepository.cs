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
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly TimesheetContext context;

        public TeamMemberRepository(TimesheetContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TeamMemberEdit>> GetAllTeamMemberAsync()
        {
            return await this.context.TeamMember.Where(x => x.IsDaleted == false).Select(data => new TeamMemberEdit { TeamMemberId = data.TeamMemberId, Name = data.Name, Mobile = data.Mobile, Email = data.Email, Notes = data.Notes, AlternateContact = data.AlternateContact, DOB = data.DOB, DOJ = data.DOJ, DesignationId = data.DesignationId }).ToListAsync();
        }

        public async Task<TeamMember?> GetTeamMemberById(int teamMemberId)
        {
            return await this.context.TeamMember.Where(x => x.TeamMemberId == teamMemberId && x.IsDaleted == false).FirstOrDefaultAsync();
        }

        public async Task<TeamMember?> InsertTeamMember(TeamMember teamMember)
        {
            await this.context.TeamMember.AddAsync(teamMember);
            var result = await this.context.SaveChangesAsync();
            return teamMember;
        }

        public async Task<TeamMember?> UpdateTeamMember(TeamMember teamMember)
        {
            var data = await this.context.TeamMember.Where(x => x.TeamMemberId == teamMember.TeamMemberId).FirstOrDefaultAsync();
            if(data != null)
            {
                data.TeamMemberId = teamMember.TeamMemberId;
                data.Name = teamMember.Name;
                data.Email = teamMember.Email;
                data.Mobile = teamMember.Mobile;
                data.Notes = teamMember.Notes;
                data.AlternateContact = teamMember.AlternateContact;
                data.DesignationId = teamMember.DesignationId;
                data.DOB = teamMember.DOB;
                data.DOJ = teamMember.DOJ;
                data.ModifiedBy = teamMember.ModifiedBy;
                data.ModifiedOn = teamMember.ModifiedOn;
                data.ModifiedFrom = teamMember.ModifiedFrom;
                var result = await this.context.SaveChangesAsync();
                return teamMember;
            }
            return null;
        }

        public async Task<DataUpdateResponse> DeleteTeamMember(TeamMember teamMember)
        {
            int result = 0;
            DataUpdateResponse response = new DataUpdateResponse();
            var data = await this.context.TeamMember.Where(x => x.TeamMemberId == teamMember.TeamMemberId && x.IsDaleted == false).FirstOrDefaultAsync();
            if(data == null)
            {
                response.Description = "Not Found";
            }
            else
            {
                data.IsDaleted = teamMember.IsDaleted;
                data.DeletedBy = teamMember.DeletedBy;
                data.DeletedOn = teamMember.DeletedOn;
                data.DeletedFrom = teamMember.DeletedFrom;
                result = await this.context.SaveChangesAsync();
                response.Description = "Record deleted";
              
            }
            response.Status = Convert.ToBoolean(result);
            response.RecordCount = result;
            return response;
        }
    }
}
