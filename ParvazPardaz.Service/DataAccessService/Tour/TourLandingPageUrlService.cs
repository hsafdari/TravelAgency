using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourLandingPageUrlService : BaseService<TourLandingPageUrl>, ITourLandingPageUrlService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourLandingPageUrl> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        #endregion

        #region Ctor
        public TourLandingPageUrlService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourLandingPageUrl>();
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourLandingPageUrlViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourLandingPageUrlViewModel>(_mappingEngine);
        }
        #endregion

        #region FindUrlsByCityId
        public IEnumerable<SelectListItem> FindUrlsByCityId(int? cityId, EnumLandingPageUrlType landingPageUrlType)
        {
            //&& c.LandingPageUrlType == landingPageUrlType
            var urlList = _dbSet.Where(c => c.IsAvailable && !c.IsDeleted && c.CityId == cityId ).Select(url => new SelectListItem() { Selected = false, Text = url.URL, Value = url.Id.ToString() }).AsEnumerable();
            return urlList;
        }
        #endregion

        #region FindUrlsByCityIdEditMode
        public IEnumerable<SelectListItem> FindUrlsByCityIdEditMode(int? cityId, int? previousUrlId, EnumLandingPageUrlType landingPageUrlType)
        {
            //&& c.LandingPageUrlType == landingPageUrlType
            var urlList = _dbSet.Where(c => ((c.IsAvailable && !c.IsDeleted && c.CityId == cityId) || (c.Id == previousUrlId && c.CityId == cityId)) ).Select(url => new SelectListItem() { Selected = false, Text = url.URL, Value = url.Id.ToString() }).AsEnumerable();
            return urlList;
        }
        #endregion

        #region CheckURLIN
        public bool CheckURLInLinkTable(string Url)
        {
            //var urlforbid= Name.Contains(',');
            var url = Url.Replace(" ", "-");
            var link = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url && x.IsDeleted == false).FirstOrDefault();

            if (link == null) return true;
            else return false;
        }
        #endregion
    }
}
