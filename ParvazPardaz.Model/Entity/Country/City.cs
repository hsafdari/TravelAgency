using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Entity.Country
{
    /// <summary>
    /// شهر
    /// </summary>
    public class City : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام شهر
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نام به زبان انگلیسی
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// مسیر عکسی از شهر
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
        public bool IsDddlFrom { get; set; }
        public bool IsDddlDestination { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual State State { get; set; }
        public int StateId { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Hotel.Hotel> Hotels { get; set; }
        public virtual ICollection<Airport> Airports { get; set; }
        public virtual ICollection<OrderInformation> OrderInformations { get; set; }
        public virtual ICollection<TourLandingPageUrl> TourLandingPageUrls { get; set; }
        #endregion
      
    }
}
