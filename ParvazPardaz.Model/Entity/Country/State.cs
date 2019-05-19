using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Country
{
    /// <summary>
    /// استان
    /// </summary>
    public class State : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام استان
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// مسیر عکسی از استان
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
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<City> Cities { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        #endregion
    }
}
