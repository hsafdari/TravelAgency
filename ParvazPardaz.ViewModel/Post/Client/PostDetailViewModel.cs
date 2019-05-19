using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Post;

namespace ParvazPardaz.ViewModel
{
    public class PostDetailViewModel
    {
        #region Properties
        /// <summary>
        /// شناسه پست
        /// </summary>
        public int Id { get; set; }

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

        public DateTime ModifierDateTime { get; set; }

        [Display(Name = "ExpireDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public Nullable<DateTime> ExpireDatetime { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PostSort { get; set; }

        [Display(Name = "AccessLevel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public AccessLevel AccessLevel { get; set; }

        [Display(Name = "IsActiveComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActiveComments { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string ImageUrl { get; set; }

        public Nullable<int> LikeCount { get; set; }

        public string ImageUrl765x535 { get; set; }
        public string Extention765x535 { get; set; }
        public string Name765x535 { get; set; }

        public string ImageUrl575x530 { get; set; }
        public string Extention575x530 { get; set; }
        public string Name575x530 { get; set; }

        public string ImageUrl370x292 { get; set; }
        public string Extention370x292 { get; set; }
        public string Name370x292 { get; set; }

        public string ImageUrl277x186 { get; set; }
        public string Extention277x186 { get; set; }
        public string Name277x186 { get; set; }

        public string ImageUrl98x98 { get; set; }
        public string Extention98x98 { get; set; }
        public string Name98x98 { get; set; }

        



        public string URL { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
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
        #endregion

        #region breadCrumbItems
        public List<BreadCrumbsItemViewModel> breadCrumbItems { get; set; }
        #endregion
        
    }
}
