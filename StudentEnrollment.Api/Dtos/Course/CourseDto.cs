namespace StudentEnrollment.Api.Dtos.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Credits {  get; set; }
}

public class CreateCourseDto
{
    public string Title { get; set; }
    public int Credits { get; set; }
}