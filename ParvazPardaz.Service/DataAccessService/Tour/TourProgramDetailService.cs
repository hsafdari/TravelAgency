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
using System.Web;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class TourProgramDetailService : BaseService<TourProgramDetail>, ITourProgramDetailService
    {
        #region Fields
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/CompanyTransfer/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<TourProgramDetail> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public TourProgramDetailService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourProgramDetail>();
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region CreateTourProgramDetail
        public TourProgramDetail CreateTourProgramDetail(TourProgramDetailViewModel viewModel)
        {
            var model = _mappingEngine.Map<TourProgramDetail>(viewModel);
            //if (viewModel.File.HasFile())
            //{
            //    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
            //    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
            //    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
            //    model.ImageUrl = relativePathWithFileNameOriginal;
            //    model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
            //    model.ImageFileName = viewModel.File.FileName;
            //    model.ImageSize = viewModel.File.ContentLength;
            //    if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
            //    {
            //        var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
            //    }
            //    //ذخیره فایل در سایز اصلی
            //    byte[] buffer = new byte[viewModel.File.ContentLength];
            //    viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
            //    System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
            //    //ذخیره فایل در سایز کوچک
            //    buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
            //    viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
            //    System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            //}
            model.ImageUrl = viewModel.ImageUrl;
            model.ImageExtension = viewModel.ImageExtension;
            model.ImageFileName = viewModel.ImageFileName;
            model.ImageSize = viewModel.ImageSize;

            _dbSet.Add(model);
            _unitOfWork.SaveAllChanges();
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id == model.Id);
        }
        #endregion


        #region RemoveHotelGallery
        public bool Remove(int id)
        {
            var model = base.GetById(x => x.Id == id);

            //if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl)))
            //{
            //    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl));
            //}
            //if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl.ThumbImage())))
            //{
            //    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl.ThumbImage()));
            //}
            if (base.Delete(x => x.Id == id) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
