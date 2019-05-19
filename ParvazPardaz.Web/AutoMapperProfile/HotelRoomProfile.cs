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
    public class HotelRoomProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<HotelRoom, AddHotelRoomViewModel>()
                .ForMember(v => v.Priority, m => m.MapFrom(model => model.Priority.Value));

            CreateMap<HotelRoom, EditHotelRoomViewModel>().IgnoreAllNonExisting()
                .ForMember(v => v.Priority, m => m.MapFrom(model => model.Priority.Value));

            CreateMap<HotelRoom, GridHotelRoomViewModel>().ForMember(v => v.Priority, m => m.MapFrom(model => model.Priority.Value))
                                                          .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            CreateMap<HotelPackageHotelRoom, EditHotelRoomInPackageViewModel>()
                .ForMember(x => x.TourId, m => m.MapFrom(model => model.HotelPackage.TourPackage.TourId))
                .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.HotelPackage.TourPackage.Tour.Title))
                .ForMember(x => x.TourPackageId, m => m.MapFrom(model => model.HotelPackage.TourPackage.Id))
                .ForMember(x => x.TourPackageTitle, m => m.MapFrom(model => model.HotelPackage.TourPackage.Title))
                .ForMember(x => x.HotelPackageId, m => m.MapFrom(model => model.HotelPackage.Id))
                //اینجا باید لیستی از عنوان های هتل ها به صورت لیستی بیاید
                .ForMember(x => x.HotelPackageTitle, m => m.MapFrom(model => model.HotelPackage.HotelPackageHotels.Select(y => y.Hotel.Title)))
                .ForMember(x => x.HotelPackageHotelRoomId, m => m.MapFrom(model => model.Id))
                .ForMember(x => x.HotelRoomTitle, m => m.MapFrom(model => model.HotelRoom.Title))
                //.ForMember(x => x.TotalCapacity, m => m.MapFrom(model => model.Capacity))
                .ForMember(x => x.TotalAdultCapacity, m => m.MapFrom(model => model.AdultCapacity))
                .ForMember(x => x.TotalChildCapacity, m => m.MapFrom(model => model.ChildCapacity))
                .ForMember(x => x.TotalInfantCapacity, m => m.MapFrom(model => model.InfantCapacity))
                .ForMember(x => x.BookingCapacity, m => m.UseValue(0))       // ????
                .ForMember(x => x.RemainBookingCapacity, m => m.UseValue(0)) // ????
                //.ForMember(x => x.CapacitySold, m => m.MapFrom(model => model.CapacitySold))
                .ForMember(x => x.AdultCapacitySold, m => m.MapFrom(model => model.AdultCapacitySold))
                .ForMember(x => x.ChildCapacitySold, m => m.MapFrom(model => model.ChildCapacitySold))
                .ForMember(x => x.InfantCapacitySold, m => m.MapFrom(model => model.InfantCapacitySold))
                .ForMember(x => x.AdultPrice, m => m.MapFrom(model => model.AdultPrice))
                .ForMember(x => x.InfantPrice, m => m.MapFrom(model => model.InfantPrice))
                .ForMember(x => x.ChildPrice, m => m.MapFrom(model => model.ChildPrice))
                //.ForMember(x => x.OtherCurrencyPrice, m => m.MapFrom(model => model.OtherCurrencyPrice))
                .ForMember(x => x.AdultOtherCurrencyPrice, m => m.MapFrom(model => model.AdultOtherCurrencyPrice))
                .ForMember(x => x.ChildOtherCurrencyPrice, m => m.MapFrom(model => model.ChildOtherCurrencyPrice))
                .ForMember(x => x.InfantOtherCurrencyPrice, m => m.MapFrom(model => model.InfantOtherCurrencyPrice))
                .ForMember(x => x.OtherCurrencyTitle, m => m.MapFrom(model => (model.Currency != null ? model.Currency.Title : "")))
                .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}