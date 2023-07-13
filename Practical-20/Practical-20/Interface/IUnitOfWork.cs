namespace Practical_20.Interface
{
    public interface IUnitOfWork :IDisposable
    {
        IStudentRepo<T> GetStudentRepo<T>() where T : class;

        Task SaveChangesAsync();

    }
}
