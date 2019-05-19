using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class CorporatesViewModel
    {
        public CorporatesViewModel()
        {
            this.Sliders = new List<SlidersUIViewModel>();
        }
        public string Title { get; set; }
        public string Name { get; set; }
        public List<SlidersUIViewModel> Sliders { get; set; }
    }
}
