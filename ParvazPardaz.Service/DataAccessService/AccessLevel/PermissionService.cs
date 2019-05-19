using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityType = ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.Model.Entity.AccessLevel;
using Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Model.Entity.Core;
using Z.EntityFramework.Plus;

namespace ParvazPardaz.Service.Contract.AccessLevel
{

    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Permission> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public PermissionService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Permission>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridPermissionViewModel> GetViewModelForGrid()
        {
            return _dbSet.AsNoTracking().ProjectTo<GridPermissionViewModel>(_mappingEngine);
            //.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
        }

        public IQueryable<GridPermissionViewModel> GetViewModelForGrid(GridPermissionViewModel searchPermissionViewModel)
        {
            var permissions = _dbSet.AsNoTracking().ProjectTo<GridPermissionViewModel>(_mappingEngine);

            #region Filter search data
            if (!string.IsNullOrEmpty(searchPermissionViewModel.PermissionName))
            {
                permissions = permissions.Where(x => x.PermissionName.Contains(searchPermissionViewModel.PermissionName));
            }
            //if (!string.IsNullOrEmpty(searchPermissionViewModel.Prop2))
            //{
            //    permissions = permissions.Where(x => x.Prop2.Contains(searchPermissionViewModel.Prop2));
            //} 
            #endregion

            return permissions;
        }
        #endregion

        #region CanAccess
        public bool CanAccess(string permission, string url)
        {
            long userId = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name).Id;
            string urlWithOutSlash = string.Concat(url.Where((s, i) => !(s == '/' && i == url.Length - 1)));
            bool IsAccess = (from r in _unitOfWork.Set<Role>()
                             join rp in _unitOfWork.Set<RolePermissionController>()
                             on r.Id equals rp.RoleId
                             join c in _unitOfWork.Set<ControllersList>()
                             on rp.ControllerId equals c.Id
                             join p in _unitOfWork.Set<Permission>()
                             on rp.PermissionId equals p.Id
                             where r.Users.Any(x => x.UserId == userId) && p.PermissionName == permission && (c.PageUrl.ToLower() == url.ToLower() || c.PageUrl.ToLower() == urlWithOutSlash.ToLower())
                             select rp.Id).Distinct().Count() != 0;
            return IsAccess;
        }
        #endregion


        public bool SetAccess(int RoleId, string Url, bool Access, string Permission)
        {
            //try
            //{
            if (Access)
            {
                #region ایجاد
                var IsExist = (from c in _unitOfWork.Set<ControllersList>()
                               where c.PageUrl == Url
                               select c).FirstOrDefault();
                var _Permission = _unitOfWork.Set<ParvazPardaz.Model.Entity.AccessLevel.Permission>().FirstOrDefault(p => p.PermissionName == Permission);

                if (IsExist == null)
                {
                    //actionitem.Method = "/Admin/" + currentController + "/" + m.Name;
                    var items = Url.Split(new[] { '/' });

                    ControllersList model = new ControllersList();

                    model.ActionName = items[3];
                    model.ControllerName = items[2];
                    model.PageName = "";
                    model.PageUrl = Url;
                    _unitOfWork.Set<ControllersList>().Add(model);
                    _unitOfWork.Set<RolePermissionController>().Add(new RolePermissionController()
                    {
                        RoleId = RoleId,
                        PermissionId = _Permission.Id,
                        ControllerId = model.Id,
                        IsDeleted = false
                    });
                    _unitOfWork.SaveAllChanges();
                }
                ///لینک صفحه یافت شد
                else
                {
                    var hasPermission = (from r in IsExist.RolePermissionControllers where r.RoleId == RoleId select r).FirstOrDefault();
                    ///قبلا دسترسی داشته است؟
                    if (hasPermission != null)
                    {
                        ///دسترسی را بردار
                        //if (!Access)
                        //{
                        //    foreach (var item in IsExist.RolePermissionControllers)
                        //    {
                        //        _unitOfWork.Set<RolePermissionController>().Remove(item);
                        //    }
                        //    _unitOfWork.SaveAllChanges();
                        //}
                    }
                    ///قبلا دسترسی نداشته است
                    else
                    {
                        IsExist.RolePermissionControllers.Add(new RolePermissionController()
                        {
                            RoleId = RoleId,
                            PermissionId = _Permission.Id,
                            ControllerId = IsExist.Id,
                            IsDeleted = false
                        });
                        _unitOfWork.SaveAllChanges();
                    }
                }
                return true;
                #endregion
            }
            else
            {
                var rpc = _unitOfWork.Set<RolePermissionController>().FirstOrDefault(x => x.RoleId == RoleId && x.ControllersList.PageUrl == Url && x.Permission.PermissionName == Permission);
                _unitOfWork.Set<RolePermissionController>().Remove(rpc);
                _unitOfWork.SaveAllChanges(false);
                return true;
            }

        }
        public List<Menu> MenuPermissions(int userId)
        {
            List<Menu> AccessMenuList = new List<Menu>();
            var Urls = (from c in _unitOfWork.Set<ControllersList>()
                        join p in _unitOfWork.Set<RolePermissionController>()
                        on c.Id equals p.ControllerId
                        where p.Role.Users.Any(x => x.UserId == userId) && p.Permission.PermissionName == "List"
                        select c.PageUrl)
                        .AsEnumerable()
                        .Select(url => new { pageUrl = url.ToLower().TrimStart('/').TrimEnd('/') })
                        .Select(x => x.pageUrl).Distinct().ToList();

            //تمامی آیتم های منوی ادمین
            var adminMenus = _unitOfWork.Set<Menu>().Where(m => !m.IsDeleted && m.MenuIsActive && m.MenuGroup.GroupName.Equals("Admin")).OrderBy(m => m.OrderId).IncludeFilter(p => p.MenuChilds.Where(w => !w.IsDeleted && w.MenuIsActive)).ToList();
            foreach (var item in adminMenus)
            {
                //آیتم های همسان پیدا می شوند
                var l2childs = item.MenuChilds.Where(x => !x.IsDeleted && x.MenuIsActive && Urls.Contains(x.MenuUrl.ToLower().TrimStart('/').TrimEnd('/'))).OrderByDescending(x => x.OrderId).ToList();
                //fing child Parent
                var ParentUrl = item;
                if (l2childs.Count > 0)
                {
                    if (!AccessMenuList.Exists(x => x.Id == ParentUrl.Id))
                    {
                        foreach (var trash in ParentUrl.MenuChilds.ToList())
                        {
                            if (!l2childs.Exists(x => x.Id == trash.Id))
                            {
                                //همه فرزندانی که در پدر هستند به جز آیتم های همسان پاک می شوند
                               ParentUrl.MenuChilds.Remove(trash);
                            }
                        }
                       
                        AccessMenuList.Add(ParentUrl);
                    }
                  
                }
            }

            return AccessMenuList;
        }
    }
}
