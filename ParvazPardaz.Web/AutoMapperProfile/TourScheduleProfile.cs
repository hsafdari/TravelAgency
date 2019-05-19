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
    public class TourScheduleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourScheduleViewModel, TourSchedule>().ForMember(x => x.NonLimit, m => m.MapFrom(model => (model.Capacity == 0) ? true : false))
                                                            .ForMember(x => x.Capacity, m => m.MapFrom(model => (model.NonLimit ? 0 : model.Capacity)))
                                                            .IgnoreAllNonExisting();

            CreateMap<TourSchedule, TourScheduleViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update)).IgnoreAllNonExisting();
        }
    }
}