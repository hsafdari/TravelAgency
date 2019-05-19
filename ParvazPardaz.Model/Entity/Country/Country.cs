using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Magazine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Country
{
    /// <summary>
    /// کشور
    /// </summary>
    public class Country : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام کشور
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نام به زبان انگلیسی
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// مسیر عکسی از کشور
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// متن سئو
        /// </summary>
        public string SeoText { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<State> States { get; set; }

        public virtual ICollection<Book.OrderInformation> OrderInformations { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Passenger> PassportPassengers { get; set; }
        #endregion
    }
}
