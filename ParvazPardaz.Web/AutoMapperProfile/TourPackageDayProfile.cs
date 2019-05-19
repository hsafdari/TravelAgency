using System;
using AutoMapper;
using System.Web;
using ParvazPardaz.ViewModel;
using System.Collections.Generic;
using ParvazPardaz.Common.Extension;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourPackageDayProfile : Profile
    {
        #region Configure
        protected override void Configure()
        {
            CreateMap<AddTourPackageDayViewModel, TourPackageDay>().ForMember(x => x.CreatorDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<TourPackageDay, EditTourPackageDayViewModel>().IgnoreAllNonExisting();
            CreateMap<EditTourPackageDayViewModel, TourPackageDay>().ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now)).IgnoreAllNonExisting();

            CreateMap<TourPackageDay, GridTourPackageDayViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                          .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                          .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                          .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
        #endregion
    }
}
