using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserAddressConfig : EntityTypeConfiguration<UserAddress>
    {
        public UserAddressConfig()
        {
            ToTable("UserAddresses", "User");

            Property(x => x.Address).IsOptional();
            Property(x => x.Street1).IsOptional().HasMaxLength(500);
            Property(x => x.Street2).IsOptional().HasMaxLength(500);
            Property(x => x.Telephone).IsOptional().HasMaxLength(15);
            Property(x => x.Fax).IsOptional().HasMaxLength(256);

            //Collection navigation property
            //HasRequired(x => x.User).WithMany(x => x.Addresses).HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            HasRequired(x => x.UserProfile).WithMany(x => x.UserAddresses).HasForeignKey(x => x.UserProfileId).WillCascadeOnDelete(false);
            HasRequired(x => x.City).WithMany().HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);

            //Reference navigation property
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
