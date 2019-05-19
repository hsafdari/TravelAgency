using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserProfileConfig : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfig()
        {
            ToTable("UserProfiles", "User");

            Property(x => x.FirstName).IsRequired();
            Property(x => x.LastName).IsRequired();
            Property(x => x.MobileNumber).IsRequired();

            Property(x => x.AvatarExtension).IsOptional().HasMaxLength(5);
            Property(x => x.AvatarFileName).IsOptional().HasMaxLength(250);
            Property(x => x.AvatarUrl).IsOptional().HasMaxLength(300);
            Property(x => x.AvatarSize).IsOptional();

            Property(x => x.BirthDate).IsOptional();
            Property(u => u.PhoneNumber).IsOptional().HasMaxLength(15);
            Property(u => u.WebSiteUrl).IsOptional().HasMaxLength(256);
            Property(u => u.Organization).IsOptional().HasMaxLength(256);
            Property(u => u.Fax).IsOptional().HasMaxLength(256);


            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //1:1 navigation
            HasRequired(x => x.User).WithOptional(x => x.UserProfile).WillCascadeOnDelete(false);

            //Reference navigation property
            HasOptional(x => x.UserGroup).WithMany(x => x.UserProfiles).HasForeignKey(x => x.UserGroupId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
