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
    public class TourSliderService : BaseService<TourSlider>, ITourSliderService
    {
        #region Fields
        private const int SliderWidth = 420;
        private const int SliderHeight = 595;
        private const int PrimarySliderWidth = 298;
        private const int PrimarySliderHeight = 420;

        private const string RelativePath = "/Uploads/TourSlider/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly ITourService _tourService;
        private readonly IDbSet<TourSlider> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public TourSliderService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase, ITourService tourService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<TourSlider>();
            _httpContextBase = httpContextBase;
            _tourService = tourService;
        }
        #endregion

        #region UploadHotelGallery
        public TourSlider Upload(AddTourSliderViewModel viewModel)
        {
            TourSlider model = new TourSlider();
            var tour = _tourService.GetById(t => t.Id == viewModel.TourId);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.File.InputStream.ResizeImageFile(SliderWidth, SliderHeight);
                viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }

            else if (viewModel.PrimarySlider.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
                string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
                model.ImageUrl = relativePathWithFileNameOriginal;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
                model.ImageFileName = viewModel.PrimarySlider.FileName;
                model.ImageSize = viewModel.PrimarySlider.ContentLength;
                model.IsPrimarySlider = true;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.PrimarySlider.ContentLength];
                viewModel.PrimarySlider.InputStream.Read(buffer, 0, viewModel.PrimarySlider.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                buffer = viewModel.PrimarySlider.InputStream.ResizeImageFile(PrimarySliderWidth, PrimarySliderHeight);
                viewModel.PrimarySlider.InputStream.Read(buffer, 0, viewModel.PrimarySlider.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }

            _dbSet.Add(model);
            tour.TourSliders.Add(model);
            _unitOfWork.SaveAllChanges();
            return model;
        }
        #endregion

        #region Remove
        public bool Remove(int id)
        {
            var model = _dbSet.Where(x => x.Id == id).FirstOrDefault();

            if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl)))
            {
                System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl));
            }
            if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl.ThumbImage())))
            {
                System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl.ThumbImage()));
            }
            if (base.Delete(x => x.Id == id) == 1)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
