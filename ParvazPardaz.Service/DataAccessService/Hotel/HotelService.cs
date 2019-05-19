using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.DataAccessService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Contract.Tour;
using ParvazPardaz.DataAccess.Infrastructure;
using System.Data.Entity;
using AutoMapper;
using ParvazPardaz.ViewModel;
using AutoMapper.QueryableExtensions;
using System.Web.Mvc;
using System.Web;
using RefactorThis.GraphDiff;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Entity.Post;
using System.IO;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;

namespace ParvazPardaz.Service.DataAccessService.Hotel
{
    public class HotelService : BaseService<ParvazPardaz.Model.Entity.Hotel.Hotel>, IHotelService
    {

        #region Fields
        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;

        private const string RelativePath = "/Uploads/HotelGallery/";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<ParvazPardaz.Model.Entity.Hotel.Hotel> _dbSet;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;
        //private readonly IHotelGalleryService _hotelGalleryService;
        private readonly ICityService _cityService;
        private readonly ITourService _tourService;
        private readonly IHotelFacilityService _hotelFacilityService;

        #endregion

        #region Ctor
        //IHotelGalleryService hotelGalleryService,
        public HotelService(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, HttpContextBase httpContextBase,
                             ICityService cityService, ITourService tourService, IHotelFacilityService hotelFacilityService)
            : base(unitOfWork, mappingEngine)
        {
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _dbSet = _unitOfWork.Set<ParvazPardaz.Model.Entity.Hotel.Hotel>();
            _httpContextBase = httpContextBase;
            //_hotelGalleryService = hotelGalleryService;
            _cityService = cityService;
            _tourService = tourService;
            _hotelFacilityService = hotelFacilityService;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridHotelViewModel> GetViewModelForGrid()
        {
            return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser).Include(i => i.HotelRank)
                                                          .AsNoTracking().ProjectTo<GridHotelViewModel>(_mappingEngine);

        }

        public IQueryable<GridHotelViewModel> GetViewModelForGrid(string username)
        {
            if (username != "")
            {
                //int _userId = System.Convert.ToInt32(username);
                return _dbSet.Where(w => w.IsDeleted == false && w.CreatorUser.UserName == username).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                          .AsNoTracking().ProjectTo<GridHotelViewModel>(_mappingEngine);
            }
            else
                return _dbSet.Where(w => w.IsDeleted == false).Include(i => i.CreatorUser).Include(i => i.ModifierUser)
                                                            .AsNoTracking().ProjectTo<GridHotelViewModel>(_mappingEngine);
            //return _dbSet.Join(_unitOfWork.Set<LinkTable>(), p => p.Id, l => l.typeId, (p, l) => new { p, l })
            //       .Where(w => w.p.IsDeleted == false).Include(x=>x.l.URL).Include(i => i.p.CreatorUser).Include(i => i.p.ModifierUser)
            //                                             .AsNoTracking().ProjectTo<GridPostViewModel>(_mappingEngine);
        }
        #endregion

        #region GetTagsForDDL
        public IEnumerable<SelectListItem> GetTagsForDDL()
        {
            return _unitOfWork.Set<Tag>().Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Name, Value = x.Name }).AsEnumerable();
        }
        #endregion

        #region CreateAsync
        public async Task<AddHotelViewModel> CreateAsync(AddHotelViewModel viewModel)
        {
            var model = _mappingEngine.Map<ParvazPardaz.Model.Entity.Hotel.Hotel>(viewModel);

            #region HotelFacilities
            if (viewModel.HotelFacility != null && viewModel.HotelFacility.Any())
            {
                var listSelectedFacility = _unitOfWork.Set<HotelFacility>().Where(r => viewModel.HotelFacility.Any(sr => sr == r.Id)).ToList();
                List<HotelFacility> facilities = new List<Model.Entity.Hotel.HotelFacility>();
                foreach (var item in listSelectedFacility)
                {
                    facilities.Add(item);
                }
                model.HotelFacilities = facilities;
            }
            #endregion

            #region Saving Hotel's Group
            PostGroup _postGroup = new PostGroup();
            model.PostGroups = new List<PostGroup>();
            if (viewModel._selectedPostGroups == null)
            {
                PostGroup postGroup = _unitOfWork.Set<PostGroup>().Where(x => x.Name == "دسته بندی نشده").FirstOrDefault();
                model.PostGroups.Add(postGroup);
            }
            else
            {
                foreach (var item in viewModel._selectedPostGroups)
                {
                    _postGroup = _unitOfWork.Set<PostGroup>().Find(item);
                    model.PostGroups.Add(_postGroup);
                }
            }
            #endregion

            #region Saving Tags
            if (viewModel.TagTitles != null && viewModel.TagTitles.Any())
            {
                //model.Tags = new List<ProductTagProduct>();
                List<Tag> tagsInDb = new List<Tag>();
                foreach (var tagTitle in viewModel.TagTitles.ToList())
                {
                    model.Tags = new List<Tag>();
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name == tagTitle);
                    if (tagInDB == null)
                    {
                        var newTag = new Tag() { Name = tagTitle };

                        //save tag in productTag table
                        _unitOfWork.Set<Tag>().Add(newTag);


                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = insertedTag.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(newTag);
                        //model.Tags.Add(newTag);
                    }
                    else
                    {
                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = tagInDB.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(tagInDB);
                        //model.Tags.Add(tagInDB);
                    }
                }
                model.Tags = tagsInDb;

            }
            #endregion

            _dbSet.Add(model);
            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.AsNoTracking().ProjectTo<AddHotelViewModel>(_mappingEngine).Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        }

        #endregion

        //#region UploadHotelGallery
        //public async Task<HotelGalleryViewModel> UploadHotelGallery(HotelGalleryViewModel viewModel)
        //{
        //    //AddHotelViewModel hotelviewModel = new AddHotelViewModel();
        //    //var model = _mappingEngine.Map<HotelGallery>(viewModel);
        //    //var hotelModel = await base.GetByIdAsync(c => c.Id == viewModel.HotelID);
        //    //_mappingEngine.Map(hotelviewModel, hotelModel);

        //    HotelGallery model = new HotelGallery();
        //    var hotelModel = base.GetById(c => c.Id == viewModel.HotelID);
        //    if (viewModel.File.HasFile())
        //    {
        //        string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
        //        string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
        //        string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);
        //        model.ImageUrl = relativePathWithFileNameOriginal;
        //        model.ImageExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
        //        model.ImageFileName = viewModel.File.FileName;
        //        model.ImageSize = viewModel.File.ContentLength;
        //        if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
        //        {
        //            var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
        //        }
        //        //ذخیره فایل در سایز اصلی
        //        byte[] buffer = new byte[viewModel.File.ContentLength];
        //        viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
        //        System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
        //        //ذخیره فایل در سایز کوچک
        //        buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
        //        viewModel.File.InputStream.Read(buffer, 0, viewModel.File.ContentLength);
        //        System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
        //    }

        //    _unitOfWork.Set<HotelGallery>().Add(model);

        //    hotelModel.HotelGalleries.Add(model);
        //    // _dbSet.Add(hotelModel);
        //    _unitOfWork.SaveAllChanges();
        //    return await _unitOfWork.Set<HotelGallery>().AsNoTracking().ProjectTo<HotelGalleryViewModel>(_mappingEngine).Where(x => x.Id == model.Id).FirstOrDefaultAsync();
        //}
        //#endregion

        //#region RemoveHotelGallery
        //public bool RemoveHotelGallery(int id)
        //{
        //    var HotelGalleryModel = _unitOfWork.Set<HotelGallery>().Where(x => x.Id == id).FirstOrDefault();

        //    if (System.IO.File.Exists(_httpContextBase.Server.MapPath(HotelGalleryModel.ImageUrl)))
        //    {
        //        System.IO.File.Delete(_httpContextBase.Server.MapPath(HotelGalleryModel.ImageUrl));
        //    }
        //    if (System.IO.File.Exists(_httpContextBase.Server.MapPath(HotelGalleryModel.ImageUrl.ThumbImage())))
        //    {
        //        System.IO.File.Delete(_httpContextBase.Server.MapPath(HotelGalleryModel.ImageUrl.ThumbImage()));
        //    }
        //    if (_hotelGalleryService.Delete(x => x.Id == id) == 1)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //#endregion

        #region UpdateAsync
        public async Task<EditHotelViewModel> UpdateAsync(EditHotelViewModel viewModel)
        {
            var hotel = _dbSet.Find(viewModel.Id);
            _mappingEngine.Map(viewModel, hotel);

            #region HotelFacility
            if (viewModel.HotelFacility != null && viewModel.HotelFacility.Any())
            {
                foreach (var item in hotel.HotelFacilities.ToList())
                {
                    hotel.HotelFacilities.Remove(item);
                }
                if (viewModel.HotelFacility != null && viewModel.HotelFacility.Any())
                {
                    var hotelFacilities = _hotelFacilityService.Filter(a => viewModel.HotelFacility.Any(x => x == a.Id)).ToList();
                    hotel.HotelFacilities = hotelFacilities;
                }
            }
            #endregion

            #region remove previous Tags
            List<Tag> tagList = hotel.Tags.ToList();
            if (tagList != null && tagList.Any())
            {
                foreach (var t in tagList)
                {
                    hotel.Tags.Remove(t);
                }
            }
            //else
            //{
            //    model.Tags = new List<Tag>();
            //}
            #endregion

            #region Saving Tags
            if (viewModel.TagTitles != null && viewModel.TagTitles.Any())
            {
                //model.Tags = new List<ProductTagProduct>();
                List<Tag> tagsInDb = new List<Tag>();
                foreach (var tagTitle in viewModel.TagTitles.ToList())
                {
                    hotel.Tags = new List<Tag>();
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name == tagTitle);
                    if (tagInDB == null)
                    {
                        var newTag = new Tag() { Name = tagTitle };

                        //save tag in productTag table
                        _unitOfWork.Set<Tag>().Add(newTag);


                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = insertedTag.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(newTag);
                        //model.Tags.Add(newTag);
                    }
                    else
                    {
                        //var newProductTagProduct = new ProductTagProduct()
                        //{
                        //    ProductTagId = tagInDB.Id
                        //};

                        //save tag in ProductTagProduct
                        tagsInDb.Add(tagInDB);
                        //model.Tags.Add(tagInDB);
                    }
                }

                hotel.Tags = tagsInDb;
            }
            #endregion

            #region Edit post Group
            PostGroup _postGroup = new PostGroup();
            List<PostGroup> postGroups = hotel.PostGroups.ToList();

            foreach (var item in postGroups)
            {
                hotel.PostGroups.Remove(item);
            }
            if (viewModel._selectedPostGroups == null)
            {
                PostGroup postGroup = _unitOfWork.Set<PostGroup>().Where(x => x.Name == "دسته بندی نشده").FirstOrDefault();
                hotel.PostGroups.Add(postGroup);
            }
            else
            {
                foreach (var item in viewModel._selectedPostGroups)
                {
                    _postGroup = _unitOfWork.Set<PostGroup>().Find(item);
                    hotel.PostGroups.Add(_postGroup);
                }
            }
            #endregion

            await _unitOfWork.SaveAllChangesAsync();
            return await _dbSet.AsNoTracking().ProjectTo<EditHotelViewModel>(_mappingEngine).Where(x => x.Id == viewModel.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region GetAllHotelsOfSelectListItem
        public IEnumerable<SelectListItem> GetAllHotelsOfSelectListItem()
        {
            return _dbSet.Where(x => x.IsDeleted == false).Select(r => new SelectListItem() { Text = r.Title, Value = r.Id.ToString() }).AsEnumerable();
        }
        #endregion

        #region GetHotelsByTourProgram
        public IEnumerable<FilterList> GetHotelsByTourProgram(int tourId)
        {
            return _unitOfWork.Set<TourProgram>().Where(w => w.TourId == tourId)
                                                 .Join(_dbSet.AsQueryable(), tp => tp.CityId, h => h.CityId, (tourProgram, hotel) =>
                                                     new FilterList
                                                     {
                                                         Id = hotel.Id,
                                                         Title = hotel.Title
                                                     }).AsEnumerable();
        }
        #endregion

        #region GetHotelsByTourProgram
        public IEnumerable<FilterListForAutoComplete> GetHotelsByTourProgramForAutoComplete(string phrase, int tourId)
        {
            return _unitOfWork.Set<TourProgram>().Where(w => w.TourId == tourId)
                                                 .Join(_dbSet.AsQueryable(), tp => tp.CityId, h => h.CityId, (tourProgram, hotel) => new
                                                 {
                                                     Id = hotel.Id,
                                                     Title = hotel.Title,
                                                     City = hotel.City.Title,
                                                     Country = hotel.City.State != null ? hotel.City.State.Country.Title : ""
                                                 }).Distinct().Where(w => w.Title.Contains(phrase) || w.City.Contains(phrase) || w.Country.Contains(phrase))
                                                     .Select(s => new FilterListForAutoComplete()
                                                     {
                                                         Id = s.Id,
                                                         HotelTitle = s.Title,
                                                         Description = s.City + "(" + s.Country + ")"
                                                     }).AsEnumerable();
        }
        #endregion

        #region LatestHotel
        public IList<PostListDetailViewModel> LatestHotel()
        {
            List<PostListDetailViewModel> hotels = (from p in _dbSet
                                                    join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                    on p.Id equals l.typeId
                                                    where l.linkType == LinkType.Hotel && l.Visible == true && p.IsActive == true && p.IsDeleted == false && l.IsDeleted == false
                                                    orderby p.PublishDatetime descending
                                                    select new PostListDetailViewModel
                                                    {
                                                        Id = p.Id,
                                                        PostRateAvg = p.PostRateAvg,
                                                        PostRateCount = p.PostRateCount,
                                                        PostSummery = p.Summary,
                                                        PublishDatetime = p.PublishDatetime,
                                                        VisitCount = p.VisitCount,
                                                        Image = p.HotelGalleries.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                        {
                                                            ImageUrl = x.ImageUrl,
                                                            Height = x.Height,
                                                            Id = x.Id,
                                                            ImageDesc = x.ImageDesc,
                                                            ImageExtension = x.ImageExtension,
                                                            ImageFileName = x.ImageFileName,
                                                            ImageTitle = x.ImageTitle,
                                                            IsPrimarySlider = x.IsPrimarySlider,
                                                            Width = x.Width

                                                        }).FirstOrDefault(),
                                                        Name = l.Name,
                                                        Rel = l.Rel,
                                                        Target = l.Target,
                                                        Title = l.Title,
                                                        URL = l.URL

                                                    }).ToList();
            return hotels;
        }
        #endregion

        #region RelatedHotels
        public IList<PostListDetailViewModel> RelatedHotel(List<string> tags)
        {
            List<PostListDetailViewModel> hotels = (from p in _dbSet
                                                    join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                    on p.Id equals l.typeId
                                                    where p.Tags.Any(x => tags.Contains(x.Name)) && p.IsDeleted.Equals(false) && l.Visible == true && l.linkType == LinkType.Post /*&& p.PostGroups.Any(c=>!c.Title.Contains("introducing-hotels"))*/
                                                    orderby p.PublishDatetime descending
                                                    select new PostListDetailViewModel
                                                    {
                                                        Id = p.Id,
                                                        PostRateAvg = p.PostRateAvg,
                                                        PostRateCount = p.PostRateCount,
                                                        PostSummery = p.Summary,
                                                        PublishDatetime = p.PublishDatetime,
                                                        VisitCount = p.VisitCount,
                                                        Image = p.HotelGalleries.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                        {
                                                            ImageUrl = x.ImageUrl,
                                                            Height = x.Height,
                                                            Id = x.Id,
                                                            ImageDesc = x.ImageDesc,
                                                            ImageExtension = x.ImageExtension,
                                                            ImageFileName = x.ImageFileName,
                                                            ImageTitle = x.ImageTitle,
                                                            IsPrimarySlider = x.IsPrimarySlider,
                                                            Width = x.Width

                                                        }).FirstOrDefault(),
                                                        Name = l.Name,
                                                        Rel = l.Rel,
                                                        Target = l.Target,
                                                        Title = l.Title,
                                                        URL = l.URL
                                                    }).ToList();
            return hotels;

        }
        public IList<PostListDetailViewModel> RelatedHotel(List<string> tags, int currentHotelId)
        {
            List<PostListDetailViewModel> hotels = (from p in _dbSet
                                                    join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                                                    on p.Id equals l.typeId
                                                    where p.Tags.Any(x => tags.Contains(x.Name)) && p.IsDeleted.Equals(false) && l.Visible == true && l.linkType == LinkType.Hotel && l.typeId != currentHotelId /*&& p.PostGroups.Any(c=>!c.Title.Contains("introducing-hotels"))*/
                                                    orderby p.PublishDatetime descending
                                                    select new PostListDetailViewModel
                                                    {
                                                        Id = p.Id,
                                                        PostRateAvg = p.PostRateAvg,
                                                        PostRateCount = p.PostRateCount,
                                                        PostSummery = p.Summary,
                                                        PublishDatetime = p.PublishDatetime,
                                                        VisitCount = p.VisitCount,
                                                        Image = p.HotelGalleries.Where(x => x.Width == 98 || x.Width == 700).Select(x => new ImageViewModel
                                                        {
                                                            ImageUrl = x.ImageUrl,
                                                            Height = x.Height,
                                                            Id = x.Id,
                                                            ImageDesc = x.ImageDesc,
                                                            ImageExtension = x.ImageExtension,
                                                            ImageFileName = x.ImageFileName,
                                                            ImageTitle = x.ImageTitle,
                                                            IsPrimarySlider = x.IsPrimarySlider,
                                                            Width = x.Width

                                                        }).FirstOrDefault(),
                                                        Name = l.Name,
                                                        Rel = l.Rel,
                                                        Target = l.Target,
                                                        Title = l.Title,
                                                        URL = l.URL

                                                    }).ToList();
            return hotels;
        }

        #endregion

        #region JoinHotelToLink
        public List<HotelDetailsViewModel> JoinHotelToLink(List<ParvazPardaz.Model.Entity.Hotel.Hotel> listModel)
        {
            List<HotelDetailsViewModel> vm = new List<HotelDetailsViewModel>();
            vm = (from p in listModel
                  join l in _unitOfWork.Set<LinkTable>().ToList()
                      on p.Id equals l.typeId
                  where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false
                  select new HotelDetailsViewModel
                  {
                      AccessLevel = p.AccessLevel,
                      ExpireDatetime = p.ExpireDatetime,
                      Id = p.Id,
                      IsActiveComments = p.IsActiveComments,
                      PostContent = p.Description,
                      PostRateAvg = p.PostRateAvg,
                      PostRateCount = p.PostRateCount,
                      PostSort = p.Sort,
                      PostSummery = p.Summary,
                      PublishDatetime = p.PublishDatetime,
                      VisitCount = l.VisitCount,
                      PostGroups = (from pg in p.PostGroups
                                    join lg in _unitOfWork.Set<LinkTable>().ToList()
                                        on pg.Id equals lg.typeId
                                    where lg.linkType == LinkType.PostGroup
                                    select new PostGroupViewModel
                                    {
                                        Name = lg.Name,
                                        Rel = lg.Rel,
                                        Target = lg.Target,
                                        Title = lg.Title,
                                        URL = lg.URL,

                                    }).ToList(),
                      //PostTags = (from tg in p.Tags
                      //            join lg in _unitOfWork.Set<LinkTable>().ToList()
                      //                on tg.Id equals lg.typeId
                      //            where lg.linkType == LinkType.PostTag
                      //            select new PostGroupViewModel
                      //            {
                      //                Name = lg.Name,
                      //                Rel = lg.Rel,
                      //                Target = lg.Target,
                      //                Title = lg.Title,
                      //                URL = lg.URL,

                      //            }).ToList(),
                      HotelGalleries=p.HotelGalleries,
                      //Thumbnail = p.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? p.HotelGalleries.FirstOrDefault().ImageUrl + p.HotelGalleries.FirstOrDefault().ImageExtension : p.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + p.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageExtension,
                      //Thumbnail = p.HotelGalleries.Where(x => (x.IsPrimarySlider == true && x.IsDeleted == false)).FirstOrDefault().ImageUrl + "-261x177" + p.HotelGalleries.Where(x => x.IsPrimarySlider == true || true).FirstOrDefault().ImageExtension,
                      //ThumbnailName = p.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? p.HotelGalleries.FirstOrDefault().ImageFileName : p.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageFileName,
                      //Name575x530 = p.PostImages.FirstOrDefault().ImageFileName,
                      //Extention575x530 = p.PostImages.FirstOrDefault().ImageExtension,
                      //ImageUrl765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl,
                      //Extention765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension,
                      //Name765x535 = p.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageFileName,

                      //ImageUrl575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageUrl,
                      //Extention575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageExtension,
                      //Name575x530 = p.PostImages.Where(x => x.Width == 575 && x.Height == 530).FirstOrDefault().ImageFileName,

                      //ImageUrl370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageUrl,
                      //Extention370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageExtension,
                      //Name370x292 = p.PostImages.Where(x => x.Width == 370 && x.Height == 292).FirstOrDefault().ImageFileName,

                      //ImageUrl277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageUrl,
                      //Extention277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageExtension,
                      //Name277x186 = p.PostImages.Where(x => x.Width == 277 && x.Height == 186).FirstOrDefault().ImageFileName,

                      //ImageUrl98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageUrl,
                      //Extention98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageExtension,
                      //Name98x98 = p.PostImages.Where(x => x.Width == 98 && x.Height == 98).FirstOrDefault().ImageFileName,

                      MetaDescription = l.Description,
                      MetaKeywords = l.Keywords,
                      Name = l.Name,
                      Rel = l.Rel,
                      Target = l.Target,
                      Title = l.Title,
                      URL = l.URL

                  }

                                ).ToList();
            foreach (var item in vm)
            {
                if (item.HotelGalleries!=null)
                {
                    var thumb = item.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault();
                    if (thumb!=null)
                    {
                        item.Thumbnail = thumb.ImageUrl + "-261x177" + thumb.ImageExtension;
                    }
                }
            }
            return vm;
        }
        #endregion

        #region FindHotelByCityId (Cascading DDL)
        public IEnumerable<SelectListItem> FindHotelByCityId(int? cityId)
        {
            var cityList = _dbSet.Where(c => c.IsDeleted == false && c.CityId == cityId).Select(s => new SelectListItem() { Selected = false, Text = s.Title, Value = s.Id.ToString() }).AsEnumerable();
            return cityList;
        }
        #endregion
    }
}
