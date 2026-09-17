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
                logger.LogInformation("Retrieving all students.");

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

                logger.LogInformation("Successfully retrieved {StudentCount} students.", studentDtos.Count);

                return BaseResponse<IEnumerable<StudentDto>>.Ok(
                    studentDtos,
                    "Students retrieved successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving all students.");

                return BaseResponse<IEnumerable<StudentDto>>.Fail(
                    "An unexpected error occurred while retrieving students.");
            }
        }

        public async Task<BaseResponse<StudentDto?>> GetStudentByIdAsync(Guid id)
        {
            try
            {
                logger.LogInformation("Retrieving student with ID {StudentId}.", id);

                var student = await studentRepositories.GetByIdAsync(id);

                if (student == null)
                {
                    logger.LogWarning("Student with ID {StudentId} was not found.", id);

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

                logger.LogInformation("Successfully retrieved student with ID {StudentId}.", id);

                return BaseResponse<StudentDto?>.Ok(dto);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while retrieving student with ID {StudentId}.",
                    id);

                return BaseResponse<StudentDto?>.Fail(
                    "An unexpected error occurred while retrieving the student.");
            }
        }

        public async Task<BaseResponse<bool>> CreateStudentAsync(CreateStudentDto dto)
        {
            try
            {
                logger.LogInformation(
                    "Creating a new student with email {Email}.",
                    dto.Email);

                var newStudent = new Student
                {
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Othername = dto.Othername,
                    Email = dto.Email,
                    DateOfBirth = dto.DateOfBirth,
                    PhoneNumber = dto.PhoneNumber,
                };

                var created = await studentRepositories.CreateAsync(newStudent);

                logger.LogInformation(
                    "Student with ID {StudentId} was created successfully.",
                    newStudent.Id);

                return BaseResponse<bool>.Ok(true, "Student created successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a student.");

                return BaseResponse<bool>.Fail(
                    "An unexpected error occurred while creating the student.");
            }
        }

        public async Task<BaseResponse<bool>> UpdateStudentAsync(UpdateStudentDto dto)
        {
            try
            {
                logger.LogInformation(
                    "Updating student with ID {StudentId}.",
                    dto.Id);

                var existing = await studentRepositories.GetByIdAsync(dto.Id);

                if (existing == null)
                {
                    logger.LogWarning(
                        "Student with ID {StudentId} was not found for update.",
                        dto.Id);

                    return BaseResponse<bool>.Fail("Student not found");
                }

                existing.Firstname = dto.Firstname;
                existing.Lastname = dto.Lastname;
                existing.Othername = dto.Othername;
                existing.Email = dto.Email;
                existing.DateOfBirth = dto.DateOfBirth;
                existing.PhoneNumber = dto.PhoneNumber;

                var updated = await studentRepositories.UpdateAsync(dto.Id, existing);

                logger.LogInformation(
                    "Student with ID {StudentId} was updated successfully.",
                    dto.Id);

                return BaseResponse<bool>.Ok(true, "Student updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while updating student with ID {StudentId}.",
                    dto.Id);

                return BaseResponse<bool>.Fail(
                    "An unexpected error occurred while updating the student.");
            }
        }

        public async Task<BaseResponse<bool>> DeleteStudentAsync(Guid id)
        {
            try
            {
                logger.LogInformation(
                    "Deleting student with ID {StudentId}.",
                    id);

                var existing = await studentRepositories.GetByIdAsync(id);

                if (existing == null)
                {
                    logger.LogWarning(
                        "Student with ID {StudentId} was not found for deletion.",
                        id);

                    return BaseResponse<bool>.Fail("Student not found");
                }

                var deleted = await studentRepositories.DeleteAsync(id, existing);

                if (!deleted)
                {
                    logger.LogWarning(
                        "Failed to delete student with ID {StudentId}.",
                        id);

                    return BaseResponse<bool>.Fail("Failed to delete student");
                }

                logger.LogInformation(
                    "Student with ID {StudentId} was deleted successfully.",
                    id);

                return BaseResponse<bool>.Ok(true, "Student deleted successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An error occurred while deleting student with ID {StudentId}.",
                    id);

                return BaseResponse<bool>.Fail(
                    "An unexpected error occurred while deleting the student.");
            }
        }
    }
}
