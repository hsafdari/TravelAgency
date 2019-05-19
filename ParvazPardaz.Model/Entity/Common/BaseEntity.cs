using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Common
{
    public abstract class BaseEntity : Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
