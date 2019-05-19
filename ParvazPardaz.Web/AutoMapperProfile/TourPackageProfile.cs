using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourPackageProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourPackageViewModel, TourPackage>().IgnoreAllNonExisting();

            CreateMap<TourPackage, TourPackageViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update))
                                                                              //.ForMember(x => x.SelectedAllows, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed).Select(w => w.AllowedBannedId).ToList()))
                                                                              //.ForMember(x => x.SelectedBans, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed == false).Select(w => w.AllowedBannedId).ToList()))
                                                                              //.ForMember(x => x.SelectedTourCategory, m => m.MapFrom(model => model.TourCategories.Select(w => w.Id).ToList()))
                                                                              //.ForMember(x => x.SelectedTourLevel, m => m.MapFrom(model => model.TourLevels.Select(w => w.Id).ToList()))
                                                                              //.ForMember(x => x.SelectedTourType, m => m.MapFrom(model => model.TourTypes.Select(w => w.Id).ToList()))
                                                                              .IgnoreAllNonExisting();
            CreateMap<TourPackage, TourPackageViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update)).IgnoreAllNonExisting();

            CreateMap<TourPackage, GridTourPackageViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                      .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                      .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                      .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                      .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.Tour.Title))
                                                      .ForMember(x=>x.TourCode,m=>m.MapFrom(model=>model.Tour.Code))
                                                      .ForMember(x=>x.DayTitle,m=>m.MapFrom(model=>model.TourPackageDay.Title)                                                      
                                                      ).IgnoreAllNonExisting();

            CreateMap<TourPackage, TourPackageClientViewModel>()     
                .IgnoreAllNonExisting();
            CreateMap<TourPackage, EditTourPackageViewModel>().IgnoreAllNonExisting();
            CreateMap<EditTourPackageViewModel, TourPackage>()
                    .ForMember(x => x.ImageURL, m => m.Condition(w => !w.IsSourceValueNull))
                    .IgnoreAllNonExisting();
        }
    }
}