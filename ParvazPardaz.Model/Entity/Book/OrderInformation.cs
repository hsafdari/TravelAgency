using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Entity.Common;

namespace ParvazPardaz.Model.Entity.Book
{
    public class OrderInformation : BaseBigEntity
    {
        #region properties
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Age { get; set; }
        public string Tel { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        #endregion

        #region Reference navigation properties
        public virtual Order Order { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Nullable<int> CityId { get; set; }
        public virtual City City { get; set; }
        public Nullable<int> NationalityId { get; set; }
        public virtual Country.Country Country { get; set; }
        #endregion
    }
}
