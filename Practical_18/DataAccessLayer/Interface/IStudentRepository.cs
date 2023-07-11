using DataAccessLayer.Model;
using DataAccessLayer.ViewModel;

namespace DataAccessLayer.Interface
{
    public interface IStudentRepository
    {
        Task<StudentView> GetStudent(int id);
        Task<IEnumerable<StudentView>> GetAllStudents();
        Task<int> Add(StudentView student);
        Task<bool> Edit(StudentView studentChanges);
        Task<bool> Delete(int id);
    }
}
