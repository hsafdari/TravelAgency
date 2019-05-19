using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class TabMagPostListViewModel
    {
        public TabMagPostListViewModel()
        {
            TabMagPostList = new List<TabMagPost>();
        }
        public List<TabMagPost> TabMagPostList { get; set; }
    }

    public class TabMagPost {
        public TabMagPost()
        {

        }
        public string ImageUrl { get; set; }
        public string PostUrl { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        public string Name { get; set; }
        public string PostSummery { get; set; }
    }
}
