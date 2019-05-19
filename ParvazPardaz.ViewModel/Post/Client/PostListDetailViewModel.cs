using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
   public class PostListDetailViewModel
    {
        #region Constructor
        public PostListDetailViewModel()
        {
            HasHotel = false;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// شناسه پست
        /// </summary>
        public int Id { get; set; }

        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Name { get; set; }

        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Thumbnail { get; set; }

        [Display(Name = "PostSummery", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string PostSummery { get; set; }

        [Display(Name = "VisitCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int VisitCount { get; set; }

        [Display(Name = "PostRateAvg", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public decimal PostRateAvg { get; set; }

        [Display(Name = "PostRateCount", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int PostRateCount { get; set; }

        [Display(Name = "PublishDatetime", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public DateTime PublishDatetime { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        public Nullable<int> LikeCount { get; set; }
        [Display(Name = "Thumbnail", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public ImageViewModel Image { get; set; }
        public List<PostGroupViewModel> PostGroups { get; set; }
        #endregion

        #region Has Hotel?
        public bool HasHotel { get; set; }
        #endregion
    }
}
