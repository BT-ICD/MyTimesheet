﻿using Core.DTOs;
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
        public async Task<IEnumerable<TeamMemberEditDTO>> GetAllTeamMemberAsync()
        {
            //return await this.context.TeamMember.Where(x => x.IsDaleted == false).Select(data => new TeamMemberEdit { TeamMemberId = data.TeamMemberId, Name = data.Name, Mobile = data.Mobile, Email = data.Email, Notes = data.Notes, AlternateContact = data.AlternateContact, DOB = data.DOB, DOJ = data.DOJ, DesignationId = data.DesignationId }).ToListAsync();

            var teamMembers = await this.context.TeamMember.Where(x => x.IsDaleted == false).Select(data => new TeamMemberEditDTO{TeamMemberId =data.TeamMemberId,Name = data.Name, Mobile = data.Mobile, Email = data.Email, Notes = data.Notes,AlternateContact = data.AlternateContact, DOB = data.DOB, DOJ = data.DOJ, DesignationId = data.DesignationId, DesignationName = data.designation.DesignationName }) .ToListAsync();

            //var designations = await this.context.Designations.Where(x => x.IsDaleted == false).Select(data => new DesignationEditDTO { DesignationId = data.DesignationId, DesignationName = data.DesignationName }).ToListAsync();

            //var teamMembersWithDesignation = teamMembers.Join(designations, tm => tm.DesignationId, d => d.DesignationId, (tm, d) => new TeamMemberEditDTO
            //{
            //    TeamMemberId = tm.TeamMemberId,
            //    Name = tm.Name,
            //    Mobile = tm.Mobile,
            //    Email = tm.Email,
            //    Notes = tm.Notes,
            //    AlternateContact = tm.AlternateContact,
            //    DOB = tm.DOB,
            //    DOJ = tm.DOJ,
            //    DesignationId = tm.DesignationId,
            //    DesignationName = tm.DesignationName
            //});

            return teamMembers;


        }

       
        public async Task<TeamMember?> GetTeamMemberById(int teamMemberId)
        {
            return await this.context.TeamMember.Include(x => x.designation).Where(x => x.TeamMemberId == teamMemberId && x.IsDaleted == false).FirstOrDefaultAsync();
        }

        public async Task<TeamMember?> InsertTeamMember(TeamMember teamMember)
        {

            //var designation = await this.context.Designations.FirstOrDefaultAsync(d => d.DesignationName == designationName);

            //if (designation != null)
            //{
            //    teamMember.designation = designation;
            //    await this.context.TeamMember.AddAsync(teamMember);
            //    var result = await this.context.SaveChangesAsync();

            //    // await this.context.Entry(designation).Reference(d => d.DesignationName).LoadAsync();

            //    var responseDTO = new TeamMemberEditDTO
            //    {
            //        TeamMemberId = teamMember.TeamMemberId,
            //        Name = teamMember.Name,
            //        Mobile = teamMember.Mobile,
            //        Email = teamMember.Email,
            //        Notes = teamMember.Notes,
            //        AlternateContact = teamMember.AlternateContact,
            //        DOB = teamMember.DOB,
            //        DOJ = teamMember.DOJ,
            //        DesignationId = teamMember.DesignationId,
            //        DesignationName = designationName
            //    };

            //    return responseDTO;
            //}

            await context.TeamMember.AddAsync(teamMember);
            var result = await context.SaveChangesAsync();

            var insertedTeamMember = await context.TeamMember.Include(x => x.designation).FirstOrDefaultAsync(x => x.TeamMemberId == teamMember.TeamMemberId);

            return insertedTeamMember;

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

                var updatedDesignation = await context.Designations.Where(d => d.DesignationId == teamMember.DesignationId).FirstOrDefaultAsync();

                // Update the loaded Designation information in the ClientContact
                data.designation = updatedDesignation;

                return data;

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
