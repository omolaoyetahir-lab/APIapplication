using StudentAPI.Application.Dto.StudentDto;
using StudentAPI.Application.Dto.StudentsDto;
using StudentAPI.Infrastructure.Context;

namespace StudentAPI.Application.Service
{
    public class StudentServices(
        IStudentRepositories studentRepositories,
        ILogger<StudentServices> logger) : IStudentService
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
                logger.LogError(ex, "An error occurred while retrieving all students.");

                return BaseResponse<IEnumerable<StudentDto>>.Fail("An unexpected error occurred while retrieving students.");
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
                logger.LogError(ex, "An error occurred while retrieving student with ID {Id}.", id);

                return BaseResponse<StudentDto?>.Fail("An unexpected error occurred while retrieving the student.");
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
                logger.LogError(ex, "An error occurred while creating a student.");

                return BaseResponse<bool>.Fail("An unexpected error occurred while creating the student.");
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
                logger.LogError(ex, "An error occurred while updating student with ID {Id}.", dto.Id);

                return BaseResponse<bool>.Fail("An unexpected error occurred while updating the student.");
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

