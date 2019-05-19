using AutoMapper;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class HotelProfile : Profile
    {
        protected override void Configure()
        {
            #region قبلی
            //hotelDetail
            CreateMap<ParvazPardaz.Model.Entity.Hotel.Hotel, HotelDetailsViewModel>()
                .ForMember(vw => vw.Images, m => m.MapFrom(x => x.HotelGalleries))
                .ForMember(vm => vm.PostGroups, m => m.Ignore())
                .ForMember(vm => vm.Name, m => m.MapFrom(model => model.Title))
                .ForMember(vm => vm.PostSummery, m => m.MapFrom(model => model.Summary))
                .ForMember(vm => vm.PostContent, m => m.MapFrom(model => model.Description))
                .ForMember(vm => vm.PostSort, m => m.MapFrom(model => model.Sort))
                .IgnoreAllNonExisting();
            CreateMap<ParvazPardaz.Model.Entity.Hotel.HotelGallery, ImageSliderViewModel>();
            #endregion

            #region اضافه شده
            CreateMap<AddHotelViewModel, ParvazPardaz.Model.Entity.Hotel.Hotel>()
                .ForMember(m => m.HotelRankId, vm => vm.MapFrom(viewmodel => viewmodel.RateId))
                .IgnoreAllNonExisting();
            CreateMap<ParvazPardaz.Model.Entity.Hotel.Hotel, AddHotelViewModel>()
                //.ForMember(vm => vm.RateId, m => m.MapFrom(model => model.HotelRankId))
                .IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Hotel.Hotel, EditHotelViewModel>()
                .ForMember(vm => vm.RateId, m => m.MapFrom(model => model.HotelRankId))
                .ForMember(vm => vm.TagTitles, m => m.MapFrom(model => model.Tags.Select(t => t.Name).ToList()))
                .IgnoreAllNonExisting();

            CreateMap<EditHotelViewModel, ParvazPardaz.Model.Entity.Hotel.Hotel>().ForMember(x => x.ImageFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageSize, opt => opt.Condition(source => !(source.ImageSize == 0)))
                                                                      .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(m => m.HotelRankId, vm => vm.MapFrom(viewmodel => viewmodel.RateId))
                                                                      .IgnoreAllNonExisting();



            CreateMap<ParvazPardaz.Model.Entity.Hotel.Hotel, GridHotelViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.FirstName + " " + model.CreatorUser.LastName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.FirstName + " " + model.CreatorUser.LastName : String.Empty))
                                                                    .ForMember(x => x.HotelTags, m => m.MapFrom(model => model.Tags.Select(x => x.Name).ToList()))
                                                                    .ForMember(x => x.PostGroups, m => m.MapFrom(model => model.PostGroups.Select(x => x.Name).ToList()))
                                                                    .ForMember(x => x.PictureCount, m => m.MapFrom(model => model.HotelGalleries.Count))
                                                                    .ForMember(vm => vm.CityTitle, m => m.MapFrom(model => model.City.Title))
                                                                    .ForMember(vm => vm.Rate, m => m.MapFrom(model => model.HotelRank.Title))
                                                                    .IgnoreAllNonExisting();


            CreateMap<ParvazPardaz.Model.Entity.Hotel.Hotel, SelectedHotelViewModel>()
                //.ForMember(x=>x.BoardTitle, m=>m.MapFrom(model=>model.))
             .ForMember(vm => vm.CityTitle, m => m.MapFrom(model => model.City.Title))
             .ForMember(vm => vm.Id, m => m.MapFrom(model => model.Id))
             .ForMember(vm => vm.RateTitle, m => m.MapFrom(model => model.HotelRank))
             .ForMember(vm => vm.Title, m => m.MapFrom(model => model.Title))
             .IgnoreAllNonExisting();
            #endregion
        }
    }
}