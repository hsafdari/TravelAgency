using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class HotelDetailsViewModel : BaseViewModelId
    {
        #region used in _PrvCarouselSlider
        public string CarouselTitle { get; set; }
        public Nullable<int> CommentCount { get; set; }
        #endregion

        #region Properties

        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Name { get; set; }

        //[Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        //public string Thumbnail { get; set; }

        [Display(Name = "PostSummery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string PostSummery { get; set; }

        [Display(Name = "PostContent", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string PostContent { get; set; }

        [Display(Name = "MetaKeywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "MetaDescription", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "VisitCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int VisitCount { get; set; }

        [Display(Name = "PostRateAvg", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public decimal PostRateAvg { get; set; }

        [Display(Name = "PostRateCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PostRateCount { get; set; }

        [Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public DateTime PublishDatetime { get; set; }

        [Display(Name = "ExpireDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public Nullable<DateTime> ExpireDatetime { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PostSort { get; set; }

        [Display(Name = "AccessLevel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public AccessLevel AccessLevel { get; set; }

        [Display(Name = "IsActiveComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActiveComments { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<ImageSliderViewModel> Images { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Thumbnail { get; set; }

        public string ThumbnailName { get; set; }
        public string HotelRank { get; set; }
        /// <summary>
        /// links
        /// </summary>
        public string URL { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        #endregion

        #region User's Post Rate From Cookie
        /// <summary>
        /// امتیاز پست توسط کاربر جاری ، اگر در کوکی باشد
        /// </summary>
        public decimal CookiePostRate { get; set; }
        #endregion

        #region Comment
        /// <summary>
        /// دیدگاه جدید
        /// </summary>
        public ClientCommentViewModel NewComment { get; set; }

        /// <summary>
        /// لیستی از دیدگاه ها
        /// </summary>
        public List<ClientCommentViewModel> CommentList { get; set; }

        public IEnumerable<PostGroupViewModel> PostGroups { get; set; }
        public IEnumerable<PostGroupViewModel> PostTags { get; set; }
        public IList<PostListDetailViewModel> RelatedPosts { get; set; }
        public IList<PostListDetailViewModel> RelatedTours { get; set; }

        public totalCommentScore CommentTotalScore { get; set; }
        #endregion

        #region GoogleMap
        [Display(Name = "LatLongIframe", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string LatLongIframe { get; set; }
        #endregion

        #region breadCrumbItems
        public List<BreadCrumbsItemViewModel> breadCrumbItems { get; set; }
        #endregion
        public virtual ICollection<HotelGallery> HotelGalleries { get; set; }
    }

    public class totalCommentScore
    {
        public totalCommentScore()
        {
            PersonCount = 0;
            TotalRate = 0;
            TotalCommentReviews = new List<CommentReviewViewModel>();
        }

        public int PersonCount { get; set; }
        public decimal TotalRate { get; set; }
        public List<CommentReviewViewModel> TotalCommentReviews { get; set; }

    }
}
