﻿using AutoMapper;
using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class OrderTypeProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<OrderType, AddOrderTypeViewModel>().IgnoreAllNonExisting();
            CreateMap<AddOrderTypeViewModel, OrderType>().IgnoreAllNonExisting();

            CreateMap<EditOrderTypeViewModel, OrderType>().IgnoreAllNonExisting();
            CreateMap<OrderType, EditOrderTypeViewModel>().IgnoreAllNonExisting();

            CreateMap<OrderType, GridOrderTypeViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();

        }
    }
}