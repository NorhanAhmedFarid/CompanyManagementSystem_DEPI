using AutoMapper;
using CompanyG02.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CompanyG02.PL.MapperProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>().ForMember(d=>d.Name,o=>o.MapFrom(s=>s.RoleName))
                .ReverseMap();    
        }
    }
}
