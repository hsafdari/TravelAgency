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
    public class TourSliderProfile :Profile
    {
        protected override void Configure()
        {
            CreateMap<TourSlider, ImageViewModel>().IgnoreAllNonExisting();
            CreateMap<ImageViewModel, TourSlider>().IgnoreAllNonExisting();
        }
    }
}