using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System.Web.Mvc;

namespace ParvazPardaz.Model.Entity.Core
{
    public class Menu : BaseEntity
    {
        #region Properties
        [Display(Name = "MenuTitle", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string MenuTitle { get; set; }

        [Display(Name = "MenuIsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool MenuIsActive { get; set; }

        [Display(Name = "MenuUrl", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string MenuUrl { get; set; }

        [Display(Name = "OrderId", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int OrderId { get; set; }

        [Display(Name = "Target", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Target { get; set; }

        [Display(Name = "MenuType", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public MenuType MenuType { get; set; }

        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string Image { get; set; }

        [Display(Name = "ShortDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [AllowHtml]
        [UIHint("TinyMCE_Modern_Menu")]
        public string ShortDescription { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Menu> MenuChilds { get; set; }
        #endregion

        #region Reference Navigation Properties
        [Display(Name = "MenuParentId", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<int> MenuParentId { get; set; }
        public virtual Menu MenuParent { get; set; }

        [Display(Name = "MenuGroupId", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int MenuGroupId { get; set; }
        public virtual MenuGroup MenuGroup { get; set; }
        #endregion
    }
}
