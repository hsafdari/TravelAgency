using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            HasKey(r => new { r.UserId, r.RoleId }).ToTable("UserRoles", "User");;
        }
    }
}
