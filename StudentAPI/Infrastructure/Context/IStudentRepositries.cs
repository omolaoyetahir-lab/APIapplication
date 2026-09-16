using StudentAPI.Application.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAPI.Infrastructure.Context
{

    public interface IStudentRepositories
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(Guid id, Student student);
        Task<bool> UpdateAsync(Guid id, Student student);
        Task<bool> DeleteAsync(Guid id, Student student);
    }
}
