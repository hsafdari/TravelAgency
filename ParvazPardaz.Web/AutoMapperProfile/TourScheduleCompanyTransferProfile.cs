using AutoMapper;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourScheduleCompanyTransferProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourScheduleCompanyTransferViewModel, TourScheduleCompanyTransfer>().ForMember(x => x.NonLimit, m => m.MapFrom(model => (model.Capacity == 0) ? true : false))
                                                            .ForMember(x => x.Capacity, m => m.MapFrom(model => (model.NonLimit ? 0 : model.Capacity)))
                                                            .ForMember(x => x.StartDateTime, m => m.MapFrom(model => model.DepartureDate.Add(model.DepartureTime)))
                                                            .ForMember(x => x.EndDateTime, m => m.MapFrom(model => model.ArrivalDate.Add(model.ArrivalTime)))
                                                            .IgnoreAllNonExisting();

            CreateMap<TourScheduleCompanyTransfer, TourScheduleCompanyTransferViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update))
                                                                   .ForMember(x => x.DepartureDate, m => m.MapFrom(model => model.StartDateTime))
                                                                   .ForMember(x => x.ArrivalDate, m => m.MapFrom(model => model.EndDateTime))
                                                                   .IgnoreAllNonExisting();

            //استفاده در صفحه رزرو تور
            CreateMap<FlightViewModel, TourScheduleCompanyTransfer>().IgnoreAllNonExisting();

        }
    }
}