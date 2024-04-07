namespace StudentEnrollment.Api.Dtos.Student;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IdNumber { get; set; }
    public string Picture { get; set; }
}
