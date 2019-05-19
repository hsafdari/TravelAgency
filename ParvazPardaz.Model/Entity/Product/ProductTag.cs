using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNameSpace = ParvazPardaz.Model.Entity.Common;

namespace ParvazPardaz.Model.Entity.Product
{
    public class ProductTag : EntityNameSpace.Entity
    {
        #region Property
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// عنوان کلیدواژه
        /// </summary>
        public string Title { get; set; } 
        #endregion

        #region Collection navigation property
        public ICollection<ProductTagProduct> ProductTagProducts { get; set; }
        #endregion
    }
}
