using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProvider_LocationMap : IEntityTypeConfiguration<ZelisProvider_Location> {
        public void Configure(EntityTypeBuilder<ZelisProvider_Location> builder) {

            builder.ToTable("ZelisProvider_Location", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisProvider_LocationID");
            builder.Property(c => c.ZelisProviderID).HasColumnName("ZelisProviderID");
            builder.Property(c => c.ProviderLocationID).HasColumnName("ProviderLocationID");
            builder.Property(c => c.Address).HasColumnName("Address");
            builder.Property(c => c.Address2).HasColumnName("Address2");
            builder.Property(c => c.City).HasColumnName("City");
            builder.Property(c => c.State).HasColumnName("State");
            builder.Property(c => c.County).HasColumnName("County");
            builder.Property(c => c.Zip).HasColumnName("Zip");
            builder.Property(c => c.Phone).HasColumnName("Phone");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");

            // Relationships
        }
    }
}