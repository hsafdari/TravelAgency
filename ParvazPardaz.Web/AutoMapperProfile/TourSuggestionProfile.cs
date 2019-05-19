using System;
using AutoMapper;
using System.Web;
using ParvazPardaz.ViewModel;
using System.Collections.Generic;
using ParvazPardaz.Common.Extension;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Magazine;

namespace ParvazPardaz.Web.AutoMapperProfile
{
    public class TourSuggestionProfile : Profile
    {
        #region Configure
        protected override void Configure()
        {
            CreateMap<AddTourSuggestionViewModel, TourSuggestion>().IgnoreAllNonExisting();
            CreateMap<TourSuggestion, AddTourSuggestionViewModel>().IgnoreAllNonExisting();

            CreateMap<TourSuggestion, EditTourSuggestionViewModel>().IgnoreAllNonExisting();
            CreateMap<EditTourSuggestionViewModel, TourSuggestion>()                                                   
                                                    .ForMember(x => x.ImageURL, m => m.Condition(w => !w.IsSourceValueNull))
                                                    .IgnoreAllNonExisting();
            CreateMap<TourSuggestion, TourSuggestionViewModel>().IgnoreAllNonExisting();
            CreateMap<TourSuggestion, GridTourSuggestionViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                        .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                        .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                        .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty)).IgnoreAllNonExisting();
        }
        #endregion
    }
}
