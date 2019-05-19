using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserGroupConfig : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfig()
        {
            ToTable("UserGroups", "User");

            Property(x => x.Title).IsRequired();
            Property(x => x.MinCreditValue).IsRequired();
            Property(x => x.Percent).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(u => u.RowVersion).IsRowVersion();
        }
    }
}
