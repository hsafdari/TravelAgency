using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// شرکت های حمل و نقل
    /// </summary>
    public class CompanyTransfer : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام شرکت مسافربری
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        public string TitleEn { get; set; }
        /// <summary>
        /// کد یاتا
        /// </summary>
        public string IataCode { get; set; }
        /// <summary>
        /// آدرس 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// شماره تلفن شرکت
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// مسیر عکسی از لوگوی شرکت
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس لوگو
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// نام فایل عکس لوگو
        /// </summary>
        public string ImageFileName { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourScheduleCompanyTransfer> TourScheduleCompanyTransfers { get; set; }
        public virtual ICollection<CompanyTransferVehicleType> CompanyTransferVehicleTypes { get; set; }
        public virtual ICollection<SelectedFlight> SelectedFlights { get; set; }
        #endregion
    }
}
