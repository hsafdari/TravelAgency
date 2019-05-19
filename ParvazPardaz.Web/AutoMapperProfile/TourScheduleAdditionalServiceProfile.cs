using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourScheduleAdditionalServiceProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourScheduleAdditionalServiceViewModel, TourScheduleAdditionalService>().ForMember(x => x.NonLimit, m => m.MapFrom(model => (model.Capacity == 0) ? true : false))
                                                                                              .ForMember(x => x.Capacity, m => m.MapFrom(model => (model.NonLimit ? 0 : model.Capacity)))
                                                                                              .IgnoreAllNonExisting();

            CreateMap<TourScheduleAdditionalService, TourScheduleAdditionalServiceViewModel>().ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update)).IgnoreAllNonExisting();
        }
    }
}