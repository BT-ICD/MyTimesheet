using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace MyTimesheetAPI.AutoMapperProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DesignationAddDTO, Designation>().ReverseMap();
            CreateMap<DesignationEditDTO, Designation>().ReverseMap();

            CreateMap<ClientAddDTO, Client>().ReverseMap();
            CreateMap<ClientEditDTO, Client>().ReverseMap();

            CreateMap<TaskTypeAddDTO, TaskType>().ReverseMap();
            CreateMap<TaskTypeEditDTO, TaskType>().ReverseMap();

            CreateMap<ClientContactAddDTO, ClientContact>().ReverseMap();
            CreateMap<ClientContactEditDTO,ClientContact>().ReverseMap();

            CreateMap<TeamMemberAddDTO, TeamMember>().ReverseMap();
            CreateMap<TeamMemberEditDTO, TeamMember>().ReverseMap() ;

            CreateMap<ProjectAddDTO, Project>().ReverseMap();
            CreateMap<ProjectEditDTO, Project>().ReverseMap();
        }
    }
}

