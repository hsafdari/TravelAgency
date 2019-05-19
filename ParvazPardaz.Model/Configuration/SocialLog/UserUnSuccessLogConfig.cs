using ParvazPardaz.Model.Entity.SocialLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.SocialLog
{
    public class UserUnSuccessLogConfig : EntityTypeConfiguration<UserUnSuccessLog>
    {
        public UserUnSuccessLogConfig()
        {
            ToTable("UserUnSuccessLog", "log");
            HasKey(x => x.Id);
            Property(x => x.UserName).IsRequired().HasMaxLength(50);
            Property(x => x.Password).IsRequired().HasMaxLength(50);
            Property(x => x.IPAddress).IsRequired().HasMaxLength(50);
            Property(x => x.RequestTime).IsRequired();
            Property(x => x.Browser).IsRequired();
        }
    }
}
