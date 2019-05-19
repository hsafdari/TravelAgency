using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridSearchLogViewModel:BigBaseViewModelOfEntity
    {
        public GridSearchLogViewModel()
        {

        }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string Browser { get; set; }
    }
}
