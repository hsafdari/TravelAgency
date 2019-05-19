using AutoMapper;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class SelectedHotelPackageProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SelectedHotelPackage, AddSelectedHotelPackageViewModel>().IgnoreAllNonExisting();
            CreateMap<AddSelectedHotelPackageViewModel, SelectedHotelPackage>().IgnoreAllNonExisting();

            CreateMap<EditSelectedHotelPackageViewModel, SelectedHotelPackage>().IgnoreAllNonExisting();
            CreateMap<SelectedHotelPackage, EditSelectedHotelPackageViewModel>().IgnoreAllNonExisting();

            CreateMap<SelectedHotelPackage, GridSelectedHotelPackageViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                          .ForMember(x => x.OrderId, m => m.MapFrom(model => model.OrderId))
                                                          .ForMember(x => x.Id, m => m.MapFrom(model => model.Id))
                                                          .ForMember(x => x.NationalCode, m => m.MapFrom(model => model.Order.OrderInformation.NationalCode))
                                                          .ForMember(x => x.FlightDateTime, m => m.MapFrom(model => model.Order.FlightDateTime))
                                                          .ForMember(x => x.ReturnFlightDateTime, m => m.MapFrom(model => model.Order.ReturnFlightDateTime))
                                                          .ForMember(x => x.SelectedHotelPackageId, m => m.MapFrom(model => model.HotelPackageId))
                                                          .ForMember(x => x.TotalDiscountPrice, m => m.MapFrom(model => model.Order.TotalDiscountPrice))
                                                          .ForMember(x => x.TotalPayPrice, m => m.MapFrom(model => model.Order.TotalDiscountPrice))
                                                          .ForMember(x => x.TotalPrice, m => m.MapFrom(model => model.Order.TotalPrice))
                                                          .ForMember(x => x.TotalTaxPrice, m => m.MapFrom(model => model.Order.TotalTaxPrice))
                                                          .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.SelectedHotels.FirstOrDefault(y => y.SelectedHotelPackageId == model.Id).SelectedHotelPackage.HotelPackage.TourPackage.Tour.Title))
                                                          .ForMember(x => x.TourPackageCode, m => m.MapFrom(model => model.SelectedHotels.FirstOrDefault(y => y.SelectedHotelPackageId == model.Id).SelectedHotelPackage.HotelPackage.TourPackage.Code))
                                                          .ForMember(x => x.TourCode, m => m.MapFrom(model => model.SelectedHotels.FirstOrDefault(y => y.SelectedHotelPackageId == model.Id).SelectedHotelPackage.HotelPackage.TourPackage.Tour.Code))
                                                          .ForMember(x => x.TourPackageTitle, m => m.MapFrom(model => model.SelectedHotels.FirstOrDefault(y => y.SelectedHotelPackageId == model.Id).SelectedHotelPackage.HotelPackage.TourPackage.Title))
                                                          .ForMember(x => x.NumberRoom, m => m.MapFrom(model => model.SelectedRooms.Count()))
                                                          .ForMember(x => x.TrackingCode, m => m.MapFrom(model => model.Order.TrackingCode))
                                                          .ForMember(x => x.hotelRoomsInPackage, m => m.MapFrom(model => model.SelectedRooms))
                                                          .ForMember(x => x.hotelsInPackage, m => m.MapFrom(model => model.SelectedHotels))
                                                          .ForMember(x => x.BuyerTitle, m => m.MapFrom(model => model.CreatorUser.UserName))
                                                          .IgnoreAllNonExisting();
            //باید نوشته شود
            //SelectedRoom,HotelRoomsInPackageViewModel
            //HotelPackage,HotelsInPackageViewModel

            CreateMap<SelectedRoom, HotelRoomsInPackageViewModel>()
                .ForMember(x => x.Title, m => m.MapFrom(model => model.HotelRoom.Title))
                .IgnoreAllNonExisting();
            CreateMap<SelectedHotel, HotelsInPackageViewModel>()
                .ForMember(x => x.CityTitle, m => m.MapFrom(model => model.Hotel.City.Title))
                 .IgnoreAllNonExisting();
            CreateMap<SelectedHotel, SelectedHotelViewModel>()
           .ForMember(x => x.BoardTitle, m => m.MapFrom(model => model.Hotel.HotelPackageHotels.FirstOrDefault(y => y.HotelPackageId == model.SelectedHotelPackageId).HotelBoard.Title))
           .ForMember(x => x.CityTitle, m => m.MapFrom(model => model.Hotel.City.Title))
           .ForMember(x => x.RateTitle, m => m.MapFrom(model => model.Hotel.HotelRank.Title))
           .ForMember(x => x.Id, m => m.MapFrom(model => model.Hotel.Id))
           .IgnoreAllNonExisting();

            //اضافی صفدری 16/04
            //CreateMap<SelectedHotel, GridSelectedHotelPackageViewModel>()
            //    .ForMember(x => x.AdultCount, m => m.MapFrom(model => model.SelectedHotelPackage.AdultCount))
            //    .ForMember(x => x.ChildCount, m => m.MapFrom(model => model.SelectedHotelPackage.ChildCount))
            //    .ForMember(x => x.InfantCount, m => m.MapFrom(model => model.SelectedHotelPackage.InfantCount))
            //    .ForMember(x => x.SelectedRoom, m => m.MapFrom(model => model.SelectedHotelPackage.SelectedRooms))
            //    .ForMember(x => x.SelectedRoomCount, m => m.MapFrom(model => model.SelectedHotelPackage.SelectedRoomCount))
            //    .ForMember(x => x.TotalServicePrice, m => m.MapFrom(model => model.SelectedHotelPackage.TotalServicePrice))
            //    .ForMember(x => x.HotelPackageId, m => m.MapFrom(model => model.SelectedHotelPackageId))
            //    .IgnoreAllNonExisting();
            //CreateMap<SelectedHotelPackage, GridSelectedHotelPackageViewModel>()

            // CreateMap<GridSelectedHotelViewModel, SelectedHotel>()
            //.ForMember(x => x.Hotel, m => m.MapFrom(model => model.SelectedHotels.Where(x => x.Id == model.HotelId)))
            //.IgnoreAllNonExisting();

            CreateMap<SelectedHotel, GridSelectedHotelViewModel>()
                .ForMember(x => x.SelectedHotels, m => m.MapFrom(model => model.Hotel))
                .IgnoreAllNonExisting();

            CreateMap<HotelPackage, SelectedHotelPackage>()
                //.ForMember(x=>x.AdultCount,m=>m.MapFrom(model=>model.))
                //.ForMember(x => x.ChildCount, m => m.MapFrom(model => model.SelectedHotelPackage.ChildCount))
                //.ForMember(x => x.InfantCount, m => m.MapFrom(model => model.SelectedHotelPackage.InfantCount))
                .ForMember(vm => vm.HotelPackageId, m => m.MapFrom(model => model.Id))
                .ForMember(vm => vm.OrderId, m => m.MapFrom(model => model.OrderId))
                .ForMember(vm => vm.SelectedRoomCount, m => m.MapFrom(model => model.HotelPackageHotelRooms.Count()))
                //.ForMember(vm => vm.TotalServicePrice, m => m.MapFrom(model => model.HotelPackageHotelRooms.Select(x => new { totalPrice = x.Price + (x.Currency != null ? (x.Currency.BaseRialPrice * (x.OtherCurrencyPrice ?? 0)) : 0) }).Select(x => x.totalPrice).Count()))
                .ForMember(vm => vm.TotalAdultPrice, m => m.MapFrom(model => model.HotelPackageHotelRooms.Select(x => new { totalPrice = x.AdultPrice + (x.Currency != null ? (x.Currency.BaseRialPrice * (x.AdultOtherCurrencyPrice ?? 0)) : 0) }).Select(x => x.totalPrice).Count()))
                .ForMember(vm => vm.TotalChildPrice, m => m.MapFrom(model => model.HotelPackageHotelRooms.Select(x => new { totalPrice = x.ChildPrice + (x.Currency != null ? (x.Currency.BaseRialPrice * (x.ChildOtherCurrencyPrice ?? 0)) : 0) }).Select(x => x.totalPrice).Count()))
                .ForMember(vm => vm.TotalInfantPrice, m => m.MapFrom(model => model.HotelPackageHotelRooms.Select(x => new { totalPrice = x.InfantPrice + (x.Currency != null ? (x.Currency.BaseRialPrice * (x.InfantOtherCurrencyPrice ?? 0)) : 0) }).Select(x => x.totalPrice).Count()))
                .IgnoreAllNonExisting();


        }
    }
}