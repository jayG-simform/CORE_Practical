using Practical_20.Models;

namespace Practical_20.Interface
{
    public interface IStudentRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T student);
        void Delete(T student);
        void Update(T student);
    }
}
