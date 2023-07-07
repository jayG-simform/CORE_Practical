using Practical17.Models;

namespace Practical17.Interface
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
        Student Add(Student student);
        Student Edit(Student studentChanges);
        Student Delete(int id);
        Student Details(int id);
    }
}
