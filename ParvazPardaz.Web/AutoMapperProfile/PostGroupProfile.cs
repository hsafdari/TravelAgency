using AutoMapper;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class PostGroupProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<PostGroup, EditPostGroupViewModel>()
                .ForMember(x => x.ParetntTitle, m => m.MapFrom(model => model.PostGroupParent.Name))
                .IgnoreAllNonExisting();
            CreateMap<EditPostGroupViewModel, PostGroup>().IgnoreAllNonExisting();

            CreateMap<AddPostGroupViewModel, PostGroup>()
                .ForMember(x=>x.Name, vm=>vm.MapFrom(viewmodel=>viewmodel.Name))
                .IgnoreAllNonExisting();
            CreateMap<PostGroup, AddPostGroupViewModel>().IgnoreAllNonExisting();

            CreateMap<PostGroup, GridPostGroupViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                                    .ForMember(x => x.Parent, m => m.MapFrom(model => model.PostGroupParent.Name))
                                                                    .IgnoreAllNonExisting();
        }
    }
}