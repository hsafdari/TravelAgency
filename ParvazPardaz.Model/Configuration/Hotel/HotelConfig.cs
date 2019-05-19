using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelConfig : EntityTypeConfiguration<Entity.Hotel.Hotel>
    {
        public HotelConfig()
        {
            ToTable("Hotels", "Hotel");
            Property(x => x.Title).HasMaxLength(100).IsRequired();
            Property(x => x.Location).HasMaxLength(100).IsOptional();
            Property(x => x.Address).HasMaxLength(150).IsRequired();
            Property(x => x.Tel).HasMaxLength(15).IsOptional();
            Property(x => x.Website).IsOptional().HasMaxLength(100);
            Property(x => x.Description).IsOptional();
            Property(x => x.Latitude).IsOptional();
            Property(x => x.Longitude).IsOptional();
            Property(x => x.LatLongIframe).IsOptional();
            Property(x => x.IsSummary).IsRequired();

            #region from postConfig
            Property(x => x.Summary).IsOptional().HasMaxLength(500);
            //Property(x => x.Content).IsRequired();
            Property(x => x.MetaKeywords).IsOptional().HasMaxLength(500);
            Property(x => x.MetaDescription).IsOptional().HasMaxLength(500);
            Property(x => x.VisitCount).IsRequired();
            Property(x => x.PostRateAvg).IsOptional().HasPrecision(18, 2);
            Property(x => x.PostRateCount).IsOptional();
            Property(x => x.PublishDatetime).IsRequired();
            Property(x => x.ExpireDatetime).IsOptional();
            Property(x => x.Sort).IsRequired();
            Property(x => x.AccessLevel).IsRequired();
            Property(x => x.IsActiveComments).IsRequired();
            Property(x => x.IsActive).IsRequired();

            HasMany(x => x.PostGroups).WithMany(x => x.Hotels).Map(z =>
            {
                z.MapLeftKey("HotelId");
                z.MapRightKey("HotelGroupId");
                z.ToTable("HotelGroupHotels", "Hotel");
            });

            HasMany(x => x.Tags).WithMany(x => x.Hotels).Map(z =>
            {
                z.MapLeftKey("HotelId");
                z.MapRightKey("TagId");
                z.ToTable("HotelsTags", "Hotel");
            });
            #endregion

            HasMany(x => x.HotelFacilities).WithMany(x => x.Hotels).Map(x =>
            {
                x.MapLeftKey("HotelId");
                x.MapRightKey("HotelFacilityId");
                x.ToTable("HotelHotelFacilities", "Hotel");
            });
            HasMany(x => x.HotelGalleries).WithMany(x => x.Hotels).Map(x =>
            {
                x.MapLeftKey("HotelId");
                x.MapRightKey("HotelGalleryId");
                x.ToTable("HotelHotelGalleries", "Hotel");
            });

            //HasMany(x => x.HotelPackages).WithMany(x => x.Hotels).Map(x =>
            //{
            //    x.MapLeftKey("HotelId");
            //    x.MapRightKey("HotelPackageId");
            //    x.ToTable("HotelHotelPackages", "Hotel");
            //}); 

            HasRequired(x => x.HotelRank).WithMany(x => x.Hotels).HasForeignKey(x => x.HotelRankId).WillCascadeOnDelete(false);
            HasRequired(x => x.City).WithMany(x => x.Hotels).HasForeignKey(x => x.CityId).WillCascadeOnDelete(false);
            //HasOptional(x => x.HotelPackage).WithMany(x => x.Hotels).HasForeignKey(x => x.HotelPackageId).WillCascadeOnDelete(false);
            
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);


            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
