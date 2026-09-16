using Microsoft.AspNetCore.Mvc;
using StudentAPI.Application.Dto.StudentDto;
using StudentAPI.Application.Dto.StudentsDto;
using StudentAPI.Application.Service;

namespace StudentAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpGet]
    public async Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudent()
    {
        var studentsDto = await studentService.GetAllStudentsAsync();
        return studentsDto;
    }

    [HttpGet("{id:guid}")]
    public async Task<BaseResponse<StudentDto?>> GetStudentById(Guid id)
    {
        var student = await studentService.GetStudentByIdAsync(id);
        return student;
    }

    [HttpPost]
    public async Task<BaseResponse<bool>> CreateStudent(
        [FromBody] CreateStudentDto student)
    {
        var result = await studentService.CreateStudentAsync(student);
        return result;
    }

    [HttpPut("{id:guid}")]
    public async Task<BaseResponse<bool>> UpdateStudent(
        Guid id,
        [FromBody] UpdateStudentDto student)
    {
        student.Id = id;

        var result = await studentService.UpdateStudentAsync(student);
        return result;
    }

    [HttpDelete("{id:guid}")]
    public async Task<BaseResponse<bool>> DeleteStudent(Guid id)
    {
        var result = await studentService.DeleteStudentAsync(id);
        return result;
    }
}

