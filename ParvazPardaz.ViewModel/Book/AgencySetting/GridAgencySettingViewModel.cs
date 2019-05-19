using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridAgencySettingViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Title { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Name { get; set; }

        [Display(Name = "LogoUrl", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string ImageUrl { get; set; }

        [Display(Name = "PrintText", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string PrintText { get; set; }
        #endregion
    }
}
