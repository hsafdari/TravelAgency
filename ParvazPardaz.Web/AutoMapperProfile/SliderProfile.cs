using AutoMapper;
using ParvazPardaz.Model;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class SliderProfile : Profile
    {
        protected override void Configure()
        {
            //مپ مدل به ویو مدل جهت نمایش در ویو اگر فیلدی در ویو مدل همنام نبود با مدل بدین صورت مپ میکنیم
            CreateMap<Slider, EditSliderViewModel>().IgnoreAllNonExisting();
            //مپ ویومدل به مدل برای ثبت داده ها ویرایش شده به همین جهت ار فیلدهایی که در ویو نیامده و در ویو مدل آمده و نخواهیم در مدل تغییری کند صرف نظر میشود
            CreateMap<EditSliderViewModel, Slider>().ForMember(x => x.ImageURL, m => m.Condition(w => !w.IsSourceValueNull)).IgnoreAllNonExisting();

            CreateMap<AddSliderViewModel, Slider>().IgnoreAllNonExisting();
            CreateMap<Slider, AddSliderViewModel>().IgnoreAllNonExisting();
            CreateMap<Slider, SlidersUIViewModel>().IgnoreAllNonExisting();

            CreateMap<Slider, GridSliderViewModel>().ForMember(x => x.SliderGroupTitle, m => m.MapFrom(model => model.SliderGroup.GroupTitle))
                                                                    .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            //مپ مدل به ویو مدل جهت نمایش در ویو اگر فیلدی در ویو مدل همنام نبود با مدل بدین صورت مپ میکنیم
            CreateMap<Slider, EditTourHomeSliderViewModel>().IgnoreAllNonExisting();
            //مپ ویومدل به مدل برای ثبت داده ها ویرایش شده به همین جهت ار فیلدهایی که در ویو نیامده و در ویو مدل آمده و نخواهیم در مدل تغییری کند صرف نظر میشود
            CreateMap<EditTourHomeSliderViewModel, Slider>().ForMember(x => x.ImageURL, m => m.Condition(w => !w.IsSourceValueNull)).IgnoreAllNonExisting();

            CreateMap<AddTourHomeSliderViewModel, Slider>().IgnoreAllNonExisting();
            CreateMap<Slider, AddTourHomeSliderViewModel>().IgnoreAllNonExisting();
            //CreateMap<Slider, SlidersUIViewModel>().IgnoreAllNonExisting();

            CreateMap<Slider, GridTourHomeSliderViewModel>().ForMember(x => x.SliderGroupTitle, m => m.MapFrom(model => model.SliderGroup.GroupTitle))
                                                                    .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            CreateMap<Slider, SlidersUITourHomeViewModel>()
                .ForMember(vm => vm.SliderGroupTitle, m => m.MapFrom(model => model.SliderGroup.GroupTitle))
                .IgnoreAllNonExisting();
        }
    }
}