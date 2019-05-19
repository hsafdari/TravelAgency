using AutoMapper;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class RoleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddRoleViewModel, Role>().ForMember(x => x.CreatorDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<Role, EditRoleViewModel>().IgnoreAllNonExisting();
            CreateMap<EditRoleViewModel, Role>().ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<Role, GridRoleViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                .ForMember(x => x.RoleName, m => m.MapFrom(model => model.Name))
                                                .IgnoreAllNonExisting();
        }
    }
}