using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.ViewModel
{
    public class FAQClientViewModel:BaseViewModelId
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
