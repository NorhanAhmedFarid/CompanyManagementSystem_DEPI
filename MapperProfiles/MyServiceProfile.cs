using AutoMapper;
using CompanyG02.DAL.Models;
using CompanyG02.PL.ViewModels;
namespace CompanyG02.PL.MapperProfiles
{
    public class MyServiceProfile : Profile
    {
        public MyServiceProfile()
        {
            CreateMap<MyServiceViewModel, MyService>().ReverseMap();
        }
    }
}
