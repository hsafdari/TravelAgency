using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Product
{
    public class ProductTagProduct : BaseEntity
    {
        #region Reference navigation properties
        /// <summary>
        /// شناسه کالا
        /// </summary>
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; }

        /// <summary>
        /// شناسه کلیدواژه کالا
        /// </summary>
        public long ProductTagId { get; set; }
        public virtual ProductTag ProductTag { get; set; } 
        #endregion

    }
}
