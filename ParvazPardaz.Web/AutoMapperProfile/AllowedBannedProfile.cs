using AutoMapper;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class AllowedBannedProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<AllowedBanned, EditAllowedBannedViewModel>().IgnoreAllNonExisting();

            CreateMap<AllowedBanned, AddAllowedBannedViewModel>().IgnoreAllNonExisting();

            CreateMap<AllowedBanned, GridAllowedBannedViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                   .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                   .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                   .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();


        }
        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }
}