using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProviderTypeMap : IEntityTypeConfiguration<ZelisProviderType> {
        public void Configure(EntityTypeBuilder<ZelisProviderType> builder) {

            builder.ToTable("ZelisProviderType", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisProviderTypeID");
            builder.Property(c => c.Description).HasColumnName("Description");

            // Relationships
        }
    }
}