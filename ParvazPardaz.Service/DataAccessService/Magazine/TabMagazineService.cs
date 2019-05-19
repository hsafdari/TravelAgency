using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel.Magazine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Core;

namespace ParvazPardaz.Service.DataAccessService.Magazine
{
    public class TabMagazineService : BaseService<TabMagazine>, ITabMagazineService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TabMagazine> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public TabMagazineService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, ICacheService cacheService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TabMagazine>();
            _cacheService = cacheService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTabMagazineViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).OrderBy(w => w.Priority).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTabMagazineViewModel>(_mappingEngine);
        }
        #endregion

        #region CreateTabMagAsync
        public async Task<bool> CreateTabMagAsync(AddTabMagazineViewModel addTabMagazineViewModel)
        {

            var model = _mappingEngine.Map<TabMagazine>(addTabMagazineViewModel);

            //افزودن گروه ها
            if (addTabMagazineViewModel.SelectedGroups != null && addTabMagazineViewModel.SelectedGroups.Any())
            {
                foreach (var selectedId in addTabMagazineViewModel.SelectedGroups)
                {
                    var groupInDb = _unitOfWork.Set<PostGroup>().FirstOrDefault(x => x.Id == selectedId);

                    if (groupInDb != null) model.Groups.Add(groupInDb);
                }
            }

            _dbSet.Add(model);

            var result = await _unitOfWork.SaveAllChangesAsync();

            if (result > 0)
            {
                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region UpdateTabMagAsync
        public async Task<bool> UpdateTabMagAsync(EditTabMagazineViewModel editTabMagazineViewModel)
        {
            var model = _unitOfWork.Set<TabMagazine>().Find(editTabMagazineViewModel.Id);
            var oldGroups = model.Groups;

            //حذف گروه های قبلی
            if (model.Groups != null && model.Groups.Any())
            {
                foreach (var group in oldGroups.ToList())
                {
                    model.Groups.Remove(group);
                }
            }
            _mappingEngine.Map(editTabMagazineViewModel, model);


            //افزودن گروه ها
            if (editTabMagazineViewModel.SelectedGroups != null && editTabMagazineViewModel.SelectedGroups.Any())
            {
                foreach (var selectedId in editTabMagazineViewModel.SelectedGroups)
                {
                    var groupInDb = _unitOfWork.Set<PostGroup>().FirstOrDefault(x => x.Id == selectedId);

                    if (groupInDb != null) model.Groups.Add(groupInDb);
                }
            }

            var result = await _unitOfWork.SaveAllChangesAsync();

            if (result > 0)
            {
                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return true;
            }
            else { return false; }
        }
        #endregion
    }
}
