using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class AddTagViewModel : BaseViewModelId
    {
        [Remote("CheckURL", "Post")]
        [RegularExpression(@"^[^<>{}\[\],*?+\\/.؟]+$", ErrorMessage = "کاراکتر ممنوعه")]
        public string Name { get; set; }
    }
}
