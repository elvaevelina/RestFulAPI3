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
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Instructor, InstructorDTO>().ReverseMap();
            CreateMap<Course, CourseAddDTO>().ReverseMap();
            CreateMap<Course, CourseUpdateDTO>().ReverseMap();

        }
    }
}