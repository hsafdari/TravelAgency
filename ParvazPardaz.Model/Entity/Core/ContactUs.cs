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
    public class ContactUs : BaseEntity
    {
        #region Constructor
        public ContactUs()
        {

        }
        #endregion

        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Content { get; set; }
        #endregion

        #region Reference Navigation Property
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        #endregion
    }
}
