using StudentAPI.Application.Dto.StudentDto;
using StudentAPI.Application.Dto.StudentsDto;
using StudentAPI.Infrastructure.Context;

namespace StudentAPI.Application.Service
{
    public class StudentServices(IStudentRepositories studentRepositories) : IStudentService
    {
        public async Task<BaseResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync()
        {
            try
            {
                var students = await studentRepositories.GetAllAsync();

                var studentDtos = students.Select(s => new StudentDto
                {
                    Id = s.Id,
                    Firstname = s.Firstname,
                    Lastname = s.Lastname,
                    Othername = s.Othername,
                    Email = s.Email,
                    DateOfBirth = s.DateOfBirth,
                    Age = s.Age,
                    PhoneNumber = s.PhoneNumber
                }).ToList();

                return BaseResponse<IEnumerable<StudentDto>>.Ok(studentDtos, "Students retrieved successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<IEnumerable<StudentDto>>.Fail(
                    $"An error occurred: {ex.Message}"
                );
            }
        }

        public async Task<BaseResponse<StudentDto?>> GetStudentByIdAsync(Guid id)
        {
            try
            {

                var student = await studentRepositories.GetByIdAsync(id);

                if (student == null)
                {
                    return BaseResponse<StudentDto?>.Fail("Student not found");
                }

                var dto = new StudentDto
                {
                    Id = student.Id,
                    Firstname = student.Firstname,
                    Lastname = student.Lastname,
                    Othername = student.Othername,
                    Email = student.Email,
                    DateOfBirth = student.DateOfBirth,
                    Age = student.Age,
                    PhoneNumber = student.PhoneNumber
                };

                return BaseResponse<StudentDto?>.Ok(dto);
            }
            catch (Exception ex)
            {
                return BaseResponse<StudentDto?>.Fail($"An error occurred: {ex.Message}");
            }
        }

        public async Task<BaseResponse<bool>> CreateStudentAsync(CreateStudentDto dto)
        {
            try
            {
                var newStudent = new Student
                {
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Othername = dto.Othername,
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    PhoneNumber = dto.PhoneNumber,
                };

                var created = await studentRepositories.CreateAsync(dto.Id, newStudent);
                return BaseResponse<bool>.Ok(true, "Student created successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Fail($"An error occurred: {ex.Message}");
            }
        }

        public async Task<BaseResponse<bool>> UpdateStudentAsync(UpdateStudentDto dto)
        {
            try
            {
                var existing = await studentRepositories.GetByIdAsync(dto.Id);

                if (existing == null)
                {
                    return BaseResponse<bool>.Fail("Student not found");
                }

                existing.Firstname = dto.Firstname;
                existing.Lastname = dto.Lastname;
                existing.Othername = dto.Othername;
                existing.Email = dto.Email;
                existing.DateOfBirth = dto.DateOfBirth;
                existing.PhoneNumber = dto.PhoneNumber;

                var updated = await studentRepositories.UpdateAsync(dto.Id, existing);

                return BaseResponse<bool>.Ok(true, "Student updated successfully");
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.Fail($"An error occurred: {ex.Message}");
            }
        }

        public async Task<BaseResponse<bool>> DeleteStudentAsync(Guid id)
        {
            var existing = await studentRepositories.GetByIdAsync(id);

            if (existing == null)
            {
                return BaseResponse<bool>.Fail("Student not found");
            }

            var deleted = await studentRepositories.DeleteAsync(id, existing);

            if (!deleted)
            {
                return BaseResponse<bool>.Fail("Failed to delete student");
            }

            return BaseResponse<bool>.Ok(true, "Student deleted successfully");
        }
    }
}