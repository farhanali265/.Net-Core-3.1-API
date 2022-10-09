using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisLogMap : IEntityTypeConfiguration<ZelisLog> {
        public void Configure(EntityTypeBuilder<ZelisLog> builder) {

            builder.ToTable("ZelisLog", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisLogID");
            builder.Property(c => c.SqsAccountID).HasColumnName("SqsAccountID");
            builder.Property(c => c.SqsApplicantID).HasColumnName("SqsApplicantID");
            builder.Property(c => c.Zip).HasColumnName("Zip");
            builder.Property(c => c.Fips).HasColumnName("Fips");
            builder.Property(c => c.NpiNumbers).HasColumnName("NpiNumbers");
            builder.Property(c => c.Networks).HasColumnName("Networks");
            builder.Property(c => c.ResponseJson).HasColumnName("ResponseJson");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");
            builder.Property(c => c.UserId).HasColumnName("UserId");

            // Relationships
        }
    }
}