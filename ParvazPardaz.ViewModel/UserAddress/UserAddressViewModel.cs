using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class UserAddressViewModel
    {
        #region Properties
        /// <summary>
        /// شناسه پروفایل کاربر
        /// </summary>
        public int UserProfileId { get; set; }
        /// <summary>
        /// منزل یا محل کار 
        /// </summary>
        [Display(Name = "AddressName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public AddressType AddressName { get; set; }
        /// <summary>
        /// خیابان1
        /// </summary>
        [Display(Name = "Street1", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Street1 { get; set; }
        /// <summary>
        /// خیابان2
        /// </summary>
        [Display(Name = "Street2", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Street2 { get; set; }

        /// <summary>
        /// کد پستی
        /// </summary>
        [Display(Name = "ZipCode", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string ZipCode { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        [Display(Name = "Telephone", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Telephone { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        [Display(Name = "Fax", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Fax { get; set; }
        /// <summary>
        /// آدرس پیش فرض؟
        /// </summary>
        [Display(Name = "IsDefault", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public bool IsDefault { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Address { get; set; }
        #endregion

        #region Reference Navigation Property
        /// <summary>
        /// شناسه شهر
        /// </summary>
        [Display(Name = "City", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public int CityId { get; set; }
        #endregion
    }
}
