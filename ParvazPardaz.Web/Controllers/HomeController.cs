using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Service.Contract.Core;
using ParvazPardaz.ViewModel;
using ParvazPardaz.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Model.Entity.Tour;
using EntityType = ParvazPardaz.Model.Entity;
using ParvazPardaz.Model.Enum;
using Z.EntityFramework.Plus;
using ParvazPardaz.Model.Entity.Hotel;
using System.Data.Entity;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.Service.Contract.Link;
using ParvazPardaz.Common.Utility;
using Newtonsoft.Json;
using ParvazPardaz.Model.Entity.Content;
using System.Web.Caching;
using System.IO;
using reCAPTCHA.MVC;
using System.Configuration;
using ParvazPardaz.Service.Contract.Country;
using AutoMapper;
using ParvazPardaz.Service.Contract.Rule;

namespace ParvazPardaz.Web.Controllers
{
    public class HomeController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewsLetterService _newsLetterService;
        private readonly IHotelService _hotelService;
        private readonly ILinkService _linkService;
        private readonly ICityService _cityService;
        private List<BreadCrumbsItemViewModel> breadCrumbItems;
        private readonly IMappingEngine _mappingEngine;
        private readonly ITermsandConditionService _termsandCondition;
        #endregion

        #region	Ctor
        public HomeController(IUnitOfWork unitOfWork, INewsLetterService newsLetterService, IHotelService hotelService,
            ILinkService linkService, ICityService cityService, IMappingEngine mappingEngine, ITermsandConditionService termsandCondition)
        {
            _unitOfWork = unitOfWork;
            _newsLetterService = newsLetterService;
            _hotelService = hotelService;
            _linkService = linkService;
            _cityService = cityService;
            _mappingEngine = mappingEngine;
            _termsandCondition = termsandCondition;
            breadCrumbItems = new List<BreadCrumbsItemViewModel>();
        }
        #endregion

        #region ساخت بردکرامبز createBreadCrumbs
        /// <summary>
        /// ایجاد breadCrumbItems
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="linkType"></param>
        public void createBreadCrumbs(PostGroup treeNode, LinkType linkType)
        {
            var linkTbl = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.linkType == linkType && x.typeId == treeNode.Id);
            var item = new BreadCrumbsItemViewModel() { Title = treeNode.Name, URL = linkTbl != null ? linkTbl.URL : "#" };
            breadCrumbItems.Add(item);
            if (treeNode.PostGroupParent != null)
            {
                createBreadCrumbs(treeNode.PostGroupParent, LinkType.PostGroup);
            }
        }
        #endregion

        #region Index
        //
        // GET: /Home/
        public ActionResult Index(string message)
        {
            ViewBag.success = message;
            HomeViewModel homeVM = new HomeViewModel();

            #region Seo parameters
            //Seo parameters
            var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.Name == "Home").FirstOrDefault();
            if (linkModel != null)
            {
                ViewBag.Title = linkModel.Title;
                ViewBag.description = linkModel.Description;
                ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                ViewBag.keywords = linkModel.Keywords;
            }
            #endregion

            var sliders = _unitOfWork.Set<Slider>().OrderBy(x => x.Priority).Where(x => !x.IsDeleted && x.ImageIsActive).ToList();

            #region اسلایدر بالای صفحه اصلی
            homeVM.TopHomeSlider = sliders.Where(x => x.SliderGroup.Name == "HomeSliderTop")
                     .Select(z => new SlidersUITourHomeViewModel
                     {
                         ImageTitle = z.ImageTitle,
                         ImageIsActive = z.ImageIsActive,
                         Priority = z.Priority,
                         ImageDescription = z.ImageDescription,
                         NavigationUrl = z.NavigationUrl,
                         ImageURL = z.ImageURL,
                         footerLine1 = z.footerLine1,
                         footerLine2 = z.footerLine2,
                         HeaderDays = z.HeaderDays,
                         NavDescription = z.NavDescription
                     }).ToList();
            #endregion

            #region اسلایدر اول تور
            homeVM.Slider1 = sliders.Where(x => x.SliderGroup.Name == "HomeSlider")
                   .Select(z => new SlidersUITourHomeViewModel
                   {
                       ImageTitle = z.ImageTitle,
                       ImageIsActive = z.ImageIsActive,
                       Priority = z.Priority,
                       ImageDescription = z.ImageDescription,
                       NavigationUrl = z.NavigationUrl,
                       ImageURL = z.ImageURL,
                       footerLine1 = z.footerLine1,
                       footerLine2 = z.footerLine2,
                       HeaderDays = z.HeaderDays,
                       NavDescription = z.NavDescription,
                       Price = z.Price ?? 0
                   }).ToList();
            #endregion

            #region اسلایدر دوم تور
            homeVM.Slider2 = sliders.Where(x => x.SliderGroup.Name == "HomeSlider2")
                     .Select(z => new SlidersUITourHomeViewModel
                     {
                         ImageTitle = z.ImageTitle,
                         ImageIsActive = z.ImageIsActive,
                         Priority = z.Priority,
                         ImageDescription = z.ImageDescription,
                         NavigationUrl = z.NavigationUrl,
                         ImageURL = z.ImageURL,
                         footerLine1 = z.footerLine1,
                         footerLine2 = z.footerLine2,
                         HeaderDays = z.HeaderDays,
                         NavDescription = z.NavDescription,
                         Price = z.Price ?? 0
                     }).ToList();
            #endregion

            #region چرا کی سفر؟
            //لینک تصویر بزرگ
            homeVM.BigPhotoUrl = sliders.Where(x => x.SliderGroup.Name.Equals("WhyCharteran")).FirstOrDefault().ImageURL;
            #endregion

            #region شرکت های تجاری
            var corporates = _unitOfWork.Set<SliderGroup>().Where(x => x.Name == "home-corporate" && x.IsActive && !x.IsDeleted).ToList();

            List<CorporatesViewModel> _corporateList = new List<CorporatesViewModel>();
            foreach (var item in corporates)
            {
                CorporatesViewModel _corporate = new CorporatesViewModel();
                _corporate.Name = item.Name;
                _corporate.Title = item.GroupTitle;
                foreach (var slider in item.Sliders.OrderBy(x => x.Priority))
                {
                    _corporate.Sliders.Add(new SlidersUIViewModel
                    {
                        NavigationUrl = slider.NavigationUrl,
                        ImageURL = slider.ImageURL,
                        ImageTitle = slider.ImageTitle
                    });
                }
                _corporateList.Add(_corporate);
            }
            homeVM.CorporateLists = _corporateList;
            #endregion

            #region اسلایدر پایین صفحه
            //اسلایدهای قصد گردش دارید را می آورد
            homeVM.sliderbellow = sliders.Where(x => x.SliderGroup.Name.Equals("HomeSliderBottom"))
                 .Select(z => new SlidersUIViewModel
                 {
                     ImageTitle = z.ImageTitle,
                     ImageIsActive = z.ImageIsActive,
                     Priority = z.Priority,
                     ImageDescription = z.ImageDescription,
                     NavigationUrl = z.NavigationUrl,
                     ImageURL = z.ImageURL
                 });
            #endregion
            
            return View(homeVM);
        }
        #endregion

        #region GetTourSearch
        public ActionResult GetTourSearch()
        {
            var viewModel = new TourSearchViewModel();
            viewModel.CitiesAllDDL = _cityService.GetAvailableDestCitiesDDL();
            viewModel.CitiesFromDDL = _cityService.GetAllFromNationalDDL();
            viewModel.CitiesDestinationDDL = _cityService.GetAllDestinationNationalDDL();
            viewModel.FlightDate = DateTime.Now.ToString("MMMM,M,d");
            ViewBag.DomesticFromDDL = _cityService.GetAllFromDomesticDDL();
            ViewBag.DomesticDestinationDDL = _cityService.GetAllDestinationDomesticDDL();
            return PartialView("_PrvTourSearch", viewModel);
        }
        #endregion

        #region GetFlightSearch
        public ActionResult GetFlightSearch()
        {
            return PartialView("_PrvFlightSearch");
        }
        #endregion

        #region GetHotelSearch
        public ActionResult GetHotelSearch()
        {
            return PartialView("_PrvHotelSearch");
        }
        #endregion

        #region signup
        /// <summary>
        /// PrivateKey For Local :        6LdDQiUTAAAAAMxoMmUB5IirGOsQtqkpgYJ0Hhq4
        /// PrivateKey For Charteran.ir : 6LeAUkQUAAAAAOSjAqxYZZgon59O813KAEXDWBI2
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="captchaValid"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[CaptchaValidator(
        // PrivateKey = "6LeAUkQUAAAAAOSjAqxYZZgon59O813KAEXDWBI2",
        // ErrorMessage = "لطفا تیک من ربات نیستم را علامت بزنید",
        // RequiredMessage = "The captcha field is required.")]
        public JsonResult signup(HomeViewModel viewmodel, bool captchaValid)
        {
            //var recaptchaVal = ViewData.ModelState["Recaptcha"];
            //if (recaptchaVal != null && recaptchaVal.Errors.Any())
            //{
            //    return Json("InvalidRecaptcha", JsonRequestBehavior.AllowGet);
            //}

            if (ModelState.IsValid)
            {
                var newsletter = _unitOfWork.Set<ParvazPardaz.Model.Entity.Core.Newsletter>().Where(x => x.Email == viewmodel.AddNewsLetter.Email).FirstOrDefault();
                if (newsletter == null)
                {
                    AddNewsLetterViewModel model = new AddNewsLetterViewModel();
                    model.Email = viewmodel.AddNewsLetter.Email;
                    var newPostGroup = _newsLetterService.Create<AddNewsLetterViewModel>(model);
                    var cookie = _newsLetterService.setCookie();
                    Response.SetCookie(cookie);
                    ModelState.Clear();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.Clear();
                    return Json("duplicate", JsonRequestBehavior.AllowGet);
                }

                //Newsletter news = new Newsletter();
                //news.Mail = popupnewsletter.PopUpMail;
                //news.Name = popupnewsletter.PopUpName;
                //news.MobileNumber = popupnewsletter.PopUpMobileNumber;
                //news.CreateDateTime = DateTime.Now;
                //db.Newsletter.Add(news);
                //db.SaveChanges();
            }
            ModelState.Clear();
            return Json("fail", JsonRequestBehavior.AllowGet);
        }
        [ValidateAntiForgeryToken]
        [Route("Home/signup1")]
        public JsonResult SigNewsLetterup(string Email)
        {
            if (ModelState.IsValid)
            {
                var newsletter = _unitOfWork.Set<ParvazPardaz.Model.Entity.Core.Newsletter>().Where(x => x.Email == Email).FirstOrDefault();
                if (newsletter == null)
                {
                    AddNewsLetterViewModel model = new AddNewsLetterViewModel();
                    model.Email = Email;
                    var newPostGroup = _newsLetterService.Create<AddNewsLetterViewModel>(model);
                    var cookie = _newsLetterService.setCookie();
                    Response.SetCookie(cookie);
                    ModelState.Clear();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.Clear();
                    return Json("duplicate", JsonRequestBehavior.AllowGet);
                }

                //Newsletter news = new Newsletter();
                //news.Mail = popupnewsletter.PopUpMail;
                //news.Name = popupnewsletter.PopUpName;
                //news.MobileNumber = popupnewsletter.PopUpMobileNumber;
                //news.CreateDateTime = DateTime.Now;
                //db.Newsletter.Add(news);
                //db.SaveChanges();
            }
            ModelState.Clear();
            return Json("fail", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Menus
        /// <summary>
        /// واکشی منوی
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenus()//int groupId)
        {
            var menuGroup = _unitOfWork.Set<MenuGroup>().Where(g => g.IsDeleted == false && g.GroupName.Contains("Main")).FirstOrDefault();
            if (menuGroup != null)
            {
                var menuItems = _unitOfWork.Set<Menu>().Where(m => m.IsDeleted == false && m.MenuIsActive && m.MenuParentId == null && m.MenuGroupId == menuGroup.Id).OrderBy(m => m.OrderId).IncludeFilter(p => p.MenuChilds.Where(w => !w.IsDeleted && w.MenuIsActive)).ToList();
                foreach (var item in menuItems)
                {
                    var l2childs = item.MenuChilds = item.MenuChilds.Where(x => !x.IsDeleted && x.MenuIsActive).OrderByDescending(x => x.OrderId).ToList();
                    foreach (var childitem in l2childs)
                    {
                        var l3childs = childitem.MenuChilds = childitem.MenuChilds.Where(x => !x.IsDeleted && x.MenuIsActive).OrderByDescending(x => x.OrderId).ToList();
                    }
                }
                return PartialView("_PrvMenu", menuItems);
            }
            return PartialView("_PrvMenu");
        }
        #endregion

        public ActionResult GetSocial()
        {
            return PartialView("_PrvSocial");
        }

        #region GetNewsLetter
        public ActionResult GetNewsLetter()
        {
            return PartialView("_PrvNewsletter");
        }
        #endregion

        #region GetFooter
        public ActionResult GetFooter()
        {
            //اولین رکورد فعال به عنوان متن قبل پانویس استفاده شده است ؛ پس در پانویس نباید بیاید
            //var beforeFooter = _unitOfWork.Set<Footer>().FirstOrDefault(x => x.Id == 1);
            var footer = _unitOfWork.Set<Footer>().Where(x => !x.IsDeleted && x.IsActive && x.FooterType == EnumFooterType.Main && x.Id > 1).Select(z => new FooterUIViewModel
            {
                Title = z.Title,
                OrderID = z.OrderID,
                IsActive = z.IsActive,
                Content = z.Content,

            }).OrderBy(x => x.OrderID);

            return PartialView("_PrvFooter", footer);
        }
        #endregion

        #region UpdateLinkTbleRate
        [HttpPost]
        public JsonResult UpdateLinkTbleRate(UpdateRateViewModel newLnkTblRate)
        {
            var lnk = _linkService.GetById(x => x.Id == newLnkTblRate.Id);

            if (Request.Cookies["RateLinkTblIdList"] == null)
            {
                #region ثبت امتیاز
                if (lnk != null)
                {
                    decimal? RateSum = (lnk.PostRateAvg * lnk.PostRateCount);
                    lnk.PostRateAvg = (RateSum + newLnkTblRate.Rate) / (lnk.PostRateCount + 1);
                    lnk.PostRateCount++;
                }

                //ذخیره شناسه پست مزبور و تاریخ امتیاز دادن ، در کوکی
                List<DataCookie> postIdList = new List<DataCookie>();
                DataCookie newData = new DataCookie() { OwnId = lnk.Id, RateDate = DateTime.Now, NewestRate = newLnkTblRate.Rate };
                postIdList.Add(newData);

                HttpCookie newRateLinkTblIdListCookie = new HttpCookie("RateLinkTblIdList")
                {
                    Value = JsonConvert.SerializeObject(postIdList),
                    Expires = DateTime.Now.AddDays(30d)
                };
                Response.SetCookie(newRateLinkTblIdListCookie);

                var isSaved = _unitOfWork.SaveAllChanges();
                return Json(new
                {
                    Report = (isSaved == 1 ? "Success" : "Error"),
                    Title = "امتیازدهی",
                    Message = (isSaved == 1 ? "امتیاز شما با موفقیت ثبت گردید" : "خطا در ثبت امتیاز. لطفا مجددا سعی نمایید")

                }, JsonRequestBehavior.AllowGet);
                #endregion
            }
            else
            {
                //تمام محتوای کوکی
                var allPostIdList = JsonConvert.DeserializeObject<List<DataCookie>>(Request.Cookies["RateLinkTblIdList"].Value).Take(100);
                //تمام منقضی نشده ها
                var notExpiredPostIdList = allPostIdList.Where(pd => pd.RateDate.AddDays(30d) > DateTime.Now).Select((item, i) => new DataCookie() { OwnId = item.OwnId, RateDate = item.RateDate, NewestRate = item.NewestRate }).ToList();

                //اگر پست در لیست منقضی نشده های داخل کوکی نـــبود 
                if (!notExpiredPostIdList.Any(pd => pd.OwnId == lnk.Id))
                {
                    #region ثبت امتیاز
                    if (lnk != null)
                    {
                        decimal? RateSum = (lnk.PostRateAvg * lnk.PostRateCount);
                        lnk.PostRateAvg = (RateSum + newLnkTblRate.Rate) / (lnk.PostRateCount + 1);
                        lnk.PostRateCount++;
                    }

                    //افزودن به لیست منقضی نشده ها
                    DataCookie newData = new DataCookie() { OwnId = lnk.Id, RateDate = DateTime.Now, NewestRate = newLnkTblRate.Rate };
                    notExpiredPostIdList.Add(newData);


                    //به روز رسانی کوکی با لیست منقضی نشده های آپدیت شده
                    //کوکی در هر بار با موارد منقضی نشده مقداردهی می شود و 
                    //موارد منقضی شده را نگه نمی دارد
                    HttpCookie updateRateLinkTblIdListCookie = new HttpCookie("RateLinkTblIdList")
                    {
                        Value = JsonConvert.SerializeObject(notExpiredPostIdList),
                    };
                    Response.SetCookie(updateRateLinkTblIdListCookie);

                    var isSaved = _unitOfWork.SaveAllChanges();
                    return Json(new
                    {
                        Report = (isSaved == 1 ? "Success" : "Error"),
                        Title = "امتیازدهی",
                        Message = (isSaved == 1 ? "امتیاز شما با موفقیت ثبت گردید" : "خطا در ثبت امتیاز. لطفا مجددا سعی نمایید")

                    }, JsonRequestBehavior.AllowGet);
                    #endregion
                }

                return Json(new
                {
                    Report = "Multiple",
                    Title = "امتیازدهی",
                    Message = "امکان امتیازدهی بیش از یک بار برای این مورد وجود ندارد"

                }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Page
        [Route("page-{url}")]
        public ActionResult Page(string url)
        {
            if (!Request.Path.EndsWith("/"))
                return RedirectPermanent(Request.Url.ToString() + "/");

            if (url != null)
            {
                #region URL : Not Null
                string myurl = Request.RawUrl; // linkTable Total Absolute Url
                var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == myurl && !x.IsDeleted && x.Visible).FirstOrDefault();
                if (linkModel != null && linkModel.linkType == LinkType.Post)
                {
                    #region LinkType: Post
                    var post = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.Id == linkModel.typeId && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();

                    #region PostGroups
                    List<PostGroupViewModel> PostGroups = (from pg in post.PostGroups
                                                           join lg in _unitOfWork.Set<LinkTable>().Where(x => x.Visible && !x.IsDeleted).ToList()
                                                               on pg.Id equals lg.typeId
                                                           where lg.linkType == LinkType.PostGroup
                                                           select new PostGroupViewModel
                                                           {
                                                               Name = lg.Name,
                                                               Rel = lg.Rel,
                                                               Target = lg.Target,
                                                               Title = pg.Title,
                                                               URL = lg.URL

                                                           }).ToList();
                    #endregion

                    var postDetail = _mappingEngine.Map<PostDetailViewModel>(post);

                    #region ساخت بردکرامبز
                    breadCrumbItems = new List<BreadCrumbsItemViewModel>();
                    //یافتن گروهی که فرزند ندارد : برگ درخت گروه ها                            
                    //&& !x.PostGroupParent.Title.Equals("SystemGroup")صفدری: این خط کد رو حذف کردم چون روی کد 90 خطا میداد و صفحات رو لود نمی کرد
                    var leaf = post.PostGroups.LastOrDefault(x => !x.PostGroupChildren.Any());// && !x.PostGroupParent.Title.Equals("SystemGroup"));
                    if (leaf != null)
                    {
                        //فرستادن برگ به تابع برای اینکه تا اجدادش را در بردکرامبزی بگذارد
                        createBreadCrumbs(leaf, LinkType.PostGroup);
                    }
                    //پر کردن بردکرامبز این پست
                    postDetail.breadCrumbItems = breadCrumbItems;
                    #endregion

                    var img = post.PostImages.FirstOrDefault();
                    postDetail.ImageUrl = (img != null ? img.ImageUrl+ "-765x535" + img.ImageExtension : "");
                    postDetail.URL = linkModel.URL;
                    postDetail.Title = post.Name;

                    //_socialLogervice.LogVisitPage(post.Id, LinkType.Post);

                    ViewBag.description = linkModel.Description;
                    ViewBag.keywords = linkModel.Keywords;
                    ViewBag.CustomMetaTags = linkModel.CustomMetaTags;

                    return View(postDetail);
                    #endregion
                }
                else
                {
                    #region Permanent redirect 301
                    //بررسی شود که لینک مورد نظر باید پرمننت.ریدایرکت 301 شود یا خیر
                    //آیا در جدول لینک.ریدایرکشن وجود دارد؟
                    //این کد در جای دیگر هم استفاده شده : /Tour/TourDetail
                    var linkRedirection301 = _unitOfWork.Set<LinkRedirection>().FirstOrDefault(x => x.OldLink == myurl);
                    if (linkRedirection301 != null)
                    {
                        return RedirectPermanent(linkRedirection301.NewLink);
                    }

                    //لاگ لینک 404
                    var notFoundLink = new NotFoundLink() { URL = myurl };
                    _unitOfWork.Set<NotFoundLink>().Add(notFoundLink);
                    _unitOfWork.SaveAllChanges();

                    //ارور 404
                    //return RedirectToAction("NoData", "Blog");
                    return HttpNotFound();
                    #endregion
                }
                #endregion
            }

            return View();
        }
        [Route("TermsandCondition")]
        public ActionResult TermsandCondition()
        {
            var List = _termsandCondition.GetViewModelForGrid().Where(x => x.IsActive).OrderBy(x => x.Priority).ToList();
            return View(List);
        }
        #endregion

    }
}