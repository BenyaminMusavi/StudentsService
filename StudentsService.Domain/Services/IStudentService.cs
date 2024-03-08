using StudentsService.Domain.Models;

namespace StudentsService.Domain.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentModel>> GetAllAsync();
    Task<StudentModel> GetByIdAsync(int id);
    Task<int> CreateAsync(StudentModel studentForCreate);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(StudentForUpdateModel studentForUpdate);
}
