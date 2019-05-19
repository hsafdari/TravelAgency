using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class TourPackageDay:BaseEntity
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public ICollection<TourPackage> TourPackages { get; set; }
    }
}
