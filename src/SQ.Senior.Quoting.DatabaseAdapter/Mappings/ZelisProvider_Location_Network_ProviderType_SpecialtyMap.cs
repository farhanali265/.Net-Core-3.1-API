using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProvider_Location_Network_ProviderType_SpecialtyMap : IEntityTypeConfiguration<ZelisProvider_Location_Network_ProviderType_Specialty> {
        public void Configure(EntityTypeBuilder<ZelisProvider_Location_Network_ProviderType_Specialty> builder) {

            builder.ToTable("ZelisProvider_Location_Network_ProviderType_Specialty", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisProvider_Location_Network_ProviderType_SpecialtyID");
            builder.Property(c => c.ZelisProvider_Location_Network_ProviderTypeID).HasColumnName("ZelisProvider_Location_Network_ProviderTypeID");
            builder.Property(c => c.ZelisSpecialtyID).HasColumnName("ZelisSpecialtyID");
            builder.Property(c => c.AcceptingNewPatients).HasColumnName("AcceptingNewPatients");
            builder.Property(c => c.AcceptingNewPatientsText).HasColumnName("AcceptingNewPatientsText");
            builder.Property(c => c.EnrollmentID).HasColumnName("EnrollmentID");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");
            builder.Property(c => c.PracticeStatus).HasColumnName("PracticeStatus");

            // Relationships
        }
    }
}