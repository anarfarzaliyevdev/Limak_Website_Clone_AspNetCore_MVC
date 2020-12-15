using AutoMapper;
using Limak.Areas.Admin.ViewModels;
using Limak.Models;
using Limak.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {

           //Registiration map
            CreateMap<ApplicationUser, RegisterViewModel>();
            
            CreateMap<RegisterViewModel, ApplicationUser>().ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email));
            //Settings map
            CreateMap<ApplicationUser, SettingsViewModel>();
            //Update user map
            CreateMap<UpdateProfileViewModel, ApplicationUser>();
            CreateMap<UpdateIdInformationViewModel, ApplicationUser>();
            //Order map
            CreateMap<OrderViewModel, Order>();
            //Customer details map
            CreateMap<ApplicationUser, CustomerDetailsViewModel>();
            //Customer edit map
            CreateMap<ApplicationUser, CustomerEditViewModel>();
            CreateMap<CustomerEditViewModel, ApplicationUser>();
            //Add declare map
            CreateMap<CustomerAddDeclarationViewModel, Declaration>();
            //Edit declare map
            CreateMap<Declaration, CustomerEditDeclarationViewModel>();
            CreateMap<CustomerEditDeclarationViewModel, Declaration>();
            //Admin create map
            CreateMap<AdminsAddAdminViewModel, ApplicationUser>().ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email));
            //Admin edit map
            CreateMap<AdminsEditAdminViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, AdminsEditAdminViewModel>();
            //customer order edit map
            CreateMap<CustomerEditOrderViewModel, Order>();
            CreateMap<Order, CustomerEditOrderViewModel>();
            //Update package
            CreateMap<PackageUpdateViewModel, Declaration>();
        }
    }
}
