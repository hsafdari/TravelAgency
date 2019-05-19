using AutoMapper;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class FooterProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Footer, AddFooterViewModel>().IgnoreAllNonExisting();
            CreateMap<AddFooterViewModel, Footer>().IgnoreAllNonExisting();

            CreateMap<EditFooterViewModel, Footer>().IgnoreAllNonExisting();
            CreateMap<Footer, EditFooterViewModel>().IgnoreAllNonExisting();
            CreateMap<Footer, FooterUIViewModel>().IgnoreAllNonExisting();
            CreateMap<Footer, GridFooterViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
    }
}