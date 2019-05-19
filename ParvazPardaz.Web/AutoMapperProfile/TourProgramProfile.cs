using AutoMapper;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourProgramProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourProgram, ListViewTourProgramViewModel>().AfterMap((tp, vm) =>
                                        {
                                            vm.Activities = string.Join(",", tp.TourProgramActivities);
                                        });
            CreateMap<TourProgramViewModel, TourProgram>().IgnoreAllNonExisting();
            CreateMap<TourProgram, TourProgramViewModel>().ForMember(x => x.SelectedActivities, m => m.MapFrom(model => model.TourProgramActivities.Select(s => s.ActivityId)))
                                                          .ForMember(x => x.CRUDMode, m => m.UseValue(CRUDMode.Update)).IgnoreAllNonExisting();
        }
    }
}