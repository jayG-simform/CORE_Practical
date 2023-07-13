using Microsoft.EntityFrameworkCore;
using Practical_20.Data;
using Practical_20.Interface;
using Practical_20.Models;

namespace Practical_20.Services
{
    public class StudentRepository<T> : IStudentRepo<T> where T : class
    {
        private DbSet<T> _context;

        public StudentRepository(AddDbContext context) 
        {
            _context = context.Set<T>();
        }
        public void Delete(T student)
        {
            _context.Remove(student);
        }

        public IEnumerable<T> GetAll()
        {
            return _context;
        }

        public T GetById(int id)
        {
            return _context.Find(id);
        }

        public void Insert(T student)
        {
            _context.Add(student);
        }

        public void Update(T student)
        {
            _context.Update(student);
        }
    }
}
