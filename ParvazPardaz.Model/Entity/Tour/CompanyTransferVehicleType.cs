using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class CompanyTransferVehicleType : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام مدل وسیله نقلیه
        /// </summary>
        public string ModelName { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual CompanyTransfer CompanyTransfer{ get; set; }
        public int CompanyTransferId { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public int VehicleTypeId { get; set; }
        #endregion
    }
}
