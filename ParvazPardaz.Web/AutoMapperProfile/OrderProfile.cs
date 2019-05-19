using AutoMapper;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model;
using ParvazPardaz.ViewModel.TourViewModels.Tour.TourClient;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            //CreateMap<Order, AddOrderViewModel>().IgnoreAllNonExisting();
            //CreateMap<AddOrderViewModel, Order>().IgnoreAllNonExisting();

            CreateMap<EditOrderViewModel, Order>().IgnoreAllNonExisting();
            CreateMap<Order, EditOrderViewModel>().IgnoreAllNonExisting();

            CreateMap<Order, GridOrderViewModel>()
                .ForMember(x => x.OrderId, m => m.MapFrom(model => model.Id))
                .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                .ForMember(x => x.BuyerTitle, m => m.MapFrom(model => (model.CreatorUser != null ? model.CreatorUser.UserName : string.Empty)))
                .ForMember(x => x.NationalCode, m => m.MapFrom(model => ((model.CreatorUser != null && model.CreatorUser.UserProfile != null) ? model.CreatorUser.UserProfile.NationalCode : string.Empty)))
                .ForMember(x => x.OrderInfo, m => m.MapFrom(model => model.OrderInformation))
                .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.SelectedHotelPackages.FirstOrDefault(y => y.OrderId == model.Id).HotelPackage.TourPackage.Tour.Title))
                .ForMember(x => x.TourCode, m => m.MapFrom(model => model.SelectedHotelPackages.FirstOrDefault(y => y.OrderId == model.Id).HotelPackage.TourPackage.Tour.Code))
                .ForMember(x => x.TourPackageCode, m => m.MapFrom(model => model.SelectedHotelPackages.FirstOrDefault(y => y.OrderId == model.Id).HotelPackage.TourPackage.Code))
                .ForMember(x => x.TourPackage, m => m.MapFrom(model => model.SelectedHotelPackages.FirstOrDefault(y => y.OrderId == model.Id).HotelPackage.TourPackage.Title))
                .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

            CreateMap<Order, TourReserveViewModel>()
                .ForMember(x => x.HotelInfos, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(y => y.SelectedHotels)))
                .ForMember(x => x.HotelRoomInfos, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(y => y.SelectedRooms)))
                .ForMember(x => x.SelectedFlights, m => m.MapFrom(model => model.SelectedFlights))
                .ForMember(x => x.PassengerList, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(z => z.SelectedRooms.Select(p => p.Passengers).ToList())))
                .IgnoreAllNonExisting();

            CreateMap<Order, VoucherViewModel>()
                .ForMember(x => x.HotelInfos, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(y => y.SelectedHotels)))
                .ForMember(x => x.HotelRoomInfos, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(y => y.SelectedRooms)))
                .ForMember(x => x.SelectedFlights, m => m.MapFrom(model => model.SelectedFlights))
                .ForMember(x => x.PassengerList, m => m.MapFrom(model => model.SelectedHotelPackages.SelectMany(z => z.SelectedRooms.SelectMany(p => p.Passengers)).ToList()))
                .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.SelectedHotelPackages.Select(y => y.HotelPackage.TourPackage.Tour.Title + "-" + y.HotelPackage.TourPackage.Title).FirstOrDefault()))
                //.ForMember(x => x., m => m.MapFrom(model => model.SelectedHotelPackages.Select(y => y.HotelPackage.TourPackage.Tour.Title + "-" + y.HotelPackage.TourPackage.Title).FirstOrDefault()))
                .IgnoreAllNonExisting();

            CreateMap<SelectedHotel, HotelClientViewModel>()
                .ForMember(x => x.hotel, m => m.MapFrom(model => model.Hotel.Title))
                .ForMember(x => x.GoogleMapIFrame, m => m.MapFrom(model => model.Hotel.LatLongIframe))
                .ForMember(x => x.location, m => m.MapFrom(model => model.Hotel.Location))
                .ForMember(x => x.ServiceTooltip, m => m.MapFrom(model => model.SelectedHotelPackage.HotelPackage.HotelPackageHotels.FirstOrDefault().HotelBoard.Name))
                .ForMember(x => x.description, m => m.MapFrom(model => model.Hotel.Summary))
                .ForMember(x=>x.location,m=>m.MapFrom(model=>model.Hotel.Location))
                .ForMember(x => x.City, m => m.MapFrom(model => model.Hotel.City.Title))
                .ForMember(x => x.Address, m => m.MapFrom(model => model.Hotel.Address))
                .ForMember(x => x.url, m => m.MapFrom(model => model.Hotel.Website))
                .ForMember(x => x.Tel, m => m.MapFrom(model => model.Hotel.Tel))
                .ForMember(x => x.Summary, m => m.MapFrom(model => model.Hotel.Summary))
                .ForMember(x => x.stars, m => m.MapFrom(model => model.Hotel.HotelRank.Title))
                .ForMember(x => x.images, m => m.MapFrom(model => model.Hotel.HotelGalleries.Where(g => g.IsDeleted == false).Select(i => new ImageViewModel { ImageUrl = i.ImageUrl + i.ImageExtension })))
                .IgnoreAllNonExisting();

            CreateMap<SelectedRoom, HotelPackageHotelRoomViewModel>()
                  .ForMember(x => x.Title, m => m.MapFrom(model => model.HotelRoom.Title))

                  .ForMember(x => x.AdultCount, m => m.MapFrom(model => model.AdultCount))
                  .ForMember(x => x.AdultRialPrice, m => m.MapFrom(model => model.AdultUnitPrice))
                  .ForMember(x => x.AdultOtherCurrencyPrice, m => m.MapFrom(model => model.AdultCurrencyPrice))

                  .ForMember(x => x.ChildCount, m => m.MapFrom(model => model.ChildCount))
                  .ForMember(x => x.ChildRialPrice, m => m.MapFrom(model => model.ChildUnitPrice))
                  .ForMember(x => x.ChildOtherCurrencyPrice, m => m.MapFrom(model => model.ChildCurrencyPrice))

                  .ForMember(x => x.InfantCount, m => m.MapFrom(model => model.InfantCount))
                  .ForMember(x => x.InfantRialPrice, m => m.MapFrom(model => model.InfantUnitPrice))
                  .ForMember(x => x.InfantOtherCurrencyPrice, m => m.MapFrom(model => model.InfantCurrencyPrice))

                  .ForMember(x => x.BaseRialPrice, m => m.MapFrom(model => model.BaseCurrencyToRialPrice))
                  .ForMember(x => x.OtherCurrencyTitle, m => m.MapFrom(model => model.CurrencySign))

                  .ForMember(x => x.VATPrice, m => m.MapFrom(model => model.VATPrice))
                //بازبینی شود صفدری
                //.ForMember(x => x.RialPrice, m => m.MapFrom(model => model.RialPrice))
                //.ForMember(x => x.OtherCurrencyPrice, m => m.MapFrom(model => model.CurrencyPrice.Value))
                //.ForMember(x => x.ReservedCapacity, m => m.MapFrom(model => model.ReserveCapacity))
                //.ForMember(x => x.TotalPassengerPrice, m => m.MapFrom(model => (model.UnitPrice * 1)))
                .IgnoreAllNonExisting();



        }
    }
}