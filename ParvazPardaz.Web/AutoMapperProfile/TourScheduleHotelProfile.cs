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
    public class TourScheduleHotelProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourScheduleHotelViewModel, TourScheduleHotel>().IgnoreAllNonExisting();

            //CreateMap<TourScheduleHotel, TourScheduleHotelViewModel>().ForMember(x => x.TourId, m => m.MapFrom(model => model.TourSchedule.TourId))
            //                                                          .ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update))
            //                                                          .IgnoreAllNonExisting();
        }

    }
}