using AutoMapper;
using ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Product;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Post;

namespace ParvazPardaz.Web.AutoMapperProfile
{


    public class CommentProfile : Profile
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region	Ctor
        public CommentProfile(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        protected override void Configure()
        {
            CreateMap<Comment, EditCommentViewModel>().IgnoreAllNonExisting();

            CreateMap<EditCommentViewModel, Comment>().IgnoreAllNonExisting();

            CreateMap<ParvazPardaz.ViewModel.ClientCommentViewModel, Comment>()
                .ForMember(x => x.ParentId, vm => vm.MapFrom(vmodel => (vmodel.ParentId != 0 ? vmodel.ParentId : null)))
                .ForMember(x => x.CommentReviews, vm => vm.MapFrom(v => v.CommentReviews.Where(w => w.Rate > 0)))
                .IgnoreAllNonExisting();

            CreateMap<Comment, ParvazPardaz.ViewModel.ClientCommentViewModel>().IgnoreAllNonExisting();

            CreateMap<Comment, GridCommentViewModel>().ForMember(x => x.CreatorDateTime, m => m.MapFrom(model => (model.CreatorDateTime.HasValue) ? model.CreatorDateTime : null))
                                                                    .ForMember(x => x.CreatorUserName, m => m.MapFrom(model => (model.CreatorUser != null) ? model.CreatorUser.UserName : String.Empty))
                                                                    .ForMember(x => x.LastModifierDate, m => m.MapFrom(model => (model.ModifierDateTime.HasValue) ? model.ModifierDateTime : null))
                                                                    .ForMember(x => x.LastModifierUserName, m => m.MapFrom(model => (model.ModifierUser != null) ? model.ModifierUser.UserName : String.Empty))
                                                                    .ForMember(v => v.Like, m => m.MapFrom(model => model.Like.Value))
                                                                    .ForMember(v => v.DisLike, m => m.MapFrom(model => model.DisLike.Value))
                                                                    .ForMember(v => v.Subject, m => m.MapFrom(model => model.Subject))
                                                                    .IgnoreAllNonExisting();
        }
    }
}