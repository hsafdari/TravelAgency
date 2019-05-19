using AutoMapper;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entity = ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Common.Extension;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Country
{
    public class StateService : BaseService<Entity.State>, IStateService
    {
        #region Fields
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/State/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Entity.State> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public StateService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Entity.State>();
            _httpContextBase = httpContextBase;

        }
        #endregion

        #region GetForGrid
        public IQueryable<GridStateViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridStateViewModel>(_mappingEngine);
        }
        #endregion

        #region GetStatesByCountryID (Cascading DDL)
        public IEnumerable<SelectListItem> FindStatesByCountryId(int? countryId)
        {
            var stateList = _dbSet.Where(c => c.IsDeleted == false && c.CountryId == countryId).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return stateList;
        }
        #endregion

        #region FindCitiesByCountryId (Cascading DDL)
        public IEnumerable<SelectListItem> FindCitiesByCountryId(int? countryId)
        {
            var cityList = _dbSet.Where(c => c.IsDeleted == false && c.CountryId == countryId).SelectMany(state => state.Cities.Where(c => !c.IsDeleted).ToList()).Distinct().Select(city => new SelectListItem() { Selected = false, Text = city.Title, Value = city.Id.ToString() }).AsEnumerable();
            return cityList;
        }
        #endregion

        #region Create
        public async Task<AddStateViewModel> CreateAsync(AddStateViewModel viewModel)
        {
            var model = _mappingEngine.Map<Entity.State>(viewModel);
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
            return await _dbSet.AsNoTracking().ProjectTo<AddStateViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditStateViewModel viewModel)
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

        #region CheckIsUniqueTitleInState
        public bool CheckIsUniqueTitleInState(string title)
        {
            var State = _unitOfWork.Set<Entity.State>().Where(x => x.Title == title && !x.IsDeleted).FirstOrDefault();

            if (State == null) return true;
            else return false;
        }
        #endregion
    }
}
