using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    class UserCommissionConfig : EntityTypeConfiguration<UserCommission>
    {
        public UserCommissionConfig()
        {
            ToTable("UserCommissions", "Core");

            Property(x => x.CommissionPercent).IsRequired();
            Property(x => x.ConditionPercent).IsOptional();
            Property(x => x.FromDate).IsOptional();
            Property(x => x.ToDate).IsOptional();
            //*1-1*/
            HasRequired(x => x.UserProfile).WithOptional(x => x.UserCommission).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();

        }
    }
}
