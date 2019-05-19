using AutoMapper;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ParvazPardaz.Service.Contract.Tour;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Country
{
    public class CityService : BaseService<City>, ICityService
    {
        #region Fields
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/City/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<City> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public CityService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<City>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridCityViewModel> GetViewModelForGrid()
        {
            var x = _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser).AsNoTracking().ToList();
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridCityViewModel>(_mappingEngine);
        }
        #endregion

        #region Get All City For DDL
        public IEnumerable<SelectListItem> GetAllCityOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }
        public IEnumerable<SelectListItem> GetAllCityOfSelectListItem(int modelCityId)
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = (x.Id == modelCityId ? true : false), Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }
        #endregion

        #region Create
        public async Task<AddCityViewModel> CreateAsync(AddCityViewModel viewModel)
        {
            var model = _mappingEngine.Map<City>(viewModel);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            _dbSet.Add(model);
            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.AsNoTracking().ProjectTo<AddCityViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditCityViewModel viewModel)
        {
            var model = await base.GetByIdAsync(c => c.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, model);
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl));
                }
                //حذف تصویر کوچک قبلی
                var splittedUrl = model.ImageUrl.Split('.');
                var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
                }
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion

        #region ShowCityNameWithCountry
        public string ShowCityNameWithCountry(int cityId)
        {
            var city = _dbSet.Where(c => c.Id == cityId && c.IsDeleted == false).AsNoTracking().FirstOrDefault();
            return city != null ? city.Title.ToUpper() + " - " + city.State.Country.Title.ToUpper() : null;
        }
        #endregion

        #region ShowCityNameWithCountryAndState
        public string ShowCityNameWithCountryAndState(int cityId)
        {
            //var city = base.GetById(c => c.Id == cityId);
            var city = _dbSet.Where(c => c.Id == cityId && c.IsDeleted == false).AsNoTracking().FirstOrDefault();
            return city != null ? city.Title.ToUpper() + " - " + city.State.Title.ToUpper() + " - " + city.State.Country.Title.ToUpper() : null;
        }
        #endregion

        #region GetCityName
        public string GetCityName(int cityId)
        {
            //var city = base.GetById(c => c.Id == cityId);
            var city = _dbSet.Where(c => c.Id == cityId && c.IsDeleted == false).AsNoTracking().FirstOrDefault();
            return city != null ? city.Title.ToUpper() : null;
        }
        #endregion

        #region CheckIsUniqueTitleInCity
        public bool CheckIsUniqueTitleInCity(string title)
        {
            var city = _unitOfWork.Set<City>().Where(x => x.Title == title && !x.IsDeleted).FirstOrDefault();

            if (city == null) return true;
            else return false;
        }
        #endregion

        #region CheckIsUniqueENTitleInCity
        public bool CheckIsUniqueENTitleInCity(string entitle)
        {
            var city = _unitOfWork.Set<City>().Where(x => x.ENTitle == entitle && !x.IsDeleted).FirstOrDefault();

            if (city == null) return true;
            else return false;
        }
        #endregion


        public IEnumerable<SelectListItem> GetAllFromNationalDDL()
        {
            return _dbSet.Where(a => a.IsDeleted == false && a.IsDddlFrom && a.State.Country.ENTitle == "Iran" && a.ENTitle == "Tehran").Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetAllDestinationNationalDDL()
        {
            return _dbSet.Where(a => a.IsDeleted == false && a.IsDddlDestination && a.State.Country.ENTitle != "Iran").Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }


        public IEnumerable<SelectListItem> GetAllFromDomesticDDL()
        {
            return _dbSet.Where(a => a.IsDeleted == false && a.IsDddlFrom && a.State.Country.ENTitle == "Iran").Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetAllDestinationDomesticDDL()
        {
            return _dbSet.Where(a => a.IsDeleted == false && a.IsDddlDestination && a.State.Country.ENTitle == "Iran").Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        /// <summary>
        /// تمام شهرهای مبدا فعال برای جستجو
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAvailableFromCitiesDDL()
        {
            return _dbSet.Where(a => !a.IsDeleted && a.IsDddlFrom).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        /// <summary>
        /// تمام شهرهای مقصد فعال برای جستجو
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAvailableDestCitiesDDL()
        {
            return _dbSet.Where(a => !a.IsDeleted && a.IsDddlDestination).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetAvailableFromCitiesDDL(int id)
        {
            return _dbSet.Where(a => !a.IsDeleted && a.IsDddlFrom).Select(x => new SelectListItem() { Selected = id == x.Id, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetAvailableDestCitiesDDL(int id)
        {
            return _dbSet.Where(a => !a.IsDeleted && a.IsDddlDestination).Select(x => new SelectListItem() { Selected = id == x.Id, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
        }

        public string GetCityNameWithCountryEN(int cityId)
        {
            var city = _dbSet.Where(c => c.Id == cityId && c.IsDeleted == false).AsNoTracking().FirstOrDefault();
            return city != null ? city.State.Country.ENTitle+"-"+city.ENTitle : null;
        }
    }
}
