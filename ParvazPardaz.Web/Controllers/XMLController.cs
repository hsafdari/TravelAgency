using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Post;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using ParvazPardaz.Service.Contract.Hotel;
using ParvazPardaz.Service.Contract.Tags;
using System.Text.RegularExpressions;
using ParvazPardaz.Service.Contract.Tour;

namespace ParvazPardaz.Web.Controllers
{
    /// <summary>
    /// یک : در نظر گرفتن روتینگ برای دسترسی به اکشن ایجاد سایتمپ
    /// دو : کدهای ایجاد نقشه سایت در این کنترلر
    /// سه : تنظیم شناسایی نقشه سایت در وب.کانفیگ > سیستم.وب > سایتمپ
    /// </summary>
    public class XMLController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostService _postService;
        private readonly IPostGroupService _postGroupService;
        private readonly IHotelService _hotelService;
        private readonly ITourService _tourService;
        private readonly ITagService _tagService;
        #endregion

        #region	Ctor
        public XMLController(IUnitOfWork unitOfWork, IPostService postService, IPostGroupService postGroupService, IHotelService hotelService, ITagService tagService, ITourService tourService)
        {
            _unitOfWork = unitOfWork;
            _postService = postService;
            _postGroupService = postGroupService;
            _hotelService = hotelService;
            _tagService = tagService;
            _tourService = tourService;
        }
        #endregion

        #region ساخت سایت مپ
        /// <summary>
        /// ایجاد و به روزرسانی : https://keysafar.com/sitemap
        /// </summary>
        [Route("sitemap")]
        public ActionResult SiteMap() //ContentResult
        {
            XmlDocument xmlDoc = new XmlDocument();

            var filesNames = new List<string>();


            #region نقشه سایت برای Post
            //ساخت سایت مپ
            XNamespace xmlns = "https://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xmlnsxsi = "https://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsischemaLocation = "https://www.sitemaps.org/schemas/sitemap/0.9 https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            List<SiteMapViewModel> allPostLinks = new List<SiteMapViewModel>();
            allPostLinks.AddRange(GetPostLinks());
            //allPostLinks.AddRange(GetHotelLinks());
            allPostLinks.AddRange(GetTourLinks());
            allPostLinks.AddRange(GetTourLandingPageUrlLinks());

            var multiPostLinks = allPostLinks.OrderByDescending(x => x.PubDate)
                .Select((item, index) => new { item, index })
                //گروه بندی 1000 آیتمی
                .GroupBy(x => x.index / 1000)
                //لیستی از آیتم ها در هر گروه
                .Select(x => x.Select(s => s.item).ToList())
                //گروه های 1000 آیتمی
                .ToList();

            foreach (var PostLinks in multiPostLinks)
            {
                var sitemapPost = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(xmlns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
                    new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
                    from item in PostLinks
                    select
                    new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", "https://keysafar.com" + item.Link)
                        //new XElement(xmlns + "lastmod", string.Format("{0}-{1}-{2}", item.PubDate.Value.Year, item.PubDate.Value.Month, item.PubDate.Value.Day)),
                        //(item.Changefreq != null ? new XElement(xmlns + "changefreq", item.Changefreq) : null),
                        //(item.Priority != null ? new XElement(xmlns + "priority", item.Priority) : null)
                      )
                    )
                );

                var i = multiPostLinks.IndexOf(PostLinks) + 1;
                string indexStr = Convert.ToString(i);

                //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
                if (!System.IO.File.Exists(Server.MapPath("~/post-sitemap" + indexStr + ".xml")))
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/post-sitemap" + indexStr + ".xml"), "");
                }

                //ذخیره در فایل
                sitemapPost.Save(Server.MapPath("~/post-sitemap" + indexStr + ".xml"));
                //filesNames.Add("/post-sitemap" + indexStr + ".xml/");
                filesNames.Add("/post-sitemap" + indexStr + ".xml");
            }

            #endregion

            #region نقشه سایت برای Hotel
            //ساخت سایت مپ
            //XNamespace xmlns = "https://www.sitemaps.org/schemas/sitemap/0.9";
            //XNamespace xmlnsxsi = "https://www.w3.org/2001/XMLSchema-instance";
            //XNamespace xsischemaLocation = "https://www.sitemaps.org/schemas/sitemap/0.9 https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            List<SiteMapViewModel> allHotelLinks = new List<SiteMapViewModel>();
            allHotelLinks.AddRange(GetHotelLinks());

            var multiHotelLinks = allHotelLinks.OrderByDescending(x => x.PubDate)
                .Select((item, index) => new { item, index })
                //گروه بندی 1000 آیتمی
                .GroupBy(x => x.index / 1000)
                //لیستی از آیتم ها در هر گروه
                .Select(x => x.Select(s => s.item).ToList())
                //گروه های 1000 آیتمی
                .ToList();

            foreach (var hotelLinks in multiHotelLinks)
            {
                var sitemapHotel = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(xmlns + "urlset",
                    new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
                    new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
                    from item in hotelLinks
                    select
                    new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", "https://keysafar.com" + item.Link)
                        //new XElement(xmlns + "lastmod", string.Format("{0}-{1}-{2}", item.PubDate.Value.Year, item.PubDate.Value.Month, item.PubDate.Value.Day)),
                        //(item.Changefreq != null ? new XElement(xmlns + "changefreq", item.Changefreq) : null),
                        //(item.Priority != null ? new XElement(xmlns + "priority", item.Priority) : null)
                      )
                    )
                );

                var i = multiHotelLinks.IndexOf(hotelLinks) + 1;
                string indexStr = Convert.ToString(i);

                //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
                if (!System.IO.File.Exists(Server.MapPath("~/hotel-sitemap" + indexStr + ".xml")))
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/hotel-sitemap" + indexStr + ".xml"), "");
                }

                //ذخیره در فایل
                sitemapHotel.Save(Server.MapPath("~/hotel-sitemap" + indexStr + ".xml"));
                filesNames.Add("/hotel-sitemap" + indexStr + ".xml");
            }

            #endregion

            #region نقشه سایت برای Tour -- تورها به سایت مپ پست انتقال یافت
            ////ساخت سایت مپ
            ////XNamespace xmlns = "https://www.sitemaps.org/schemas/sitemap/0.9";
            ////XNamespace xmlnsxsi = "https://www.w3.org/2001/XMLSchema-instance";
            ////XNamespace xsischemaLocation = "https://www.sitemaps.org/schemas/sitemap/0.9 https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            //List<SiteMapViewModel> alltourLinks = new List<SiteMapViewModel>();
            //alltourLinks.AddRange(GetTourLinks());

            //var multitourLinks = alltourLinks.OrderByDescending(x => x.PubDate)
            //    .Select((item, index) => new { item, index })
            //    //گروه بندی 1000 آیتمی
            //    .GroupBy(x => x.index / 1000)
            //    //لیستی از آیتم ها در هر گروه
            //    .Select(x => x.Select(s => s.item).ToList())
            //    //گروه های 1000 آیتمی
            //    .ToList();

            //foreach (var tourLinks in multitourLinks)
            //{
            //    var sitemaptour = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
            //    new XElement(xmlns + "urlset",
            //        new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
            //        new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
            //        from item in tourLinks
            //        select
            //        new XElement(xmlns + "url",
            //            new XElement(xmlns + "loc", "https://keysafar.com" + item.Link)
            //            //new XElement(xmlns + "lastmod", string.Format("{0}-{1}-{2}", item.PubDate.Value.Year, item.PubDate.Value.Month, item.PubDate.Value.Day)),
            //            //(item.Changefreq != null ? new XElement(xmlns + "changefreq", item.Changefreq) : null),
            //            //(item.Priority != null ? new XElement(xmlns + "priority", item.Priority) : null)
            //          )
            //        )
            //    );

            //    var i = multitourLinks.IndexOf(tourLinks) + 1;
            //    string indexStr = Convert.ToString(i);

            //    //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
            //    if (!System.IO.File.Exists(Server.MapPath("~/tour-sitemap" + indexStr + ".xml")))
            //    {
            //        System.IO.File.WriteAllText(Server.MapPath("~/tour-sitemap" + indexStr + ".xml"), "");
            //    }

            //    //ذخیره در فایل
            //    sitemaptour.Save(Server.MapPath("~/tour-sitemap" + indexStr + ".xml"));
            //    filesNames.Add("/tour-sitemap" + indexStr + ".xml");
            //}

            #endregion

            #region نقشه سایت برای Tag
            //ساخت سایت مپ
            //XNamespace xmlns = "https://www.sitemaps.org/schemas/sitemap/0.9";
            //XNamespace xmlnsxsi = "https://www.w3.org/2001/XMLSchema-instance";
            //XNamespace xsischemaLocation = "https://www.sitemaps.org/schemas/sitemap/0.9 https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            var multiTagLinks = GetTagLinks().Select((item, index) => new { item, index })
                //گروه بندی 1000 آیتمی
                .GroupBy(x => x.index / 1000)
                //لیستی از آیتم ها در هر گروه
                .Select(x => x.Select(s => s.item).ToList())
                //گروه های 1000 آیتمی
                .ToList();

            foreach (var tagLinks in multiTagLinks)
            {
                var sitemapTag = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
               new XElement(xmlns + "urlset",
                   new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
                   new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
                   from item in tagLinks
                   select
                   new XElement(xmlns + "url",
                       new XElement(xmlns + "loc", "https://keysafar.com" + item.Link)
                       //new XElement(xmlns + "lastmod", string.Format("{0}-{1}-{2}", item.PubDate.Value.Year, item.PubDate.Value.Month, item.PubDate.Value.Day)),
                       //(item.Changefreq != null ? new XElement(xmlns + "changefreq", item.Changefreq) : null),
                       //(item.Priority != null ? new XElement(xmlns + "priority", item.Priority) : null)
                     )
                   )
               );

                var i = multiTagLinks.IndexOf(tagLinks) + 1;
                string indexStr = Convert.ToString(i);

                //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
                if (!System.IO.File.Exists(Server.MapPath("~/tag-sitemap" + indexStr + ".xml")))
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/tag-sitemap" + indexStr + ".xml"), "");
                }

                //ذخیره در فایل
                sitemapTag.Save(Server.MapPath("~/tag-sitemap" + indexStr + ".xml"));
                //filesNames.Add("/tag-sitemap" + indexStr + ".xml/");
                filesNames.Add("/tag-sitemap" + indexStr + ".xml");
            }
            #endregion

            #region نقشه سایت برای PostGroup
            //ساخت سایت مپ
            //XNamespace xmlns = "https://www.sitemaps.org/schemas/sitemap/0.9";
            //XNamespace xmlnsxsi = "https://www.w3.org/2001/XMLSchema-instance";
            //XNamespace xsischemaLocation = "https://www.sitemaps.org/schemas/sitemap/0.9 https://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd";

            var multiPostGroupLinks = GetPostGroupLinks().Select((item, index) => new { item, index })
                //گروه بندی 1000 آیتمی
                .GroupBy(x => x.index / 1000)
                //لیستی از آیتم ها در هر گروه
                .Select(x => x.Select(s => s.item).ToList())
                //گروه های 1000 آیتمی
                .ToList();

            foreach (var postGroupLinks in multiPostGroupLinks)
            {
                var sitemapPostGroup = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
               new XElement(xmlns + "urlset",
                   new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
                   new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
                   from item in postGroupLinks
                   select
                   new XElement(xmlns + "url",
                       new XElement(xmlns + "loc", "https://keysafar.com" + item.Link)
                       //new XElement(xmlns + "lastmod", string.Format("{0}-{1}-{2}", item.PubDate.Value.Year, item.PubDate.Value.Month, item.PubDate.Value.Day)),
                       //(item.Changefreq != null ? new XElement(xmlns + "changefreq", item.Changefreq) : null),
                       //(item.Priority != null ? new XElement(xmlns + "priority", item.Priority) : null)
                     )
                   )
               );

                var i = multiPostGroupLinks.IndexOf(postGroupLinks) + 1;
                string indexStr = Convert.ToString(i);

                //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
                if (!System.IO.File.Exists(Server.MapPath("~/postgroup-sitemap" + indexStr + ".xml")))
                {
                    System.IO.File.WriteAllText(Server.MapPath("~/postgroup-sitemap" + indexStr + ".xml"), "");
                }

                //ذخیره در فایل
                sitemapPostGroup.Save(Server.MapPath("~/postgroup-sitemap" + indexStr + ".xml"));
                //filesNames.Add("/postgroup-sitemap" + indexStr + ".xml/");
                filesNames.Add("/postgroup-sitemap" + indexStr + ".xml");
            }
            #endregion

            #region گردآوری آدرس فایل های ایجاد شده در یک فایل
            var allSitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
               new XElement(xmlns + "urlset",
                   new XAttribute(XNamespace.Xmlns + "xsi", xmlnsxsi),
                   new XAttribute(xmlnsxsi + "schemaLocation", xsischemaLocation),
                   from item in filesNames
                   select
                   new XElement(xmlns + "url",
                       new XElement(xmlns + "loc", "https://keysafar.com" + item)
                       //new XElement("changefreq", "always"),
                       //new XElement("lastmod", item.PubDate),
                       //new XElement("priority", "1")
                     )
                   )
               );

            //اگر فایل ایکس.ام.ال وجود ندارد ایجاد کن
            if (!System.IO.File.Exists(Server.MapPath("~/sitemap.xml")))
            {
                System.IO.File.WriteAllText(Server.MapPath("~/sitemap.xml"), "");
            }

            // All elements with an empty namespace...
            foreach (var node in allSitemap.Root.Descendants()
                                    .Where(n => n.Name.NamespaceName == ""))
            {
                node.Attributes("xmlns").Remove();
            }

            //ذخیره در فایل
            allSitemap.Save(Server.MapPath("~/sitemap.xml"));

            #endregion

            //بازگرداندن همان متن ذخیره شده
            //return Content(sitemap.ToString(), "text/xml");
            return Redirect("/sitemap.xml");
        }
        #endregion

        #region گرفتن لینک ها
        /// <summary>
        /// گرفتن تمام آیتم هایی که قرار است در نقشه ی سایت اضافه شوند
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetLinks()
        {
            //post's groups
            #region post group
            var siteMapPostGroups = _postGroupService.GetAll()
                    .Join(_unitOfWork.Set<LinkTable>(), pg => pg.Id, lt => lt.typeId, (pg, lt) => new { pg, lt })
                    .Where(w => w.lt.linkType == LinkType.PostGroup && !w.lt.IsDeleted && w.lt.Visible)
                    .Select(s => new SiteMapViewModel()
                    {
                        Id = s.pg.Id,
                        Title = s.pg.Name,
                        Description = s.pg.Title,
                        PubDate = s.pg.ModifierDateTime != null ? s.pg.ModifierDateTime : s.pg.CreatorDateTime,
                        Link = s.lt.URL

                    }).ToList();
            #endregion

            //posts
            #region posts
            var siteMapPosts = _postService.GetAll()
                    .Join(_unitOfWork.Set<LinkTable>(), p => p.Id, lt => lt.typeId, (p, lt) => new { p, lt })
                    .Where(w => w.lt.linkType == LinkType.Post && !w.lt.IsDeleted && w.lt.Visible)
                    .Select(s => new SiteMapViewModel()
                    {
                        Id = s.p.Id,
                        Title = s.p.Name,
                        Description = s.p.PostSummery,
                        PubDate = s.p.ModifierDateTime != null ? s.p.ModifierDateTime : s.p.CreatorDateTime,
                        Link = s.lt.URL

                    }).ToList();
            #endregion

            //hotels
            #region Hotels
            var siteMapHotels = _hotelService.GetAll()
                .Join(_unitOfWork.Set<LinkTable>(), h => h.Id, lt => lt.typeId, (h, lt) => new { h, lt })
                .Where(w => w.lt.linkType == LinkType.Hotel && !w.lt.IsDeleted && w.lt.Visible)
                 .Select(s => new SiteMapViewModel()
                 {
                     Id = s.h.Id,
                     Title = s.h.Title,
                     Description = s.h.Summary,
                     PubDate = s.h.ModifierDateTime != null ? s.h.ModifierDateTime : s.h.CreatorDateTime,
                     Link = s.lt.URL

                 }).ToList();
            #endregion

            //gathered post's groups, posts and hotels.
            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapPostGroups);
            siteMapAllElements.AddRange(siteMapPosts);
            siteMapAllElements.AddRange(siteMapHotels);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های پست برای سایت مپ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetPostLinks()
        {
            //posts
            #region posts
            var siteMapPosts = _postService.GetAll()
                    .Join(_unitOfWork.Set<LinkTable>(), p => p.Id, lt => lt.typeId, (p, lt) => new { p, lt })
                    .Where(w => w.lt.linkType == LinkType.Post && !w.lt.IsDeleted && w.lt.Visible)
                    //.OrderByDescending(o => o.p.ModifierDateTime)
                    //.OrderByDescending(o => o.p.CreatorDateTime)
                    .Select(s => new SiteMapViewModel()
                    {
                        Id = s.p.Id,
                        Title = s.p.Name,
                        Description = s.p.PostSummery,
                        PubDate = s.p.ModifierDateTime != null ? s.p.ModifierDateTime : s.p.CreatorDateTime,
                        Link = s.lt.URL,
                        Changefreq = s.lt.Changefreq,
                        Priority = s.lt.Priority

                    }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapPosts);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های هتل برای سایت مپ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetHotelLinks()
        {
            //hotels
            #region Hotels
            var siteMapHotels = _hotelService.GetAll()
                .Join(_unitOfWork.Set<LinkTable>(), h => h.Id, lt => lt.typeId, (h, lt) => new { h, lt })
                .Where(w => w.lt.linkType == LinkType.Hotel && !w.lt.IsDeleted && w.lt.Visible)
                //.OrderByDescending(o => o.h.ModifierDateTime)
                //.OrderByDescending(o => o.h.CreatorDateTime)
                 .Select(s => new SiteMapViewModel()
                 {
                     Id = s.h.Id,
                     Title = s.h.Title,
                     Description = s.h.Summary,
                     PubDate = s.h.ModifierDateTime != null ? s.h.ModifierDateTime : s.h.CreatorDateTime,
                     Link = s.lt.URL,
                     Changefreq = s.lt.Changefreq,
                     Priority = s.lt.Priority

                 }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            //gathered post's groups, posts and hotels.
            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapHotels);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های هتل برای سایت مپ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetTourLinks()
        {
            //Tours
            #region Tours
            var siteMapTours = _tourService.GetAll()
                .Join(_unitOfWork.Set<LinkTable>(), t => t.Id, lt => lt.typeId, (t, lt) => new { t, lt })
                .Where(w => w.lt.linkType == LinkType.TourLanding && !w.lt.IsDeleted && w.lt.Visible) //LinkType.Tour
                //.OrderByDescending(o => o.t.ModifierDateTime)
                //.OrderByDescending(o => o.t.CreatorDateTime)                
                 .Select(s => new SiteMapViewModel()
                 {
                     Id = s.t.Id,
                     Title = s.t.Title,
                     Description = s.t.ShortDescription,
                     PubDate = s.t.ModifierDateTime != null ? s.t.ModifierDateTime : s.t.CreatorDateTime,
                     Link = s.lt.URL,
                     Changefreq = s.lt.Changefreq,
                     Priority = s.lt.Priority

                 }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            //gathered post's groups, posts and hotels.
            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapTours);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های آدرس لندینگ پیج تور
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetTourLandingPageUrlLinks()
        {
            //آدرس لندینگ پیج تور
            #region آدرس لندینگ پیج تور
            var siteMapTourLandingPageUrls = _tourService.GetAll()
                .Join(_unitOfWork.Set<LinkTable>(), t => t.Id, lt => lt.typeId, (t, lt) => new { t, lt })
                .Where(w => w.lt.linkType == LinkType.TabMagazine && !w.lt.IsDeleted && w.lt.Visible)
                //.OrderByDescending(o => o.t.ModifierDateTime)
                //.OrderByDescending(o => o.t.CreatorDateTime)                
                 .Select(s => new SiteMapViewModel()
                 {
                     Id = s.t.Id,
                     Title = s.t.Title,
                     Description = s.t.ShortDescription,
                     PubDate = s.t.ModifierDateTime != null ? s.t.ModifierDateTime : s.t.CreatorDateTime,
                     Link = s.lt.URL,
                     Changefreq = s.lt.Changefreq,
                     Priority = s.lt.Priority

                 }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            //gathered post's groups, posts and hotels.
            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapTourLandingPageUrls);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های پست گروپ برای سایت مپ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetPostGroupLinks()
        {
            //post's groups
            #region post group
            var siteMapPostGroups = _postGroupService.GetAll()
                    .Join(_unitOfWork.Set<LinkTable>(), pg => pg.Id, lt => lt.typeId, (pg, lt) => new { pg, lt })
                    .Where(w => w.lt.linkType == LinkType.PostGroup && !w.lt.IsDeleted && w.lt.Visible && !w.pg.Title.Equals("Discount") && !w.pg.PostGroupParent.Title.Equals("Discount") && !w.pg.Title.Equals("tours-discount") && !w.pg.PostGroupParent.Title.Equals("tours-discount"))
                    //.OrderByDescending(o => o.pg.ModifierDateTime)
                    //.OrderByDescending(o => o.pg.CreatorDateTime)
                    .Select(s => new SiteMapViewModel()
                    {
                        Id = s.pg.Id,
                        Title = s.pg.Name,
                        Description = s.pg.Title,
                        PubDate = s.pg.ModifierDateTime != null ? s.pg.ModifierDateTime : s.pg.CreatorDateTime,
                        Link = s.lt.URL,
                        Changefreq = s.lt.Changefreq,
                        Priority = s.lt.Priority

                    }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            //gathered post's groups, posts and hotels.
            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapPostGroups);

            return siteMapAllElements;
        }

        /// <summary>
        /// لینک های تگ برای سایت مپ
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteMapViewModel> GetTagLinks()
        {
            //tags
            #region tags
            var siteMapTags = _tagService.GetAll()
                    .Join(_unitOfWork.Set<LinkTable>(), t => t.Id, lt => lt.typeId, (t, lt) => new { t, lt })
                    .Where(w => w.lt.linkType == LinkType.PostTag && !w.lt.IsDeleted && w.lt.Visible)
                    //.OrderByDescending(o => o.t.ModifierDateTime)
                    //.OrderByDescending(o => o.t.CreatorDateTime)
                    .Select(s => new SiteMapViewModel()
                    {
                        Id = s.t.Id,
                        Title = s.t.Name,
                        Description = s.t.Name,
                        PubDate = s.t.ModifierDateTime != null ? s.t.ModifierDateTime : s.t.CreatorDateTime,
                        Link = s.lt.URL,
                        Changefreq = s.lt.Changefreq,
                        Priority = s.lt.Priority

                    }).ToList().OrderByDescending(x => x.PubDate);
            #endregion

            var siteMapAllElements = new List<SiteMapViewModel>();
            siteMapAllElements.AddRange(siteMapTags);

            return siteMapAllElements;
        }
        #endregion
    }
}