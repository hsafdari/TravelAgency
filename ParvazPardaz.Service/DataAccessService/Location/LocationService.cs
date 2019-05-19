using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Magazine;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.Contract.Location;
using System.Web.Mvc;
using ParvazPardaz.Model.Entity.Link;
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Service.DataAccessService.Location
{
    public class LocationService : BaseService<EntityNS.Location>, ILocationService
    {
        #region Fields
        private const int Width50 = 50;
        private const int Height50 = 50;
        private const string RelativePath = "/Uploads/Location/";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.Location> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public LocationService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.Location>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridLocationViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridLocationViewModel>(_mappingEngine);
        }
        #endregion

        #region GetAllLocationOfSelectListItem
        public IEnumerable<SelectListItem> GetAllLocationOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
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

        public async Task<AddLocationViewModel> CreateAsync(AddLocationViewModel viewModel)
        {
            var model = _mappingEngine.Map<EntityNS.Location>(viewModel);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageURL = relativePathWithFileNameOriginal;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFileExact(Width50, Height50);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            _dbSet.Add(model);
            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.AsNoTracking().ProjectTo<AddLocationViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }

        public async Task<int> EditAsync(EditLocationViewModel viewModel)
        {
            //حذف تصویر قبلی ؛ اگر تصویری برای آپلود انتخاب شده باشد
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(viewModel.ImageUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(viewModel.ImageUrl));
                }
                //حذف تصویر کوچک قبلی
                if (viewModel.ImageUrl != null)
                {
                    var splittedUrl = viewModel.ImageUrl.Split('.');
                    var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                    if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                    {
                        System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
                    }
                }
            }
            var model = await base.GetByIdAsync(c => c.Id == viewModel.Id);
            _mappingEngine.Map(viewModel, model);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageURL = relativePathWithFileNameOriginal;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFileExact(Width50, Height50);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }

            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion
    }
}
