using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.SocialLog
{
    public class SocialLogConfig : EntityTypeConfiguration<ParvazPardaz.Model.Entity.SocialLog.SocialLog>
    {
        public SocialLogConfig()
        {
            ToTable("SocialLog", "log");
            Property(x => x.TypeId).IsRequired();
            Property(x => x.LinkType).IsRequired();
        }
    }
}
