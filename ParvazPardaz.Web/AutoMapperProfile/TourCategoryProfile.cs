using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourCategoryProfile: Profile
    {
        protected override void Configure()
        {
            CreateMap<TourCategory, AddTourCategoryViewModel>();

            CreateMap<TourCategory, EditTourCategoryViewModel>().IgnoreAllNonExisting();
            CreateMap<TourCategory, GridTourCategoryViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
           CreateMap<TourCategory, AddTourCategoryViewModel>().IgnoreAllNonExisting();

        }
    }
}