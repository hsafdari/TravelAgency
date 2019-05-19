using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelRuleViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Title { get; set; }
        
        [Display(Name = "Rule", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        [AllowHtml]
        public string Rule { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public bool IsActive { get; set; }
        #endregion
    }
}
