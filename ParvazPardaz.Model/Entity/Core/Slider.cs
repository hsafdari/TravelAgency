using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParvazPardaz.Model.Entity.Core
{
    public class Slider : BaseEntity
    {
        #region Properties
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; }
        public string ImageDescription { get; set; }
        public bool ImageIsActive { get; set; }
        public int Priority { get; set; }
        public string NavigationUrl { get; set; }
        public string HeaderDays { get; set; }
        public string NavDescription { get; set; }
        public decimal? Price { get; set; }
        public string footerLine1 { get; set; }
        public string footerLine2 { get; set; }
        public DateTime? Expirationdate { get; set; }
        #endregion

        #region Reference Navigation Property
        public int SliderGroupID { get; set; }
        public virtual SliderGroup SliderGroup { get; set; }
        #endregion
    }
}
