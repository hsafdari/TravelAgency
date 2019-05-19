using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class SearchListViewModel
    {

        [Display(Name = "HotelRank", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public int HotelRankId { get; set; }
        public IEnumerable<SelectListItem> HotelRankList { get; set; }
        [Display(Name = "SearchTitle", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string SearchTitle { get; set; }
    }
}
