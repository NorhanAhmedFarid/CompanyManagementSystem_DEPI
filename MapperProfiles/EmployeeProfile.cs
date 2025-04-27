using AutoMapper;
using CompanyG02.DAL.Models;
using CompanyG02.PL.ViewModels;

namespace CompanyG02.PL.MapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            // .ForMember(e=>e.Name,m=>m.MapFrom(vm=>vm.EmpName));
        }
    }
}
