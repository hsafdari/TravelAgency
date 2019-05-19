using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class AccountProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SignupViewModel, User>().ForMember(x => x.CreatorDateTime, m => m.UseValue(DateTime.Now))
                                              //.ForMember(x => x.DisplayName, m => m.UseValue("System User"))
                                              //.ForMember(x => x.Gender, m => m.UseValue(Gender.Man))
                                              .IgnoreAllNonExisting();

            CreateMap<User, MyProfileViewModel>().IgnoreAllNonExisting();
            CreateMap<MyProfileViewModel, User>()//.ForMember(x => x.Gender, m => m.Condition(model => !model.IsSourceValueNull))
                                                 //.ForMember(x => x.FullName, m => m.MapFrom(model => model.FirstName + " " + model.LastName))
                                                 .ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now))
                                                 //.ForMember(x => x.ImageFileName, m => m.Condition(o=> !o.IsSourceValueNull))
                                                 //.ForMember(x => x.ImageSize, m => m.Condition(w => !w.IsSourceValueNull))
                                                 //.ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                 //.ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                 .IgnoreAllNonExisting();
        }
    }
}