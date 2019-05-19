using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class EditTagViewModel : BaseViewModelId
    {
        [RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string Name { get; set; }
    }
}
