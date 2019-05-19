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
    public class OrderInformationProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OrderInformation, AddOrderInformationViewModel>().IgnoreAllNonExisting();
            CreateMap<AddOrderInformationViewModel, OrderInformation>().IgnoreAllNonExisting();

            CreateMap<EditOrderInformationViewModel, OrderInformation>().IgnoreAllNonExisting();
            CreateMap<OrderInformation, EditOrderInformationViewModel>().IgnoreAllNonExisting();

            CreateMap<OrderInformation, GridOrderInformationViewModel>()
                                                          .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                          .ForMember(x => x.Title, m => m.MapFrom(model => model.Title))
                                                          .ForMember(x=>x.CityId,m=>m.MapFrom(model=>model.CityId.Value))                                                          
                                                          .IgnoreAllNonExisting();
            


        }
    }
}