using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class SearchViewModel
    {
        #region Constructor
        public SearchViewModel()
        {
        } 
        #endregion

        #region Properties
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }
        public string currentUrl { get; set; }
        #endregion
    }
}
