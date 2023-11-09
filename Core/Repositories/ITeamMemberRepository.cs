using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITeamMemberRepository
    {
        Task<IEnumerable<TeamMemberEditDTO>> GetAllTeamMemberAsync();
        Task<TeamMember?> GetTeamMemberById(int teamMemberId);
        Task<TeamMember?> InsertTeamMember(TeamMember teamMember);
        Task<TeamMember?> UpdateTeamMember(TeamMember teamMember);
        Task<DataUpdateResponse> DeleteTeamMember(TeamMember teamMember);
    }
}
