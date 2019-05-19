using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.Contract.Users;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class SupportService : BaseService<Support>, ISupportService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Support> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly IApplicationUserManager _userService;
        #endregion

        #region Ctor
        public SupportService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IApplicationUserManager userService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Support>();
            _userService = userService;
        }
        #endregion


        #region GetForGrid
        public IQueryable<GridSupportViewModel> GetViewModelForGrid()
        {
            //شناسه کاربر جاری
            var currentUserId = HttpContext.Current.Request.GetUserId();

            //آیا این کاربر نقش مدیر سیستمی را دارد؟
            Task<bool> isInRoleSystemAdministrator = Task.Run(async () =>
            {
                bool isInRole = await _userService.IsInRoleAsync(currentUserId.Value, "SystemAdministrator");
                return isInRole;
            });

            //اگر مدیر سیستمی است ، هم فعال و هم غیر فعال ها را ببیند
            if (isInRoleSystemAdministrator.Result)
            {
                return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                              .AsNoTracking().ProjectTo<GridSupportViewModel>(_mappingEngine);
            }
            else // در غیر این صورت فــقــط فعال ها را ببیند
            {
                return _dbSet.Where(w => w.IsDeleted == false && w.IsActive).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                              .AsNoTracking().ProjectTo<GridSupportViewModel>(_mappingEngine);
            }
        }
        #endregion
    }
}
