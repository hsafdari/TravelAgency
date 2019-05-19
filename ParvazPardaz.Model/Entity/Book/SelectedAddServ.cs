using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    /// <summary>
    /// SelectedAdditionalService Table
    /// </summary>
    public class SelectedAddServ : BaseEntity
    {
        #region properties
        public string AddServTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal TotalPrice { get; set; }
        #endregion

        #region Reference navigation properties
        public int AddServId { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }

        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        #endregion

        #region Collection navigartion property
        public virtual ICollection<Passenger> Passengers { get; set; }
        #endregion
    }
}
