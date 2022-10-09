using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ApplicantTrackingMap : IEntityTypeConfiguration<ApplicantTracking> {
        public void Configure(EntityTypeBuilder<ApplicantTracking> builder) {

            builder.ToTable("ApplicantTracking", "dbo");
            builder.HasKey(c => c.Id);

            // Column Mappings
            builder.Property(c => c.Id).HasColumnName("Id");
            builder.Property(c => c.UserId).HasColumnName("UserID");
            builder.Property(c => c.Email).HasColumnName("Email");
            builder.Property(c => c.InteractionId).HasColumnName("InteractionId");
            builder.Property(c => c.ZIPCode).HasColumnName("ZIPCode");
            builder.Property(c => c.FipsCode).HasColumnName("FipsCode");
            builder.Property(c => c.EntrySource).HasColumnName("EntrySource");
            builder.Property(c => c.EntryToken).HasColumnName("EntryToken");
            builder.Property(c => c.EntryKeyword).HasColumnName("EntryKeyword");
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(c => c.AccountId).HasColumnName("AccountId");
            builder.Property(c => c.IndividualId).HasColumnName("IndividualId");
            builder.Property(c => c.QrsApplicantId).HasColumnName("QRSApplicantID");
            builder.Property(c => c.ScreenSize).HasColumnName("ScreenSize");
            builder.Property(c => c.DeviceType).HasColumnName("DeviceType");

            // Relationships
        }
    }
}