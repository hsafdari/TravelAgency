using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ParvazPardaz.Model.Entity.Core
{
    public class SliderGroup : BaseEntity
    {
        #region Properties
        public string GroupTitle { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }
        public int Priority { get; set; }
        #endregion

        #region Collection Navigation Propery
        public ICollection<Slider> Sliders { get; set; }
        #endregion
    }
}
