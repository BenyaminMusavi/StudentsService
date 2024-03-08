using Microsoft.EntityFrameworkCore;
using StudentsService.DAL.Entities;
using StudentsService.DAL.Mappers;
using StudentsService.Domain.Models;
using StudentsService.Domain.Repositories;

namespace StudentsService.DAL.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentContext _ctx;
    public StudentRepository(StudentContext studentContext)
    {
        _ctx = studentContext;
    }

    public async Task<int> CreateAsync(StudentModel studentForCreate)
    {
        Student student = new Student
        {
            FirstName = studentForCreate.FirstName,
            LastName = studentForCreate.LastName,
            Description = studentForCreate.Description,
            StudentNumber = studentForCreate.StudentNumber,
        };
        foreach (var item in studentForCreate.PhoneNumbers)
        {
            student.PhoneNumbers.Add(new PhoneNumber
            {
                Value = item
            });
        }
        await _ctx.AddAsync(student);
        await _ctx.SaveChangesAsync();
        return student.Id;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var student = new Student { Id = id };
        _ctx.Remove(student);
        var result = await _ctx.SaveChangesAsync();
        return result;
    }

    public async Task<IEnumerable<StudentModel>> GetAllAsync()
    {
        return await _ctx.Students.Include(c => c.PhoneNumbers)
           .AsNoTracking()
           .Select(c => c.ToModel())
           .ToListAsync();
    }

    public async Task<StudentModel> GetByIdAsync(int id)
    {
        return (await _ctx.Students.Include(c => c.PhoneNumbers)
                                  .AsNoTracking()
                                  .Where(c => c.Id == id)
                                  .FirstOrDefaultAsync())
                                  .ToModel();
    }

    public async Task<int> UpdateAsync(StudentForUpdateModel studentForUpdate)
    {
        var student = new Student
        {
            Id = studentForUpdate.Id,
            FirstName = studentForUpdate.FirstName,
            LastName = studentForUpdate.LastName,
            Description = studentForUpdate.Description,
        };
        _ctx.Entry(student).Property(c => c.FirstName).IsModified = true;
        _ctx.Entry(student).Property(c => c.LastName).IsModified = true;
        _ctx.Entry(student).Property(c => c.Description).IsModified = true;

        var result = await _ctx.SaveChangesAsync();
        return result;
    }
}
