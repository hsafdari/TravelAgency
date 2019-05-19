using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ParvazPardaz.Model.Entity.Common;

namespace ParvazPardaz.Model.Entity.Core
{
    public class MenuGroup : BaseEntity
    {
        #region Properties
        public string GroupName { get; set; }
        public bool IsRoot { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Menu> Menus { get; set; }
        #endregion
    }
}