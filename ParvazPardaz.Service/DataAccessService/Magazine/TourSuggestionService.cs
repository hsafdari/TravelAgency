using System;
using System.Collections.Generic;
using System.Data.Entity;
using ParvazPardaz.Model.Entity.Common;
using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.DataAccessService.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Service.Contract.Magazine;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Magazine;
using System.Web;

namespace ParvazPardaz.Service.DataAccessService.Magazine
{
    public class TourSuggestionService : BaseService<TourSuggestion>, ITourSuggestionService
    {
        #region Fields
        private const int Width50 = 50;
        private const int Height50 = 50;
        private const string RelativePath = "/Uploads/TourSuggestion/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourSuggestion> _dbSet;
        private readonly IMappingEngine _mappingEngine;        
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public TourSuggestionService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourSuggestion>();
            _httpContextBase = httpContextBase;
        }

        public async Task<AddTourSuggestionViewModel> CreateAsync(AddTourSuggestionViewModel viewModel)
        {
            var model = _mappingEngine.Map<TourSuggestion>(viewModel);
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
            return await _dbSet.AsNoTracking().ProjectTo<AddTourSuggestionViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }

        public async Task<int> EditAsync(EditTourSuggestionViewModel viewModel)
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
                if (viewModel.ImageUrl!=null)
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

        public IQueryable<TourSuggestionViewModel> GetSuggestions(int id)
        {
            return _dbSet.Where(w => !w.IsDeleted && w.LocationId==id && w.ImageIsActive).AsNoTracking().OrderBy(x=>x.Priority).ProjectTo<TourSuggestionViewModel>(_mappingEngine);
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridTourSuggestionViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).OrderBy(x => x.Priority).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridTourSuggestionViewModel>(_mappingEngine);
        }
        #endregion
    }
}

