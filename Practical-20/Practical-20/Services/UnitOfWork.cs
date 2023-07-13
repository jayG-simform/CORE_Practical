using Practical_20.Data;
using Practical_20.Interface;

namespace Practical_20.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AddDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AddDbContext context)
        {
           _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IStudentRepo<T> GetStudentRepo<T>() where T : class 
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IStudentRepo<T>)_repositories[typeof(T)];
            }

            var repository = new StudentRepository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
