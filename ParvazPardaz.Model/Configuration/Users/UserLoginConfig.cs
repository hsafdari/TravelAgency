using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserLoginConfig : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfig()
        {
            HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("UserLogins","User");
        }
    }
}
