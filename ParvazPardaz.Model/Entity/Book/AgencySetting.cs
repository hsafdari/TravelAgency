using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class AgencySetting : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        public string PrintText { get; set; }
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
        public string ImageFileName { get; set; }
        public long ImageSize { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
