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
            CreateMap<ClientContactAddDTO, ClientContacts>().ReverseMap();
            CreateMap<ClientContactEditDTO, ClientContacts>().ReverseMap();
        }
    }
}
