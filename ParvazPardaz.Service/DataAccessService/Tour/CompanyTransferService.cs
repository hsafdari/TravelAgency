using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using ParvazPardaz.Common.Extension;
using System.Web;
using System.Web.Mvc;


namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class CompanyTransferService : BaseService<CompanyTransfer>, ICompanyTransferService
    {
        #region Fields
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/CompanyTransfer/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<CompanyTransfer> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public CompanyTransferService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<CompanyTransfer>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridCompanyTransferViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridCompanyTransferViewModel>(_mappingEngine);
        }
        #endregion

        #region Get All Company Transfer For DDL
        public IEnumerable<SelectListItem> GetAllCompanyTransferOfSelectListItem()
        {
            return _dbSet.Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();

        }
        #endregion

        #region Create
        public async Task<AddCompanyTransferViewModel> CreateAsync(AddCompanyTransferViewModel viewModel)
        {
            var model = _mappingEngine.Map<CompanyTransfer>(viewModel);
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
            return await _dbSet.AsNoTracking().ProjectTo<AddCompanyTransferViewModel>(_mappingEngine).FirstOrDefaultAsync();
        }
        #endregion

        #region Edit
        public async Task<int> EditAsync(EditCompanyTransferViewModel viewModel)
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
    }
}
