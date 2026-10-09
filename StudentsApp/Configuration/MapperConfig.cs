using AutoMapper;
using StudentsApp.DTO;
using StudentsApp.Models;

namespace StudentsApp.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<StudentInsertDTO, Student>().ReverseMap();
        CreateMap<StudentUpdateDTO, Student>().ReverseMap();
        CreateMap<StudentReadOnlyDTO, Student>().ReverseMap();
    }
}
