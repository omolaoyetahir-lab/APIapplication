using StudentAPI.Application.Dto.StudentDto;
using StudentAPI.Application.Dto.StudentsDto;

namespace StudentAPI.Application.Service
{
    public interface IStudentService
    {
        Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync();
        Task<BaseResponse<StudentDto?>> GetStudentByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateStudentAsync(CreateStudentDto student);
        Task<BaseResponse<bool>> UpdateStudentAsync(UpdateStudentDto student);
        Task<BaseResponse<bool>> DeleteStudentAsync(Guid id);
    }
}
