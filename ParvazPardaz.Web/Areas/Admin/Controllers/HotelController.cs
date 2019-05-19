using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Common.HtmlHelpers.Models;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Model.Entity.Post;
using AutoMapper;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Service.Contract.Post;
using Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class HotelController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelService _hotelService;
        private readonly IHotelGalleryService _hotelGalleryService;
        private readonly IHotelRankService _hotelRankService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IHotelFacilityService _hotelFacilityService;
        private readonly IMappingEngine _mappingEngine;
        private readonly IPostService _postService;

        #endregion

        #region	Ctor
        public HotelController(IUnitOfWork unitOfWork, IHotelService hotelService, IHotelGalleryService hotelGalleryService, IHotelRankService hotelRankService, ICountryService countryService, ICityService cityService, IHotelFacilityService hotelFacilityService, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _hotelService = hotelService;
            _hotelGalleryService = hotelGalleryService;
            _hotelRankService = hotelRankService;
            _countryService = countryService;
            _cityService = cityService;
            _hotelFacilityService = hotelFacilityService;
            _postService = postService;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "HotelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        
        public ActionResult GetHotel([DataSourceRequest]DataSourceRequest request)
        {
            var query = _hotelService.GetViewModelForGrid();
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region GetHotels
        public JsonResult GetHotelsByTourProgram(string phrase, int tourId)
        {
            return Json(_hotelService.GetHotelsByTourProgramForAutoComplete(phrase, tourId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotels(string term)
        {
            var result = _hotelService.Filter(x => x.IsDeleted == false && x.Title.Contains(term)).Select(s => new { Id = s.Id, Title = s.Title + "(" + s.City + "-" + s.City.State.Country.Title + ")" }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "HotelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            AddHotelViewModel addHotelViewModel = new AddHotelViewModel();
            addHotelViewModel.KeywordsDDL = _hotelService.GetTagsForDDL();
            addHotelViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            addHotelViewModel._selectedPostGroups = new List<int>();

            ViewBag.HotelRanks = _hotelRankService.GetAllHotelRanksOfSelectListItem();
            //ViewBag.HotelFacilities = _hotelFacilityService.GetAllHotelFacilityOfSelectListItem();
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem();
            ViewBag.hotelFacility = new SelectList(_unitOfWork.Set<HotelFacility>().Where(t => t.IsDeleted == false).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title");

            return View(addHotelViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddHotelViewModel addHotelViewModel)
        {

            //addHotelViewModel.HotelFacility = addHotelViewModel.HotelFacility.Where(x => x > 0).ToList();

            //ذخیره کلیدواژه ها بصورت همروند
            #region Save Tags
            if (addHotelViewModel.TagTitles != null && addHotelViewModel.TagTitles.Any())
            {
                foreach (var tagTitle in addHotelViewModel.TagTitles.ToList())
                {
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name.Equals(tagTitle));
                    if (tagInDB == null)
                    {
                        Tag newTag = new Tag() { Name = tagTitle };
                        _unitOfWork.Set<Tag>().Add(newTag);
                    }
                }

                _unitOfWork.SaveAllChanges();
            }
            #endregion

            if (ModelState.IsValid)
            {
                var newHotel = await _hotelService.CreateAsync(addHotelViewModel);
                return RedirectToAction("CreateGallary", new { hotelId = newHotel.Id });
            }
            addHotelViewModel.KeywordsDDL = _hotelService.GetTagsForDDL();
            addHotelViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            addHotelViewModel._selectedPostGroups = addHotelViewModel._selectedPostGroups;

            ViewBag.HotelRanks = _hotelRankService.GetAllHotelRanksOfSelectListItem();
            ViewBag.HotelFacilities = _hotelFacilityService.GetAllHotelFacilityOfSelectListItem();
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem();
            ViewBag.hotelFacility = new SelectList(_unitOfWork.Set<HotelFacility>().Where(t => t.IsDeleted == false).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title");

            return View(addHotelViewModel);
        }
        #endregion

        #region CreateGallary
        public ActionResult CreateGallary(int hotelId)
        {
            ImageSliderViewModel model = new ImageSliderViewModel();
            model.PostId = hotelId;
            model.EditModeFileUploads = _hotelService.GetById(x => x.Id == hotelId).HotelGalleries
                                                  //.Where(t => t.IsPrimarySlider == false)
                                                  .Select(s => new EditModeFileUpload()
                                                  {
                                                      Name = s.ImageFileName,
                                                      Size = s.ImageSize,
                                                      Type = s.ImageExtension,
                                                      Url = s.ImageUrl + "-261x177" + s.ImageExtension,
                                                      Id = s.Id

                                                  }).ToList();
            return View(model);

        }

        [HttpPost]
        public JsonResult CreateGallaryUpload(ImageSliderViewModel addTourSliderViewModel)
        {
            HotelGallery postimage = _hotelGalleryService.UpoloadGallery(addTourSliderViewModel);
            return Json(postimage.Id, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ChangeThumb 
        public ActionResult ChangeThumb(int id)
        {
            var chageThumbVM = new ChangeThumbViewModel();
            var hotel = _unitOfWork.Set<Hotel>().Include(x => x.HotelGalleries).FirstOrDefault(x => x.Id == id);
            if (hotel.HotelGalleries != null && hotel.HotelGalleries.Any())
            {
                chageThumbVM.Id = hotel.Id;
                if (hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault() != null)
                {
                    chageThumbVM.DefaultThumbImage = hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageUrl + "-261x177" + hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().ImageExtension;
                    chageThumbVM.defaultImgId = hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().Id;
                }
                else
                    chageThumbVM.DefaultThumbImage = "";

                //ImageRadioButtonsViewModel imgradios=new ImageRadioButtonsViewModel
                chageThumbVM.ThumbImageURLRadioButtons = hotel.HotelGalleries.Select(image => new ImageRadioButtonsViewModel()
                {
                    ImageId = image.Id,
                    ImageUrl = image.ImageUrl + "-261x177" + image.ImageExtension,

                }).ToList();
            }
            return View(chageThumbVM);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeThumb(ChangeThumbViewModel changeThumbVM)
        {
            var hotel = _unitOfWork.Set<Hotel>().FirstOrDefault(x => x.Id == changeThumbVM.Id);
            if (hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault() != null)
            {

                var imgPrimaryId = hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().Id;
                if (imgPrimaryId != changeThumbVM.ThumbImageURLRadioButtonIds.FirstOrDefault())
                {
                    hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true).FirstOrDefault().IsPrimarySlider = false;
                    hotel.HotelGalleries.Where(x => x.Id == changeThumbVM.ThumbImageURLRadioButtonIds.FirstOrDefault()).FirstOrDefault().IsPrimarySlider = true;

                }

            }
            else
            {
                hotel.HotelGalleries.Where(x => x.Id == changeThumbVM.ThumbImageURLRadioButtonIds.FirstOrDefault()).FirstOrDefault().IsPrimarySlider = true;
            }
            await _unitOfWork.SaveAllChangesAsync();
            //var selectedThumbUrl = changeThumbVM.ThumbImageURLRadioButtons.FirstOrDefault();
            //if (hotel != null && selectedThumbUrl != null)
            //{
            //    hotel.Thumbnail = selectedThumbUrl;
            //}

            //await _unitOfWork.SaveAllChangesAsync();

            return RedirectToAction("Index", new { msg = "update" });
        }
        #endregion

        #region UploadHotelGallery - غیر فعال
        //public JsonResult UploadHotelGallery(HotelGalleryViewModel hotelGalleryViewModel, HttpPostedFileBase File)
        //{
        //    var newHotelGallery = _hotelService.UploadHotelGallery(hotelGalleryViewModel);
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "HotelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<ActionResult> Edit(int id)
        {
            //var model = _hotelService.GetById(x => x.Id == id);
            var model = _unitOfWork.Set<Hotel>().Include(x => x.HotelFacilities).Include(x => x.PostGroups).Include(x => x.HotelGalleries).FirstOrDefault(x => x.Id == id);
            EditHotelViewModel editHotelViewModel = await _hotelService.GetViewModelAsync<EditHotelViewModel>(x => x.Id == id);
            editHotelViewModel.KeywordsDDL = _hotelService.GetTagsForDDL();
            editHotelViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();
            editHotelViewModel._selectedPostGroups = model.PostGroups.Select(z => z.Id).ToList();
            //editHotelViewModel.ChkBoxHotelFacilities = _unitOfWork.Set<PostGroup>().Select(x => new ChkBoxHotelFacilityViewModel() { Id = x.Id, Title = x.Title }).ToList();

            ViewBag.HotelRanks = _hotelRankService.GetAllHotelRanksOfSelectListItem();
            //ViewBag.HotelFacilities = _hotelFacilityService.GetAllHotelFacilityOfSelectListItem();
            ViewBag.hotelFacility = new MultiSelectList(_unitOfWork.Set<HotelFacility>().Where(t => t.IsDeleted == false).OrderBy(t => t.Title).Select(s => new { Id = s.Id, Title = s.Title }), "Id", "Title", model.HotelFacilities.Select(i => i.Id).ToList());
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem(model.CityId);

            //editHotelViewModel.CityTitle = _unitOfWork.Set<City>().FirstOrDefault(x => x.Id == editHotelViewModel.CityId).Title;
            return View(editHotelViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHotelViewModel editHotelViewModel)
        {
            //if (editHotelViewModel.CityId <= 0 || string.IsNullOrEmpty(editHotelViewModel.CityTitle))
            //{
            //    ViewBag.HotelRanks = _hotelRankService.GetAllHotelRanksOfSelectListItem();
            //    ViewBag.HotelFacilities = _hotelFacilityService.GetAllHotelFacilityOfSelectListItem();
            //    ModelState.AddModelError("", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.Required, "City Title"));
            //    return View(editHotelViewModel);
            //}

            //ذخیره کلیدواژه ها بصورت همروند
            #region Save Tags
            if (editHotelViewModel.TagTitles != null && editHotelViewModel.TagTitles.Any())
            {
                foreach (var tagTitle in editHotelViewModel.TagTitles.ToList())
                {
                    var tagInDB = _unitOfWork.Set<Tag>().FirstOrDefault(x => x.Name.Equals(tagTitle));
                    if (tagInDB == null)
                    {
                        Tag newTag = new Tag() { Name = tagTitle };
                        _unitOfWork.Set<Tag>().Add(newTag);
                    }
                }

                _unitOfWork.SaveAllChanges();
            }
            #endregion

            if (ModelState.IsValid)
            {
                var update = await _hotelService.UpdateAsync(editHotelViewModel);
                return RedirectToAction("Index", new { msg = "update" });
            }

            editHotelViewModel.KeywordsDDL = _hotelService.GetTagsForDDL();
            editHotelViewModel._postGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive).ToList();

            ViewBag.HotelRanks = _hotelRankService.GetAllHotelRanksOfSelectListItem();
            ViewBag.HotelFacilities = _hotelFacilityService.GetAllHotelFacilityOfSelectListItem();
            ViewBag.CityDDL = _cityService.GetAllCityOfSelectListItem();

            return View(editHotelViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "HotelManagement", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]

        public async Task<JsonResult> Delete(int id)
        {
            var model = await _hotelService.DeleteLogicallyAsync(x => x.Id == id);
            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region RemoveUpload - غیر فعال
        //[HttpPost]
        //public JsonResult RemoveUpload(int id)
        //{
        //    var removeHotelGallery = _hotelService.RemoveHotelGallery(id);
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region DeleteGallery
        public JsonResult DeleteGallery(int id)
        {
            if (_hotelGalleryService.RemoveGallery(id))
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region CheckURL
        public JsonResult CheckURL(string Title)
        {
            var check = _postService.CheckURL(Title);
            if (check == true)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("تکراری است", JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "SarinaPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public ActionResult SarinaPrice()
        {
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Uploads/");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");

            var Price = System.Configuration.ConfigurationManager.AppSettings["HotelPrice"];
            if (objAppsettings != null)
            {
                ViewBag.Price = objAppsettings.Settings["HotelPrice"].Value;
            }
            return View();
        }
        [HttpPost]
        public ActionResult SarinaPrice(string price)
        {
            //System.Configuration.ConfigurationManager.AppSettings["HotelPrice"];
            Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/Uploads/");
            AppSettingsSection objAppsettings = (AppSettingsSection)objConfig.GetSection("appSettings");
            //Edit
            if (objAppsettings != null)
            {
                objAppsettings.Settings["HotelPrice"].Value = price;
                objConfig.Save();
            }
            ViewBag.Price = price;
            return View();           
        }
    }
}
