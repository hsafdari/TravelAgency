using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class Passenger : BaseEntity
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string TicketNo { get; set; }
        public Gender Gender { get; set; }
        public AgeRange AgeRange { get; set; }
        public Nullable<DateTime> Birthdate { get; set; }
        #endregion

        #region En Properties
        public string EnFirstName { get; set; }
        public string EnLastName { get; set; }
        public string PassportNo { get; set; }
        public Nullable<DateTime> PassportExpirationDate { get; set; }
        public EnumNationality NationalityTitle { get; set; }
        #endregion

        #region Reference navigation properties
        public Nullable<int> BirthCountryId { get; set; }
        public virtual ParvazPardaz.Model.Entity.Country.Country BirthCountry { get; set; }

        public Nullable<int> PassportExporterCountryId { get; set; }
        public virtual ParvazPardaz.Model.Entity.Country.Country PassportExporterCountry { get; set; }

        //قبلا در ارتباط با جدول Order بود
        //public long OrderId { get; set; }
        //public virtual Order Order { get; set; }

        /// <summary>
        /// این مسافر در کدام اتاق قرار دارد؟
        /// </summary>
        public int SelectedRoomId { get; set; }
        public virtual SelectedRoom SelectedRoom { get; set; }
        #endregion

        #region Collection navigation property
        public virtual ICollection<SelectedAddServ> SelectedAddServs { get; set; }
        #endregion
    }
}
