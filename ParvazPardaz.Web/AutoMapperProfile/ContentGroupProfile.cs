﻿using AutoMapper;
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class ContentGroupProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentGroup, AddContentGroupViewModel>().IgnoreAllNonExisting();
            CreateMap<AddContentGroupViewModel, ContentGroup>().IgnoreAllNonExisting();

            CreateMap<EditContentGroupViewModel, ContentGroup>().IgnoreAllNonExisting();
            CreateMap<ContentGroup, EditContentGroupViewModel>().IgnoreAllNonExisting();

            CreateMap<ContentGroup, GridContentGroupViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
    }
}