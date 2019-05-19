using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class SliderService : BaseService<Slider>, ISliderService
    {
        #region Fields
        private const int Width50 = 50;
        private const int Height50 = 50;
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/Slider/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Slider> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public SliderService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase, ICacheService cacheService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<Slider>();
            _httpContextBase = httpContextBase;
            _cacheService = cacheService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridSliderViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridSliderViewModel>(_mappingEngine);
        }
        #endregion

        #region Create
        public async Task<AddSliderViewModel> CreateAsync(AddSliderViewModel viewModel)
        {
            var model = _mappingEngine.Map<Slider>(viewModel);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageURL = relativePathWithFileNameOriginal;
                //model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                //model.ImageFileName = viewModel.File.FileName;
                //model.ImageSize = viewModel.File.ContentLength;
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

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _dbSet.AsNoTracking().ProjectTo<AddSliderViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditSliderViewModel viewModel)
        {
            //حذف تصویر قبلی ؛ اگر تصویری برای آپلود انتخاب شده باشد
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(viewModel.ImageURL)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(viewModel.ImageURL));
                }
                //حذف تصویر کوچک قبلی
                var splittedUrl = viewModel.ImageURL.Split('.');
                var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
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
                //model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                //model.ImageFileName = viewModel.File.FileName;
                //model.ImageSize = viewModel.File.ContentLength;
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

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion


        public IQueryable<GridTourHomeSliderViewModel> GetViewModelForGridHome()
        {
            return _dbSet.Where(w => w.IsDeleted == false && (w.SliderGroup.Name == "HomeSlider" || w.SliderGroup.Name == "HomeSlider2")).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourHomeSliderViewModel>(_mappingEngine);
        }

        public async Task<AddTourHomeSliderViewModel> CreateAsyncHome(AddTourHomeSliderViewModel viewModel)
        {
            var model = _mappingEngine.Map<Slider>(viewModel);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageURL = relativePathWithFileNameOriginal;
                //model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                //model.ImageFileName = viewModel.File.FileName;
                //model.ImageSize = viewModel.File.ContentLength;
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

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _dbSet.AsNoTracking().ProjectTo<AddTourHomeSliderViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }

        public async Task<int> EditAsyncHome(EditTourHomeSliderViewModel viewModel)
        {
            //حذف تصویر قبلی ؛ اگر تصویری برای آپلود انتخاب شده باشد
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(viewModel.ImageURL)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(viewModel.ImageURL));
                }
                //حذف تصویر کوچک قبلی
                var splittedUrl = viewModel.ImageURL.Split('.');
                var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
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
                //model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                //model.ImageFileName = viewModel.File.FileName;
                //model.ImageSize = viewModel.File.ContentLength;
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

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _unitOfWork.SaveAllChangesAsync();
        }


        public IQueryable<GridTourHomeSliderViewModel> GetViewModelForGridTourLanding()
        {
            return _dbSet.Where(w => w.IsDeleted == false && w.SliderGroup.Name == "TourLanding").Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourHomeSliderViewModel>(_mappingEngine);
        }
    }
}
