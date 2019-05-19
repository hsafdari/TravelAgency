using System;
using AutoMapper;
using System.Web;
using ParvazPardaz.ViewModel;
using System.Collections.Generic;
using ParvazPardaz.Common.Extension;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class HotelBoardProfile : Profile
    {
        #region Configure
        protected override void Configure()
        {
            CreateMap<AddHotelBoardViewModel, HotelBoard>().IgnoreAllNonExisting();
            //.ForMember(x => x.CreatorDateTime, m => m.UseValue(DateTime.Now))

            CreateMap<HotelBoard, EditHotelBoardViewModel>().IgnoreAllNonExisting();
            CreateMap<EditHotelBoardViewModel, HotelBoard>().ForMember(x => x.ModifierDateTime, m => m.UseValue(DateTime.Now))
                                                                       .ForMember(x => x.ImageSize, opt => opt.Condition(source => !(source.ImageSize == 0)))
                                                                      .ForMember(x => x.ImageExtension, m => m.Condition(w => !w.IsSourceValueNull))
                                                                      .ForMember(x => x.ImageUrl, m => m.Condition(w => !w.IsSourceValueNull))
                                                                       .IgnoreAllNonExisting();
            CreateMap<HotelBoard, AddHotelBoardViewModel>().IgnoreAllNonExisting();
            //CreateMap<HotelBoard, HotelBoard>().IgnoreAllNonExisting();
            CreateMap<HotelBoard, GridHotelBoardViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                           .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                           .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                           .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
        #endregion
    }
}
