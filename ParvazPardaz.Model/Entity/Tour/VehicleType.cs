using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// نوع وسیله نقلیه هر شرکت حمل و نقل : هواپیا، اتوبوس و غیره
    /// </summary>
    public class VehicleType : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نوع وسلیه نقلیه
        /// </summary>
        public string Title { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<CompanyTransferVehicleType> CompanyTransferVehicleTypes{ get; set; }
        public virtual ICollection<VehicleTypeClass> VehicleTypeClasses { get; set; }
        #endregion
    }
}
