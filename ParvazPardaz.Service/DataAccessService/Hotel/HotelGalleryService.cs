using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.DataAccessService.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using System.Web;
using System.IO;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelGalleryService : BaseService<HotelGallery>, IHotelGalleryService
    {
        #region Fields
        private const string RelativePath = "/Uploads/HotelImage/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IDbSet<HotelGallery> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public HotelGalleryService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IHotelService hotelService, HttpContextBase httpContextBase)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<HotelGallery>();
            _hotelService = hotelService;
            _httpContextBase = httpContextBase;
        }
        #endregion

        //#region GetForGrid
        //public IQueryable<GridHotelGalleryViewModel> GetViewModelForGrid()
        //{
        //    return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
        //                                                  .AsNoTracking().ProjectTo<GridHotelGalleryViewModel>(_mappingEngine);
        //}
        //#endregion

        #region UpoloadGallery
        public HotelGallery UpoloadGallery(ImageSliderViewModel viewModel)
        {
            HotelGallery model = new HotelGallery();
            var hotel = _hotelService.GetById(t => t.Id == viewModel.PostId);
            if (viewModel.File.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-700x525" + System.IO.Path.GetExtension(viewModel.File.FileName);
                string relativePathWithFileNameThumb261x177 = relativePathWithFileName + "-261x177" + System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageUrl = relativePathWithFileName;
                model.Width = 700;
                model.Height = 525;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                model.ImageFileName = viewModel.File.FileName;
                model.ImageSize = viewModel.File.ContentLength;
                model.IsPrimarySlider = viewModel.IsPrimarySlider;
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File.ContentLength];
                viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                Stream strThumbBuffer = new MemoryStream(buffer);
                byte[] bufferThumb = new byte[viewModel.File.ContentLength];
                bufferThumb = strThumbBuffer.ResizeImageFileExact(261, 177);
                viewModel.File.InputStream.Read(bufferThumb, 0, viewModel.File.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb261x177), bufferThumb);
            }
            _dbSet.Add(model);
            hotel.HotelGalleries.Add(model);
            _unitOfWork.SaveAllChanges();
            return model;
        }
        #endregion

        #region RemoveGallery
        public bool RemoveGallery(int id)
        {
            var model = _unitOfWork.Set<HotelGallery>().Where(x => x.Id == id).FirstOrDefault();

            if (model != null)
            {
                var relatedHotelInJunction = model.Hotels.ToList();
                try
                {
                    if (File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-700x525" + model.ImageExtension)))
                    {
                        System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-700x525" + model.ImageExtension));
                    }
                    if (File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-261x177" + model.ImageExtension)))
                    {
                        System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-261x177" + model.ImageExtension));
                    }
                }
                catch (Exception)
                {
                }

                foreach (var h in relatedHotelInJunction)
                {
                    model.Hotels.Remove(h);
                }

                _unitOfWork.Set<HotelGallery>().Remove(model);
                _unitOfWork.SaveAllChanges(false);

                return true;
            }
            else
                return false;
        }
        #endregion

    }
}
