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
    public class TourProgramDetailProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TourProgramDetailViewModel, TourProgramDetail>()
                .ForMember(x => x.Description, vm => vm.MapFrom(v => v.DetailDescription))
                .ForMember(x => x.ActivityId, vm => vm.MapFrom(v => v.TourActivityId))
                .IgnoreAllNonExisting();
        }
    }
}