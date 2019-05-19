using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{

    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            ToTable("Roles","User");
            HasMany(x => x.Users).WithRequired().HasForeignKey(x => x.RoleId);

            Property(r => r.RowVersion).IsRowVersion();
        }
    }
}
