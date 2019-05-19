using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.Magazine
{
    public class GridTabMagazineViewModel : BaseViewModelOfEntity
    {
        #region Properties
        [Display(Name = "Location", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string CountryTitle { get; set; }

        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Title { get; set; }

        [Display(Name = "Name", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Name { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool IsActive { get; set; }

        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int Priority { get; set; }

        [Display(Name = "SelectedGroups", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public List<string> SelectedGroupsTitle { get; set; }
        #endregion
    }
}
