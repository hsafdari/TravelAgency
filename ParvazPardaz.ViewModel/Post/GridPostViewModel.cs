using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPostViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Name { get; set; }

        [Display(Name = "LinkTableTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string LinkTableTitle { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Thumbnail { get; set; }

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
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string ImageUrl { get; set; }
        [Display(Name = "PostGroup", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> PostGroups { get; set; }
        [Display(Name = "Tags", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> PostTags { get; set; }
        [Display(Name = "PictureCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PictureCount { get; set; }
        public string PageUrl { get; set; }
        public Nullable<int> LikeCount { get; set; }
    }
}
