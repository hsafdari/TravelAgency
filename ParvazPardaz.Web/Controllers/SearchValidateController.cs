using AutoMapper;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.Service.Contract.SocialLog;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;
//using ParvazPardaz.Service.Contract.SocialLog;
using ParvazPardaz.Model.Entity.SocialLog;
using ParvazPardaz.Service.Contract.Tour;

namespace ParvazPardaz.Web.Controllers
{
    public class SearchValidateController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly ITourService _tourService;
        private readonly IPostService _postService;
        private readonly IHotelService _hotelService;
        private readonly ISocialLogService _socialLogervice;
        private readonly ISearchLogService _searchLogService;
        private List<BreadCrumbsItemViewModel> breadCrumbItems;
        private List<PostGroup> _relatedGroups { get; set; }

        #endregion

        #region	Ctor
        public SearchValidateController(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IPostService postService, IHotelService hotelService, ISocialLogService socialLogervice, ISearchLogService searchLogService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _tourService = tourService;
            _postService = postService;
            _hotelService = hotelService;
            _relatedGroups = new List<PostGroup>();
            _socialLogervice = socialLogervice;
            _searchLogService = searchLogService;
            breadCrumbItems = new List<BreadCrumbsItemViewModel>();
        }
        #endregion

        #region SearchValidate
        //
        // GET: /Search/
        [Route("SearchValidate")]
        [HttpPost]
        public ActionResult Index(SearchResultViewModel search)//SearchViewModel search)
        {
            if (search.Title == null || search.Title.Trim() == "")
            {
                return Json(new { status = "SearchContentRequired" }, JsonRequestBehavior.AllowGet);
            }

            #region log
            SearchLog newLog = new SearchLog()
            {
                Title = search.Title,
                Source = "/" + search.currentUrl,
                Browser = System.Web.HttpContext.Current.Request.GetBrowser(),
            };
            _unitOfWork.Set<SearchLog>().Add(newLog);
            _unitOfWork.SaveAllChanges();
            search.currentUrl = search.Title.Replace(" ", "+");
            #endregion

            return Json(new { status = "Success", title = search.Title.Trim().Replace(" ", "+"), pageIndex = search.PageIndex, pageSize = search.PageSize, url = search.currentUrl }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Search
        [Route("Search")]
        [Route("Search/{q}/")]
        [Route("Search/{q}/{PageIndex}/")]
        [Route("Search/{q}/{PageIndex}/{PageSize}")]
        public ActionResult Search(SearchResultViewModel search)//string q = "", int PageIndex = 0, int PageSize = 9)//string q)
        {
          //  SearchResultViewModel search = new SearchResultViewModel();
            search.q = search.q.Replace("+", " ");
            //search.PageIndex = PageIndex;
            //search.PageSize = PageSize;

            #region View Model
            bool isBeHiddenBtnMore = false;
            SearchResultViewModel viewmodel = new SearchResultViewModel();
            //viewmodel.PostListViewModel.PostDetail = _postService.PostSearchLists(out isBeHiddenBtnMore, search.PageIndex, search.PageSize, search.Title);
            viewmodel.q = viewmodel.Title = search.q;
            ViewBag.Title = string.Format("جستجوی '{0}'", search.q);
            //viewmodel.PostListViewModel.Description = search.Title;
            ViewBag.description = search.q;
            ViewBag.keywords = search.q;
            
            #endregion

            if (search.PageIndex == 0)
            {
                viewmodel.TourList = _tourService.SearchTours(search.q);
                viewmodel.MagItemList = _postService.MagSearchLists(out isBeHiddenBtnMore, search.PageIndex, search.PageSize, search.q);
                viewmodel.IsBeHiddenBtnMore = isBeHiddenBtnMore;
                return View("SearchResult", viewmodel);
            }
            else
            {
                viewmodel.MagItemList = _postService.MagSearchLists(out isBeHiddenBtnMore, search.PageIndex, search.PageSize, search.q);
                viewmodel.IsBeHiddenBtnMore = isBeHiddenBtnMore;
                ViewBag.Index = viewmodel.PageIndex + 1;
                return PartialView("~/Views/SearchValidate/_PrvMagSearchedResult.cshtml", viewmodel);
            }
        }
        #endregion

        #region GetMenuSearchPartial
        public ActionResult GetMenuSearchPartial()
        {
            var newSearch = new SearchViewModel();
            return PartialView("_PrvMenuSearch", newSearch);
        }
        #endregion

        #region GetMenuBottomSearchPartial
        public ActionResult GetMenuBottomSearchPartial()
        {
            var newSearch = new SearchViewModel();
            return PartialView("_PrvMenuBottomSearch", newSearch);
        }
        #endregion
    }
}