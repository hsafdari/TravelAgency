using AutoMapper;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AddTourViewModel, ParvazPardaz.Model.Entity.Tour.Tour>()
                //.ForMember(m => m.RequiredDocuments, vm => vm.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Tour.Tour, AddTourViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update))
                                                                              .ForMember(x => x.SelectedAllows, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed).Select(w => w.AllowedBannedId).ToList()))
                                                                              .ForMember(x => x.SelectedBans, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed == false).Select(w => w.AllowedBannedId).ToList()))
                                                                              .ForMember(x => x.SelectedTourCategory, m => m.MapFrom(model => model.TourCategories.Select(w => w.Id).ToList()))
                                                                              .ForMember(x => x.SelectedTourLevel, m => m.MapFrom(model => model.TourLevels.Select(w => w.Id).ToList()))
                                                                              .ForMember(x => x.SelectedTourType, m => m.MapFrom(model => model.TourTypes.Select(w => w.Id).ToList()))
                                                                              .IgnoreAllNonExisting();

            CreateMap<EditTourViewModel, ParvazPardaz.Model.Entity.Tour.Tour>().IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Tour.Tour, EditTourViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update))
                                                                              .ForMember(x => x.SelectedAllows, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed).Select(w => w.AllowedBannedId).ToList()))
                                                                              .ForMember(x => x.SelectedBans, m => m.MapFrom(model => model.TourAllowBans.Where(w => w.IsAllowed == false).Select(w => w.AllowedBannedId).ToList()))
                                                                              .ForMember(x => x.SelectedTourCategory, m => m.MapFrom(model => model.TourCategories.Select(w => w.Id).ToList()))
                                                                              .ForMember(x => x.SelectedTourLevel, m => m.MapFrom(model => model.TourLevels.Select(w => w.Id).ToList()))
                                                                              .ForMember(x => x.SelectedTourType, m => m.MapFrom(model => model.TourTypes.Select(w => w.Id).ToList()))
                                                                              //.ForMember(vm => vm.TourLandingPageUrlId, m => m.MapFrom(model => model.TourLandingPageUrlId))
                                                                              .IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Tour.Tour, GridTourViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                      .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                      .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                      .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                      .ForMember(x => x.Recomended, m => m.MapFrom(model => model.Recomended ? "✔" : "✘"))
                                                      .IgnoreAllNonExisting();

        }

    }
}