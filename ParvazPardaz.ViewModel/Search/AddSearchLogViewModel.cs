using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class AddSearchLogViewModel : BaseViewModelBigId
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string Browser { get; set; }
    }
}
