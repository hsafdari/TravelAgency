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
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Service.DataAccessService.Post;
using System.IO;

namespace ParvazPardaz.Service.DataAccessService.Tour
{
    public class PostImageService : BaseService<PostImage>, IPostImageService
    {
        #region Fields
        private const int SliderWidth = 420;
        private const int SliderHeight = 595;
        private const int PrimarySliderWidth = 298;
        private const int PrimarySliderHeight = 420;
        private const int FirstPagePrimarySliderWidth = 315;
        private const int FirstPagePrimarySliderHeight = 60;

        private const string RelativePath = "/Uploads/PostImage/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostService _postService;
        private readonly IDbSet<PostImage> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        #endregion

        #region Ctor
        public PostImageService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase, PostService postService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<PostImage>();
            _httpContextBase = httpContextBase;
            _postService = postService;
        }
        #endregion

        #region Upload
        public PostImage Upload(AddPostImageViewModel viewModel)
        {
            PostImage model = new PostImage();
            var post = _postService.GetById(t => t.Id == viewModel.PostId);
            if (viewModel.File765.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-765x535" + System.IO.Path.GetExtension(viewModel.File765.FileName);
                string relativePathWithFileNameThumb736 = relativePathWithFileName + "-736x512" + System.IO.Path.GetExtension(viewModel.File765.FileName);
                //string relativePathWithFileNameThumb = relativePathWithFileName + "_765-535" + System.IO.Path.GetExtension(viewModel.File765.FileName);
                model.ImageUrl = relativePathWithFileName;
                model.Width = 765;
                model.Height = 535;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File765.FileName);
                model.ImageFileName = viewModel.File765.FileName;
                model.ImageSize = viewModel.File765.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File765.ContentLength];
                viewModel.File765.InputStream.Read(buffer, 0, viewModel.File765.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                Stream Img736 = new MemoryStream(buffer);
                byte[] buffer736 = new byte[viewModel.File765.ContentLength];
                buffer736 = Img736.ResizeImageFileExact(736, 512);
                viewModel.File765.InputStream.Read(buffer736, 0, viewModel.File765.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb736), buffer736);
            }
            if (viewModel.File575.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-575x530" + System.IO.Path.GetExtension(viewModel.File575.FileName);
                //string relativePathWithFileNameThumb = relativePathWithFileName + "_575-530" + System.IO.Path.GetExtension(viewModel.File575.FileName);

                string relativePathWithFileNameThumb271 = relativePathWithFileName + "-271x248" + System.IO.Path.GetExtension(viewModel.File575.FileName);
                string relativePathWithFileNameThumb565 = relativePathWithFileName + "-565x521" + System.IO.Path.GetExtension(viewModel.File575.FileName);


                model.ImageUrl = relativePathWithFileName;
                model.Width = 575;
                model.Height = 530;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File575.FileName);
                model.ImageFileName = viewModel.File575.FileName;
                model.ImageSize = viewModel.File575.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File575.ContentLength];
                viewModel.File575.InputStream.Read(buffer, 0, viewModel.File575.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);

                //ذخیره فایل در سایز کوچک
                Stream Img271 = new MemoryStream(buffer);
                byte[] buffer271 = new byte[viewModel.File575.ContentLength];
                buffer271 = Img271.ResizeImageFileExact(271, 248);
                viewModel.File575.InputStream.Read(buffer271, 0, viewModel.File575.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb271), buffer271);


                Stream Img565 = new MemoryStream(buffer);
                byte[] buffer565 = new byte[viewModel.File575.ContentLength];
                buffer565 = Img565.ResizeImageFileExact(565, 521);
                viewModel.File575.InputStream.Read(buffer565, 0, viewModel.File575.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb565), buffer565);


            }
            if (viewModel.File370.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-370x292" + System.IO.Path.GetExtension(viewModel.File370.FileName);
                //string relativePathWithFileNameThumb = relativePathWithFileName + "_370-292" + System.IO.Path.GetExtension(viewModel.File370.FileName);
                model.ImageUrl = relativePathWithFileName;
                model.Width = 370;
                model.Height = 292;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File370.FileName);
                model.ImageFileName = viewModel.File370.FileName;
                model.ImageSize = viewModel.File370.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File370.ContentLength];
                viewModel.File370.InputStream.Read(buffer, 0, viewModel.File370.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                //buffer = viewModel.File765.InputStream.ResizeImageFile(SliderWidth, SliderHeight);
                //viewModel.File765.InputStream.Read(buffer, 0, viewModel.File765.ContentLength);
                //System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            if (viewModel.File277.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-277x186" + System.IO.Path.GetExtension(viewModel.File277.FileName);
                //string relativePathWithFileNameThumb = relativePathWithFileName + "_277-186" + System.IO.Path.GetExtension(viewModel.File277.FileName);
                model.ImageUrl = relativePathWithFileName;
                model.Width = 277;
                model.Height = 186;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File277.FileName);
                model.ImageFileName = viewModel.File277.FileName;
                model.ImageSize = viewModel.File277.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File277.ContentLength];
                viewModel.File277.InputStream.Read(buffer, 0, viewModel.File277.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                //buffer = viewModel.File765.InputStream.ResizeImageFile(SliderWidth, SliderHeight);
                //viewModel.File765.InputStream.Read(buffer, 0, viewModel.File765.ContentLength);
                //System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            }
            if (viewModel.File98.HasFile())
            {
                string relativePathWithFileName = RelativePath + System.Guid.NewGuid().ToString().Replace("-", "");
                string relativePathWithFileNameOriginal = relativePathWithFileName + "-98x98" + System.IO.Path.GetExtension(viewModel.File98.FileName);
                //string relativePathWithFileNameThumb = relativePathWithFileName + "_98-98" + System.IO.Path.GetExtension(viewModel.File98.FileName);

                string relativePathWithFileNameThumb65 = relativePathWithFileName + "-65x65" + System.IO.Path.GetExtension(viewModel.File98.FileName);
                string relativePathWithFileNameThumb83 = relativePathWithFileName + "-83x83" + System.IO.Path.GetExtension(viewModel.File98.FileName);
                string relativePathWithFileNameThumb35 = relativePathWithFileName + "-35x31" + System.IO.Path.GetExtension(viewModel.File98.FileName);

                model.ImageUrl = relativePathWithFileName;
                model.Width = 98;
                model.Height = 98;
                model.ImageExtension = System.IO.Path.GetExtension(viewModel.File98.FileName);
                model.ImageFileName = viewModel.File98.FileName;
                model.ImageSize = viewModel.File98.ContentLength;
                model.IsPrimarySlider = false;
                if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                {
                    var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                }
                //ذخیره فایل در سایز اصلی
                byte[] buffer = new byte[viewModel.File98.ContentLength];
                viewModel.File98.InputStream.Read(buffer, 0, viewModel.File98.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                //ذخیره فایل در سایز کوچک
                Stream Img65 = new MemoryStream(buffer);
                byte[] buffer65 = new byte[viewModel.File98.ContentLength];
                buffer65 = Img65.ResizeImageFileExact(65, 65);
                viewModel.File98.InputStream.Read(buffer65, 0, viewModel.File98.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb65), buffer65);


                Stream Img83 = new MemoryStream(buffer);
                byte[] buffer83 = new byte[viewModel.File98.ContentLength];
                buffer83 = Img83.ResizeImageFileExact(83, 83);
                viewModel.File98.InputStream.Read(buffer83, 0, viewModel.File98.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb83), buffer83);

                Stream Img35 = new MemoryStream(buffer);
                byte[] buffer35 = new byte[viewModel.File98.ContentLength];
                buffer35 = Img35.ResizeImageFileExact(35, 31);
                viewModel.File98.InputStream.Read(buffer35, 0, viewModel.File98.ContentLength);
                System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb35), buffer35);
            }

            //else if (viewModel.PrimarySlider.HasFile())
            //{
            //    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
            //    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
            //    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
            //    string relativePathWithFileNameTour = relativePathWithFileName + "_tour" + System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
            //    model.ImageUrl = relativePathWithFileNameOriginal;
            //    model.ImageExtension = System.IO.Path.GetExtension(viewModel.PrimarySlider.FileName);
            //    model.ImageFileName = viewModel.PrimarySlider.FileName;
            //    model.ImageSize = viewModel.PrimarySlider.ContentLength;
            //    model.IsPrimarySlider = true;
            //    if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
            //    {
            //        var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
            //    }
            //    //ذخیره فایل در سایز اصلی
            //    byte[] buffer = new byte[viewModel.PrimarySlider.ContentLength];
            //    viewModel.PrimarySlider.InputStream.Read(buffer, 0, viewModel.PrimarySlider.ContentLength);
            //    System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
            //    //ذخیره فایل در سایز کوچک
            //    buffer = viewModel.PrimarySlider.InputStream.ResizeImageFile(FirstPagePrimarySliderWidth, FirstPagePrimarySliderHeight);
            //    viewModel.PrimarySlider.InputStream.Read(buffer, 0, viewModel.PrimarySlider.ContentLength);
            //    System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
            //    ////ذخیره فایل در سایز کوچک صفحه اول
            //    //buffer = viewModel.PrimarySlider.InputStream.ResizeImageFile(FirstPagePrimarySliderWidth, FirstPagePrimarySliderHeight);
            //    //viewModel.PrimarySlider.InputStream.Read(buffer, 0, viewModel.PrimarySlider.ContentLength);
            //    //System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameTour), buffer);
            //}

            _dbSet.Add(model);
            post.PostImages.Add(model);
            _unitOfWork.SaveAllChanges();
            return model;
        }
        #endregion

        #region Remove
        public bool Remove(int id)
        {
            var model = _dbSet.Where(x => x.Id == id).FirstOrDefault();

            if (model.Width == 765)
            {
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-736x512" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-736x512" + model.ImageExtension));
                }
            }
            if (model.Width == 575)
            {
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-271x248" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-271x248" + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-565x521" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-565x521" + model.ImageExtension));
                }


            }
            if (model.Width == 370)
            {
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension));
                }
            }
            if (model.Width == 277)
            {
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension));
                }
            }
            if (model.Width == 98)
            {
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-" + model.Width + "x" + model.Height + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-65x65" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-65x65" + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-83x83" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-83x83" + model.ImageExtension));
                }
                if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.ImageUrl + "-35x31" + model.ImageExtension)))
                {
                    System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-35x31" + model.ImageExtension));
                }


            }

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

        public PostImage UpoloadGallery(ImageSliderViewModel viewModel)
        {
            PostImage model = new PostImage();
            var post = _postService.GetById(t => t.Id == viewModel.PostId);
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
            post.PostImages.Add(model);
            _unitOfWork.SaveAllChanges();
            return model;
        }



        public bool RemoveGallery(int id)
        {
            var model = _dbSet.Where(x => x.Id == id).FirstOrDefault();

            if (model.Width == 700)
            {
                System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-700x525" + model.ImageExtension));
                System.IO.File.Delete(_httpContextBase.Server.MapPath(model.ImageUrl + "-261x177" + model.ImageExtension));
                return true;
            }
            else
                return false;
        }
    }
}
