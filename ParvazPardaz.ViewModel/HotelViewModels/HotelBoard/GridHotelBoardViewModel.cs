using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelBoardViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "TitleFA", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Title { get; set; }

        [Display(Name = "TitleEN", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }
        #endregion
    }
}
