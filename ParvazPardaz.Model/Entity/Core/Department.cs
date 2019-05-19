using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model
{
    public class Department : BaseEntity
    {
        #region Constructor
        public Department()
        {

        } 
        #endregion

        #region Properies
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public string DepartmentEmail { get; set; }
        public bool IsActive { get; set; } 
        #endregion

        #region Collection navigation
        public virtual ICollection<ContactUs> ContactUses { get; set; }
        //public virtual ICollection<EntityRequest> Request { get; set; }
        #endregion
    }
}
