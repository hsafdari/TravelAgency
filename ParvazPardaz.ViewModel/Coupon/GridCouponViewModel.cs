using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridCouponViewModel : BaseViewModelOfEntity
    {
        #region Properties
          [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string Title { get; set; }
          [Display(Name = "Code", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public string Code { get; set; }
          [Display(Name = "DiscountPercent", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public decimal DiscountPercent { get; set; }
          [Display(Name = "ExpireDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]

        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// کد کاربری اعتبار سنجی شده
        /// </summary>
        [Display(Name = "ValidatedUserId", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]        
        public string ValidatedUser { get; set; }
        /// <summary>
        /// تاریخ اعتبار سنجی شده
        /// </summary>
          [Display(Name = "ValidatedDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        
        public DateTime? ValidatedDate { get; set; }
          [Display(Name = "Id", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]       
        public int? OrderId { get; set; }
        #endregion
    }
}
