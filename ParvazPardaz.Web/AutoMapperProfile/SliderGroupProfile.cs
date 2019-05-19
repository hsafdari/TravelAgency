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
    public class SliderGroupProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SliderGroup, AddSliderGroupViewModel>().IgnoreAllNonExisting();
            CreateMap<AddSliderGroupViewModel, SliderGroup>().IgnoreAllNonExisting();

            CreateMap<EditSliderGroupViewModel, SliderGroup>().IgnoreAllNonExisting();
            CreateMap<SliderGroup, EditSliderGroupViewModel>().IgnoreAllNonExisting();

            CreateMap<SliderGroup, GridSliderGroupViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            CreateMap<SliderGroup, TourSliderGroupViewModel>()
                .ForMember(vm => vm.Title, m => m.MapFrom(model => model.GroupTitle))
                //.ForMember(vm => vm.Sliders, m => m.MapFrom(model => model.Sliders.Where(w => !w.IsDeleted && w.ImageIsActive && (w.Expirationdate != null ? System.Data.Entity.DbFunctions.TruncateTime(w.Expirationdate.Value) >= System.Data.Entity.DbFunctions.TruncateTime(DateTime.Now) : true)).ToList()))
                //شرط رو در اینکلود-فیلتر قبل مپینگ گذاشتیم
                .ForMember(vm => vm.Sliders, m => m.MapFrom(model => model.Sliders.ToList()))
                .IgnoreAllNonExisting();
        }
    }
}