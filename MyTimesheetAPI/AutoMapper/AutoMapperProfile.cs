using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace MyTimesheetAPI.AutoMapperProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DesignationAddDTO, Designation>();
            CreateMap<DesignationEditDTO, Designation>();
        }
    }
}
