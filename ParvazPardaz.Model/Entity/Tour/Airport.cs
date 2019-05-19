using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class Airport : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string IataCode { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual City City { get; set; }
        public int CityId { get; set; }
        #endregion
    }
}
