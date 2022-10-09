using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProviderMap : IEntityTypeConfiguration<ZelisProvider> {
        public void Configure(EntityTypeBuilder<ZelisProvider> builder) {

            builder.ToTable("ZelisProvider", "dbo");
            builder.HasKey(c => c.ZelisProviderID);

            // Column Mappings
            builder.Property(c => c.ZelisProviderID).HasColumnName("ZelisProviderID");
            builder.Property(c => c.ProviderID).HasColumnName("ProviderID");
            builder.Property(c => c.NpiNumber).HasColumnName("NpiNumber");
            builder.Property(c => c.FirstName).HasColumnName("FirstName");
            builder.Property(c => c.MiddleName).HasColumnName("MiddleName");
            builder.Property(c => c.LastName).HasColumnName("LastName");
            builder.Property(c => c.Suffix).HasColumnName("Suffix");
            builder.Property(c => c.FacilityName).HasColumnName("FacilityName");
            builder.Property(c => c.MinimumDistanceInMiles).HasColumnName("MinimumDistanceInMiles");
            builder.Property(c => c.Degrees).HasColumnName("Degrees");
            builder.Property(c => c.MedicalSchools).HasColumnName("MedicalSchools");
            builder.Property(c => c.Gender).HasColumnName("Gender");
            builder.Property(c => c.BoardCertifications).HasColumnName("BoardCertifications");
            builder.Property(c => c.MatchScore).HasColumnName("MatchScore");
            builder.Property(c => c.ZelisLogID).HasColumnName("ZelisLogID");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");

            // Relationships
        }
    }
}