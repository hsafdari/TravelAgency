using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class Task : BaseEntity
    {
        #region Properies
        public string Title { get; set; }

        public bool IsDate { get; set; }
        #endregion

        #region Collection navigartion property
        public virtual ICollection<OrderTask> OrderTasks { get; set; }
        #endregion
    }
}
