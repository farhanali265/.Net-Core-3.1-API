using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class NetworkFipMap : IEntityTypeConfiguration<NetworkFip> {
        public void Configure(EntityTypeBuilder<NetworkFip> builder) {

            builder.ToTable("NetworkFips", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ID");
            builder.Property(c => c.FIPS).HasColumnName("FIPS");
            builder.Property(c => c.Network_ID).HasColumnName("Network_ID");
            builder.Property(c => c.Active).HasColumnName("Active");

            // Relationships
        }
    }
}