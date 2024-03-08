using StudentsService.DAL.Entities;
using StudentsService.Domain.Models;

namespace StudentsService.DAL.Mappers;

public static class StudentMappers
{
    public static StudentModel ToModel (this Student student)
    {
        return new StudentModel
        {
            Id = student.Id,
            Description = student.Description,
            FirstName = student.FirstName,
            LastName = student.LastName,    
            StudentNumber = student.StudentNumber,
            PhoneNumbers = student.PhoneNumbers.Select(d=>d.Value).ToList(),
        };
    }
}
