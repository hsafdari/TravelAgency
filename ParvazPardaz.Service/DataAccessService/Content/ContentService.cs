using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Content;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EntityNS = ParvazPardaz.Model.Entity.Content;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Service.DataAccessService.Content
{
    public class ContentService : BaseService<EntityNS.Content>, IContentService
    {
        #region Fields
        private const int Width = 98;//447;
        private const int Height = 98;//342;

        private const string RelativePath = "/Uploads/ContentImage/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<EntityNS.Content> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public ContentService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase, ICacheService cacheService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<EntityNS.Content>();
            _httpContextBase = httpContextBase;
            _cacheService = cacheService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridContentViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.ContentGroup).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridContentViewModel>(_mappingEngine);
        }
        #endregion

        #region Create
        public async Task<AddContentViewModel> CreateAsync(AddContentViewModel viewModel)
        {
            var model = _mappingEngine.Map<EntityNS.Content>(viewModel);
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
                buffer = viewModel.File.InputStream.ResizeImageFile(Width, Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            _dbSet.Add(model);

            #region UnAvalilable Selected TourLandingPageUrl
            var landingPageUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == viewModel.TourLandingPageUrlId);
            if (landingPageUrl != null)
            {
                landingPageUrl.IsAvailable = false;
            }
            #endregion

            await _unitOfWork.SaveAllChangesAsync();

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _dbSet.AsNoTracking().ProjectTo<AddContentViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditContentViewModel viewModel)
        {
            var model = await base.GetByIdAsync(c => c.Id == viewModel.Id);

            #region Update previous and new tour's landing page url
            //فعال کردن آدرس قبلی که برای این تور انتخاب شده بوده(در صورت وجود) ، برای اینکه برای تور دیگر قابل استفاده باشد
            var prevoiusTourLandPUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == model.TourLandingPageUrlId.Value);
            if (prevoiusTourLandPUrl != null)
            {
                prevoiusTourLandPUrl.IsAvailable = true;
            }

            //اگه تور فعال باشه ، به طور قطع باید براش آدرسی انتخاب شده باشه
            if (viewModel.IsActive && viewModel.TourLandingPageUrlId != null && viewModel.TourLandingPageUrlId > 0)
            {
                //غیر فعال کردن آدرس تور انتخاب شده برای این تور
                var selectedTourLandPUrl = _unitOfWork.Set<TourLandingPageUrl>().FirstOrDefault(x => x.Id == viewModel.TourLandingPageUrlId.Value);
                if (selectedTourLandPUrl != null)
                {
                    selectedTourLandPUrl.IsAvailable = false;
                }
            }
            #endregion

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
                buffer = viewModel.File.InputStream.ResizeImageFile(Width, Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }

            //مقداردهی فایل مرتبط با کش صفحه اول
            _cacheService.HomeCacheFileSetCurrentTime();

            return await _unitOfWork.SaveAllChangesAsync();
        }
        #endregion
    }
}
