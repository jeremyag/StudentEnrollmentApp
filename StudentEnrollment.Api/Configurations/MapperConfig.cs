﻿using AutoMapper;
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
        CreateMap<Course, CourseDetailsDto>()
            .ForMember(q => q.Students, x => x.MapFrom(course => course.Enrollments.Select(stu => stu.Student)));

        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();

        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, CreateStudentDto>().ReverseMap();
        CreateMap<Student, StudentDetailsDto>()
            .ForMember(q => q.Courses, x => x.MapFrom(student => student.Enrollments.Select(course => course.Course)));
    }
}
