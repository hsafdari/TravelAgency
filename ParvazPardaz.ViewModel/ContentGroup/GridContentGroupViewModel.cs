using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridContentGroupViewModel : BaseViewModelOfEntity
    {
        #region constructor
        public GridContentGroupViewModel()
        {

        }
        #endregion

        #region properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Title { get; set; }

        [Display(Name = "TitleEN", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string TitleEN { get; set; }
        #endregion
    }
}
