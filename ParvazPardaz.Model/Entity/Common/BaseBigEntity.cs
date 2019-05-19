using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Common
{
    public abstract class BaseBigEntity : Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
