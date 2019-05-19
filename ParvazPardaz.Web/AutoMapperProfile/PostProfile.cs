using AutoMapper;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class PostProfile : Profile
    {
        protected override void Configure()
        {
            //مپ مدل به ویو مدل جهت نمایش در ویو اگر فیلدی در ویو مدل همنام نبود با مدل بدین صورت مپ میکنیم
            CreateMap<ParvazPardaz.Model.Entity.Post.Post, EditPostViewModel>().IgnoreAllNonExisting();

            //مپ ویومدل به مدل برای ثبت داده ها ویرایش شده به همین جهت ار فیلدهایی که در ویو نیامده و در ویو مدل آمده و نخواهیم در مدل تغییری کند صرف نظر میشود
            CreateMap<EditPostViewModel, ParvazPardaz.Model.Entity.Post.Post>().ForMember(x => x.ImageFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageSize, opt => opt.Condition(source => !(source.ImageSize == 0)))
                                                                      .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .IgnoreAllNonExisting();

            CreateMap<AddPostViewModel, ParvazPardaz.Model.Entity.Post.Post>().IgnoreAllNonExisting();
            CreateMap<ParvazPardaz.Model.Entity.Post.Post, AddPostViewModel>().IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Post.Post, GridPostViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null && model.CreatorUser.UserProfile != null) ? model.CreatorUser.UserProfile.DisplayName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null && model.ModifierUser.UserProfile != null) ? model.ModifierUser.UserProfile.DisplayName : String.Empty))
                                                                    .ForMember(x => x.PostGroups, m => m.MapFrom(model => model.PostGroups.Select(x => x.Name).ToList()))
                                                                    .ForMember(x => x.PostTags, m => m.MapFrom(model => model.Tags.Select(x => x.Name).ToList()))
                                                                    .ForMember(x => x.PictureCount, m => m.MapFrom(model => model.PostImages.Count))
                                                                    .IgnoreAllNonExisting();



            //PostDetail
            CreateMap<ParvazPardaz.Model.Entity.Post.Post, PostDetailViewModel>()
                .ForMember(vm => vm.PostGroups, m => m.Ignore())
                .ForMember(vm => vm.ModifierDateTime, m => m.MapFrom(model => model.ModifierDateTime.Value))
                .IgnoreAllNonExisting();
            //hotelDetail
            CreateMap<ParvazPardaz.Model.Entity.Post.Post, HotelDetailsViewModel>()
                .ForMember(vw => vw.Images, m => m.MapFrom(x => x.PostImages))
                 .ForMember(vm => vm.PostGroups, m => m.Ignore())
                .IgnoreAllNonExisting();


            CreateMap<ParvazPardaz.Model.Entity.Post.PostImage, ImageSliderViewModel>();



        }
    }
}