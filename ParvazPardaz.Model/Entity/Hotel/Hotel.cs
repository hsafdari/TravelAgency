using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Country;
using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Model.Entity.Post;
using EntityType = ParvazPardaz.Model.Entity.Comment;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Entity.Hotel
{
    /// <summary>
    /// هتل
    /// </summary>
    public class Hotel : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام هتل
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// موقعیت هتل
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// آدرس هتل
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// تلفن هتل
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// آدرس وبسایت هتل
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// نقشه گوگل : آی-فریم طول و عرض جغرافیایی
        /// </summary>
        public string LatLongIframe { get; set; }
        /// <summary>
        /// آیا هتل به طور خلاصه ایجاد شده است؟
        /// </summary>
        public bool IsSummary { get; set; }
        public string HotelRule { get; set; }
        public string CancelationPolicy { get; set; }
        #endregion

        #region from post properties
        public string Summary { get; set; }
        //public string Content { get; set; } => Description in Hotel
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public int VisitCount { get; set; }
        public decimal PostRateAvg { get; set; }
        public int PostRateCount { get; set; }
        public DateTime PublishDatetime { get; set; }
        public Nullable<DateTime> ExpireDatetime { get; set; }
        public int Sort { get; set; }
        public ParvazPardaz.Model.Enum.AccessLevel AccessLevel { get; set; }
        public bool IsActiveComments { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// مسیر عکس 
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

        #region from post relations
        //public virtual ICollection<HotelGroup> HotelGroups { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        //public virtual HotelPackage HotelPackage { get; set; }
        public Nullable<int> HotelPackageId { get; set; }

        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<PostGroup> PostGroups { get; set; }
        public virtual ICollection<TourScheduleHotel> TourScheduleHotels { get; set; }
        public virtual ICollection<HotelFacility> HotelFacilities { get; set; }
        public virtual ICollection<HotelGallery> HotelGalleries { get; set; }
        //public virtual ICollection<HotelPackage> HotelPackages { get; set; } //باید بعدا برداشته شود
        public virtual ICollection<HotelPackageHotel> HotelPackageHotels { get; set; }
        
        public virtual ICollection<SelectedHotel> SelectedHotels { get; set; }

        #endregion

        #region Reference Navigation Properties
        public virtual HotelRank HotelRank { get; set; }
        public int HotelRankId { get; set; }
        public virtual City City { get; set; }
        public int CityId { get; set; }
        #endregion
    }
}
