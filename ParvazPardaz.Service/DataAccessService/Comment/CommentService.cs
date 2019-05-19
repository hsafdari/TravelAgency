using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using EntityType = ParvazPardaz.Model.Entity.Comment;
//using ParvazPardaz.Model.Entity.Product;
using ParvazPardaz.Service.Contract.Product;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.DataAccessService.Product
{
    public class CommentService : BaseService<EntityType.Comment>, ICommentService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityType.Comment> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public CommentService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityType.Comment>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridCommentViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                .Join(_unitOfWork.Set<LinkTable>(), jc => jc.OwnId, jL => jL.typeId, (jc, jL) => new { jc, jL })
                .Where(w => w.jL.linkType.ToString() == w.jc.CommentType.ToString())
                .Select(comment => new GridCommentViewModel()
                {
                    OwnId = comment.jc.OwnId,
                    OwnTitle = "",
                    CommentType = comment.jc.CommentType,
                    Id = comment.jc.Id,
                    Name = comment.jc.Name,
                    Email = comment.jc.Email,
                    CommentText = comment.jc.CommentText,
                    IsApproved = comment.jc.IsApproved,
                    CreatorDateTime = comment.jc.CreatorDateTime,
                    CreatorUserName = comment.jc.CreatorUser.UserName,
                    LastModifierDate = comment.jc.ModifierDateTime,
                    LastModifierUserName = comment.jc.ModifierUser.UserName,
                    Like = comment.jc.Like,
                    DisLike = comment.jc.DisLike,
                    Rate = comment.jc.Rate,
                    RateCount = comment.jc.RateCount,
                    Subject = comment.jc.Subject,
                    OwnLink = comment.jL.URL

                }).Distinct().AsQueryable();

            ////(comment.CommentType == Model.Enum.CommentType.PostComment ? _unitOfWork.Set<ParvazPardaz.Model.Entity.Post.Post>().FirstOrDefault(x => x.Id == comment.OwnId).Name : _unitOfWork.Set<ParvazPardaz.Model.Entity.Product.Product>().FirstOrDefault(x => x.Id == comment.OwnId).Name),
        }
        #endregion

        public string getString(int id)
        {
            return "متن";
        }
    }
}
