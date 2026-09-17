using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Application.Dto.StudentDto;

namespace StudentAPI.Infrastructure.Context
{
    public class StudentRepositories(ApplicationDbContext context) : IStudentRepositories
    {
        public async Task<List<Student>> GetAllAsync()
        {
            return await context.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        public async Task<bool> CreateAsync( Student student)
        {
            context.Students.Add(student);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid id, Student student)
        {
            context.Students.Update(student);
            await context.SaveChangesAsync();
            return await context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteAsync(Guid id, Student student)
        {
            context.Students.Remove(student);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
