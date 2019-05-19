using AutoMapper;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.DataAccess.Infrastructure;

namespace ParvazPardaz.Web.AutoMapperProfile
{

    public class SelectedFlightProfile : Profile
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region	Ctor
        public SelectedFlightProfile(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        protected override void Configure()
        {
            CreateMap<SelectedFlight, AddSelectedFlightViewModel>().IgnoreAllNonExisting();
            CreateMap<AddSelectedFlightViewModel, SelectedFlight>().IgnoreAllNonExisting();

            CreateMap<EditSelectedFlightViewModel, SelectedFlight>().IgnoreAllNonExisting();
            CreateMap<SelectedFlight, EditSelectedFlightViewModel>().IgnoreAllNonExisting();

            CreateMap<SelectedFlight, GridSelectedFlightViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                          .ForMember(x => x.TrackingCode, m => m.MapFrom(model => model.Order.TrackingCode))
                                                          .ForMember(x => x.AdultCount, m => m.MapFrom(model => model.Order.AdultCount))
                                                          .ForMember(x => x.ChildCount, m => m.MapFrom(model => model.Order.ChildCount))
                                                          .ForMember(x => x.InfantCount, m => m.MapFrom(model => model.Order.InfantCount))
                                                          .ForMember(x => x.ReturnFlightDateTime, m => m.MapFrom(model => model.Order.ReturnFlightDateTime))
                                                          .ForMember(x => x.FlightDateTime, m => m.MapFrom(model => model.Order.FlightDateTime))
                                                          .ForMember(x => x.AirlineIATACode, m => m.MapFrom(model => model.AirlineIATACode))
                                                          .ForMember(x => x.BuyerTitle, m => m.MapFrom(model => model.CreatorUser.UserName))
                                                          .ForMember(x => x.NationalCode, m => m.MapFrom(model => (model.Order.CreatorUser.UserProfile != null ? model.Order.CreatorUser.UserProfile.NationalCode : "")))
                                                          .ForMember(x => x.TourTitle, m => m.MapFrom(model => model.TourSchedule.TourPackage.Tour.Title))
                                                          .ForMember(x => x.TourCode, m => m.MapFrom(model => model.TourSchedule.TourPackage.Tour.Code))
                                                          .ForMember(x => x.TourPackageCode, m => m.MapFrom(model => model.TourSchedule.TourPackage.Code))
                                                          .ForMember(x => x.TourPackage, m => m.MapFrom(model => model.TourSchedule.TourPackage.Title))
                                                          .ForMember(x => x.FlightType, m => m.MapFrom(model => model.FlightType))
                                                         
                                                          .IgnoreAllNonExisting();

            CreateMap<SelectedFlight, TourPacakgeFlightsViewModel>()
                 .ForMember(x => x.from, m => m.MapFrom(model => model.FromIATACode))
                 .ForMember(x => x.to, m => m.MapFrom(model => model.ToIATACode))
                 .ForMember(x => x.airline, m => m.MapFrom(model => model.AirlineIATACode))
                 .ForMember(x => x.airlineIcon, m => m.MapFrom(model=>model.CompanyTransfer.ImageUrl))
                 .ForMember(x => x.FlightDate, m => m.MapFrom(model => model.FlightDateTime.ToString("yyyy/MM/dd HH:mm")))
                 .ForMember(x => x.FlightDateTitle, m => m.MapFrom(model => model.FlightDateTime.ToString("yyyy/MM/dd")))
                 .ForMember(x => x.FlightDateTimeEn, m => m.MapFrom(model => model.FlightDateTime.Year + "/" + model.FlightDateTime.Month + "/" + model.FlightDateTime.Day + " "+String.Format("{0:HH:mm}",model.FlightDateTime)))
                 .ForMember(x => x.FlightDateEn, m => m.MapFrom(model => model.FlightDateTime.Year + "/" + model.FlightDateTime.Month + "/" + model.FlightDateTime.Day))
                 .ForMember(x => x.FlightDirection, m => m.MapFrom(model => model.FlightDirection))
                 .ForMember(x => x.FlightNumber, m => m.MapFrom(model => model.FlightNo))
                 .ForMember(x => x.FromCity, m => m.MapFrom(model => model.FromIATACode))
                 .ForMember(x=>x.FLightClass,m=>m.MapFrom(model=>model.VehicleTypeClass.Code))
                 .ForMember(x=>x.FLightClassTitle,m=>m.MapFrom(model=>model.VehicleTypeClass.Title))
                 .ForMember(x=>x.FLightClassTitleEn,m=>m.MapFrom(model=>model.VehicleTypeClass.TitleEn))
                 .ForMember(x => x.airlineEn,m=>m.MapFrom(model=>model.CompanyTransfer.TitleEn))
               .IgnoreAllNonExisting().AfterMap((src, dest) =>
               {
                   var city = _unitOfWork.Set<Airport>().Where(x => x.IataCode == src.FromIATACode || x.IataCode == dest.to).ToList();
                   var fromCity = city.Where(x => x.IataCode == src.FromIATACode).FirstOrDefault();
                   dest.FromCity = fromCity.City.Title;
                   dest.FromCityEn = fromCity.City.ENTitle;
                   dest.FromAirport = fromCity.Title;
                   dest.FromAirportEn = fromCity.TitleEn;                   
                   var toCity = city.Where(x => x.IataCode == src.ToIATACode).FirstOrDefault();
                   dest.ToCity = toCity.Title;
                   dest.ToCityEn = toCity.City.ENTitle;
                   dest.ToAirport = toCity.Title;
                   dest.ToAirportEn = toCity.TitleEn;
               });
        }

    }
}