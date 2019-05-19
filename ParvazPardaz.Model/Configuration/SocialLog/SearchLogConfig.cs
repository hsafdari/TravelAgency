using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.SocialLog
{
    public class SearchLogConfig: EntityTypeConfiguration<ParvazPardaz.Model.Entity.SocialLog.SearchLog>
    {
        public SearchLogConfig()
        {
            ToTable("SearchLog", "log");
            Property(x => x.Title).HasMaxLength(400).IsRequired();
            Property(x => x.Description).HasMaxLength(400);
            Property(x => x.Source).HasMaxLength(200);
            Property(x => x.Browser);
        }
    }
}
