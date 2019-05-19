using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelRule : BaseEntity
    {
        #region Properties
        public string Title { get; set; }

        public string Rule { get; set; }

        public bool IsActive { get; set; }
        #endregion
    }
}
