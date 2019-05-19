﻿using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class OrderType : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        #endregion

        #region Collection navigation property
        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
