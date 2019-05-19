using AutoMapper;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class HotelRankProfile : Profile
    {
        protected override void Configure()
        {
            //مپ مدل به ویو مدل جهت نمایش در ویو اگر فیلدی در ویو مدل همنام نبود با مدل بدین صورت مپ میکنیم
            CreateMap<HotelRank, EditHotelRankViewModel>().IgnoreAllNonExisting();
            //مپ ویومدل به مدل برای ثبت داده ها ویرایش شده به همین جهت ار فیلدهایی که در ویو نیامده و در ویو مدل آمده و نخواهیم در مدل تغییری کند صرف نظر میشود
            CreateMap<EditHotelRankViewModel, HotelRank>().ForMember(x => x.ImageFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageSize, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.Icon, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .IgnoreAllNonExisting();

            CreateMap<AddHotelRankViewModel, HotelRank>().IgnoreAllNonExisting();
            CreateMap<HotelRank, AddHotelRankViewModel>().IgnoreAllNonExisting();

            CreateMap<HotelRank, GridHotelRankViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
    }
}