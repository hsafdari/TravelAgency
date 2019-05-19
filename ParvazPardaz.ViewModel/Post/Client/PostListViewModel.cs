using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class PostListViewModel
    {
        public PostListViewModel()
        {
            SearchList = new SearchListViewModel();
        }
        #region Properties
        public IList<PostListDetailViewModel> PostDetail { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Title { get; set; }
        //public DateTime CreatDateTime { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string CustomMetaTags { get; set; }
        public List<PostListViewModel> RelatedPost { get; set; }
        public SearchListViewModel SearchList { get; set; }
        #endregion

        #region related groups
        public IEnumerable<PostGroupViewModel> RelatedGroups { get; set; }
        #endregion

        #region breadCrumbItems
        public List<BreadCrumbsItemViewModel> breadCrumbItems { get; set; }
        #endregion
    }
}
