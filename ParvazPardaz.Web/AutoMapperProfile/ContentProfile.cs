using AutoMapper;
using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class ContentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Content, EditContentViewModel>().IgnoreAllNonExisting();
            CreateMap<EditContentViewModel, Content>().ForMember(x => x.ImageFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                                                    .ForMember(x => x.ImageSize, opt => opt.Condition(source => !(source.ImageSize == 0)))
                                                    .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                    .ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                    .IgnoreAllNonExisting();

            CreateMap<AddContentViewModel, Content>().IgnoreAllNonExisting();
            CreateMap<Content, AddContentViewModel>().IgnoreAllNonExisting();

            CreateMap<Content, GridContentViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                                    .ForMember(x => x.ContentGroupTitle, m => m.MapFrom(model => model.ContentGroup.Title)).IgnoreAllNonExisting();

        }
    }
}