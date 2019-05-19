using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// خدمات اضافه تور که میتواند شامل ویزا و بیمه و  ... باشد
    /// </summary>
    public class AdditionalService : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام سرویس
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیحات سرویس
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourScheduleAdditionalService> TourScheduleAdditionalServices { get; set; }
        public virtual ICollection<SelectedAddServ> SelectedAddServs { get; set; }
        #endregion
    }
}
