using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.ViewModel;
using Microsoft.EntityFrameworkCore;
using Practical_18.DataContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practical_18.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext context;
        private readonly IMapper mapper;

        public StudentRepository(StudentDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        async Task<int> IStudentRepository.Add(StudentView student)
        {
            try
            {
                var data = mapper.Map<Student>(student);
                data.Id = 0;
                await context.Students.AddAsync(data);
                await context.SaveChangesAsync();
                int id = await context.Students.MaxAsync(s => s.Id);
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        async Task<bool> IStudentRepository.Delete(int id)
        {
            var st = await context.Students.Where(a=>a.Id==id).FirstOrDefaultAsync();
            if (st != null)
            {
                context.Remove(st);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<bool> IStudentRepository.Edit(StudentView studentChanges)
        {
            var st = await context.Students.FirstOrDefaultAsync(x=>x.Id==studentChanges.Id);
            if (st != null)
            {
                st.Name = studentChanges.Name;
                st.Age = studentChanges.Age;
                st.Address = studentChanges.Address;
                context.Students.Update(st);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<IEnumerable<StudentView>> IStudentRepository.GetAllStudents()
        {
            var data = await context.Students.ToListAsync();
            return mapper.Map<IEnumerable<StudentView>>(data);
        }

        async Task<StudentView> IStudentRepository.GetStudent(int id)
        {
            Student? data = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            return mapper.Map<StudentView>(data);
           
        }
    }
}
