using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Link
{
    public class DefaultPageConfig : EntityTypeConfiguration<ParvazPardaz.Model.Entity.Link.DefaultPage>
    {
        public DefaultPageConfig()
        {
            ToTable("DefaultPage", "link");
            Property(x => x.typeId).IsRequired();
            Property(x => x.URL).HasMaxLength(400).IsRequired();
            Property(x => x.Name).HasMaxLength(250);
            Property(x => x.Title).HasMaxLength(250);
            Property(x => x.Target).HasMaxLength(50);
            Property(x => x.Rel).HasMaxLength(50);            
            Property(x => x.Visible).IsRequired();
            Property(x => x.Rel);            
            Property(x => x.VisitCount);
            Property(x => x.PostRateAvg);
            Property(x => x.PostRateCount);
            Property(x => x.ImageUrl).HasMaxLength(250);
        }
    }
}
