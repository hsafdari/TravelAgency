using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class FirstBlogPageViewModel
    {
       
        public IEnumerable<PostDetailViewModel> firstSlider { get; set; }
        public IEnumerable<PostDetailViewModel> secondBox { get; set; }
        public IEnumerable<PostDetailViewModel> BigBox { get; set; }
        public IEnumerable<PostDetailViewModel> middleSlider { get; set; }
        public IEnumerable<PostDetailViewModel> LastUpdate { get; set; }

      
        public string Title { get; set; }
        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }
        
    }
}
