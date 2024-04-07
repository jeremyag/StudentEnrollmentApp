using StudentEnrollment.Api.Dtos.Course;
using StudentEnrollment.Api.Dtos.Student;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Dtos.Enrollment;

public class EnrollmentDto
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public virtual CourseDto Course { get; set; }
    public virtual StudentDto Student { get; set; }
}
