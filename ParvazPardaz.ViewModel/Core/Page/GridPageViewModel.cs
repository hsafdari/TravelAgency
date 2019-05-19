using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridPageViewModel : BaseViewModelOfEntity
    {
        [Display(Name = "PageTitle", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string PageTitle { get; set; }

        [Display(Name = "PageName", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string PageName { get; set; }

        [Display(Name = "PageHeaderImg", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string PageHeaderImg { get; set; }

        [Display(Name = "PageDesc", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string PageDesc { get; set; }

        //public string PageContent { get; set; }

        //public string pageMetatages { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool PageIsActive { get; set; }

        [Display(Name = "PageDatetime", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<System.DateTime> PageDatetime { get; set; }

        //public Nullable<int> PageUserId { get; set; }

        //public bool IsActiveComments { get; set; }
    }
}
