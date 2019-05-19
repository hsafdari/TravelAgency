using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class SearchResultViewModel
    {
        #region Constructor
        public SearchResultViewModel()
        {
            PageIndex = 0;
            PageSize = 9;
            IsBeHiddenBtnMore = false;
            //PostListViewModel = new PostListViewModel();
        }
        #endregion

        #region Paggination properties
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool IsBeHiddenBtnMore { get; set; }
        #endregion

        #region Searching properties
        public string Title { get; set; }
        public string q { get; set; }
        public string currentUrl { get; set; }
        #endregion

        #region Search result properties
        public PostListViewModel PostListViewModel { get; set; }
        public List<TabMagPost> TourList { get; set; }
        public List<TabMagPost> MagItemList { get; set; }
        #endregion
    }
}
