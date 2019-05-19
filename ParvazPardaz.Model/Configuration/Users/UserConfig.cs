using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Users
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("Users", "User");
            HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            Property(u => u.PhoneNumber).IsOptional().HasMaxLength(15);
            Property(u => u.Email).IsOptional().HasMaxLength(256);
            Property(u => u.UserName).IsRequired().HasMaxLength(256);
            Property(x => x.Gender).IsRequired();
            Property(x => x.FirstName).IsOptional().HasMaxLength(25);
            Property(x => x.LastName).IsOptional().HasMaxLength(25);
            Property(x => x.FullName).IsOptional().HasMaxLength(50);
            Property(x => x.RecoveryPasswordCode).IsOptional().HasMaxLength(200);
            Property(x => x.RecoveryPasswordCreatedDateTime).IsOptional();
            Property(x => x.RecoveryPasswordExpireDate).IsOptional();
            Property(x => x.RecoveryPasswordStatus).IsOptional();

            Property(u => u.RowVersion).IsRowVersion();
        }
    }
}
