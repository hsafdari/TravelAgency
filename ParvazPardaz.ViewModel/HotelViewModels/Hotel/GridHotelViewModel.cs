using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام هتل
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Title { get; set; }
        /// <summary>
        /// فعال؟
        /// </summary>
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActive { get; set; }

        /// <summary>
        /// کلیدواژه ها
        /// </summary>
        [Display(Name = "Tags", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> HotelTags { get; set; }

        /// <summary>
        /// گروه ها
        /// </summary>
        [Display(Name = "PostGroup", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public List<string> PostGroups { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string ImageUrl { get; set; }

        [Display(Name = "ServiceDesc", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Display(Name = "PostSummery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Summary { get; set; }

        [Display(Name = "MetaKeywords", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string MetaKeywords { get; set; }

        [Display(Name = "MetaDescription", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string MetaDescription { get; set; }

        [Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public DateTime PublishDatetime { get; set; }

        [Display(Name = "SortOrder", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int Sort { get; set; }

        [Display(Name = "IsActiveComments", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public bool IsActiveComments { get; set; }

        [Display(Name = "PictureCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PictureCount { get; set; }

        /// <summary>
        /// موقعیت هتل
        /// </summary>
        [Display(Name = "HotelLocation", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Location { get; set; }

        /// <summary>
        /// آدرس هتل
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Address { get; set; }
        /// <summary>
        /// تلفن هتل
        /// </summary>
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Tel { get; set; }
        /// <summary>
        /// هتل چند ستاره است؟
        /// </summary>
        [Display(Name = "HotelRate", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Rate { get; set; }
        /// <summary>
        /// شهر هتل
        /// </summary>
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string CityTitle { get; set; }
        [Display(Name = "HotelRule", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string HotelRule { get; set; }
        [Display(Name = "CancelationPolicy", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string CancelationPolicy { get; set; }

    }
}
