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
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Common.Extension;
using System.Web;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourPackageService : BaseService<TourPackage>, ITourPackageService
    {
        #region Fields
        private const int Width50 = 50;
        private const int Height50 = 50;
        private const string RelativePath = "/Uploads/TourPackage/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourPackage> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;

        #endregion

        #region Ctor
        public TourPackageService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, ITourCategoryService tourCategoryService, ITourLevelService tourLevelService
                            , ITourTypeService tourTypeService, IAllowedBannedService allowedBannedService, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourPackage>();
            _httpContextBase = httpContextBase;

        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourPackageViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourPackageViewModel>(_mappingEngine);
        }
        #endregion

        #region GetUsersSeller
        public IEnumerable<SelectListItem> GetUsersSeller()
        {
            return _unitOfWork.Set<ParvazPardaz.Model.Entity.Users.User>().Where(x => x.IsDeleted).Select(a => new SelectListItem() { Text = a.FullName, Value = a.Id.ToString() }).AsEnumerable();//&& x.Roles.Any(z=>z.RoleId==5)

        }
        #endregion

        #region FindTourPackageByTourId
        public SelectList FindTourPackageByTourId(int? tourId)
        {
            var DDL = new SelectList(_dbSet.Where(x => !x.IsDeleted && x.TourId == tourId).Select(s => new { Title = s.Title + " ••• " + ParvazPardaz.Resource.Tour.Tours.DateTitle + ": " + s.DateTitle, Value = s.Id.ToString() }), "Value", "Title");
            return DDL;
        }

        public async Task<int> EditAsync(EditTourPackageViewModel viewModel)
        {         //حذف تصویر قبلی ؛ اگر تصویری برای آپلود انتخاب شده باشد
            if (viewModel.File.HasFile())
            {
                //حذف تصویر بزرگ قبلی
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(viewModel.ImageURL)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(viewModel.ImageURL));
                }
                //حذف تصویر کوچک قبلی
                if (viewModel.ImageURL != null)
                {
                    var splittedUrl = viewModel.ImageURL.Split('.');
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
