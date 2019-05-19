using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Link
{
    public class LinkTableConfig : EntityTypeConfiguration<ParvazPardaz.Model.Entity.Link.LinkTable>
    {
        public LinkTableConfig()
        {
            ToTable("LinkTable", "link");
            Property(x => x.typeId).IsRequired();
            Property(x => x.URL).IsRequired().HasMaxLength(400);
            Property(x => x.Name).HasMaxLength(400);
            Property(x => x.Title).HasMaxLength(400);
            Property(x => x.Target).HasMaxLength(100);
            Property(x => x.Keywords).HasMaxLength(400);
            Property(x => x.Description).HasMaxLength(400);
            Property(x => x.Visible).IsRequired();
            Property(x => x.Rel).HasMaxLength(100);
            Property(x => x.CustomMetaTags).HasMaxLength(800);
            Property(x => x.VisitCount);
            Property(x => x.PostRateAvg);
            Property(x => x.PostRateCount);
            Property(x => x.Changefreq).IsOptional();
            Property(x => x.Priority).IsOptional();
        }
    }
}
