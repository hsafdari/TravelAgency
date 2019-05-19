using AutoMapper;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class LinkProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<LinkTable, EditLinkViewModel>()
                .ForMember(vm=>vm.ControllerUrl,m=>m.Ignore())
                //.ForMember(vm => vm.ControllerUrl, m => m.MapFrom(model => model.URL.Split('/')[1]))
                //.ForMember(vm => vm.EndUrl, m => m.MapFrom(model => model.URL.Substring(model.URL.LastIndexOf("/") + 1, (model.URL.Length) - (model.URL.LastIndexOf("/") + 1))))
                .IgnoreAllNonExisting();
            CreateMap<EditLinkViewModel, LinkTable>()
               
                .IgnoreAllNonExisting();
            CreateMap<LinkTable, GridLinkViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                   .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                   .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                   .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                                   .IgnoreAllNonExisting();
            
        }
    }
}