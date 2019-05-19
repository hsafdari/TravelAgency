using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Entity.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.ViewModel;
using AutoMapper;
using ParvazPardaz.Service.Contract.Post;
using EntityType = ParvazPardaz.Model.Entity.Hotel;
using EntityTourType = ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Model.Entity.Hotel;
using System.Web.UI;
using ParvazPardaz.Model.Entity.SocialLog;
using ParvazPardaz.Service.Contract.SocialLog;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Entity.Magazine;
using Newtonsoft.Json;
using ParvazPardaz.Common.Utility;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Model.Entity.Tour;
using Z.EntityFramework.Plus;
using ParvazPardaz.Service.Contract.Product;
using ParvazPardaz.Model.Entity.Comment;
using Microsoft.AspNet.Identity;
using ParvazPardaz.Model.Entity.Users;
using Infrastructure;
using System.Net;
using ParvazPardaz.Service.Contract.Tour;

namespace ParvazPardaz.Web.Controllers
{
    public class BlogController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly ITourService _tourService;
        private readonly IPostService _postService;
        private readonly IHotelService _hotelService;
        private readonly ISocialLogService _socialLogervice;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly ICommentService _commentService;
        private List<BreadCrumbsItemViewModel> breadCrumbItems;

        private List<PostGroup> _relatedGroups { get; set; }

        //private readonly IDbSet<Post> _dbSet;
        //private readonly IDbSet<PostGroup> _dbSet;
        //private readonly IDbSet<Tag> _dbSet;
        #endregion

        #region	Ctor
        public BlogController(IUnitOfWork unitOfWork, IMappingEngine mappingEngine, IPostService postService, IHotelService hotelService, ISocialLogService socialLogervice, ICountryService countryService, ICommentService commentService, ITourService tourService, ICityService cityService)
        {
            _unitOfWork = unitOfWork;
            _mappingEngine = mappingEngine;
            _tourService = tourService;
            _postService = postService;
            _hotelService = hotelService;
            _relatedGroups = new List<PostGroup>();
            _socialLogervice = socialLogervice;
            _countryService = countryService;
            _commentService = commentService;
            breadCrumbItems = new List<BreadCrumbsItemViewModel>();
            _cityService = cityService;
        }
        #endregion

        #region Get Menus
        /// <summary>
        /// واکشی منوی
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenus()//int groupId)
        {
            var r = Request;
            var menuGroup = _unitOfWork.Set<MenuGroup>().Where(g => g.IsDeleted == false && g.Id == 2).FirstOrDefault();
            if (menuGroup != null)
            {
                var menuItems = _unitOfWork.Set<Menu>().Where(m => m.IsDeleted == false && m.MenuIsActive && m.MenuGroupId == menuGroup.Id && m.MenuParent == null).OrderBy(m => m.OrderId).IncludeFilter(x => x.MenuChilds.Where(w => !w.IsDeleted)).ToList();
                return PartialView("_PrvMenu", menuItems);
            }
            return PartialView("_PrvMenu");
        }
        #endregion

        #region GetFooter
        public ActionResult GetFooter()
        {
            //اولین رکورد فعال به عنوان متن قبل پانویس استفاده شده است ؛ پس در پانویس نباید بیاید
            //var beforeFooter = _unitOfWork.Set<Footer>().FirstOrDefault(x => x.Id == 1);
            var footer = _unitOfWork.Set<Footer>().Where(x => !x.IsDeleted && x.IsActive && x.FooterType==EnumFooterType.Blog).OrderBy(x => x.OrderID).Select(z => new FooterUIViewModel
            {
                Title = z.Title,
                OrderID = z.OrderID,
                IsActive = z.IsActive,
                Content = z.Content,
            });
            return PartialView("_PrvFooter", footer);
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region GetPostGroupsTreeNodes
        //public void GetPostGroupsTreeNodes(PostGroup currentPostGroup)
        //{
        //    _relatedGroups.Add(currentPostGroup);

        //    if (currentPostGroup.PostGroupChildren != null && currentPostGroup.PostGroupChildren.Any())
        //    {
        //        foreach (var childGroup in currentPostGroup.PostGroupChildren)
        //        {
        //            GetPostGroupsTreeNodes(childGroup);
        //        }
        //    }
        //}
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

        #region Link
        [Route("tourism/{*url}")]
        [Route("hotel/{*url}")]
        //[OutputCache(CacheProfile = "BlogMagazineCache")]
        public ActionResult Link(string url)
        {
            bool hasHotel = false;

            if (!Request.Path.EndsWith("/"))
                return RedirectPermanent(Request.Url.ToString() + "/");
            if (url == null)
            {
                #region URL: Null
                var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == "Blog").FirstOrDefault();
                FirstBlogPageViewModel model = new FirstBlogPageViewModel();
                //اسلایدر اول
               // Random rnd = new Random(DateTime.Now.Millisecond);
                var postSlider = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.IsActive == true && x.IsDeleted == false).Where(x => x.PostGroups.Any(c => c.Id == 3) && !x.PostGroups.Any(c => c.Title.Contains("introducing-hotels"))).OrderByDescending(x => x.CreatorDateTime).Take(15).ToList();
                List<Post> postSliderModel = new List<Post>();
                if (postSlider.Count == 15 || postSlider.Count == 10 || postSlider.Count == 5)
                {
                    postSliderModel = postSlider;
                }
                else if (postSlider.Count < 15 && postSlider.Count > 10)
                {
                    postSliderModel = postSlider.Take(10).ToList();
                }

                else if (postSlider.Count < 10 && postSlider.Count > 5)
                {
                    postSliderModel = postSlider.Take(5).ToList();
                }
                else if (postSlider.Count < 5)
                {
                    postSliderModel = null;
                }
                model.firstSlider = _postService.JoinToLink(postSliderModel);


                //باکس دوم
               // Random rnd2 = new Random(DateTime.Now.Millisecond);
                var postSecondBox = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.IsActive).Where(x => x.PostGroups.Any(c => c.Id == 4)).OrderByDescending(x => x.CreatorDateTime).Take(6).ToList();
                //model.secondBox = _postService.JoinToLink(postSecondBox);
                model.secondBox = _postService.JoinToLink(postSecondBox);

                //باکس بزرگ
                //var postBigBox = _unitOfWork.Set<Post>().Include(x => x.PostGroups).Include(x => x.PostImages).Where(x => x.IsActive == true).Where(x => x.PostGroups.Any(c => c.Id == 5) && !x.PostGroups.Any(c => c.Title.Contains("introducing-hotels"))).OrderByDescending(x => x.CreatorDateTime).Take(5).ToList();
                //Random rnd3 = new Random(DateTime.Now.Millisecond);
                var postBigBox = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.IsActive).Where(x => x.PostGroups.Any(c => c.Id == 14) && !x.PostGroups.Any(c => c.Title.Contains("introducing-hotels"))).OrderByDescending(x => x.CreatorDateTime).Take(5).ToList();
                model.BigBox = _postService.JoinToLink(postBigBox);

                //اسلایدر وسط
                //هتل ها
               // Random rnd4 = new Random(DateTime.Now.Millisecond);
                var postMiddleSlider = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.IsActive).Where(x => x.PostGroups.Any(c => c.Id == 6)).OrderByDescending(x => x.CreatorDateTime).Take(10).ToList();
                model.middleSlider = _postService.JoinToLink(postMiddleSlider);


                ViewBag.Title = linkModel.Title;
                ViewBag.description = linkModel.Description;
                ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                ViewBag.keywords = linkModel.Keywords;

                model.MetaKeywords = linkModel.Keywords;
                model.MetaDescription = linkModel.Description;
                model.Title = linkModel.Title;

                return View("Link", model);
                #endregion
            }
            else
            {
                //string myurl = "/tourism/" + url;
                string myurl = Request.RawUrl; // linkTable Total Absolute Url
                var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == myurl && !x.IsDeleted && x.Visible).FirstOrDefault();
                if (linkModel != null)
                {
                    if (linkModel.linkType == LinkType.PostGroup || linkModel.linkType == LinkType.PostTag)
                    {
                        #region LinkType: PostGroup, PostTag
                        PostListViewModel postlistVM = new PostListViewModel();
                        postlistVM.Title = linkModel.Title;
                        ViewBag.Title = linkModel.Title;
                        postlistVM.URL = linkModel.URL;
                        postlistVM.Description = linkModel.Description;
                        ViewBag.description = linkModel.Description;
                        postlistVM.CustomMetaTags = linkModel.CustomMetaTags;
                        ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                        ViewBag.keywords = linkModel.Keywords;
                        postlistVM.PostDetail = _postService.PostLists(linkModel.typeId, linkModel.linkType, out hasHotel);
                        //if (linkModel.typeId==15 && linkModel.linkType==LinkType.PostGroup)
                        //{
                        postlistVM.SearchList.HotelRankList = _unitOfWork.Set<HotelRank>().Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
                        //}

                        _socialLogervice.LogVisitPage(linkModel.typeId, linkModel.linkType);

                        #region if linkType == PostGroup : RelatedGroup
                        //اگر گروه پست بود ، گروه های مرتبط را بیاور
                        if (linkModel.linkType == LinkType.PostGroup)
                        {
                            //کل گروه های پست ، در یک بار واکشی
                            var allPostGroups = _unitOfWork.Set<PostGroup>().Where(x => !x.IsDeleted && x.IsActive == true);
                            //بدست آوردن ریشه گروه کلیک شده
                            var currentPostGroup = allPostGroups.Where(x => x.Id == linkModel.typeId).FirstOrDefault();
                             PostGroup currentGroup =new PostGroup();
                            if (currentPostGroup!=null)
                            {
                                if (currentPostGroup.ParentId != null)
                                {
                                    currentGroup = allPostGroups.Where(x => x.Id == currentPostGroup.ParentId).FirstOrDefault();
                                }
                                //اولین فرزندان پدر ، گروه های مرتبط خواهد بود
                                if (currentGroup.PostGroupChildren != null && currentGroup.PostGroupChildren.Any())
                                {
                                    foreach (var chGroup in currentGroup.PostGroupChildren.Where(x => !x.IsDeleted).ToList())
                                    {
                                        _relatedGroups.Add(chGroup);
                                    }
                                }


                                //پر کردن گروه های مرتبط
                                postlistVM.RelatedGroups = (from pg in _relatedGroups
                                                            join lg in _unitOfWork.Set<LinkTable>().Where(x => x.Visible && !x.IsDeleted).ToList()
                                                                on pg.Id equals lg.typeId
                                                            where lg.linkType == LinkType.PostGroup
                                                            select new PostGroupViewModel
                                                            {
                                                                Name = lg.Name,
                                                                Rel = lg.Rel,
                                                                Target = lg.Target,
                                                                Title = lg.Title,
                                                                URL = lg.URL

                                                            }).ToList();

                                #region ساخت بردکرامبز
                                breadCrumbItems = new List<BreadCrumbsItemViewModel>();

                                //فرستادن برگ به تابع برای اینکه تا اجدادش را در بردکرامبزی بگذارد
                                createBreadCrumbs(currentPostGroup, LinkType.PostGroup);

                                //پر کردن بردکرامبز این پست
                                postlistVM.breadCrumbItems = breadCrumbItems;
                                #endregion
                            }
                        }
                        #endregion

                        ViewBag.HadHotel = hasHotel;

                        return View("BlogList", postlistVM);
                        #endregion
                    }
                    else if (linkModel.linkType == LinkType.Post)
                    {
                        #region LinkType: Post
                        var post = _unitOfWork.Set<Post>().Include(x => x.PostImages).Where(x => x.Id == linkModel.typeId && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
                        var hotelDetail = _mappingEngine.Map<HotelDetailsViewModel>(post);
                        var postDetail = _mappingEngine.Map<PostDetailViewModel>(post);
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

                        List<PostGroupViewModel> PostTags = (from pg in post.Tags
                                                             join lg in _unitOfWork.Set<LinkTable>().Where(x => x.Visible && !x.IsDeleted).ToList()
                                                                 on pg.Id equals lg.typeId
                                                             where lg.linkType == LinkType.PostTag
                                                             select new PostGroupViewModel
                                                             {
                                                                 Name = lg.Name,
                                                                 Rel = lg.Rel,
                                                                 Target = lg.Target,
                                                                 Title = lg.Title,
                                                                 URL = lg.URL

                                                             }).ToList();

                        var tagsName = (from t in PostTags select t.Name).ToList();
                        IList<PostListDetailViewModel> RelatedPosts = _postService.LatestPost().Take(3).ToList();
                        var cityTitle = linkModel.URL.Split('-')[1];
                        var city = _cityService.Filter(x => x.Title.Contains(cityTitle)).FirstOrDefault();
                        var cityId = city != null ? city.Id : 0;
                        IList<PostListDetailViewModel> RelatedTours = _tourService.RelatedTours(cityId);
                        ViewBag.Title = linkModel.Title;
                        ViewBag.description = linkModel.Description;
                        ViewBag.keywords = linkModel.Keywords;
                        ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                        hotelDetail.URL = linkModel.URL;

                        //string name=System.Threading.Thread.CurrentThread.CurrentCulture.EnglishName;
                        if (post.PostGroups.Where(x => x.Title == "introducing-hotels").FirstOrDefault() != null)
                        {
                            hotelDetail.PostGroups = PostGroups;
                            hotelDetail.PostTags = PostTags;
                            hotelDetail.RelatedPosts = RelatedPosts;
                            hotelDetail.RelatedTours = RelatedTours;
                            //hotelDetail.Images = post.PostImages;

                            //بعد از انتقال دیتابیس و جدا کرددن هتل هایی که در پست هستند و انتقال ان به جدول هتل  این کامنت باز شود
                            //_socialLogervice.LogVisitPage(post.Id, LinkType.Hotel);

                            return View("HotelDetail", hotelDetail);
                        }
                        else
                        {
                            #region ساخت بردکرامبز
                            breadCrumbItems = new List<BreadCrumbsItemViewModel>();
                            //یافتن گروهی که فرزند ندارد : برگ درخت گروه ها                            
                            //&& !x.PostGroupParent.Title.Equals("SystemGroup")صفدری: این خط کد رو حذف کردم چون روی کد 90 خطا میداد و صفحات رو لود نمی کرد
                            var leaf = post.PostGroups.Where(x => !x.PostGroupChildren.Any() && x.PostGroupParent!=null && !x.PostGroupParent.Title.Equals("SystemGroup")).LastOrDefault();
                            if (leaf != null)
                            {
                                //فرستادن برگ به تابع برای اینکه تا اجدادش را در بردکرامبزی بگذارد
                                createBreadCrumbs(leaf, LinkType.PostGroup);
                            }
                            //پر کردن بردکرامبز این پست
                            postDetail.breadCrumbItems = breadCrumbItems;
                            #endregion

                            postDetail.ImageUrl = post.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageUrl + "-765x535" + post.PostImages.Where(x => x.Width == 765 && x.Height == 535).FirstOrDefault().ImageExtension;
                            postDetail.PostGroups = PostGroups;
                            postDetail.PostTags = PostTags;
                            postDetail.RelatedPosts = RelatedPosts;
                            postDetail.URL = linkModel.URL;
                            postDetail.Title = post.Name;
                            postDetail.PostRateAvg = (int)linkModel.PostRateAvg.Value;
                            postDetail.PostRateCount = linkModel.PostRateCount.Value;
                            _socialLogervice.LogVisitPage(post.Id, LinkType.Post);


                            //گرفتن امتیازی که کاربر جاری قبلا به این کالا داده است با بررسی کوکی
                            #region گرفتن ای دی پست در صورت قبلا لایک شدن از کوکی
                            if (Request.Cookies["LikePost"] != null)
                            {
                                //تمام محتوای کوکی
                                var allProductIdList = JsonConvert.DeserializeObject<List<DataCookie>>(Request.Cookies["LikePost"].Value).Take(100);
                                //تمام منقضی نشده ها
                                //var notExpiredProductIdList = allProductIdList.Where(pd => pd.RateDate.AddDays(30d) > DateTime.Now).Select((item, i) => new DataCookie() { OwnId = item.OwnId, RateDate = item.RateDate, NewestRate = item.NewestRate }).ToList();

                                //اگر شناسه این پست در کوکی بود
                                var cookieProductItem = allProductIdList.FirstOrDefault(r => r.OwnId == post.Id);
                                if (cookieProductItem != null)
                                {
                                    ViewBag.like = "liked";
                                }
                            }
                            #endregion

                            return View("Detail", postDetail);
                        }


                        #endregion
                    }
                    else if (linkModel.linkType == LinkType.Hotel)
                    {
                        #region LinkType: Hotel

                        var hotel = _unitOfWork.Set<EntityType.Hotel>().Include(x => x.HotelGalleries).Where(x => x.Id == linkModel.typeId && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
                        var hotelDetail = _mappingEngine.Map<HotelDetailsViewModel>(hotel);

                        #region Tags
                        List<PostGroupViewModel> HTags = (from pg in hotel.Tags
                                                          join lg in _unitOfWork.Set<LinkTable>().Where(x => x.Visible && !x.IsDeleted).ToList()
                                                              on pg.Id equals lg.typeId
                                                          where lg.linkType == LinkType.PostTag
                                                          select new PostGroupViewModel
                                                          {
                                                              Name = lg.Name,
                                                              Rel = lg.Rel,
                                                              Target = lg.Target,
                                                              Title = lg.Title,
                                                              URL = lg.URL

                                                          }).ToList();

                        var tagsName = (from t in HTags select t.Name).ToList();
                        #endregion

                        #region group
                        List<PostGroupViewModel> PostGroups = (from pg in hotel.PostGroups
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

                        IList<PostListDetailViewModel> RelatedHotels = _hotelService.LatestHotel().Take(3).ToList();
                        ViewBag.Title = linkModel.Title;
                        ViewBag.description = linkModel.Description;
                        ViewBag.keywords = linkModel.Keywords;
                        ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
                        hotelDetail.URL = linkModel.URL;

                        //hotelDetail.PostGroups = PostGroups;
                        hotelDetail.PostTags = HTags;
                        hotelDetail.RelatedPosts = RelatedHotels;
                        hotelDetail.HotelRank = hotel.HotelRank.Icon;
                        if (hotel.HotelGalleries.Count!=0)
                        {
                        hotelDetail.Thumbnail = hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault() == null ? hotel.HotelGalleries.FirstOrDefault().ImageUrl + "-700X525" + hotel.HotelGalleries.FirstOrDefault().ImageExtension : hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageUrl + "-700X525" + hotel.HotelGalleries.Where(x => x.IsPrimarySlider == true && x.IsDeleted == false).FirstOrDefault().ImageExtension;
                        }

                        IList<PostListDetailViewModel> relatedTours = _tourService.RelatedTours(hotel.CityId);
                        hotelDetail.RelatedTours = relatedTours;

                        _socialLogervice.LogVisitPage(hotel.Id, LinkType.Hotel);

                        #region ساخت بردکرامبز
                        breadCrumbItems = new List<BreadCrumbsItemViewModel>();
                        //یافتن گروهی که فرزند ندارد : برگ درخت گروه ها
                        var leaf = hotel.PostGroups.LastOrDefault(x => !x.PostGroupChildren.Any() && !x.PostGroupParent.Title.Equals("SystemGroup"));
                        if (leaf != null)
                        {
                            //فرستادن برگ به تابع برای اینکه تا اجدادش را در بردکرامبزی بگذارد
                            createBreadCrumbs(leaf, LinkType.PostGroup);
                        }
                        //پر کردن بردکرامبز این پست
                        hotelDetail.breadCrumbItems = breadCrumbItems;
                        #endregion


                        #region comments
                        hotelDetail.CommentList = _unitOfWork.Set<Comment>().Include(x => x.CommentReviews).Where(x => !x.IsDeleted && x.IsApproved && x.CommentType == CommentType.Hotel && x.OwnId == hotelDetail.Id).Select(x => new ClientCommentViewModel()
                        {
                            CommentReviews = x.CommentReviews.Select(r => new CommentReviewViewModel() { ReviewId = r.ReviewId, Rate = r.Rate, Title = r.Review.Title }).ToList(),
                            CommentText = x.CommentText,
                            CommentType = x.CommentType,
                            CreatorDateTime = x.CreatorDateTime,
                            DisLike = x.DisLike,
                            Like = x.Like,
                            Name = x.CreatorUser.FullName,
                            OwnId = x.OwnId,
                            ParentId = x.ParentId,
                            Rate = x.Rate,
                            RateCount = x.RateCount,
                            Subject = x.Subject,
                            Id = x.Id
                        }).ToList();

                        hotelDetail.CommentTotalScore = new totalCommentScore()
                        {
                            PersonCount = hotelDetail.CommentList.Count,
                            TotalRate = (hotelDetail.CommentList != null && hotelDetail.CommentList.Any()) ? (hotelDetail.CommentList.Select(x => x.Rate).Sum() / hotelDetail.CommentList.Count) : 0,
                            TotalCommentReviews = hotelDetail.CommentList.SelectMany(x => x.CommentReviews).Distinct().Select(r => new CommentReviewViewModel()
                            {
                                Title = r.Title,
                                Rate = hotelDetail.CommentList.SelectMany(rr => rr.CommentReviews).Distinct().Where(w => w.ReviewId == r.ReviewId) != null ? (hotelDetail.CommentList.SelectMany(rr => rr.CommentReviews).Distinct().Where(w => w.ReviewId == r.ReviewId).Select(rrr => rrr.Rate).Sum() / hotelDetail.CommentList.SelectMany(rr => rr.CommentReviews).Distinct().Where(w => w.ReviewId == r.ReviewId).Select(rrr => rrr.Rate).Count()) : 0
                            }).ToList()
                        };
                        #endregion

                        return View("HotelDetail", hotelDetail);


                        #endregion
                    }
                    else if (linkModel.linkType == LinkType.Tour)
                    {
                        #region LinkType: Tour
                        return Redirect("/Tour/TouDetail/" + linkModel.typeId.ToString());
                        #endregion
                    }
                }
                else
                {
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
                    //return new HttpNotFoundResult();
                    //throw new HttpException(404, "Page Not Found");
                    //return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                }
            }

            return View();
        }
        #endregion

        #region GetPostsByGroup
        public ActionResult GetPostsByGroup(string url)
        {
            bool hasHotel = false;

            var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url).FirstOrDefault();

            #region LinkType: PostGroup, PostTag
            PostListViewModel postlistVM = new PostListViewModel();
            postlistVM.Title = linkModel.Title;
            ViewBag.Title = linkModel.Title;
            postlistVM.URL = linkModel.URL;
            postlistVM.Description = linkModel.Description;
            ViewBag.description = linkModel.Description;
            postlistVM.CustomMetaTags = linkModel.CustomMetaTags;
            ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
            ViewBag.keywords = linkModel.Keywords;
            postlistVM.PostDetail = _postService.PostLists(linkModel.typeId, linkModel.linkType, out hasHotel);
            //if (linkModel.typeId==15 && linkModel.linkType==LinkType.PostGroup)
            //{
            postlistVM.SearchList.HotelRankList = _unitOfWork.Set<HotelRank>().Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
            //}

            #region if linkType == PostGroup : RelatedGroup
            //اگر گروه پست بود ، گروه های مرتبط را بیاور
            if (linkModel.linkType == LinkType.PostGroup)
            {
                //کل گروه های پست ، در یک بار واکشی
                var allPostGroups = _unitOfWork.Set<PostGroup>().Where(x => x.IsDeleted == false && x.IsActive == true);

                //بدست آوردن ریشه گروه کلیک شده
                var currentPostGroup = allPostGroups.FirstOrDefault(x => x.Id == linkModel.typeId);
                if (currentPostGroup.ParentId != null)
                {
                    currentPostGroup = allPostGroups.FirstOrDefault(x => x.Id == currentPostGroup.ParentId);
                }

                //اولین فرزندان پدر ، گروه های مرتبط خواهد بود
                if (currentPostGroup.PostGroupChildren != null && currentPostGroup.PostGroupChildren.Any())
                {
                    foreach (var chGroup in currentPostGroup.PostGroupChildren.ToList())
                    {
                        _relatedGroups.Add(chGroup);
                    }
                }

                //پر کردن گروه های مرتبط
                postlistVM.RelatedGroups = (from pg in _relatedGroups
                                            join lg in _unitOfWork.Set<LinkTable>().ToList()
                                                on pg.Id equals lg.typeId
                                            where lg.linkType == LinkType.PostGroup
                                            select new PostGroupViewModel
                                            {
                                                Name = lg.Name,
                                                Rel = lg.Rel,
                                                Target = lg.Target,
                                                Title = lg.Title,
                                                URL = lg.URL

                                            }).ToList();
            }
            #endregion

            return PartialView("_PrvGetPostsByGroup", postlistVM);
            #endregion

        }
        #endregion

        #region GetPostsOrderBy
        public ActionResult GetPostsOrderBy(string url, string orderby)
        {
            bool hasHotel = false;

            var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url).FirstOrDefault();

            #region LinkType: PostGroup, PostTag
            PostListViewModel postlistVM = new PostListViewModel();
            postlistVM.Title = linkModel.Title;
            ViewBag.Title = linkModel.Title;
            postlistVM.URL = linkModel.URL;
            postlistVM.Description = linkModel.Description;
            ViewBag.description = linkModel.Description;
            postlistVM.CustomMetaTags = linkModel.CustomMetaTags;
            ViewBag.CustomMetaTags = linkModel.CustomMetaTags;
            ViewBag.keywords = linkModel.Keywords;
            postlistVM.PostDetail = _postService.PostLists(linkModel.typeId, linkModel.linkType, orderby, out hasHotel);
            //if (linkModel.typeId==15 && linkModel.linkType==LinkType.PostGroup)
            //{
            postlistVM.SearchList.HotelRankList = _unitOfWork.Set<HotelRank>().Where(a => a.IsDeleted == false).Select(x => new SelectListItem() { Selected = false, Text = x.Title, Value = x.Id.ToString() }).AsEnumerable();
            //}

            #region if linkType == PostGroup : RelatedGroup
            //اگر گروه پست بود ، گروه های مرتبط را بیاور
            if (linkModel.linkType == LinkType.PostGroup)
            {
                //کل گروه های پست ، در یک بار واکشی
                var allPostGroups = _unitOfWork.Set<PostGroup>().Where(x => x.IsDeleted == false && x.IsActive == true);

                //بدست آوردن ریشه گروه کلیک شده
                var currentPostGroup = allPostGroups.FirstOrDefault(x => x.Id == linkModel.typeId);
                if (currentPostGroup.ParentId != null)
                {
                    currentPostGroup = allPostGroups.FirstOrDefault(x => x.Id == currentPostGroup.ParentId);
                }

                //اولین فرزندان پدر ، گروه های مرتبط خواهد بود
                if (currentPostGroup.PostGroupChildren != null && currentPostGroup.PostGroupChildren.Any())
                {
                    foreach (var chGroup in currentPostGroup.PostGroupChildren.ToList())
                    {
                        _relatedGroups.Add(chGroup);
                    }
                }

                //پر کردن گروه های مرتبط
                postlistVM.RelatedGroups = (from pg in _relatedGroups
                                            join lg in _unitOfWork.Set<LinkTable>().ToList()
                                                on pg.Id equals lg.typeId
                                            where lg.linkType == LinkType.PostGroup
                                            select new PostGroupViewModel
                                            {
                                                Name = lg.Name,
                                                Rel = lg.Rel,
                                                Target = lg.Target,
                                                Title = lg.Title,
                                                URL = lg.URL

                                            }).ToList();
            }
            #endregion
            ViewBag.HadHotel = hasHotel;

            return PartialView("_PrvGetPostsByGroup", postlistVM);
            #endregion
        }
        #endregion

        #region LatestUpdate
        public ActionResult LatestUpdate()
        {
            IList<PostListDetailViewModel> model = new List<PostListDetailViewModel>();
            model = _postService.LastUpdated(10);
            //var posts = _unitOfWork.Set<Post>().Include(x => x.PostGroups).Include(x => x.PostImages).Where(x => x.IsActive == true && x.IsDeleted == false).OrderByDescending(x => x.CreatorDateTime).Take(5).ToList();
            //  model = _postService.JoinToLink(posts);
            return PartialView("_PrvLastUpdateMarquee", model);
        }
        #endregion

        #region Detail
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region LatestUpdateSideBar
        public ActionResult LatestUpdateSideBar()
        {
            IList<PostListDetailViewModel> model = _postService.LastUpdated(10);
            return PartialView("_PrvSideBar", model);
        }
        #endregion

        //public ActionResult NoData()
        //{
        //    return View();
        //}

        #region SeeTabMagazine
       // [Route("tourism-{enTitle}")]
      
        public ActionResult CountryTabMagazines(string enTitle = "")
        {
            //string url = string.Format("/tourism-{0}/", enTitle);
            #region اگر ابتدای آدرس tourism باشد
            //اگر ابتدای آدرس tourism باشد
            //در جدول LinkRedirection بگردد
            var currentUrl = Request.Url.LocalPath;
            if (currentUrl.Contains("tourism"))
            {
                //کدهای زیر برای قبل از حالتی هست که تصمیم گرفتیم تور های غیرفعال/حذف شده را هم نشان دهد ولی به کاربر بگوید که این تور غیر فعال است
                //بررسی شود که لینک مورد نظر باید پرمننت.ریدایرکت 301 شود یا خیر
                //آیا در جدول لینک.ریدایرکشن وجود دارد؟
                //این کد در جای دیگر هم استفاده شده : /Blog/Link
                if (!currentUrl.EndsWith("/"))
                {
                    currentUrl += "/";
                }
                var linkRedirection301 = _unitOfWork.Set<LinkRedirection>().FirstOrDefault(x => x.OldLink == currentUrl);
                if (linkRedirection301 != null)
                {
                    return RedirectPermanent(linkRedirection301.NewLink);
                }
                else
                {
                    //لاگ لینک 404
                    var notFoundLink = new NotFoundLink() { URL = currentUrl };
                    _unitOfWork.Set<NotFoundLink>().Add(notFoundLink);
                    _unitOfWork.SaveAllChanges();

                    //ارور 404
                    return RedirectToAction("NoData", "Blog");
                }
            }
            #endregion


            string url = string.Format("/tour-{0}/", enTitle);
            var linkTable = _unitOfWork.Set<LinkTable>().FirstOrDefault(x => x.URL.Equals(url) && !x.IsDeleted);

            if (linkTable != null)
            {
                CountryTabMagsViewModel tabs = new CountryTabMagsViewModel();

                //موقعیت مکان مربوط به تب های مجله گردشگری
                var location = _unitOfWork.Set<Location>().FirstOrDefault(x => x.Id == linkTable.typeId);

                tabs.CountryTitle = location.Title;
                tabs.SeoText = location.SeoText;
                tabs.PostRateAvg =(int)linkTable.PostRateAvg.Value;
                tabs.PostRateCount = linkTable.PostRateCount.Value;
                tabs.LinkTblId = linkTable.Id;

                //تب های کشور
                var tabMagInDbList = _unitOfWork.Set<TabMagazine>().Where(x => x.CountryId == location.Id).OrderBy(x => x.Priority).ToList();

                var allLinkTables = _unitOfWork.Set<LinkTable>();
                var allHotels = _unitOfWork.Set<Hotel>().Where(h => h.Id > 20);
                var allPosts = _unitOfWork.Set<Post>();
                var allTours = _unitOfWork.Set<Tour>();

                //تب ها و محتوای هر کدام
                foreach (var tabMagInDb in tabMagInDbList)
                {
                    //گروه های آن
                    var allTabGroups = tabMagInDb.Groups.ToList();

                    TabMagazineContent tabContent = new TabMagazineContent();
                    tabContent.TabMagazine = tabMagInDb;

                    #region پست های مرتبط هر تب
                    //تمامی پست های مرتبط
                    var allRelatedPost = new List<Post>();
                    if (allTabGroups != null && allTabGroups.Any())
                    {
                        allRelatedPost = allTabGroups.SelectMany(x => x.Posts).Distinct().ToList();
                    }

                    //واکشی تصویری خاص از هر کدام پست ها
                    foreach (var p in allRelatedPost)
                    {
                        var post = allPosts.Include(x => x.PostImages).Include(x => x.PostGroups).FirstOrDefault(x => x.Id == p.Id);

                        //اولین تصویر پست
                        var img = new PostImage();
                        img = post.PostImages.FirstOrDefault(x => x.Width == 277 && x.Height == 186);
                        p.ImageUrl = img != null ? img.ImageUrl + "-277x186" + img.ImageExtension : "";
                    }

                    TabMagPostListViewModel tabMagPost = new TabMagPostListViewModel();
                    tabMagPost.TabMagPostList = allRelatedPost.Join(allLinkTables, jrp => jrp.Id, lt => lt.typeId, (jrp, lt) => new { jrp, lt })
                        .Where(x => x.lt.linkType == LinkType.Post && x.lt.Visible && !x.lt.IsDeleted)
                        .Select(x => new TabMagPost()
                        {
                            //ImageUrl = (from post in _unitOfWork.Set<Post>() where post.Id == x.jrp.Id select post.PostImages.FirstOrDefault(i => i.IsPrimarySlider).ImageUrl).FirstOrDefault(),
                            ImageUrl = x.jrp.ImageUrl,
                            Name = x.jrp.Name,
                            PostSummery = x.jrp.PostSummery,
                            PostUrl = x.lt.URL,
                            Target = x.lt.Target,
                            Rel = x.lt.Rel

                        }).ToList();

                    //TabMagazineContent tabContent = new TabMagazineContent();
                    //tabContent.TabMagazine = tabMagInDb;
                    tabContent.TabMagPostList = tabMagPost.TabMagPostList;

                    #endregion

                    #region هتل های مرتبط هر تب
                    //تمامی هتل های مرتبط
                    var allRelatedHotels = new List<Hotel>();
                    if (allTabGroups != null && allTabGroups.Any())
                    {
                        allRelatedHotels = allTabGroups.SelectMany(x => x.Hotels).Distinct().ToList();
                    }

                    //واکشی تصویری خاص برای هرکدام از هتل ها
                    foreach (var h in allRelatedHotels)
                    {
                        var hotel = allHotels.Include(x => x.HotelGalleries).FirstOrDefault(x => x.Id == h.Id);

                        var img = new HotelGallery();
                        //اولین تصویر هتل که در جدول هتل درج شده بوده
                        img = hotel.HotelGalleries.FirstOrDefault(x => x.IsPrimarySlider && x.Width == 700 && x.Height == 525);
                        if (img == null)
                        {
                            img = hotel.HotelGalleries.FirstOrDefault(x => x.Width == 700 && x.Height == 525);
                        }
                        h.ImageUrl = img != null ? img.ImageUrl + "-261x177" + img.ImageExtension : "";
                    }

                    TabMagPostListViewModel tabMagHotel = new TabMagPostListViewModel();
                    tabMagHotel.TabMagPostList = allRelatedHotels.Join(allLinkTables, jrh => jrh.Id, lt => lt.typeId, (jrh, lt) => new { jrh, lt })
                        .Where(x => x.lt.linkType == LinkType.Hotel && x.lt.Visible && !x.lt.IsDeleted)
                        .Select(x => new TabMagPost()
                        {
                            //ImageUrl = (from post in _unitOfWork.Set<Post>() where post.Id == x.jrp.Id select post.PostImages.FirstOrDefault(i => i.IsPrimarySlider).ImageUrl).FirstOrDefault(),
                            ImageUrl = x.jrh.ImageUrl,
                            Name = x.jrh.Title,
                            PostSummery = x.jrh.Summary,
                            PostUrl = x.lt.URL,
                            Target = x.lt.Target,
                            Rel = x.lt.Rel

                        }).ToList();

                    tabContent.TabMagPostList.AddRange(tabMagHotel.TabMagPostList);

                    #endregion

                    #region تورهای مرتبط با هر تب
                    //تمامی پست های مرتبط
                    var allRelatedTours = new List<Tour>();
                    if (allTabGroups != null && allTabGroups.Any())
                    {
                        //Recomended : این فیلد برای فعال/غیرفعال بودن استفاده شده است
                        allRelatedTours = allTabGroups.SelectMany(x => x.Tours).Where(x => !x.IsDeleted && x.Recomended).OrderBy(x => x.Priority).Distinct().ToList();
                    }

                    //واکشی تصویری خاص از هر کدام تور ها
                    foreach (var t in allRelatedTours)
                    {
                        var tour = allTours.Include(x => x.TourSliders).FirstOrDefault(x => x.Id == t.Id);

                        //اولین تصویر تور
                        var img = tour.TourSliders.FirstOrDefault(x => x.IsPrimarySlider);
                        //آدرس تصویر را موقتا در فیلد [کد] نگه داشتیم 
                        t.Code = img != null ? img.ImageUrl : "";
                    }

                    TabMagPostListViewModel tabMagTour = new TabMagPostListViewModel();
                    tabMagTour.TabMagPostList = allRelatedTours.Join(allLinkTables, jrt => jrt.TourLandingPageUrlId, lt => lt.typeId, (jrt, lt) => new { jrt, lt })
                        .Where(x => x.lt.linkType == LinkType.TourLanding && x.jrt.Recomended && !x.lt.IsDeleted) //&& x.lt.Visible && !x.lt.IsDeleted)
                        .Select(x => new TabMagPost()
                        {
                            ImageUrl = x.jrt.Code,
                            Name = x.jrt.Title,
                            PostSummery = x.jrt.ShortDescription,
                            PostUrl = x.lt.URL,
                            Target = x.lt.Target,
                            Rel = x.lt.Rel

                        }).ToList();

                    //TabMagazineContent tabContent = new TabMagazineContent();
                    //tabContent.TabMagazine = tabMagInDb;
                    tabContent.TabMagPostList.AddRange(tabMagTour.TabMagPostList);
                    #endregion

                    #region واکشی اولین گروه داخلی که حاوی کلمه نوروز باشد + تورهای مرتبط با آن

                    //گروه های فرزند متعلق به گروه های این تب
                    var allChildTabGroups = allTabGroups.SelectMany(x => x.PostGroupChildren.Where(y => !y.IsDeleted && y.IsActive && (y.Name.Contains("نوروز") || y.Title.ToLower().Contains("nowruz")))).ToList();
                    //تورهای مرتبط با اولین گروه فرزند
                    var allChildRelatedTours = new List<Tour>();
                    if (allChildTabGroups != null && allChildTabGroups.Any())
                    {
                        //تورهای مرتبط با اولین گروه فرزند
                        allChildRelatedTours = allChildTabGroups.First().Tours.Where(x => !x.IsDeleted && x.Recomended).OrderBy(x => x.Priority).Distinct().ToList();

                        //واکشی تصویری خاص برای هر کدوم از تورها
                        foreach (var t in allChildRelatedTours)
                        {
                            var tour = allTours.Include(x => x.TourSliders).FirstOrDefault(x => x.Id == t.Id);
                            //اولین تصویر تور
                            var img = tour.TourSliders.FirstOrDefault(x => x.IsPrimarySlider);
                            //آدرس تصویر را موقتا در فیلد [کد] نگه داشتیم 
                            t.Code = img != null ? img.ImageUrl : "";
                        }

                        TabMagPostListViewModel tabMagChildTour = new TabMagPostListViewModel();
                        tabMagChildTour.TabMagPostList = allChildRelatedTours.Join(allLinkTables, jrt => jrt.TourLandingPageUrlId, lt => lt.typeId, (jrt, lt) => new { jrt, lt })
                            .Where(x => x.lt.linkType == LinkType.TourLanding && x.jrt.Recomended && !x.lt.IsDeleted)
                            .Select(x => new TabMagPost()
                            {
                                ImageUrl = x.jrt.Code,
                                Name = x.jrt.Title,
                                PostSummery = x.jrt.ShortDescription,
                                PostUrl = x.lt.URL,
                                Target = x.lt.Target,
                                Rel = x.lt.Rel

                            }).ToList();

                        if (tabMagChildTour.TabMagPostList != null && tabMagChildTour.TabMagPostList.Any())
                        {
                            TabMagNestedItemViewModel childTabMag = new TabMagNestedItemViewModel();
                            childTabMag.ChildGroupTitle = allChildTabGroups.FirstOrDefault().Name;
                            childTabMag.TabMagChildPostList.AddRange(tabMagChildTour.TabMagPostList);
                            tabContent.ChildTabMag = childTabMag;
                        }
                    }
                    #endregion

                    // پر کردن هر تب به همراه محتویات آن
                    tabs.TabList.Add(tabContent);
                }

                ViewBag.PageTitle = linkTable.Title;
                ViewBag.description = linkTable.Description;
                ViewBag.keywords = linkTable.Keywords;
                ViewBag.CustomMetaTags = linkTable.CustomMetaTags;

                if (tabMagInDbList != null) return View(tabs);
                else return null;
            }
            else
            {
                //return RedirectToAction("NoData");
                return HttpNotFound();
            }

        }

        [Route("TourMagazine/")]
        public ActionResult GetTabDetail(int tabId)
        {
            //واکشی tab magazine
            var tabMagInDbList = _unitOfWork.Set<TabMagazine>().FirstOrDefault(x => x.Id == tabId);

            //گروه های آن
            var allTabGroups = tabMagInDbList.Groups.ToList();

            //واکشی پست های مرتبط با هر کدام از این گروه ها + بدون تکرار
            #region پست های مرتبط
            var allRelatedPost = new List<Post>();
            if (allTabGroups != null && allTabGroups.Any())
            {
                allRelatedPost = allTabGroups.SelectMany(x => x.Posts).Distinct().ToList();
            }

            foreach (var p in allRelatedPost)
            {
                var post = _unitOfWork.Set<Post>().Include(x => x.PostImages).Include(x => x.PostGroups).FirstOrDefault(x => x.Id == p.Id);

                var img = new PostImage();
                if (post.PostGroups.Any(x => x.Title == "introducing-hotels"))
                {
                    //اولین تصویر هتل که در جدول پست درج شده بوده
                    img = post.PostImages.FirstOrDefault(x => x.Width == 700 && x.Height == 525);
                    p.ImageUrl = img != null ? img.ImageUrl + "-261x177" + img.ImageExtension : "";
                }
                else
                {
                    //اولین تصویر پست
                    img = post.PostImages.FirstOrDefault(x => x.Width == 277 && x.Height == 186);
                    p.ImageUrl = img != null ? img.ImageUrl + "-277x186" + img.ImageExtension : "";
                }
                //var img = post.PostImages.FirstOrDefault(x => x.Width == 277 && x.Height == 186);
                //if (img == null)
                //{
                //    img = post.PostImages.FirstOrDefault(x => x.Width == 700 && x.Height == 525);
                //    p.ImageUrl = img != null ? img.ImageUrl + "-261x177" + img.ImageExtension : "";
                //}
                //else
                //{
                //    p.ImageUrl = img != null ? img.ImageUrl + "-277x186" + img.ImageExtension : "";
                //}

            }
            TabMagPostListViewModel tabMagPost = new TabMagPostListViewModel();
            tabMagPost.TabMagPostList = allRelatedPost.Join(_unitOfWork.Set<LinkTable>(), jrp => jrp.Id, lt => lt.typeId, (jrp, lt) => new { jrp, lt })
                .Where(x => x.lt.linkType == LinkType.Post && x.lt.Visible && !x.lt.IsDeleted)
                .Select(x => new TabMagPost()
                {
                    //ImageUrl = (from post in _unitOfWork.Set<Post>() where post.Id == x.jrp.Id select post.PostImages.FirstOrDefault(i => i.IsPrimarySlider).ImageUrl).FirstOrDefault(),
                    ImageUrl = x.jrp.ImageUrl,
                    Name = x.jrp.Name,
                    PostSummery = x.jrp.PostSummery,
                    PostUrl = x.lt.URL,
                    Target = x.lt.Target,
                    Rel = x.lt.Rel

                }).ToList();
            #endregion

            //واکشی هتل های مرتبط
            #region هتل های مرتبط
            var allRelatedHotels = new List<Hotel>();
            if (allTabGroups != null && allTabGroups.Any())
            {
                allRelatedHotels = allTabGroups.SelectMany(x => x.Hotels).Distinct().ToList();
            }

            foreach (var h in allRelatedHotels)
            {
                var hotel = _unitOfWork.Set<Hotel>().Include(x => x.HotelGalleries).FirstOrDefault(x => x.Id == h.Id);

                var img = new HotelGallery();
                //اولین تصویر هتل که در جدول هتل درج شده بوده
                img = hotel.HotelGalleries.FirstOrDefault(x => x.Width == 700 && x.Height == 525);
                h.ImageUrl = img != null ? img.ImageUrl + "-261x177" + img.ImageExtension : "";
            }

            TabMagPostListViewModel tabMagHotel = new TabMagPostListViewModel();
            tabMagHotel.TabMagPostList = allRelatedHotels.Join(_unitOfWork.Set<LinkTable>(), jrh => jrh.Id, lt => lt.typeId, (jrh, lt) => new { jrh, lt })
                .Where(x => x.lt.linkType == LinkType.Hotel && x.lt.Visible && !x.lt.IsDeleted)
                .Select(x => new TabMagPost()
                {
                    //ImageUrl = (from post in _unitOfWork.Set<Post>() where post.Id == x.jrp.Id select post.PostImages.FirstOrDefault(i => i.IsPrimarySlider).ImageUrl).FirstOrDefault(),
                    ImageUrl = x.jrh.ImageUrl,
                    Name = x.jrh.Title,
                    PostSummery = x.jrh.Summary,
                    PostUrl = x.lt.URL,
                    Target = x.lt.Target,
                    Rel = x.lt.Rel

                }).ToList();
            #endregion

            TabMagPostListViewModel tabMagAllContent = new TabMagPostListViewModel();
            tabMagAllContent.TabMagPostList.AddRange(tabMagPost.TabMagPostList);
            tabMagAllContent.TabMagPostList.AddRange(tabMagHotel.TabMagPostList);
            tabMagAllContent.TabMagPostList = tabMagAllContent.TabMagPostList.Take(18).ToList();

            //بازپس فرستادن پارشال ویوو
            return PartialView("_PrvTabMagazineContent", tabMagAllContent);
        }
        #endregion

        #region like
        [HttpPost]
        public ActionResult like(int id)
        {

            //var countID = _postService.like(id, "post",Request);
            var post = _postService.GetById(x => x.Id == id);
            //if (post.LikeCount == null)
            //{
            //    post.LikeCount = 1;
            //    List<DataCookie> LikeIdList = new List<DataCookie>();
            //    DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
            //    LikeIdList.Add(newData);

            //    HttpCookie newRateProductIdListCookie = new HttpCookie("LikePost")
            //    {
            //        Value = JsonConvert.SerializeObject(LikeIdList),
            //        Expires = DateTime.Now.AddDays(30d)
            //    };
            //    Response.SetCookie(newRateProductIdListCookie);
            //    _unitOfWork.SaveAllChanges();
            //    return Json(new
            //    {
            //        LikeCount = post.LikeCount,
            //        report="success",

            //    }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //اگر کوکی نبود
            #region noCookie
            if (Request.Cookies["LikePost"] == null)
            {
                //اگر تا به حال کسی لایک نکرده بود
                if (post.LikeCount == null)
                {
                    post.LikeCount = 1;
                    List<DataCookie> LikeIdList = new List<DataCookie>();
                    DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
                    LikeIdList.Add(newData);

                    HttpCookie newRateProductIdListCookie = new HttpCookie("LikePost")
                    {
                        Value = JsonConvert.SerializeObject(LikeIdList),
                        Expires = DateTime.Now.AddDays(30d)
                    };
                    Response.SetCookie(newRateProductIdListCookie);
                    _unitOfWork.SaveAllChanges();
                    return Json(new
                    {
                        LikeCount = post.LikeCount,
                        report = "success",

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    post.LikeCount = post.LikeCount + 1;
                    List<DataCookie> LikeIdList = new List<DataCookie>();
                    DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
                    LikeIdList.Add(newData);

                    HttpCookie newRateProductIdListCookie = new HttpCookie("LikePost")
                    {
                        Value = JsonConvert.SerializeObject(LikeIdList),
                        Expires = DateTime.Now.AddDays(30d)
                    };
                    Response.SetCookie(newRateProductIdListCookie);
                    _unitOfWork.SaveAllChanges();
                    return Json(new
                    {
                        LikeCount = post.LikeCount,
                        report = "success",

                    }, JsonRequestBehavior.AllowGet);
                }

            }
            #endregion
            else
            {
                //تمام محتوای کوکی
                var allLikeIdList = JsonConvert.DeserializeObject<List<DataCookie>>(Request.Cookies["LikePost"].Value).Take(100).ToList();
                //تمام منقضی نشده ها
                //var notExpiredProductIdList = allLikeIdList.Where(pd => pd.RateDate.AddDays(30d) > DateTime.Now).Select((item, i) => new DataCookie() { OwnId = item.OwnId, RateDate = item.RateDate, NewestRate = item.NewestRate }).ToList();

                //اگر پست در لیست  داخل کوکی نـــبود 
                if (!allLikeIdList.Any(pd => pd.OwnId == post.Id))
                {
                    //اگر تا به حال کسی لایک نکرده بود
                    if (post.LikeCount == null)
                    {
                        post.LikeCount = 1;
                        DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
                        allLikeIdList.Add(newData);

                        HttpCookie updateRateProductIdListCookie = new HttpCookie("LikePost")
                        {
                            Value = JsonConvert.SerializeObject(allLikeIdList),
                        };
                        Response.SetCookie(updateRateProductIdListCookie);
                        _unitOfWork.SaveAllChanges();
                        return Json(new
                        {
                            LikeCount = post.LikeCount,
                            report = "success",

                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        post.LikeCount = post.LikeCount + 1;
                        DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now };
                        allLikeIdList.Add(newData);

                        HttpCookie updateRateProductIdListCookie = new HttpCookie("LikePost")
                        {
                            Value = JsonConvert.SerializeObject(allLikeIdList),
                        };
                        Response.SetCookie(updateRateProductIdListCookie);
                        _unitOfWork.SaveAllChanges();
                        return Json(new
                        {
                            LikeCount = post.LikeCount,
                            report = "success",

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        LikeCount = post.LikeCount,
                        report = "likeBefore",

                    }, JsonRequestBehavior.AllowGet);

                }
            }
            //_unitOfWork.SaveAllChanges();
            //return Json(new
            //{
            //    LikeCount = post.LikeCount,
            //    report = "success",

            //}, JsonRequestBehavior.AllowGet);

        }

        //_unitOfWork.SaveAllChanges();            
        //return Json(new
        //{
        //    LikeCount = post.LikeCount,
        //    Title = "امتیازدهی",
        //    Message ="خطا در ثبت امتیاز. لطفا مجددا سعی نمایید"

        //}, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region UpdatePostRate
        [HttpPost]
        public JsonResult UpdatePostRate(UpdateRateViewModel newPostRate)
        {
            var post = _postService.GetById(x => x.Id == newPostRate.Id);

            if (Request.Cookies["RatePostIdList"] == null)
            {
                #region ثبت امتیاز
                if (post != null)
                {
                    decimal RateSum = (post.PostRateAvg * post.PostRateCount);
                    post.PostRateAvg = (RateSum + newPostRate.Rate) / (post.PostRateCount + 1);
                    post.PostRateCount++;
                }

                //ذخیره شناسه پست مزبور و تاریخ امتیاز دادن ، در کوکی
                List<DataCookie> postIdList = new List<DataCookie>();
                DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now, NewestRate = newPostRate.Rate };
                postIdList.Add(newData);

                HttpCookie newRatePostIdListCookie = new HttpCookie("RatePostIdList")
                {
                    Value = JsonConvert.SerializeObject(postIdList),
                    Expires = DateTime.Now.AddDays(30d)
                };
                Response.SetCookie(newRatePostIdListCookie);

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
                var allPostIdList = JsonConvert.DeserializeObject<List<DataCookie>>(Request.Cookies["RatePostIdList"].Value).Take(100);
                //تمام منقضی نشده ها
                var notExpiredPostIdList = allPostIdList.Where(pd => pd.RateDate.AddDays(30d) > DateTime.Now).Select((item, i) => new DataCookie() { OwnId = item.OwnId, RateDate = item.RateDate, NewestRate = item.NewestRate }).ToList();

                //اگر پست در لیست منقضی نشده های داخل کوکی نـــبود 
                if (!notExpiredPostIdList.Any(pd => pd.OwnId == post.Id))
                {
                    #region ثبت امتیاز
                    if (post != null)
                    {
                        decimal RateSum = (post.PostRateAvg * post.PostRateCount);
                        post.PostRateAvg = (RateSum + newPostRate.Rate) / (post.PostRateCount + 1);
                        post.PostRateCount++;
                    }

                    //افزودن به لیست منقضی نشده ها
                    DataCookie newData = new DataCookie() { OwnId = post.Id, RateDate = DateTime.Now, NewestRate = newPostRate.Rate };
                    notExpiredPostIdList.Add(newData);


                    //به روز رسانی کوکی با لیست منقضی نشده های آپدیت شده
                    //کوکی در هر بار با موارد منقضی نشده مقداردهی می شود و 
                    //موارد منقضی شده را نگه نمی دارد
                    HttpCookie updateRatePostIdListCookie = new HttpCookie("RatePostIdList")
                    {
                        Value = JsonConvert.SerializeObject(notExpiredPostIdList),
                    };
                    Response.SetCookie(updateRatePostIdListCookie);

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

        #region GetUpdatedPostRate
        [HttpPost]
        public JsonResult GetUpdatedPostRate(long Id)
        {
            return Json(new
            {
                status = "success",
                data = 5.00
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region NoData : Page 404
        public ActionResult NoData()
        {
            //return PartialView("_PrvHttpNotFound");
            //return HttpNotFound();
            var r = Request;
            Response.StatusCode = 404;
            return View();
        }
        #endregion

        #region Comment
        public ActionResult InitialCommentForm(CommentType commentType, int ownId, int? parentId)
        {
            var loggedInUserId = Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.Id == loggedInUserId);

            ClientCommentViewModel newComment = new ClientCommentViewModel()
            {
                CommentType = commentType,
                OwnId = ownId,
                ParentId = parentId,
                CommentReviews = _unitOfWork.Set<Review>().Where(x => !x.IsDeleted && x.IsActive && commentType == CommentType.Hotel).Select(x => new CommentReviewViewModel() { Title = x.Title, ReviewId = x.Id }).ToList(),

                Email = loggedInUser.Email,
                Name = (loggedInUser.FirstName != null && loggedInUser.LastName != null) ? string.Format("{0} {1}", loggedInUser.FirstName, loggedInUser.LastName) : null
            };
            return PartialView("_PrvNewComment", newComment);
        }

        public ActionResult AddComment(ClientCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = Request["g-recaptcha-response"];
                if (response == null || !ReCaptcha.IsValid(response))
                {
                    return Json("InvalidCaptcha", JsonRequestBehavior.AllowGet);
                }

                var insertedCommentvm = _commentService.Create<ClientCommentViewModel>(commentViewModel);

                var insertedComment = _commentService.GetById(x => x.Id == insertedCommentvm.Id);
                foreach (var item in commentViewModel.CommentReviews)
                {
                    if (item.Rate > 0)
                    {
                        //تعداد کل موارد نقد و بررسی که امتیاز داده شده
                        insertedComment.RateCount += 1;
                        //به روز رسانی امتیاز کل در کامنت
                        insertedComment.Rate = (insertedComment.Rate + item.Rate.Value) / insertedComment.RateCount;
                    }
                }
                _unitOfWork.SaveAllChanges();

                if (insertedCommentvm.Id > 0)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                return Json("UnSuccess", JsonRequestBehavior.AllowGet);
            }
            return Json("InValidData", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LikeComment
        public ActionResult LikeComment(int commentId)
        {
            var comment = _unitOfWork.Set<Comment>().FirstOrDefault(x => x.Id == commentId);
            comment.Like += 1;
            var result = _unitOfWork.SaveAllChanges();
            if (result > 0)
            {
                return Json(commentId.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(commentId.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region DislikeComment
        public ActionResult DislikeComment(int commentId)
        {
            var comment = _unitOfWork.Set<Comment>().FirstOrDefault(x => x.Id == commentId);
            comment.DisLike += 1;
            var result = _unitOfWork.SaveAllChanges();
            if (result > 0)
            {
                return Json(commentId.ToString(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(commentId.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region HasHotel
        public ActionResult HasHotel(string url)
        {
            var linkModel = _unitOfWork.Set<LinkTable>().Where(x => x.URL == url).FirstOrDefault();
            if (linkModel != null)
            {
                var result = (from d in _unitOfWork.Set<Hotel>()
                              join l in _unitOfWork.Set<LinkTable>().AsEnumerable()
                              on d.Id equals l.typeId
                              where l.linkType == LinkType.Hotel && l.Visible == true && l.IsDeleted == false && d.IsDeleted == false && d.IsActive == true && d.PostGroups.Any(x => x.Id == linkModel.typeId && x.IsActive == true && x.IsDeleted == false)
                              select d).ToList();

                return Json(result.Count() > 0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}