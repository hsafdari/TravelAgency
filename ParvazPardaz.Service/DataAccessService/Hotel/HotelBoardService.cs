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
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Hotel;
using System.Web.Mvc;
using Entity = ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Common.Extension;
using System.Web;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelBoardService : BaseService<HotelBoard>, IHotelBoardService
    {
        #region Fields
        private const int A5Width = 100;
        private const int A5Height = 100;
        private const int A6Width = 50;
        private const int A6Height = 50;

        private const string RelativePath = "/Uploads/HotelBoard/";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<HotelBoard> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public HotelBoardService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine,HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<HotelBoard>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridHotelBoardViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => !w.IsDeleted).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridHotelBoardViewModel>(_mappingEngine);
        }
        #endregion

        #region GetDDL
        public SelectList GetDDL()
        {
            return new SelectList(_dbSet.Where(x => !x.IsDeleted && x.IsActive).ToList(), "Id", "Name");
        }
        #endregion
        #region Create
        public async Task<AddHotelBoardViewModel> CreateAsync(AddHotelBoardViewModel viewModel)
        {
            var model = _mappingEngine.Map<Entity.HotelBoard>(viewModel);
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
                //buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            _dbSet.Add(model);
            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.AsNoTracking().ProjectTo<AddHotelBoardViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion


        public async Task<int> EditAsync(EditHotelBoardViewModel viewModel)
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
                //buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
                await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            return await _unitOfWork.SaveAllChangesAsync();
        }
    }
}

