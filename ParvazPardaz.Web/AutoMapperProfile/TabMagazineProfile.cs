using AutoMapper;
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.ViewModel.Magazine;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Post;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TabMagazineProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TabMagazine, AddTabMagazineViewModel>().IgnoreAllNonExisting();
            CreateMap<AddTabMagazineViewModel, TabMagazine>().IgnoreAllNonExisting();

            CreateMap<EditTabMagazineViewModel, TabMagazine>().IgnoreAllNonExisting();
            CreateMap<TabMagazine, EditTabMagazineViewModel>()
                .ForMember(vm => vm.SelectedGroups, m => m.MapFrom(model => model.Groups.Select(t => t.Id).ToList()))
                .IgnoreAllNonExisting();

            CreateMap<TabMagazine, GridTabMagazineViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                          .ForMember(vm => vm.CountryTitle, m => m.MapFrom(model => model.Location.Title))
                                                          .ForMember(vm => vm.SelectedGroupsTitle, m => m.MapFrom(model => model.Groups.Select(t => t.Name).ToList()))
                                                          .IgnoreAllNonExisting();
        }
    }
}