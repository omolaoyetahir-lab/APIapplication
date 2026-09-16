namespace StudentAPI.Application.Dto.StudentsDto;

public class StudentDto
{
    public Guid Id { get; set; } 
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Othername { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
