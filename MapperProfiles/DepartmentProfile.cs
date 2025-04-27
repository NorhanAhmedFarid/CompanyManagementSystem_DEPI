using AutoMapper;
using CompanyG02.DAL.Models;
using CompanyG02.PL.ViewModels;




namespace CompanyG02.PL.MapperProfiles
{

    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

        }
    }
}
