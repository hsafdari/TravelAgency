using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// تور
    /// </summary>
    public class Tour : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان تور
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// عنوان تور
        /// </summary>
        public string LinkTableTitle { get; set; }
        /// <summary>
        /// نشان میدهد که تور از سمت آژانس توصیه میشود ؟
        /// </summary>
        public bool Recomended { get; set; }
        /// <summary>
        /// توضیح تور
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// برای نمایش در صفحه اول
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// کد تور  به صورت کدی منحصر به فرد در تور تعریف گردد این کد به صورت حروف و اعداد می باشد که با کد عددی زمابندی جمع می شود
        /// شاید این کد منحصر به فرد را در سیستم حسابداری خود وارد نمایند که درآمدشان را محاسبه کند
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public Nullable<int> Priority { get; set; }
        #endregion

        #region Collection Navigation Properties
        //public virtual ICollection<TourSchedule> TourSchedules { get; set; }
        public virtual ICollection<TourProgram> TourPrograms { get; set; }
        public virtual ICollection<TourType> TourTypes { get; set; }
        public virtual ICollection<TourLevel> TourLevels { get; set; }
        public virtual ICollection<TourCategory> TourCategories { get; set; }
        public virtual ICollection<TourAllowBanned> TourAllowBans { get; set; }
        public virtual ICollection<TourSlider> TourSliders { get; set; }
        public virtual ICollection<FAQQuestion> FAQQuestions { get; set; }
        public virtual ICollection<FAQ> FAQs { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
        public virtual ICollection<PostGroup> PostGroups { get; set; }
        public virtual ICollection<RequiredDocument> RequiredDocuments { get; set; }
        #endregion

        #region Reference navigation property
        //public virtual TourLandingPageUrl TourLandingPageUrl { get; set; }
        public Nullable<int> TourLandingPageUrlId { get; set; }
        #endregion
    }
}
