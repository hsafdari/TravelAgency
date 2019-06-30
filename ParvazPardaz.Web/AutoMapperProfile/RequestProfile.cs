using AutoMapper;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel.TourViewModels.Request;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class RequestProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Request, GridRequestViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                      .ForMember(x => x.FullName, m => m.MapFrom(model => (model.CreatorUser != null ? model.CreatorUser.FirstName + " " + model.CreatorUser.LastName : null)))
                                                      .ForMember(x => x.Email, m => m.MapFrom(model => (model.CreatorUser != null ? model.CreatorUser.Email : null)))
                                                      .ForMember(x => x.UserName, m => m.MapFrom(model => (model.CreatorUser != null ? model.CreatorUser.UserName : null)))
                                                      .ForMember(x => x.PhoneNumber, m => m.MapFrom(model => (model.CreatorUser != null ? model.CreatorUser.PhoneNumber : null)));
        }
    }
}