using AutoMapper;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class PassengerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Passenger, GridPassengerViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                          .ForMember(x => x.BuyerTitle, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.BirthCountry, m => m.MapFrom(model => model.BirthCountry.Title))
                                                          .ForMember(x => x.TrackingCode, m => m.MapFrom(model => model.SelectedRoom.SelectedHotelPackage.Order.TrackingCode))
                                                          .ForMember(x => x.FlightDateTime, m => m.MapFrom(model => model.SelectedRoom.SelectedHotelPackage.Order.FlightDateTime))
                                                          .ForMember(x => x.ReturnFlightDateTime, m => m.MapFrom(model => model.SelectedRoom.SelectedHotelPackage.Order.ReturnFlightDateTime))
                                                          .ForMember(x => x.PassportExporterCountry, m => m.MapFrom(model => model.BirthCountry.Title))
                                                          .ForMember(x => x.Birthdate, m => m.MapFrom(model => (model.Birthdate != null ? model.Birthdate : null)))
                                                          .ForMember(x => x.OrderId, m => m.MapFrom(model => model.SelectedRoom.SelectedHotelPackage.OrderId))
                //.ForMember(x=>x.NationalityTitle,m=>m.MapFrom(model=>model.NationalityTitle))
                                                          .IgnoreAllNonExisting();
            CreateMap<AddPassengerViewModel, Passenger>()
            .ForMember(m => m.TicketNo, vm => vm.UseValue(""))
            .IgnoreAllNonExisting();

            CreateMap<Passenger, AddPassengerViewModel>()
                .ForMember(x => x.BirthCountryTitle, m => m.MapFrom(model => model.BirthCountry.Title))
                .ForMember(x => x.EnBirthCountryTitle, m => m.MapFrom(model => model.BirthCountry.ENTitle))
                .IgnoreAllNonExisting();
        }
    }
}