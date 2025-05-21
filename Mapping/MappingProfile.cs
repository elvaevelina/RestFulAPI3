// filepath: SimpleRESTApi/Mapping/MappingProfile.cs
using AutoMapper;
using SimpleRESTApi.Models;
using SimpleRESTApi.DTO;

namespace SimpleRESTApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Instructor, InstructorDTO>();
        }
    }
}