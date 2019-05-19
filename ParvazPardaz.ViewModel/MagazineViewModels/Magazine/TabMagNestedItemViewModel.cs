using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TabMagNestedItemViewModel
    {
        #region Constructor
        public TabMagNestedItemViewModel()
        {
            TabMagChildPostList = new List<TabMagPost>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// عنوان گروه
        /// </summary>
        public string ChildGroupTitle { get; set; }

        /// <summary>
        /// لیستی از تورهایی که مرتبط با این زیر گروه هستند
        /// </summary>
        public List<TabMagPost> TabMagChildPostList { get; set; } 
        #endregion
    }
}
