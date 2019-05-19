using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace ParvazPardaz.Service.DataAccessService.Post
{
    public class PostGroupService : BaseService<PostGroup>, IPostGroupService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<PostGroup> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public PostGroupService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<PostGroup>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridPostGroupViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridPostGroupViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllPostGroupOfSelectListItem
        public IEnumerable<SelectListItem> GetAllPostGroupOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Name, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion

        #region GetAllNullParentPostGroupOfSelectListItem
        public IEnumerable<SelectListItem> GetAllNullParentPostGroupOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Where(x => x.ParentId == null).Select(x => new SelectListItem() { Selected = false, Text = x.Name, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion

        #region GetAllnotNullParentPostGroupOfSelectListItem
        public IEnumerable<SelectListItem> GetAllnotNullParentPostGroupOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Where(x => x.ParentId != null).Select(x => new SelectListItem() { Selected = false, Text = "(" + x.PostGroupParent.Name + ")" + x.Name, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion

        #region TourGroupDDL
        public MultiSelectList GetTourGroupMSL()
        {
            return new MultiSelectList(_dbSet.Where(x => !x.IsDeleted && x.IsActive && x.Title.ToLower().Contains("tour") && !x.Title.ToLower().Contains("tourist")).ToList(), "Id", "Name");
        }
        #endregion

        #region SeedDatabase
        public void SeedDatabase()
        {
            //const string systemAdminEmail = "admin@gmail.com";
            //const string systemAdminUserName = systemAdminEmail;
            //const string systemAdminPassword = "123456";

            //const string systemAdminDisplayName = "System Administrator";

            var postGroup = _dbSet.FirstOrDefault(u => u.Name == "دسته بندی نشده");
            if (postGroup == null)
            {
                postGroup = new PostGroup
                {
                    //DisplayName = systemAdminDisplayName,
                    Name = "دسته بندی نشده",
                    ParentId = null,
                    IsActive = true,
                    CreatorDateTime = DateTime.Now,
                };

                this.Create(postGroup);
                //this.SetLockoutEnabled(user.Id, false);
            }



            //var userRoles = _roleManager.FindUserRoles(user.Id);
            //if (userRoles.Any(a => a == StandardRoles.SystemAdministrator)) return;
            //this.AddToRole(user.Id, StandardRoles.SystemAdministrator);
        }
        #endregion
    }
}
