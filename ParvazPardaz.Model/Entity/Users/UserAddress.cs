using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Users
{
    public class UserAddress : BaseEntity
    {
        #region Properties
        /// <summary>
        /// منزل یا محل کار 
        /// </summary>
        public AddressType AddressName { get; set; }
        /// <summary>
        /// خیابان1
        /// </summary>
        public string Street1 { get; set; }
        /// <summary>
        /// خیابان2
        /// </summary>
        public string Street2 { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// کد پستی
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// آدرس پیش فرض؟
        /// </summary>
        public bool IsDefault { get; set; }
        #endregion

        #region Reference Navigation Properties
        /// <summary>
        /// شناسه پروفایل کاربر
        /// </summary>
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        /// <summary>
        /// شناسه شهر
        /// </summary>
        public int CityId { get; set; }
        public virtual City City { get; set; }
        #endregion
    }
}
