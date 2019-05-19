using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class HotelFacilityProfile : Profile
    {
        protected override void Configure()
        {
            //مپ ویو مدل به مدل جهت ثبت در مدل
            CreateMap<AddHotelFacilityViewModel, HotelFacility>().IgnoreAllNonExisting();
            CreateMap<HotelFacility, AddHotelFacilityViewModel>().IgnoreAllNonExisting();

            CreateMap<HotelFacility, GridHotelFacilityViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                   .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                   .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                   .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            CreateMap<EditHotelFacilityViewModel, HotelFacility>().IgnoreAllNonExisting();
            CreateMap<HotelFacility, EditHotelFacilityViewModel>().IgnoreAllNonExisting();
        }
    }
}