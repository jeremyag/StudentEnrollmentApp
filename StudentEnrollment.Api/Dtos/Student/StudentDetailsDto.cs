using StudentEnrollment.Api.Dtos.Course;
using StudentEnrollment.Api.Dtos.Enrollment;

namespace StudentEnrollment.Api.Dtos.Student;

public class StudentDetailsDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string Picture { get; set; }
    public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
}
