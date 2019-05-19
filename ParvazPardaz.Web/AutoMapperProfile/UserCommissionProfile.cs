using System;
using AutoMapper;
using System.Web;
using ParvazPardaz.ViewModel;
using System.Collections.Generic;
using ParvazPardaz.Common.Extension;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class UserCommissionProfile : Profile
    {
        #region Configure
        protected override void Configure()
        {

            CreateMap<UserCommission, EditUserCommissionViewModel>().IgnoreAllNonExisting();
            CreateMap<EditUserCommissionViewModel, UserCommission>().ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<UserCommission, GridUserCommissionViewModel>()
                                                          .ForMember(x=>x.Organization,m=>m.MapFrom(model=>model.UserProfile.Organization))
                                                          .ForMember(x => x.NationalCode, m => m.MapFrom(model => model.UserProfile.NationalCode))
                                                          .ForMember(x => x.UserName, m => m.MapFrom(model => model.UserProfile.User.UserName))                                                          
                                                          .ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
        #endregion
    }
}
