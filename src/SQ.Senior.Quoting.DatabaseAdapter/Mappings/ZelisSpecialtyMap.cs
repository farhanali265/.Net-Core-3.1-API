using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisSpecialtyMap : IEntityTypeConfiguration<ZelisSpecialty> {
        public void Configure(EntityTypeBuilder<ZelisSpecialty> builder) {

            builder.ToTable("ZelisSpecialty", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisSpecialtyID");
            builder.Property(c => c.Description).HasColumnName("Description");

            // Relationships
        }
    }
}