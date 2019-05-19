using ParvazPardaz.Model.Entity.SocialLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.SocialLog
{
    public class UserLogConfig : EntityTypeConfiguration<UserLog>
    {
        public UserLogConfig()
        {
            ToTable("UserLogs", "log");
            HasKey(x => x.Id);
            Property(x => x.UserId).IsRequired();
            Property(x => x.LogDateTime).IsRequired();
            Property(x => x.IPAddress).IsRequired();
            Property(x => x.IsLogined).IsRequired();
            Property(x => x.Browser).IsOptional();
         }
    }
}
