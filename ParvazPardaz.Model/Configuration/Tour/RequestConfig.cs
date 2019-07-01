using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class RequestConfig : EntityTypeConfiguration<Request>
    {
        public RequestConfig()
        {
            ToTable("Requests", "Tour");
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
