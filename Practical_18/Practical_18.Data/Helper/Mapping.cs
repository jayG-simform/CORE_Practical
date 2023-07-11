using AutoMapper;
using DataAccessLayer.Model;
using DataAccessLayer.ViewModel;


namespace Practical_18.Data.Helper
{
    internal class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<Student, StudentView>().ReverseMap();
        }
    }
}
