using AutoMapper;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {

            CreateMap<AddUserViewModel, User>().ForMember(x => x.CreatorDateTime, m => m.UseValue(DateTime.Now))
                                               .ForMember(x => x.DisplayName, m => m.UseValue("System User"))
                                               .ForMember(x => x.FullName, m => m.MapFrom(model => model.FirstName + " " + model.LastName))
                                               .IgnoreAllNonExisting();

            CreateMap<User, EditUserViewModel>().ForMember(x => x.SelectedRoles, m => m.MapFrom(model => model.Roles.Select(r => r.RoleId).ToList()))
                                                .IgnoreAllNonExisting();

            CreateMap<EditUserViewModel, User>().ForMember(x => x.Gender, m => m.Condition(model => !model.IsSourceValueNull))
                                                .ForMember(x => x.FullName, m => m.MapFrom(model => model.FirstName + " " + model.LastName))
                                                .ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();


            CreateMap<User, GridUserViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                       .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.UserName)))
                                                       .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                       .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.UserName)))
                                                       .ForMember(x => x.FirstName, m => m.MapFrom(model => model.UserProfile.FirstName))
                                                       .ForMember(x => x.LastName, m => m.MapFrom(model => model.UserProfile.LastName))
                                                       .ForMember(x => x.DisplayName, m => m.MapFrom(model => model.UserProfile.DisplayName))
                                                       .ForMember(x => x.Email, m => m.MapFrom(model => model.UserProfile.RecoveryEmail))
                                                       .ForMember(x => x.PhoneNumber, m => m.MapFrom(model => model.UserProfile.PhoneNumber))
                                                       .ForMember(x => x.NationalCode, m => m.MapFrom(model => model.UserProfile.NationalCode))
                                                       .ForMember(x => x.Organization, m => m.MapFrom(model => model.UserProfile.Organization))
                                                       .IgnoreAllNonExisting();


            CreateMap<User, MyProfileViewModel>()
                .ForMember(x => x._roles, m => m.Ignore())
                .ForMember(x => x.UserAddresses, m => m.MapFrom(model => model.UserProfile.UserAddresses))
                .ForMember(x => x.AvatarFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                .ForMember(x => x.AvatarExtension, m => m.MapFrom(model => model.UserProfile.AvatarExtension))
                .ForMember(x => x.AvatarUrl, m => m.MapFrom(model => model.UserProfile.AvatarUrl))
                //.ForMember(x => x.AvatarSize, m => m.MapFrom(model => model.UserProfile.AvatarSize))
                .ForMember(x => x._selectedRoles, m => m.MapFrom(model => model.Roles.Select(x => x.RoleId)))
                .ForMember(x => x.MobileNumber, m => m.MapFrom(model => (model.UserProfile != null ? model.UserProfile.MobileNumber : (model.PhoneNumber != null ? model.PhoneNumber : ""))))
                .ForMember(x => x.BirthDate, m => m.MapFrom(model => model.UserProfile.BirthDate))
                .ForMember(x => x.Facebook, m => m.MapFrom(model => model.UserProfile.Facebook))
                .ForMember(x => x.Fax, m => m.MapFrom(model => model.UserProfile.Fax))
                .ForMember(x => x.FirstName, m => m.MapFrom(model => model.UserProfile.FirstName))
                .ForMember(x => x.Instagram, m => m.MapFrom(model => model.UserProfile.Instagram))
                .ForMember(x => x.LastName, m => m.MapFrom(model => model.UserProfile.LastName))
                .ForMember(x => x.LinkedIn, m => m.MapFrom(model => model.UserProfile.LinkedIn))
                .ForMember(x => x.Organization, m => m.MapFrom(model => model.UserProfile.Organization))
                .ForMember(x => x.OtherSocialNetwork, m => m.MapFrom(model => model.UserProfile.OtherSocialNetwork))
                .ForMember(x => x.PhoneNumber, m => m.MapFrom(model => model.UserProfile.PhoneNumber))
                .ForMember(x => x.RecoveryEmail, m => m.MapFrom(model => (model.UserProfile.RecoveryEmail != null ? model.UserProfile.RecoveryEmail : model.Email)))
                .ForMember(x => x.Telegram, m => m.MapFrom(model => model.UserProfile.Telegram))
                .ForMember(x => x.Twitter, m => m.MapFrom(model => model.UserProfile.Twitter))
                .ForMember(x => x.WebSiteUrl, m => m.MapFrom(model => model.UserProfile.WebSiteUrl))
                .ForMember(x => x.Gender, m => m.MapFrom(model => model.UserProfile.Gender != null ? model.UserProfile.Gender : Model.Enum.Gender.Man))
                .ForMember(x => x.NationalCode, m => m.MapFrom(model => model.UserProfile.NationalCode))
                .ForMember(x => x.OwnDescription, m => m.MapFrom(model => model.UserProfile.OwnDescription))
                .ForMember(x => x.PublicDescription, m => m.MapFrom(model => model.UserProfile.PublicDescription))
                //.ForMember(x => x.ProfileType, m => m.MapFrom(model => model.UserProfile.ProfileType))
                .IgnoreAllNonExisting();

            CreateMap<MyProfileViewModel, User>().ForMember(x => x.Gender, m => m.Condition(model => !model.IsSourceValueNull))
                                                 .ForMember(x => x.FullName, m => m.MapFrom(model => model.FirstName + " " + model.LastName))
                                                 .ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now))
                                                 .IgnoreAllNonExisting();



            //mapping for UserPfofile
            CreateMap<MyProfileViewModel, ParvazPardaz.Model.Entity.Users.UserProfile>()
                .ForMember(x => x.BirthDate, y => y.MapFrom(m => m.BirthDate))
                .IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.Model.Entity.Users.UserProfile, MyProfileViewModel>().ForMember(x => x.AvatarFileName, m => m.Condition(o => o.PropertyMap.SourceMember != null && !o.IsSourceValueNull))
                                                                      .ForMember(x => x.AvatarExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.AvatarUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x._selectedRoles, m => m.MapFrom(model => model.User.Roles.Select(x => x.RoleId)))
                                                                      .ForMember(x => x.ProfileType, m => m.MapFrom(model => model.ProfileType))
                                                                      .IgnoreAllNonExisting();

            CreateMap<AddUserUserProfileViewModel, ParvazPardaz.Model.Entity.Users.UserProfile>().IgnoreAllNonExisting();

            //mapping for UserAddress
            CreateMap<UserAddressViewModel, UserAddress>().IgnoreAllNonExisting();
            CreateMap<UserAddress, UserAddressViewModel>().IgnoreAllNonExisting();

        }
    }
}