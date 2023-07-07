using Practical17.Data;
using Practical17.Interface;
using Practical17.Models;

namespace Practical17.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext context;

        public StudentRepository(StudentDbContext context)
        {
            this.context = context;
        }
        public Student Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            Student st = context.Students.Find(id);
            if (st != null)
            {
                context.Students.Remove(st);
                context.SaveChanges();
            }
            return st;
        }

        public Student Edit(Student studentChanges)
        {
            var st = context.Students.Attach(studentChanges);
            st.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return studentChanges;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }

        public Student GetStudent(int id)
        {
            return context.Students.Find(id);

        }
        public Student Details(int id)
        {
            return context.Students.Find(id);
            
        }
    }
}
