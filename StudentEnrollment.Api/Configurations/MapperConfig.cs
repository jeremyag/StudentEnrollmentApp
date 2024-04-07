using AutoMapper;
using StudentEnrollment.Api.Dtos.Course;
using StudentEnrollment.Api.Dtos.Enrollment;
using StudentEnrollment.Api.Dtos.Student;
using StudentEnrollment.Data;
namespace StudentEnrollment.Api.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CreateCourseDto>().ReverseMap();

        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();

        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, CreateStudentDto>().ReverseMap();
    }
}
